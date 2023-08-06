using UnityEditor;
using UnityEngine;

namespace Editor.Gs2Inventory.Prefabs
{
    public static class Gs2InventoryInventoryModelContainer
    {
        [MenuItem("GameObject/UI/Game Server Services/Inventory/Namespace/InventoryModelContainer", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Inventory/Prefabs/InventoryModelContainer.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}