using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace AutoCustomEditor
{
    [CustomEditor(typeof(AutoCustomParameter))]
    public class AutoCustomParameterEditor : Editor
    {
        /// <summary>
        /// AutoCustomParameterのプロパティタイプ
        /// ※AutoCustomParameterの変数名と要素名を同じにする
        /// </summary>
        private enum Property
        {
            Items,
        }

        private SerializedProperty[] _properties;
        private ReorderableList _itemList;
        private string[] _targetPropertyNames;

        public ReorderableList.ReorderCallbackDelegate OnReorderCallback { get; set; }

        private void OnEnable()
        {
            var propertyNames = Enum.GetNames(typeof(Property));
            _properties = new SerializedProperty[propertyNames.Length];
            for (var i = 0; i < _properties.Length; ++i)
            {
                _properties[i] = serializedObject.FindProperty(propertyNames[i]);
            }

            _itemList = new ReorderableList(serializedObject, GetSerializedProperty(Property.Items))
            {
                elementHeightCallback = GetItemHeight,
                drawElementCallback = DrawItem,
                onReorderCallback = (list) => OnReorderCallback?.Invoke(list),
                headerHeight = 0f,
            };
        }

        public override void OnInspectorGUI()
        {
        }

        public void DrawInspector()
        {
            serializedObject.Update();
            _itemList.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }

        public void SetTargetNames(string[] names)
        {
            _targetPropertyNames = names;
        }

        private float GetItemHeight(int index)
        {
            var item = GetItemFromIndex(index);
            return Utility.GetItemHeight(item);
        }

        private void DrawItem(Rect rect, int index, bool isActive, bool isFocused)
        {
            Utility.DrawItem(rect, GetItemFromIndex(index), _targetPropertyNames);
        }

        private SerializedProperty GetItemFromIndex(int index)
        {
            return GetSerializedProperty(Property.Items).GetArrayElementAtIndex(index);
        }

        private SerializedProperty GetSerializedProperty(Property type)
        {
            return _properties[(int) type];
        }
    }
}
