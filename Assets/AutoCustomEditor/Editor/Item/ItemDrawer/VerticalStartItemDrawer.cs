using UnityEditor;

namespace AutoCustomEditor
{
    public class VerticalStartItemDrawer : ItemDrawerBase
    {
        public VerticalStartItemDrawer(ItemParameter parameter, SerializedObject target) : base(parameter, target)
        {
        }

        public override void Draw(AutoCustomEditorState state)
        {
            EditorGUILayout.BeginVertical();

            state.VerticalLevel++;
        }
    }
}
