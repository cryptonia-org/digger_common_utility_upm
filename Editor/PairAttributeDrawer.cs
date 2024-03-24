using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace CommonUtility
{
    [CustomPropertyDrawer(typeof(InspectorDictionaryPairAttribute))]
    public class PairAttributeDrawer : PropertyDrawer
    {
        private const float _ratio = 0.35f;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return Mathf.Max(
                EditorGUI.GetPropertyHeight(property.FindPropertyRelative("Key"), label, true),
                EditorGUI.GetPropertyHeight(property.FindPropertyRelative("Value"), label, true));
        }
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            {
                Rect keyPosition = new Rect(position.x, position.y, position.width * _ratio, position.height);

                Rect valuePosition = new Rect(
                    position.x + keyPosition.width + EditorGUIUtility.standardVerticalSpacing,
                    position.y,
                    position.width * (1f - _ratio) - EditorGUIUtility.standardVerticalSpacing,
                    position.height);

                EditorGUI.BeginChangeCheck();
                {
                    var key = property.FindPropertyRelative("Key");
                    var value = property.FindPropertyRelative("Value");

                    EditorGUI.PropertyField(keyPosition, key, GUIContent.none, true);
                    EditorGUI.PropertyField(valuePosition, value, GUIContent.none, true);
                }
                if (EditorGUI.EndChangeCheck())
                    property.serializedObject.ApplyModifiedProperties();
            }
            EditorGUI.EndProperty();
        }

    }

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