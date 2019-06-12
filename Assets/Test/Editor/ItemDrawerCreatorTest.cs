using UnityEditor;
using UnityEngine;

namespace AutoCustomEditor
{
    public enum ItemTypeTest
    {
        None,
        HorizontalEnd,
        HorizontalStart,
        IntentLevel,
        Label,
        Space,
        VerticalEnd,
        VerticalStart,
        CodeField,
        PropertyField,
        Slider,
        TextureArea,
    }

    public static class ItemDrawerCreatorTest
    {
        public static ItemDrawerBase Create(ItemParameter parameter, SerializedObject target)
        {
            if (parameter == null)
            {
                return null;
            }

            ItemDrawerBase drawer = null;
            switch (parameter.ItemType)
            {
                case ItemType.None: break;
                case ItemType.HorizontalEnd: drawer = new HorizontalEndItemDrawer(parameter, target); break;
                case ItemType.HorizontalStart: drawer = new HorizontalStartItemDrawer(parameter, target); break;
                case ItemType.IntentLevel: drawer = new IntentLevelItemDrawer(parameter, target); break;
                case ItemType.Label: drawer = new LabelItemDrawer(parameter, target); break;
                case ItemType.Space: drawer = new SpaceItemDrawer(parameter, target); break;
                case ItemType.VerticalEnd: drawer = new VerticalEndItemDrawer(parameter, target); break;
                case ItemType.VerticalStart: drawer = new VerticalStartItemDrawer(parameter, target); break;
                case ItemType.CodeField: drawer = new CodeFieldItemDrawer(parameter, target); break;
                case ItemType.PropertyField: drawer = new PropertyFieldItemDrawer(parameter, target); break;
                case ItemType.Slider: drawer = new SliderItemDrawer(parameter, target); break;
                case ItemType.TextureArea: drawer = new TextureAreaItemDrawer(parameter, target); break;
                default:
                    Debug.LogError("failed to create ItemDrawer : " + parameter.ItemType);
                    break;
            }

            return drawer;
        }
    }
}