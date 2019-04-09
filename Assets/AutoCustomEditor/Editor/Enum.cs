namespace AutoCustomEditor
{
    public enum ItemType
    {
        None,
        PropertyField,
    }

    public enum ItemParameterDrawType
    {
        None,
        PropertyNameAndDrawName
    }

    /// <summary>
    /// ItemParameterのプロパティタイプ
    /// ※ItemParameterの変数名と要素名を同じにする
    /// </summary>
    public enum ItemParameterProperty
    {
        ItemType,
        PropertyName,
        DrawName,
        Values,
    }
}
