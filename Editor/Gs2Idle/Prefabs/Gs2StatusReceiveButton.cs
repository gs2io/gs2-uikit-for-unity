using UnityEditor;
using UnityEngine;

namespace Editor.Gs2Idle.Prefabs
{
    public static class Gs2IdleStatusReceiveButton
    {
        [MenuItem("GameObject/UI/Game Server Services/Idle/Namespace/User/Status/Action/Receive", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Idle/Prefabs/StatusReceiveButton.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}