using UnityEditor;

namespace AutoCustomEditor
{
    public class IntentLevelDrawer : ItemDrawerBase
    {
        private int _intentChangeLevel;

        public IntentLevelDrawer(ItemParameter parameter, SerializedObject target) : base(parameter, target)
        {
            _intentChangeLevel = GetValueFromIndex(parameter.Ints, 0);
        }

        public override void Draw()
        {
            EditorGUI.indentLevel += _intentChangeLevel;
        }
    }
}
