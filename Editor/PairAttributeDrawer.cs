﻿using System;
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
                float halfWidth = position.width * 0.5f;

                Rect keyPosition = new Rect(position.x, position.y, halfWidth, position.height);
                Rect valuePosition = new Rect(position.x + halfWidth, position.y, halfWidth, position.height);

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
}