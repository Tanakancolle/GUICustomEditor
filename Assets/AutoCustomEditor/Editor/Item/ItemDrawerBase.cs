using UnityEditor;
using UnityEngine;

namespace AutoCustomEditor
{
    public abstract class ItemDrawerBase
    {
        protected ItemDrawerBase(ItemParameter parameter, SerializedObject target)
        {
        }

        public abstract void Draw();

        protected virtual GUIContent GetGUIContent(string draw)
        {
            return new GUIContent(draw);
        }
    }
}
