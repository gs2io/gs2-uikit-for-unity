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

using Gs2.Unity.Gs2Exchange.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Exchange.Context;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Exchange.Editor
{
    [CustomEditor(typeof(Gs2ExchangeAwaitExchangedAtEnabler))]
    public class Gs2ExchangeAwaitExchangedAtEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2ExchangeAwaitExchangedAtEnabler;

            if (original == null) return;

            var context = original.GetComponent<Gs2ExchangeOwnAwaitContext>() ?? original.GetComponentInParent<Gs2ExchangeOwnAwaitContext>();
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2ExchangeOwnAwaitContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2ExchangeOwnAwaitContext>();
                }
            }
            else {
                if (context.transform.parent.GetComponent<Gs2ExchangeOwnAwaitList>() != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2ExchangeOwnAwaitContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("Await is auto assign from Gs2ExchangeOwnAwaitList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2ExchangeOwnAwaitContext), false);
                    EditorGUI.indentLevel++;
                    context.Await_ = EditorGUILayout.ObjectField("Await", context.Await_, typeof(OwnAwait), false) as OwnAwait;
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.Await_?.NamespaceName.ToString());
                    EditorGUILayout.TextField("AwaitName", context.Await_?.AwaitName.ToString());
                    EditorGUI.indentLevel--;
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2ExchangeAwaitExchangedAtEnabler.Expression.In || original.expression == Gs2ExchangeAwaitExchangedAtEnabler.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableExchangedAts"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableExchangedAt"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}