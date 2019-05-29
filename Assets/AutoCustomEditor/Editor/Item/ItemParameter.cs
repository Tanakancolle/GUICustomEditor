using System;

namespace AutoCustomEditor
{

    /// <summary>
    /// ItemParameterのプロパティタイプ
    /// ※ItemParameterの変数名と要素名を同じにする
    /// </summary>
    public enum ItemParameterProperty
    {
        ItemType,
        PropertyName,
        DrawName,
        Ints,
        Floats,
    }

    [Serializable]
    public class ItemParameter
    {
        public ItemType ItemType;
        public string PropertyName;
        public string DrawName;
        public int[] Ints;
        public float[] Floats;
    }
}
