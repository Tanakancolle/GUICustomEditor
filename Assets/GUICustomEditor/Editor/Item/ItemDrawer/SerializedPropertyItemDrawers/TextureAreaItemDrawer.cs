using UnityEditor;
using UnityEngine;

namespace AutoCustomEditor
{
    public class TextureAreaItemDrawer : SerializedPropertyItemDrawerBase
    {
        private float _width;
        private float _height;

        public TextureAreaItemDrawer(ItemParameter parameter, SerializedObject target) : base(parameter, target)
        {
            _width = GetValueFromIndex(parameter.Floats, 0);
            _height = GetValueFromIndex(parameter.Floats, 1);
        }

       public override void Draw(GUICustomEditorState state)
        {
            if (_property == null)
            {
                return;
            }


            EditorGUILayout.ObjectField (_property, typeof(Texture), GUIContent.none, GUILayout.Width(_width + state.IntentLevel * 15f), GUILayout.Height(_height));
        }
    }
}
