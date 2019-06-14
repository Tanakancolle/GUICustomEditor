using System;
using UnityEditor;
using UnityEngine;

namespace AutoCustomEditor
{

    public static class MaterialItemDrawerCreator
    {
        public static MaterialItemDrawerBase Create(ItemParameter parameter, MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            if (parameter == null)
            {
                return null;
            }

            MaterialItemDrawerBase drawer = null;
            switch (parameter.ItemType)
            {
                case ItemType.None:
                    break;
                case ItemType.Label:

                    break;
                case ItemType.PropertyField:
                    break;
                case ItemType.Space:
                    break;
                case ItemType.IntentLevel:
                    break;
                case ItemType.CodeField:
                    break;
                case ItemType.Slider:
                    break;
                case ItemType.TextureArea:
                    break;
                case ItemType.HorizontalStart:
                    break;
                case ItemType.HorizontalEnd:
                    break;
                case ItemType.VerticalStart:
                    break;
                case ItemType.VerticalEnd:
                    break;
                default:
                    Debug.LogError("failed to create ItemDrawer : " + parameter.ItemType);
                    break;
            }

            return drawer;
        }
    }
}
