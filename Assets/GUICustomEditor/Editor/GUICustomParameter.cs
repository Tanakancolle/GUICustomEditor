using UnityEngine;

namespace AutoCustomEditor
{
    public class GUICustomParameter : ScriptableObject
    {
        [SerializeField]
        public ItemParameter[] Items;
    }
}
