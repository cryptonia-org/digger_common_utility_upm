#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace CommonUtility
{
    [CustomPropertyDrawer(typeof(InspectorDictionary<,>))]
    public class InspectorDictionaryAttributeDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginChangeCheck();
            EditorGUI.PropertyField(position, property, new GUIContent(property.name), true);
            bool changed = EditorGUI.EndChangeCheck();

            if (changed)
            {
                var editTime = property.FindPropertyRelative("_dirty");
                editTime.boolValue = true;
            }
        }

    }
}
#endif