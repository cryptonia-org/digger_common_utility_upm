using System;
using System.Collections.Generic;
using UnityEngine;

using Color = UnityEngine.Color;
using Object = UnityEngine.Object;

namespace CommonUtility.Extensions
{
    public static class GizmosExtensions
    {
        private const long _lifeDurationMs = 20; // ~ 60 fps
        private static long _lastClear;
        private static readonly List<(long Date, Object Object)> _toDestroy = new List<(long, Object)>();

        private static long TimeSinceStartup => (long)DateTime.Now.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;

        public static void DrawCube(Transform relative, Vector3 scale, Color color) => DrawCube(relative.localToWorldMatrix, scale * 0.5f, scale, color);

        public static void DrawCube(Transform relative, Vector3 center, Vector3 scale, Color color) => DrawCube(relative.localToWorldMatrix, center, scale, color);

        public static void DrawCube(Matrix4x4 matrix, Vector3 center, Vector3 scale, Color color) => DrawRelative(matrix, color, () => Gizmos.DrawCube(center, scale));

        public static void DrawWireCube(Transform relative, Vector3 scale, Color color) => DrawWireCube(relative.localToWorldMatrix, scale * 0.5f, scale, color);

        public static void DrawWireCube(Transform relative, Vector3 center, Vector3 scale, Color color) => DrawWireCube(relative.localToWorldMatrix, center, scale, color);

        public static void DrawWireCube(Matrix4x4 matrix, Vector3 center, Vector3 scale, Color color) => DrawRelative(matrix, color, () => Gizmos.DrawWireCube(center, scale));

        public static void DrawText(string text, Vector3 worldPosition, Color textColor, Vector2 anchor, float textSize = 15f)
        {
#if UNITY_EDITOR
            UnityEditor.SceneView view = UnityEditor.SceneView.currentDrawingSceneView;
            if (!view)
                return;
            Vector3 screenPosition = view.camera.WorldToScreenPoint(worldPosition);
            if (screenPosition.y < 0 || screenPosition.y > view.camera.pixelHeight || screenPosition.x < 0 || screenPosition.x > view.camera.pixelWidth || screenPosition.z < 0)
                return;
            float pixelRatio = UnityEditor.HandleUtility.GUIPointToScreenPixelCoordinate(Vector2.right).x - UnityEditor.HandleUtility.GUIPointToScreenPixelCoordinate(Vector2.zero).x;
            UnityEditor.Handles.BeginGUI();
            GUIStyle style = new GUIStyle(GUI.skin.label)
            {
                fontSize = (int)textSize,
                normal = new GUIStyleState() { textColor = textColor },
            };
            Vector2 size = style.CalcSize(new GUIContent(text)) * pixelRatio;
            Vector2 alignedPosition =
                ((Vector2)screenPosition +
                size * ((anchor + Vector2.left + Vector2.up) / 2f)) * (Vector2.right + Vector2.down) +
                Vector2.up * view.camera.pixelHeight;
            GUI.Label(new Rect(alignedPosition / pixelRatio, size / pixelRatio), text, style);
            UnityEditor.Handles.EndGUI();
#endif
        }

        public static void DrawWireMeshes(GameObject prefab, Transform origin, Color color)
        {
            if (origin == null)
                return;

            DrawWireMeshes(prefab, color, origin.position, origin.rotation, origin.localScale);
        }

        public static void DrawWireMeshes(GameObject prefab, Color color, Vector3 position, Quaternion rotation, Vector3 scale)
        {
#if UNITY_EDITOR
            if (prefab == null)
                return;

            MeshFilter[] meshFilters = prefab.GetComponentsInChildren<MeshFilter>();
            if (meshFilters.Length == 0)
                return;

            List<CombineInstance> combineInstances = new List<CombineInstance>(meshFilters.Length);
            bool empty = true;

            for (int i = 0; i < meshFilters.Length; i++)
            {
                Mesh mesh = meshFilters[i].sharedMesh;
                if (mesh == null || mesh.vertexCount == 0)
                    continue;

                CombineInstance combineInstance = new CombineInstance();
                combineInstance.mesh = mesh;
                combineInstance.transform = meshFilters[i].transform.localToWorldMatrix;
                combineInstances.Add(combineInstance);
                empty = false;
            }

            if (empty)
                return;

            Mesh result = new Mesh();
            result.CombineMeshes(combineInstances.ToArray(), true, true, false);

            Color defaultColor = Gizmos.color;
            Gizmos.color = color;
            Gizmos.DrawWireMesh(result, position, rotation, scale);
            Gizmos.color = defaultColor;

            Register(result);
            ClearOld();
#endif
        }

        private static void Register(Object obj) => _toDestroy.Add((TimeSinceStartup, obj));

        private static void ClearOld()
        {
            if (_lastClear == TimeSinceStartup)
                return;

            _lastClear = TimeSinceStartup;

            for (int i = _toDestroy.Count - 1; i >= 0; i--)
            {
                (long Date, Object Object) pair = _toDestroy[i];
                if (TimeSinceStartup - pair.Date < _lifeDurationMs)
                    continue;

                if (pair.Object != null)
                    Object.DestroyImmediate(pair.Object);

                _toDestroy.RemoveAt(i);
            }
        }

        private static void DrawRelative(Matrix4x4 matrix, Color color, Action draw)
        {
            Matrix4x4 oldMatrix = Gizmos.matrix;
            Color oldColor = Gizmos.color;

            Gizmos.color = color;
            Gizmos.matrix = matrix;

            draw?.Invoke();

            Gizmos.matrix = oldMatrix;
            Gizmos.color = oldColor;
        }
    }
}