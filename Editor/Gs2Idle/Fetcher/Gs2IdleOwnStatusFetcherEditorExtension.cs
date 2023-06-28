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

using Gs2.Unity.Gs2Idle.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Idle.Context;
using Gs2.Unity.UiKit.Gs2Idle.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Idle.Editor
{
    [CustomEditor(typeof(Gs2IdleOwnStatusFetcher))]
    public class Gs2IdleOwnStatusFetcherEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2IdleOwnStatusFetcher;

            if (original == null) return;

            var context = original.GetComponent<Gs2IdleOwnStatusContext>() ?? original.GetComponentInParent<Gs2IdleOwnStatusContext>();
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2IdleOwnStatusContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2IdleOwnStatusContext>();
                }
            }
            else {
                if (context.transform.parent.GetComponent<Gs2IdleOwnStatusList>() != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2IdleOwnStatusContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("Status is auto assign from Gs2IdleOwnStatusList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2IdleOwnStatusContext), false);
                    EditorGUI.indentLevel++;
                    context.Status = EditorGUILayout.ObjectField("Status", context.Status, typeof(OwnStatus), false) as OwnStatus;
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.Status?.NamespaceName.ToString());
                    EditorGUILayout.TextField("CategoryName", context.Status?.CategoryName.ToString());
                    EditorGUI.indentLevel--;
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }
            
            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onError"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}