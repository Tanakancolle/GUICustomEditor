using System;
using UnityEditor;

namespace AutoCustomEditor
{
    public class SliderItemDrawer : SerializedPropertyItemDrawerBase
    {
        private float _min;
        private float _max;

        public SliderItemDrawer(ItemParameter parameter, SerializedObject target) : base(parameter, target)
        {
            _min = GetValueFromIndex(parameter.Floats, 0);
            _max = GetValueFromIndex(parameter.Floats, 1);
        }

       public override void Draw(GUICustomEditorState state)
        {
            if (_property == null)
            {
                return;
            }

            switch (_property.propertyType)
            {
                case SerializedPropertyType.Generic:
                    break;
                case SerializedPropertyType.Integer:
                    EditorGUILayout.IntSlider(_property, (int)_min, (int)_max, _drawNameContent);
                    break;
                case SerializedPropertyType.Boolean:
                    break;
                case SerializedPropertyType.Float:
                    EditorGUILayout.Slider(_property, _min, _max, _drawNameContent);
                    break;
                case SerializedPropertyType.String:
                    break;
                case SerializedPropertyType.Color:
                    break;
                case SerializedPropertyType.ObjectReference:
                    break;
                case SerializedPropertyType.LayerMask:
                    break;
                case SerializedPropertyType.Enum:
                    break;
                case SerializedPropertyType.Vector2:
                    break;
                case SerializedPropertyType.Vector3:
                    break;
                case SerializedPropertyType.Vector4:
                    break;
                case SerializedPropertyType.Rect:
                    break;
                case SerializedPropertyType.ArraySize:
                    break;
                case SerializedPropertyType.Character:
                    break;
                case SerializedPropertyType.AnimationCurve:
                    break;
                case SerializedPropertyType.Bounds:
                    break;
                case SerializedPropertyType.Gradient:
                    break;
                case SerializedPropertyType.Quaternion:
                    break;
                case SerializedPropertyType.ExposedReference:
                    break;
                case SerializedPropertyType.FixedBufferSize:
                    break;
                case SerializedPropertyType.Vector2Int:
                    break;
                case SerializedPropertyType.Vector3Int:
                    break;
                case SerializedPropertyType.RectInt:
                    break;
                case SerializedPropertyType.BoundsInt:
                    break;
            }
        }
    }
}
