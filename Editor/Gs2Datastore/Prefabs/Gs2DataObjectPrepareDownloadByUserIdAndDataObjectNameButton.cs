using UnityEditor;
using UnityEngine;

namespace Editor.Gs2Datastore.Prefabs
{
    public static class Gs2DatastoreDataObjectPrepareDownloadByUserIdAndDataObjectNameButton
    {
        [MenuItem("GameObject/UI/Game Server Services/Datastore/Namespace/User/DataObject/Action/PrepareDownloadByUserIdAndDataObjectName", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Datastore/Prefabs/DataObjectPrepareDownloadByUserIdAndDataObjectNameButton.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}