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

using Gs2.Unity.Gs2JobQueue.ScriptableObject;
using Gs2.Unity.UiKit.Gs2JobQueue.Context;
using Gs2.Unity.UiKit.Gs2JobQueue.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2JobQueue.Editor
{
    [CustomEditor(typeof(Gs2JobQueueJobRunAction))]
    public class Gs2JobQueueJobRunActionEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2JobQueueJobRunAction;

            if (original == null) return;

            var context = original.GetComponentInParent<Gs2JobQueueOwnJobContext>();
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2JobQueueOwnJobContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2JobQueueOwnJobContext>();
                }
            }
            else {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2JobQueueOwnJobContext), false);
                EditorGUI.indentLevel++;
                EditorGUILayout.ObjectField("OwnJob", context.Job, typeof(OwnJob), false);
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", context.Job?.NamespaceName.ToString());
                EditorGUILayout.TextField("JobName", context.Job?.JobName.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onRunComplete"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onError"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}