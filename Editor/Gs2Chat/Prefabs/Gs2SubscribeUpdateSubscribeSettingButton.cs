using UnityEditor;
using UnityEngine;

namespace Editor.Gs2Chat.Prefabs
{
    public static class Gs2ChatSubscribeUpdateSubscribeSettingButton
    {
        [MenuItem("GameObject/UI/Game Server Services/Chat/Namespace/User/Subscribe/Action/UpdateSubscribeSetting", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Chat/Prefabs/SubscribeUpdateSubscribeSettingButton.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}