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

#if GS2_ENABLE_LOCALIZATION

using Gs2.Unity.Gs2Enhance.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Enhance.Context;
using Gs2.Unity.UiKit.Gs2Enhance.Fetcher;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace Gs2.Unity.UiKit.Gs2Enhance.Localization.Editor
{
    [CustomEditor(typeof(Gs2EnhanceUnleashRateModelLocalizationVariables))]
    public class Gs2EnhanceUnleashRateModelLocalizationVariablesEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2EnhanceUnleashRateModelLocalizationVariables;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2EnhanceUnleashRateModelFetcher>() ?? original.GetComponentInParent<Gs2EnhanceUnleashRateModelFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2EnhanceUnleashRateModelFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2EnhanceUnleashRateModelFetcher>();
                }
            }
            else {
                if (fetcher.gameObject.GetComponentInParent<Gs2EnhanceUnleashRateModelList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2EnhanceUnleashRateModelFetcher), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("UnleashRateModel is auto assign from Gs2EnhanceUnleashRateModelList.", MessageType.Info);
                }
                else {
                    var context = original.GetComponent<Gs2EnhanceUnleashRateModelContext>() ?? original.GetComponentInParent<Gs2EnhanceUnleashRateModelContext>(true);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2EnhanceUnleashRateModelFetcher), false);
                    EditorGUI.indentLevel++;
                    context.UnleashRateModel = EditorGUILayout.ObjectField("UnleashRateModel", context.UnleashRateModel, typeof(UnleashRateModel), false) as UnleashRateModel;
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
            original.target = EditorGUILayout.ObjectField("Target", original.target, typeof(LocalizeStringEvent), true) as LocalizeStringEvent;

            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif