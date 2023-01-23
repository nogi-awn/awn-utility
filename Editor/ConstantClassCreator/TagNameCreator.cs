using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace AwnUtility.Editor
{
    public static class TagNameCreator
    {
        private const string CommandName = "Tools/Create/Tag Name Class";

        [MenuItem(CommandName)]
        public static void Create()
        {
            ConstantClassSetting setting = AwnUtilityEditorSettings.instance.tagNameClass;
            ConstantClassCreator.Create<string>(
                setting.filePath,
                InternalEditorUtility.tags.Select(tag =>
                    new ConstantClassCreator.ConstantField(
                        tag,
                        $"\"{tag}\""
                    )
                ), 
                setting.comment);
        }

        [MenuItem(CommandName, true)]
        public static bool CanCreate() => ConstantClassCreator.CanCreate();
    }
}