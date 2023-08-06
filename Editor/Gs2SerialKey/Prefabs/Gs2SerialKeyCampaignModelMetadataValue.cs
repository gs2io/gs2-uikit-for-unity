using UnityEditor;
using UnityEngine;

namespace Editor.Gs2SerialKey.Prefabs
{
    public static class Gs2SerialKeyCampaignModelMetadataValue
    {
        [MenuItem("GameObject/UI/Game Server Services/SerialKey/Namespace/CampaignModel/Label/MetadataValue", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2SerialKey/Prefabs/CampaignModelMetadataValue.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}