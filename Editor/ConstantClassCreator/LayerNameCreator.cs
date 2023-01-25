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
            AwnUtilityEditorSettings settings = AwnUtilityEditorSettings.instance;
            ConstantClassSetting classSetting = settings.layerNameClass;
            var constantClass = new ConstantClassCreator(classSetting.className, classSetting.comment);

            InternalEditorUtility.layers.ForEach(
                layerName =>
                constantClass.AddConstantField<int>(layerName,LayerMask.NameToLayer(layerName).ToString())
            );
            if(settings.includeLayerMask)
            {
                InternalEditorUtility.layers.ForEach(
                    layerName =>
                    constantClass.AddConstantField<int>(layerName + settings.layerMaskSuffix, (1 << LayerMask.NameToLayer(layerName)).ToString())
                );
            }
            constantClass.Create(settings.constantClassPath);
        }

        [MenuItem(CommandName, true)]
        public static bool CanCreate() => ConstantClassCreator.CanCreate();
    }
}