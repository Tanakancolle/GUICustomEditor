using UnityEditor;

namespace AutoCustomEditor
{
    public class HorizontalStartItemDrawer : ItemDrawerBase
    {
        public HorizontalStartItemDrawer(ItemParameter parameter, SerializedObject target) : base(parameter, target)
        {
        }

        public override void Draw(AutoCustomEditorState state)
        {
            EditorGUILayout.BeginHorizontal();

            state.HorizontalLevel++;
        }
    }
}
