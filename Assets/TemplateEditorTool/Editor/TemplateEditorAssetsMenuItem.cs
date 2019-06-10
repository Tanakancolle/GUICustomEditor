using UnityEditor;
using System.Collections.Generic;

namespace TemplateEditor
{
   public class TemplateEditorAssetsMenuItem
    {
        private const string MenuItemPrefix = "Assets/Create/Template/";

        [MenuItem(MenuItemPrefix, false, 0)]
       public static void Dummy()
        {
        }


        [MenuItem(MenuItemPrefix + "AutoCustomEditorGroup", false, 1000)]
        public static void AutoCustomEditorGroup()
        {
            TemplateUtility.OpenEditorWindow("a89a2000ddcd44d4ca39bbc9f726a3b6");
       }

    }
}