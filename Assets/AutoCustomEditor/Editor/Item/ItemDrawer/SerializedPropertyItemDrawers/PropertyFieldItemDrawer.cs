using UnityEditor;

namespace AutoCustomEditor
{
    public class PropertyFieldItemDrawer : SerializedPropertyItemDrawerBase
    {
        public PropertyFieldItemDrawer(ItemParameter parameter, SerializedObject target) : base(parameter, target)
        {
        }

        public override void Draw(AutoCustomEditorState state)
        {
            if (_property == null)
            {
                return;
            }
            EditorGUILayout.PropertyField(_property, _drawNameContent, _property.isArray);
        }
    }
}
