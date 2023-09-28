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
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantAssignment
// ReSharper disable NotAccessedVariable
// ReSharper disable RedundantUsingDirective
// ReSharper disable Unity.NoNullPropagation
// ReSharper disable InconsistentNaming

#pragma warning disable CS0472

using Gs2.Unity.Gs2Exchange.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Exchange.Context;
using Gs2.Unity.UiKit.Gs2Exchange.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Exchange.Editor
{
    [CustomEditor(typeof(Gs2ExchangeOwnAwaitContext))]
    public class Gs2ExchangeOwnAwaitContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2ExchangeOwnAwaitContext;

            if (original == null) return;

            serializedObject.Update();

            if (original.Await_ == null) {
                if (original.GetComponentInParent<Gs2ExchangeOwnAwaitList>(true) != null) {
                    EditorGUILayout.HelpBox("OwnAwait is auto assign from Gs2ExchangeOwnAwaitList.", MessageType.Info);
                }
                else {
                    EditorGUILayout.HelpBox("OwnAwait not assigned.", MessageType.Error);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("_await"), true);
                }
            }
            else {
                original.Await_ = EditorGUILayout.ObjectField("OwnAwait", original.Await_, typeof(OwnAwait), false) as OwnAwait;
                EditorGUI.BeginDisabledGroup(true);
                if (original.Await_ != null) {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", original.Await_?.NamespaceName?.ToString());
                    EditorGUILayout.TextField("AwaitName", original.Await_?.AwaitName?.ToString());
                    EditorGUI.indentLevel--;
                }
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}