/*
 * Copyright 2016 Game Server Services, Inc. or its affiliates. All Rights
 * Reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License").
 * You may not use this file except in compliance with the License.
 * A copy of the License is located at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 * or in the "license" file accompanying this file. This file is distributed
 * on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
 * express or implied. See the License for the specific language governing
 * permissions and limitations under the License.
 */
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable CheckNamespace

using Gs2.Unity.Gs2Datastore.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Datastore.Context;
using Gs2.Unity.UiKit.Gs2Datastore.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Datastore.Editor
{
    [CustomEditor(typeof(Gs2DatastoreOwnDataObjectContext))]
    public class Gs2DatastoreOwnDataObjectContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2DatastoreOwnDataObjectContext;

            if (original == null) return;

            if (original.DataObject == null) {
                if (original.transform.parent.GetComponent<Gs2DatastoreOwnDataObjectList>() != null) {
                    EditorGUILayout.HelpBox("OwnDataObject is auto assign from Gs2DatastoreOwnDataObjectList.", MessageType.Info);
                }
                else {
                    EditorGUILayout.HelpBox("OwnDataObject not assigned.", MessageType.Error);
                    EditorGUILayout.ObjectField("OwnDataObject", original.DataObject, typeof(OwnDataObject), false);
                }
            }
            else {
                EditorGUILayout.ObjectField("OwnDataObject", original.DataObject, typeof(OwnDataObject), false);
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", original.DataObject?.NamespaceName.ToString());
                EditorGUILayout.TextField("DataObjectName", original.DataObject?.DataObjectName.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }
            
            serializedObject.Update();
            serializedObject.ApplyModifiedProperties();
        }
    }
}