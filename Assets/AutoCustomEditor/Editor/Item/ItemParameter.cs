using System;

namespace AutoCustomEditor
{
    [Serializable]
    public class ItemParameter
    {
        public ItemType ItemType;
        public string PropertyName;
        public string DrawName;
        public object[] Values;
    }
}
