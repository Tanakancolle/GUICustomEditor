using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace AutoCustomEditor
{
    public abstract class AutoCustomEditorBase : Editor
    {
        protected List<ItemDrawerBase> _drwaerList = new List<ItemDrawerBase>();

        protected AutoCustomParameter _parameter;
        protected AutoCustomParameterEditor _parameterEditor;
        protected bool _isFoldout;

        protected virtual void OnEnable()
        {
            Refresh();
        }

        protected virtual void Refresh()
        {
            if (_parameter == null)
            {
                var assetName = GetParameterName();
                _parameter = GetParameter(assetName);
                if (_parameter == null)
                {
                    return;
                }

                _parameterEditor = Editor.CreateEditor(_parameter) as AutoCustomParameterEditor;

                var proprtyNameList = new List<string>();
                var property = new SerializedObject(target).GetIterator();

                while (property.NextVisible(true))
                {
                    proprtyNameList.Add(property.name);
                }

                _parameterEditor.SetTargetNames(proprtyNameList.ToArray());
            }

            if (_parameter.Items == null)
            {
                return;
            }

            _drwaerList.Clear();
            foreach (var item in _parameter.Items)
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
            var state = new AutoCustomEditorState();
            serializedObject.Update();
            foreach (var drawer in _drwaerList)
            {
                drawer.Draw(state);
            }
            serializedObject.ApplyModifiedProperties();

            EditorGUI.indentLevel -= state.IntentLevel;
            for (int i = state.HorizontalLevel; i > 0; --i)
            {
                EditorGUILayout.EndHorizontal();
            }
            for (int i = state.VerticalLevel; i > 0; --i)
            {
                EditorGUILayout.EndVertical();
            }


            GUILayout.Space(EditorGUIUtility.singleLineHeight);
            Utility.DrawSeparator();

            if (_parameter == null)
            {
                if (GUILayout.Button("Create Parameter"))
                {
                    var path = AssetDatabase.GetAssetPath(target);
                    path = Path.GetDirectoryName(path);

                    if (path.Contains("/Editor") == false)
                    {
                        path = Path.Combine(path, "Editor");
                    }

                    var instance = CreateInstance<AutoCustomParameter>();
                    AssetDatabase.CreateAsset(instance, Path.Combine(path, GetParameterName()) + ".asset");
                    AssetDatabase.Refresh();
                    Refresh();
                }

                return;
            }

            _isFoldout = EditorGUILayout.Foldout(_isFoldout, "Custom Editor Parameter");

            if (_isFoldout == false)
            {
                return;
            }

            if (_parameterEditor != null)
            {
                EditorGUI.BeginChangeCheck();
                _parameterEditor.DrawInspector();
                if (EditorGUI.EndChangeCheck())
                {
                    Refresh();
                }
            }
            else
            {
                EditorGUILayout.LabelField("Parameter Not Found");
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

        private string GetParameterName()
        {
            return target.GetType().Name + "Parameter";
        }
    }
}
