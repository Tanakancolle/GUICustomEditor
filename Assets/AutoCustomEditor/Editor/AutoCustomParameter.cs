using UnityEngine;

namespace AutoCustomEditor
{
    [CreateAssetMenu]
    public class AutoCustomParameter : ScriptableObject
    {
        [SerializeField]
        public ItemParameter[] Items;
    }
}
