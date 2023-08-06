using UnityEditor;
using UnityEngine;

namespace Editor.Gs2SerialKey.Prefabs
{
    public static class Gs2SerialKeyCampaignModelContainer
    {
        [MenuItem("GameObject/UI/Game Server Services/SerialKey/Namespace/CampaignModelContainer", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2SerialKey/Prefabs/CampaignModelContainer.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}