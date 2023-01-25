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
            var constantClass = new ConstantClassCreator(setting.className, setting.comment);

            InternalEditorUtility.tags
                .ForEach(
                    tagName => constantClass.AddConstantField<string>(tagName, $"\"{tagName}\"")
                );
            constantClass.Create(AwnUtilityEditorSettings.instance.constantClassPath);
        }

        [MenuItem(CommandName, true)]
        public static bool CanCreate() => ConstantClassCreator.CanCreate();
    }
}