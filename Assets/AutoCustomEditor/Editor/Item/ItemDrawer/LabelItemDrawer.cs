using UnityEditor;
using UnityEngine;

namespace AutoCustomEditor
{
    public class LabelItemDrawer : ItemDrawerBase
    {
        private GUIContent _labelContent;

        public LabelItemDrawer(ItemParameter parameter, SerializedObject target) : base(parameter, target)
        {
            _labelContent = GetGUIContent(parameter.DrawName);
        }

        public override void Draw(AutoCustomEditorState state)
        {
            EditorGUILayout.LabelField(_labelContent);
        }
    }
}
