using AutoCustomEditor;
using UnityEditor;

namespace Test.Editor
{
    [CustomEditor(typeof(TestScriptableObject))]
    public class TestScriptableObjectEditor : AutoCustomEditorBase
    {
    }
}
