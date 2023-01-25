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
    public class ConstantClassCreator
    {
        private string className, comment;
        private List<ConstantField> fields = new();
        public ConstantClassCreator(string className, string comment)
        {
            this.className = className;
            this.comment = comment;
        }

        internal struct ConstantField
        {
            internal ConstantField(Type type, string unformattedIdentifier, string value)
            {
                this.type = type;
                this.identifier = TrimForIdentifier(unformattedIdentifier);
                this.value = value;
            }
            internal Type type;
            internal string identifier;
            internal string value;
        }
        public ConstantClassCreator AddConstantField<TValue>(string unformattedIdentifier, string value)
        {
            fields.Add(new ConstantField(typeof(TValue), unformattedIdentifier, value));
            return this;
        }

        public void Create(string filePath)
        {
            string directoryName = Path.GetDirectoryName(filePath);
            if(!Directory.Exists(directoryName))
                Directory.CreateDirectory(directoryName);
            
            File.WriteAllText(
                filePath + className + ".cs",
                CreateScript(),
                Encoding.UTF8
                );
            AssetDatabase.Refresh(ImportAssetOptions.ImportRecursive);
        }

        public string CreateScript()
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

            foreach(var field in fields)
            {
                builder
                    .Append(Indent)
                    .AppendFormat(
                        @"public const {0} {1} = {2};",
                        field.type.FullName,
                        field.identifier,
                        field.value)
                    .AppendLine();
            }
            
            builder.AppendLine("}");
            return builder.ToString();
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
        public static string TrimForIdentifier(string str)
        {
            Array.ForEach(InvalidCharacters, ic => str = str.Replace(ic, string.Empty));
            return str;
        }
        public static bool CanCreate()
        {
            return !EditorApplication.isPlaying && !Application.isPlaying && !EditorApplication.isCompiling;
        }
    }

}