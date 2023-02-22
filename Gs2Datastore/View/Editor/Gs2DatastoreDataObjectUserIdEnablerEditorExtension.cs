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
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Datastore.Editor
{
    [CustomEditor(typeof(Gs2DatastoreDataObjectUserIdEnabler))]
    public class Gs2DatastoreDataObjectUserIdEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2DatastoreDataObjectUserIdEnabler;

            if (original == null) return;

            var context = original.GetComponentInParent<Gs2DatastoreOwnDataObjectContext>();
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2DatastoreOwnDataObjectContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2DatastoreOwnDataObjectContext>();
                }
            }
            else {
                if (context.transform.parent.GetComponent<Gs2DatastoreOwnDataObjectList>() != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2DatastoreOwnDataObjectContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("DataObject is auto assign from Gs2DatastoreOwnDataObjectList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2DatastoreOwnDataObjectContext), false);
                    EditorGUI.indentLevel++;
                    EditorGUILayout.ObjectField("DataObject", context.DataObject, typeof(OwnDataObject), false);
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.DataObject?.NamespaceName.ToString());
                    EditorGUILayout.TextField("DataObjectName", context.DataObject?.DataObjectName.ToString());
                    EditorGUI.indentLevel--;
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2DatastoreDataObjectUserIdEnabler.Expression.In || original.expression == Gs2DatastoreDataObjectUserIdEnabler.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableUserIds"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableUserId"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}