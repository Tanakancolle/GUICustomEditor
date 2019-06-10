using System;
using UnityEditor;
using UnityEngine;

namespace AutoCustomEditor
{
    public enum ItemType
    {
        None,
        Label,
        PropertyField,
        Space,
        IntentLevel,
        CodeTextArea,
        Slider,
        TextureArea,
        HorizontalStart,
        HorizontalEnd,
        VerticalStart,
        VerticalEnd,
    }

    public static class ItemDrawerCreator
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
                case ItemType.None:
                    break;
                case ItemType.Label:
                    drawer = new LabelItemDrawer(parameter, target);
                    break;
                case ItemType.PropertyField:
                    drawer = new PropertyFieldItemDrawer(parameter, target);
                    break;
                case ItemType.Space:
                    drawer = new SpaceItemDrawer(parameter, target);
                    break;
                case ItemType.IntentLevel:
                    drawer = new IntentLevelDrawer(parameter, target);
                    break;
                case ItemType.CodeTextArea:
                    drawer = new CodeFieldItemDrawer(parameter, target);
                    break;
                case ItemType.Slider:
                    drawer = new SliderItemDrawer(parameter, target);
                    break;
                case ItemType.TextureArea:
                    drawer = new TextureAreaItemDrawer(parameter, target);
                    break;
                case ItemType.HorizontalStart:
                    drawer = new HorizontalStartItemDrawer(parameter, target);
                    break;
                case ItemType.HorizontalEnd:
                    drawer = new HorizontalEndItemDrawer(parameter, target);
                    break;
                case ItemType.VerticalStart:
                    drawer = new VerticalStartItemDrawer(parameter, target);
                    break;
                case ItemType.VerticalEnd:
                    drawer = new VerticalEndItemDrawer(parameter, target);
                    break;
                default:
                    Debug.LogError("failed to create ItemDrawer : " + parameter.ItemType);
                    break;
            }

            return drawer;
        }
    }
}
