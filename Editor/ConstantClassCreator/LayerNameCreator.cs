using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace AwnUtility.Editor
{
    public static class LayerNameCreator
    {
        private const string CommandName = "Tools/Create/Layer Name Class";
        [MenuItem(CommandName)]
        public static void Create()
        {
            ConstantClassSetting setting = AwnUtilityEditorSettings.instance.layerNameClass;
            ConstantClassCreator.Create<int>(
                setting.filePath,
                InternalEditorUtility.layers.Select(layer =>
                    new ConstantClassCreator.ConstantField(
                        layer,
                        LayerMask.NameToLayer(layer).ToString()
                    )
                ),
                setting.comment);
        }

        [MenuItem(CommandName, true)]
        public static bool CanCreate() => ConstantClassCreator.CanCreate();
    }
}