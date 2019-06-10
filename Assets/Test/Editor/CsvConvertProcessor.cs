using System.Collections.Generic;
using TemplateEditor;
using UnityEngine;

public class CsvConvertProcessor : ScriptableObject, IProcessChain
{
    private enum ReplaceWordType
    {
        Header,
        Rows,
    }

    private static readonly string[] ReplaceWords =
    {
        "Header",
        "Rows",
    };

    [SerializeField]
    private TextAsset _targetTextAsset;

    public void Process(ProcessMetadata metadata, Dictionary<string, object> result)
    {
        var lines = _targetTextAsset.text.Replace("\r", "").Split('\n');
        var rows = new List<string[]>();
        var isHeader = true;
        foreach (var line in lines)
        {
            var row = line.Split(',');
            if (isHeader)
            {
                isHeader = false;
                result.Add(this.ConvertReplaceWord(ReplaceWords[(int) ReplaceWordType.Header], result), row);
                continue;
            }

            rows.Add(row);
        }

        result.Add(this.ConvertReplaceWord(ReplaceWords[(int) ReplaceWordType.Rows], result), rows);
    }

    public string[] GetReplaceWords()
    {
        return ReplaceWords;
    }

    public string GetDescription()
    {
        return "CSVの行毎にRowsに格納します";
    }

    public ProcessFileType GetFileType()
    {
        return ProcessFileType.ScriptableObject;
    }
}
