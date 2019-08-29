using UnityEditor;

namespace AutoCustomEditor
{
    public class HorizontalEndItemDrawer : ItemDrawerBase
    {
        public HorizontalEndItemDrawer(ItemParameter parameter, SerializedObject target) : base(parameter, target)
        {
        }

        public override void Draw(GUICustomEditorState state)
        {
            if (state.HorizontalLevel <= 0)
            {
                return;
            }

            EditorGUILayout.EndHorizontal();

            state.HorizontalLevel--;
        }
    }
}
