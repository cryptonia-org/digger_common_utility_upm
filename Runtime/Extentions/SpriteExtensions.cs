using UnityEngine;

namespace CommonUtility.Extensions
{
    public static class SpriteExtensions
    {
        public static Mesh GenerateMesh(this Sprite sprite, float? scale = null)
        {
            Vector3[] vertices = new Vector3[sprite.vertices.Length];
            for (int i = 0; i < vertices.Length; i++)
                vertices[i] = sprite.vertices[i];

            if (scale.HasValue)
                Resize(vertices, scale.Value);

            int[] triangles = new int[sprite.triangles.Length];
            for (int i = 0; i < triangles.Length; i++)
                triangles[i] = sprite.triangles[i];

            Mesh mesh = new Mesh();
            mesh.name = $"{sprite.name}_generatedMesh";
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = sprite.uv;
            mesh.RecalculateBounds();

            return mesh;
        }

        private static void Resize(Vector3[] vertices, float scale)
        {
            Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            Vector3 max = new Vector3(float.MinValue, float.MinValue, float.MinValue);

            for (int i = 0; i < vertices.Length; i++)
            {
                if (vertices[i].x < min.x)
                    min.x = vertices[i].x;
                if (vertices[i].x > max.x)
                    max.x = vertices[i].x;

                if (vertices[i].y < min.y)
                    min.y = vertices[i].y;
                if (vertices[i].y > max.y)
                    max.y = vertices[i].y;

                if (vertices[i].z < min.z)
                    min.z = vertices[i].z;
                if (vertices[i].z > max.z)
                    max.z = vertices[i].z;
            }

            Vector3 center = (min + max) * 0.5f;
            float sourceScale = (max - min).magnitude;
            float scaleMult = scale / sourceScale;

            for (int i = 0; i < vertices.Length; i++)
                vertices[i] = (vertices[i] - center) * scaleMult;
        }
    }
}