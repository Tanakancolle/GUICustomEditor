using UnityEditor;
using UnityEngine;

namespace AutoCustomEditor
{
    public class CodeFieldItemDrawer : SerializedPropertyItemDrawerBase
    {
        private Vector2 _scrollPosition;
        private int _fontSize;
        private float _height;

        public CodeFieldItemDrawer(ItemParameter parameter, SerializedObject target) : base(parameter, target)
        {
            _fontSize = GetValueFromIndex(parameter.Ints, 0);
            if (_fontSize == 0)
            {
                _fontSize = 12;
            }
            _height = GetValueFromIndex(parameter.Floats, 0);
        }

        public override void Draw(AutoCustomEditorState state)
        {
            if (_property == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(_drawNameContent.text) == false)
            {
                EditorGUILayout.LabelField(_drawNameContent);
            }

            _property.stringValue = SyntaxHighlightEditor.SyntaxHighlightUtility.DrawCSharpCode(ref _scrollPosition, _property.stringValue, _fontSize, _height);
        }
    }
}
