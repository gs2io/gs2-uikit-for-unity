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

using Gs2.Unity.Gs2Inbox.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Inbox.Context;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Inbox.Editor
{
    [CustomEditor(typeof(Gs2InboxMessageReadAtEnabler))]
    public class Gs2InboxMessageReadAtEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2InboxMessageReadAtEnabler;

            if (original == null) return;

            var context = original.GetComponentInParent<Gs2InboxOwnMessageContext>();
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2InboxOwnMessageContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2InboxOwnMessageContext>();
                }
            }
            else {
                if (context.transform.parent.GetComponent<Gs2InboxOwnMessageList>() != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2InboxOwnMessageContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("Message is auto assign from Gs2InboxOwnMessageList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2InboxOwnMessageContext), false);
                    EditorGUI.indentLevel++;
                    EditorGUILayout.ObjectField("Message", context.Message, typeof(OwnMessage), false);
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.Message?.NamespaceName.ToString());
                    EditorGUILayout.TextField("MessageName", context.Message?.MessageName.ToString());
                    EditorGUI.indentLevel--;
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2InboxMessageReadAtEnabler.Expression.In || original.expression == Gs2InboxMessageReadAtEnabler.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableReadAts"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableReadAt"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}