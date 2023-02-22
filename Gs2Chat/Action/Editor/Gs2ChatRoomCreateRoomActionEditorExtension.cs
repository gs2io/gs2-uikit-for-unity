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

using Gs2.Unity.Gs2Chat.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Chat.Context;
using Gs2.Unity.UiKit.Gs2Chat.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Chat.Editor
{
    [CustomEditor(typeof(Gs2ChatRoomCreateRoomAction))]
    public class Gs2ChatRoomCreateRoomActionEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2ChatRoomCreateRoomAction;

            if (original == null) return;

            var context = original.GetComponentInParent<Gs2ChatNamespaceContext>();
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2ChatNamespaceContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2ChatNamespaceContext>();
                }
            }
            else {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2ChatNamespaceContext), false);
                EditorGUI.indentLevel++;
                EditorGUILayout.ObjectField("Namespace", context.Namespace, typeof(Namespace), false);
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", context.Namespace?.NamespaceName.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("NamespaceName"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Name"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Metadata"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Password"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("WhiteListUserIds"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onChangeNamespaceName"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onChangeName"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onChangeMetadata"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onChangePassword"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onChangeWhiteListUserIds"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onCreateRoomComplete"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onError"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}