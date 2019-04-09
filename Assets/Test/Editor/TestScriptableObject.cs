using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class TestScriptableObject: ScriptableObject
{
    [SerializeField]
    private int _intValue;

    [SerializeField]
    private int _floatValue;
}
