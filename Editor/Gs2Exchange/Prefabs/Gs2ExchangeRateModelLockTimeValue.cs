using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Editor.Gs2Exchange.Prefabs
{
    public static class Gs2ExchangeRateModelLockTimeValue
    {
        [MenuItem("GameObject/UI/Game Server Services/Exchange/Namespace/RateModel/Label/LockTimeValue", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Exchange/Prefabs/RateModelLockTimeValue.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}