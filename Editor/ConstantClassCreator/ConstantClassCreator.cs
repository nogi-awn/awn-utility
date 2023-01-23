using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace AwnUtility.Editor
{
    /// <summary>
    /// 定数をメンバーに持つ静的クラスを生成するクラス
    /// </summary>
    public static class ConstantClassCreator
    {
        public struct ConstantField
        {
            public ConstantField(string unformattedIdentifier, string value)
            {
                this.identifier = TrimForIdentifier(unformattedIdentifier);
                this.value = value;
            }
            public string identifier;
            public string value;
        }
        private static readonly string[] InvalidCharacters =
        {
            " ", "!", "\"", "#", "$",
            "%", "&", "\'", "(", ")",
            "-", "=", "^",  "~", "\\",
            "|", "[", "{",  "@", "`",
            "]", "}", ":",  "*", ";",
            "+", "/", "?",  ".", ">",
            ",", "<"
        };
        private static readonly string Indent = "    ";

        public static void Create<TValue>(string path, IEnumerable<ConstantField> fields, string comment = null)
        {
            if(!CanCreate())
                return;

            string directoryName = Path.GetDirectoryName(path);
            if(!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
            
            File.WriteAllText(
                path,
                CreateScript<TValue>(Path.GetFileNameWithoutExtension(path), fields, comment),
                Encoding.UTF8
                );
            AssetDatabase.Refresh(ImportAssetOptions.ImportRecursive);
        }
        public static string CreateScript<TValue>(string className, IEnumerable<ConstantField> fields, string comment = null)
        {
            var builder = new StringBuilder();

            if(comment != null)
            {
                builder
                    .AppendLine("/// <summary>")
                    .AppendFormat("/// {0}", comment).AppendLine()
                    .AppendLine("/// </summary>");
            }

            builder
                .AppendFormat("public static class {0}", className).AppendLine()
                .AppendLine("{");

            string typeName = typeof(TValue).FullName;
            foreach(var field in fields)
            {
                builder
                    .Append(Indent)
                    .AppendFormat(
                        @"public const {0} {1} = {2};",
                        typeName,
                        field.identifier,
                        field.value)
                    .AppendLine();
            }
            
            builder.AppendLine("}");
            return builder.ToString();
        }

        public static bool CanCreate()
        {
            return !EditorApplication.isPlaying && !Application.isPlaying && !EditorApplication.isCompiling;
        }

        public static string TrimForIdentifier(string str)
        {
            Array.ForEach(InvalidCharacters, ic => str = str.Replace(ic, string.Empty));
            return str;
        }
    }
}