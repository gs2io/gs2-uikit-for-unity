using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Editor.Gs2Enhance.Prefabs
{
    public static class Gs2EnhanceUnleashRateModelNameValue
    {
        [MenuItem("GameObject/UI/Game Server Services/Enhance/Namespace/UnleashRateModel/Label/NameValue", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Enhance/Prefabs/UnleashRateModelNameValue.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}