using UnityEditor;
using UnityEngine;

namespace Editor.Gs2Exchange.Prefabs
{
    public static class Gs2ExchangeIncrementalRateModelList
    {
        [MenuItem("GameObject/UI/Game Server Services/Exchange/Namespace/IncrementalRateModelList", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Exchange/Prefabs/IncrementalRateModelList.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}