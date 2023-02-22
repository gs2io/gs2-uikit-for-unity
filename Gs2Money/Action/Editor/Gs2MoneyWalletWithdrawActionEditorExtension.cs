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

using Gs2.Unity.Gs2Money.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Money.Context;
using Gs2.Unity.UiKit.Gs2Money.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Money.Editor
{
    [CustomEditor(typeof(Gs2MoneyWalletWithdrawAction))]
    public class Gs2MoneyWalletWithdrawActionEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2MoneyWalletWithdrawAction;

            if (original == null) return;

            var context = original.GetComponentInParent<Gs2MoneyOwnWalletContext>();
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2MoneyOwnWalletContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2MoneyOwnWalletContext>();
                }
            }
            else {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2MoneyOwnWalletContext), false);
                EditorGUI.indentLevel++;
                EditorGUILayout.ObjectField("OwnWallet", context.Wallet, typeof(OwnWallet), false);
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", context.Wallet?.NamespaceName.ToString());
                EditorGUILayout.TextField("Slot", context.Wallet?.Slot.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Count"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("PaidOnly"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onChangeCount"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onChangePaidOnly"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onWithdrawComplete"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onError"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}