using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.UIElements;
using UnityEngine;

namespace AutoCustomEditor
{
    public static class Utility
    {
        public static readonly float SingleLineTotalHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        private static readonly float DefaultHeight = SingleLineTotalHeight * 2f;

        /// <summary>
        /// ItemParameterPropertyが示すプロパティ名
        /// アクセス回数が多いため、キャッシュしておく
        /// </summary>
        private static readonly string[] ItemParameterPropertyNames = Enum.GetNames(typeof(ItemParameterProperty));

        public static SerializedProperty GetItemProperty(SerializedProperty item, ItemParameterProperty type)
        {
            return item.FindPropertyRelative(ItemParameterPropertyNames[(int) type]);
        }

        public static float GetItemHeight(SerializedProperty item)
        {
            var type = GetItemProperty(item, ItemParameterProperty.ItemType);
            return GetHeight((ItemType) type.enumValueIndex);
        }

        public static float GetHeight(ItemType type)
        {
            // デフォルト以外の高さの場合はここに記述
            switch (type)
            {
                case ItemType.None:
                    return SingleLineTotalHeight;
            }

            return DefaultHeight;
        }

        public static void DrawItem(Rect rect, SerializedProperty item, string[] propertyNames)
        {
            var itemType = GetItemProperty(item, ItemParameterProperty.ItemType);
            rect.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(rect, itemType);
            rect.y += EditorGUIUtility.singleLineHeight;
            switch ((ItemType) itemType.enumValueIndex)
            {
                case ItemType.None:
                    break;
                case ItemType.Label:
                    DrawHorizontalPropertyField(
                        rect, "Label", GetItemProperty(item, ItemParameterProperty.DrawName)
                    );
                    break;
                case ItemType.PropertyField:
                    rect.width /= 2;
                    DrawHorizontalPropertyField(
                        rect, "Label", GetItemProperty(item, ItemParameterProperty.DrawName)
                    );

                    rect.x += rect.width;
                    var property = GetItemProperty(item, ItemParameterProperty.PropertyName);
                    var selectValue = property.stringValue;
                    var index = 0;
                    if (string.IsNullOrEmpty(selectValue) == false)
                    {
                        var count = 0;
                        foreach (var name in propertyNames)
                        {
                            if (name == selectValue)
                            {
                                index = count;
                                break;
                            }
                            count++;
                        }
                    }

                    var select = EditorGUI.Popup(rect, index, propertyNames);
                    if (select != index)
                    {
                        property.stringValue = propertyNames[select];
                    }

                    break;
                case ItemType.Space:
                    var spaceSizeProperty = GetItemProperty(item, ItemParameterProperty.Floats);
                    InsertPropertyArrayIndexIfNeeded(spaceSizeProperty, 0);
                    EditorGUI.Slider(rect, spaceSizeProperty.GetArrayElementAtIndex(0), 0f, 100f, "Space");
                    break;
                case ItemType.IntentLevel:
                    var intentLevelProperty = GetItemProperty(item, ItemParameterProperty.Ints);
                    InsertPropertyArrayIndexIfNeeded(intentLevelProperty, 0);
                    EditorGUI.IntSlider(rect, intentLevelProperty.GetArrayElementAtIndex(0), -10, 10, "IntentLevel");
                    break;
            }
        }

        private static void DrawHorizontalPropertyField(Rect rect, string[] label, SerializedProperty[] properties)
        {
            if (properties == null || properties.Length == 0)
            {
                return;
            }

            rect.width /= properties.Length;
            var singleWidth = rect.width;
            for (var i = 0; i < properties.Length; ++i)
            {
                DrawHorizontalPropertyField(rect, label[i], properties[i]);
                rect.x += singleWidth;
            }
        }

        private static void DrawHorizontalPropertyField(Rect rect, string label, SerializedProperty property)
        {
            var width = rect.width;
            var content = CreateContent(label);
            var labelSize = EditorStyles.label.CalcSize(content);

            // Draw Label
            rect.width = labelSize.x;
            EditorGUI.LabelField(rect, content);
            rect.x += rect.width;
            rect.width = width - labelSize.x;

            // Draw PropertyField
            EditorGUI.PropertyField(rect, property, GUIContent.none);
        }

        public static GUIContent CreateContent(string drawName)
        {
            return new GUIContent(drawName);
        }

        public static void InsertPropertyArrayIndexIfNeeded(SerializedProperty property, int index)
        {
            if (property.arraySize <= index)
            {
                property.InsertArrayElementAtIndex(index);
            }
        }

        public static void DrawSeparator()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
            EditorGUILayout.EndHorizontal();
        }
    }
}
