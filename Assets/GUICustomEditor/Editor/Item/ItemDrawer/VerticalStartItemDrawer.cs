using UnityEditor;

namespace AutoCustomEditor
{
    public class VerticalStartItemDrawer : ItemDrawerBase
    {
        public VerticalStartItemDrawer(ItemParameter parameter, SerializedObject target) : base(parameter, target)
        {
        }

        public override void Draw(GUICustomEditorState state)
        {
            EditorGUILayout.BeginVertical();

            state.VerticalLevel++;
        }
    }
}
