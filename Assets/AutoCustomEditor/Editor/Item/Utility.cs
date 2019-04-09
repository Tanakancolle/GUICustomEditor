using System;
using UnityEditor;
using UnityEngine;

namespace AutoCustomEditor
{
    public static class Utility
    {
        public static readonly float SingleLineTotalHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

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
            return GetHeight(GetItemParameterDrawType(type));
        }

        public static float GetHeight(ItemParameterDrawType type)
        {
            // デフォルト以外の高さの場合はここに記述
            switch (type)
            {
                case ItemParameterDrawType.PropertyNameAndDrawName:
                    return SingleLineTotalHeight * 2f;
            }

            return SingleLineTotalHeight;
        }

        public static void DrawItem(Rect rect, SerializedProperty item)
        {
            var itemType = GetItemProperty(item, ItemParameterProperty.ItemType);
            rect.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(rect, itemType);
            rect.y += EditorGUIUtility.singleLineHeight;
            var drawType = GetItemParameterDrawType((ItemType) itemType.enumValueIndex);
            switch (drawType)
            {
                case ItemParameterDrawType.PropertyNameAndDrawName:
                    const float labelWidth = 55f;
                    var propertyWidth = rect.width / 2f - labelWidth;
                    rect.width = labelWidth;
                    EditorGUI.LabelField(rect, "Property");
                    rect.x += labelWidth;
                    rect.width = propertyWidth;
                    EditorGUI.PropertyField(rect, GetItemProperty(item, ItemParameterProperty.PropertyName), GUIContent.none);
                    rect.x += rect.width;
                    rect.width = labelWidth;
                    EditorGUI.LabelField(rect, "Display");
                    rect.x += labelWidth;
                    rect.width = propertyWidth;
                    EditorGUI.PropertyField(rect, GetItemProperty(item, ItemParameterProperty.DrawName), GUIContent.none);
                    break;
            }
        }

        public static ItemParameterDrawType GetItemParameterDrawType(ItemType type)
        {
            switch (type)
            {
                case ItemType.None:
                    return ItemParameterDrawType.None;
                case ItemType.PropertyField:
                    return ItemParameterDrawType.PropertyNameAndDrawName;
                default:
                    throw new ArgumentOutOfRangeException("type", type, null);
            }
        }

        public static GUIContent CreateContent(string drawName)
        {
            return new GUIContent(drawName);
        }
    }
}
