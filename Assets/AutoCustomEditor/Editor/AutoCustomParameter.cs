using UnityEngine;

namespace AutoCustomEditor
{
    public class AutoCustomParameter : ScriptableObject
    {
        [SerializeField]
        public ItemParameter[] Items;
    }
}
