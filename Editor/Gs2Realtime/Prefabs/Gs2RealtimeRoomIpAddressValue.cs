using UnityEditor;
using UnityEngine;

namespace Editor.Gs2Realtime.Prefabs
{
    public static class Gs2RealtimeRoomIpAddressValue
    {
        [MenuItem("GameObject/UI/Game Server Services/Realtime/Namespace/Room/Label/IpAddressValue", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Realtime/Prefabs/RoomIpAddressValue.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}