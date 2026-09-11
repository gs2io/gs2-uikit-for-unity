using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Editor.Gs2Gateway.Prefabs
{
    public static class Gs2GatewayFirebaseTokenDeleteFirebaseTokenButton
    {
        [MenuItem("GameObject/UI/Game Server Services/Gateway/Namespace/User/FirebaseToken/Action/DeleteFirebaseToken", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Gateway/Prefabs/FirebaseTokenDeleteFirebaseTokenButton.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}