using UnityEditor;

namespace AutoCustomEditor
{
    public class IntentLevelItemDrawer : ItemDrawerBase
    {
        private int _intentChangeLevel;

        public IntentLevelItemDrawer(ItemParameter parameter, SerializedObject target) : base(parameter, target)
        {
            _intentChangeLevel = GetValueFromIndex(parameter.Ints, 0);
        }

        public override void Draw(GUICustomEditorState state)
        {
            EditorGUI.indentLevel += _intentChangeLevel;
            state.IntentLevel += _intentChangeLevel;
        }
    }
}
