using System;
using UnityEditor;
using UnityEngine;

namespace AutoCustomEditor
{
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
                case ItemType.PropertyField:
                    drawer = new PropertyFieldItemDrawer(parameter, target);
                    break;
                default:
                    Debug.LogError("failed to create ItemDrawer : " + parameter.ItemType);
                    break;
            }

            return drawer;
        }
    }
}
