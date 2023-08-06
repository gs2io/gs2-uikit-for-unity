using UnityEditor;
using UnityEngine;

namespace Editor.Gs2Datastore.Prefabs
{
    public static class Gs2DatastoreDataObjectRestoreDataObjectButton
    {
        [MenuItem("GameObject/UI/Game Server Services/Datastore/Namespace/User/DataObject/Action/RestoreDataObject", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(
                "Packages/io.gs2.unity.sdk.uikit/Editor/Gs2Datastore/Prefabs/DataObjectRestoreDataObjectButton.prefab"
            );

            var instance = GameObject.Instantiate(prefab, Selection.activeTransform);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
}