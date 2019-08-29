using UnityEditor;
using UnityEngine;

namespace AutoCustomEditor
{
    public abstract class SerializedPropertyItemDrawerBase : ItemDrawerBase
    {
        protected SerializedProperty _property;
        protected GUIContent _drawNameContent;

        protected SerializedPropertyItemDrawerBase(ItemParameter parameter, SerializedObject target) : base(parameter, target)
        {
            _property = target.FindProperty(parameter.PropertyName);
            _drawNameContent = GetGUIContent(parameter.DrawName);
        }
    }
}
