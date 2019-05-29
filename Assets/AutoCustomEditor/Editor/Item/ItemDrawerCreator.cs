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
                default:
                    Debug.LogError("failed to create ItemDrawer : " + parameter.ItemType);
                    break;
            }

            return drawer;
        }
    }
}
