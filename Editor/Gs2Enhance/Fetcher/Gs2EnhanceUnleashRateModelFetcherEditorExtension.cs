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

using Gs2.Unity.Gs2Enhance.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Enhance.Context;
using Gs2.Unity.UiKit.Gs2Enhance.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Enhance.Editor
{
    [CustomEditor(typeof(Gs2EnhanceUnleashRateModelFetcher))]
    public class Gs2EnhanceUnleashRateModelFetcherEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2EnhanceUnleashRateModelFetcher;

            if (original == null) return;

            var context = original.GetComponent<Gs2EnhanceUnleashRateModelContext>() ?? original.GetComponentInParent<Gs2EnhanceUnleashRateModelContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2EnhanceUnleashRateModelContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2EnhanceUnleashRateModelContext>();
                }
            }
            else {
                if (context.gameObject.GetComponentInParent<Gs2EnhanceUnleashRateModelList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2EnhanceUnleashRateModelContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("UnleashRateModel is auto assign from Gs2EnhanceUnleashRateModelList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2EnhanceUnleashRateModelContext), false);
                    EditorGUI.indentLevel++;
                    EditorGUILayout.ObjectField("UnleashRateModel", context.UnleashRateModel, typeof(UnleashRateModel), false);
                    if (context.UnleashRateModel != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.UnleashRateModel?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("RateName", context.UnleashRateModel?.RateName?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }
            
            serializedObject.Update();
            serializedObject.ApplyModifiedProperties();
        }
    }
}