using System;
using Boo.Lang;
using UnityEditor;
using UnityEngine;

namespace AutoCustomEditor
{
    public abstract class AutoCustomEditorBase : Editor
    {
        protected List<ItemDrawerBase> _drwaerList = new List<ItemDrawerBase>();

        protected virtual void OnEnable()
        {
            var assetName = target.GetType().Name + "Parameter";
            var parameter = GetParameter(assetName);
            if (parameter == null)
            {
                Debug.LogError("AutoCustomParameterを継承したScriptableObjectがありません : " + assetName);
                return;
            }

            _drwaerList.Clear();
            foreach (var item in parameter.Items)
            {
                var drawer = ItemDrawerCreator.Create(item, serializedObject);
                if (drawer == null)
                {
                    continue;
                }

                _drwaerList.Add(drawer);
            }
        }

        public override void OnInspectorGUI()
        {
            foreach (var drawer in _drwaerList)
            {
                drawer.Draw();
            }
        }

        private AutoCustomParameter GetParameter(string assetName)
        {
            foreach (var guid in AssetDatabase.FindAssets("t:ScriptableObject " + assetName))
            {
                var parameter = AssetDatabase.LoadAssetAtPath<AutoCustomParameter>(AssetDatabase.GUIDToAssetPath(guid));
                if (parameter != null)
                {
                    return parameter;
                }
            }

            return null;
        }
    }
}
