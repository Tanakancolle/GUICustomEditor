using UnityEditor;

namespace AutoCustomEditor
{
    public class VerticalEndItemDrawer : ItemDrawerBase
    {
        public VerticalEndItemDrawer(ItemParameter parameter, SerializedObject target) : base(parameter, target)
        {
        }

        public override void Draw(AutoCustomEditorState state)
        {
            if (state.VerticalLevel <= 0)
            {
                return;
            }

            EditorGUILayout.EndVertical();

            state.VerticalLevel--;
        }
    }
}
