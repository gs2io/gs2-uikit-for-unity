using UnityEditor;
using UnityEngine;

namespace Editor.Gs2Enchant.Prefabs
{
    public static class Gs2EnchantBalanceParameterModelList
    {
        [MenuItem("GameObject/UI/Game Server Services/Enchant/Namespace/BalanceParameterModelList", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Enchant/Prefabs/BalanceParameterModelList.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}