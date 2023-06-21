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

using Gs2.Unity.Gs2Account.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Account.Context;
using Gs2.Unity.UiKit.Gs2Account.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Account.Editor
{
    [CustomEditor(typeof(Gs2AccountAccountAuthenticationAction))]
    public class Gs2AccountAccountAuthenticationActionEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2AccountAccountAuthenticationAction;

            if (original == null) return;

            var context = original.GetComponent<Gs2AccountAccountContext>() ?? original.GetComponentInParent<Gs2AccountAccountContext>();
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2AccountAccountContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2AccountAccountContext>();
                }
            }
            else {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2AccountAccountContext), false);
                EditorGUI.indentLevel++;
                context.Account = EditorGUILayout.ObjectField("Account", context.Account, typeof(Account), false) as Account;
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", context.Account?.NamespaceName.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("KeyId"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Password"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onChangeKeyId"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onChangePassword"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onAuthenticationComplete"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onError"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}