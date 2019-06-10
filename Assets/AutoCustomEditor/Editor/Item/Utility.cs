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
                case ItemType.HorizontalStart:
                case ItemType.HorizontalEnd:
                case ItemType.VerticalStart:
                case ItemType.VerticalEnd:
                    return SingleLineTotalHeight;
                case ItemType.CodeTextArea:
                    return SingleLineTotalHeight * 4;
                case ItemType.Slider:
                    return SingleLineTotalHeight * 3;
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
                    DrawPropertyPopup(rect, item, propertyNames);
                    break;
                case ItemType.Space:
                    var spaceSizeProperty = GetItemProperty(item, ItemParameterProperty.Floats);
                    InsertPropertyArrayIndexIfNeeded(spaceSizeProperty, 0);
                    EditorGUI.Slider(rect, spaceSizeProperty.GetArrayElementAtIndex(0), 0f, 100f, "Space");
                    break;
                case ItemType.IntentLevel:
                    var intentLevelProperty = GetItemProperty(item, ItemParameterProperty.Ints);
                    InsertPropertyArrayIndexIfNeeded(intentLevelProperty, 0);
                    EditorGUI.IntSlider(rect, intentLevelProperty.GetArrayElementAtIndex(0), -10, 10, "Intent Level");
                    break;
                case ItemType.CodeTextArea:
                    var startX = rect.x;
                    var startWidth = rect.width;
                    rect.width /= 2;
                    DrawHorizontalPropertyField(
                        rect, "Label", GetItemProperty(item, ItemParameterProperty.DrawName)
                    );

                    rect.x += rect.width;
                    DrawPropertyPopup(rect, item, propertyNames);

                    rect.x = startX;
                    rect.width = startWidth;
                    rect.y += rect.height;

                    var fontSizeProperty = GetItemProperty(item, ItemParameterProperty.Ints);
                    InsertPropertyArrayIndexIfNeeded(fontSizeProperty, 0);
                    EditorGUI.IntSlider(rect, fontSizeProperty.GetArrayElementAtIndex(0), 0, 100, "Font Size");

                    rect.y += rect.height;
                    var textHeightProperty = GetItemProperty(item, ItemParameterProperty.Floats);
                    InsertPropertyArrayIndexIfNeeded(textHeightProperty, 0);
                    EditorGUI.Slider(rect, textHeightProperty.GetArrayElementAtIndex(0), 0f, Screen.height, "Height");
                    break;
                case ItemType.Slider:

                    rect.width /= 2;
                    DrawHorizontalPropertyField(
                        rect, "Label", GetItemProperty(item, ItemParameterProperty.DrawName)
                    );

                    rect.x += rect.width;
                    DrawPropertyPopup(rect, item, propertyNames);

                    rect.x -= rect.width;
                    rect.y += rect.height;
                    rect.width *= 2;

                    var minMaxProperty = GetItemProperty(item, ItemParameterProperty.Floats);
                    InsertPropertyArrayIndexIfNeeded(minMaxProperty, 0);
                    InsertPropertyArrayIndexIfNeeded(minMaxProperty, 1);
                    DrawHorizontalPropertyField(
                        rect,
                        new[] {"Min", "Max"},
                        new[] {minMaxProperty.GetArrayElementAtIndex(0), minMaxProperty.GetArrayElementAtIndex(1)});
                    break;
                case ItemType.TextureArea:
                    rect.width /= 3;
                    DrawPropertyPopup(rect, item, propertyNames);
                    rect.x += rect.width;
                    rect.width *= 2;
                    var sizeProperty = GetItemProperty(item, ItemParameterProperty.Floats);
                    InsertPropertyArrayIndexIfNeeded(sizeProperty, 0);
                    InsertPropertyArrayIndexIfNeeded(sizeProperty, 1);
                    DrawHorizontalPropertyField(
                        rect,
                        new[] {"Width", "Height"},
                        new[] {sizeProperty.GetArrayElementAtIndex(0), sizeProperty.GetArrayElementAtIndex(1)});
                    break;
            }
        }

        private static void DrawPropertyPopup(Rect rect, SerializedProperty item, string[] propertyNames)
        {
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
