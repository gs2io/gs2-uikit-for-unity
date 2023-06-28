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

#if GS2_ENABLE_LOCALIZATION

using Gs2.Unity.Gs2Exchange.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Exchange.Context;
using Gs2.Unity.UiKit.Gs2Exchange.Fetcher;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace Gs2.Unity.UiKit.Gs2Exchange.Localization.Editor
{
    [CustomEditor(typeof(Gs2ExchangeAwaitLocalizationVariables))]
    public class Gs2ExchangeAwaitLocalizationVariablesEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2ExchangeAwaitLocalizationVariables;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2ExchangeOwnAwaitFetcher>() ?? original.GetComponentInParent<Gs2ExchangeOwnAwaitFetcher>();
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2ExchangeOwnAwaitFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2ExchangeOwnAwaitFetcher>();
                }
            }
            else {
                if (fetcher.transform.parent.GetComponent<Gs2ExchangeOwnAwaitList>() != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2ExchangeOwnAwaitFetcher), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("Await is auto assign from Gs2ExchangeOwnAwaitList.", MessageType.Info);
                }
                else {
                    var context = original.GetComponent<Gs2ExchangeOwnAwaitContext>() ?? original.GetComponentInParent<Gs2ExchangeOwnAwaitContext>();
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2ExchangeOwnAwaitFetcher), false);
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
            original.target = EditorGUILayout.ObjectField("Target", original.target, typeof(LocalizeStringEvent), true) as LocalizeStringEvent;

            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif