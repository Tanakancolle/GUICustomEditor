using UnityEditor;
using UnityEngine;

namespace AutoCustomEditor
{
    public abstract class MaterialItemDrawerBase
    {
        protected MaterialItemDrawerBase(ItemParameter parameter, MaterialEditor materialEditor, MaterialProperty[] properties)
        {
        }

        public abstract void Draw(AutoCustomEditorState state, MaterialEditor materialEditor);

        protected virtual GUIContent GetGUIContent(string draw)
        {
            return new GUIContent(draw);
        }

        protected T GetValueFromIndex<T>(T[] list, int index)
        {
            if (list == null || list.Length <= index)
            {
                return default(T);
            }

            return list[index];
        }
    }
}
