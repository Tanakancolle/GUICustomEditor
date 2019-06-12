using System.Collections.Generic;
using System.IO;
using TemplateEditor;
using UnityEngine;

namespace AutoCustomEditor
{
    public class ItemDrawerCreatorProcessor : ScriptableObject, IProcessChain
    {
        private enum ReplaceWordType
        {
            Types,
        }

        private static readonly string[] ReplaceWords =
        {
            "Types",
        };

        [SerializeField]
        private string _findPath;

        [SerializeField]
        private string _fileNameSuffix;

        public void Process(ProcessMetadata metadata, Dictionary<string, object> result)
        {
            var paths = Directory.GetFiles(_findPath, "*" + _fileNameSuffix + ".cs", SearchOption.AllDirectories);

            var types = new List<string>();
            foreach (var path in paths)
            {
                var fileName = Path.GetFileNameWithoutExtension(path);
                types.Add(fileName.Remove(fileName.Length - _fileNameSuffix.Length));
            }

            result.Add(ReplaceWords[(int) ReplaceWordType.Types], types);
        }

        public string[] GetReplaceWords()
        {
            return ReplaceWords;
        }

        public string GetDescription()
        {
            return "CSファイル名を取得し、特定の文字列を削除してTypesに格納します";
        }

        public ProcessFileType GetFileType()
        {
            return ProcessFileType.ScriptableObject;
        }
    }
}
