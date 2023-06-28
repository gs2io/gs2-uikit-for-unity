using UnityEditor;
using UnityEngine;

namespace Editor.Gs2Stamina.Prefabs
{
    public static class Gs2StaminaNextRecoverCountDown
    {
        [MenuItem("GameObject/UI/Game Server Services/Stamina/NextRecoverCountDown", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Stamina/Prefabs/NextRecoverCountDown.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}
