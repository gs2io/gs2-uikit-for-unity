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
using Gs2.Unity.UiKit.Gs2Exchange.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Exchange.Editor
{
    [CustomEditor(typeof(Gs2ExchangeRateModelContext))]
    public class Gs2ExchangeRateModelContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2ExchangeRateModelContext;

            if (original == null) return;

            if (original.RateModel == null) {
                if (original.transform.parent.GetComponent<Gs2ExchangeRateModelList>() != null) {
                    EditorGUILayout.HelpBox("RateModel is auto assign from Gs2ExchangeRateModelList.", MessageType.Info);
                }
                else {
                    EditorGUILayout.HelpBox("RateModel not assigned.", MessageType.Error);
                    EditorGUILayout.ObjectField("RateModel", original.RateModel, typeof(RateModel), false);
                }
            }
            else {
                EditorGUILayout.ObjectField("RateModel", original.RateModel, typeof(RateModel), false);
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", original.RateModel?.NamespaceName.ToString());
                EditorGUILayout.TextField("RateName", original.RateModel?.RateName.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }
            
            serializedObject.Update();
            serializedObject.ApplyModifiedProperties();
        }
    }
}