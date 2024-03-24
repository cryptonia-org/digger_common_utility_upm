using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace CommonUtility.InspectorField.Editor
{
    [CustomPropertyDrawer(typeof(Field<>))]
    public class InspectorFieldDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Object targetObject = property.serializedObject.targetObject;
            Field container = targetObject?.GetType().GetField(property.name, BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(targetObject) as Field;
            container?.Validate();

            if (container != null)
                label.text += $" ({container.ValueType.Name})";

            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.PropertyField(position, property.FindPropertyRelative("_value"), label);
            EditorGUI.EndProperty();
        }
    }
}