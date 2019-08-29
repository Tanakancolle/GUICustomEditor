using System;
using UnityEditor;
using UnityEngine;

namespace AutoCustomEditor
{
    public class SpaceItemDrawer : ItemDrawerBase
    {
        private float _space;

        public SpaceItemDrawer(ItemParameter parameter, SerializedObject target) : base(parameter, target)
        {
            _space = GetValueFromIndex(parameter.Floats, 0);
        }

        public override void Draw(GUICustomEditorState state)
        {
            GUILayout.Space(_space);
        }
    }
}
