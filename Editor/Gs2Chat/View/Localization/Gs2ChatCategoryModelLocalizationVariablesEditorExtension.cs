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

using Gs2.Unity.Gs2Chat.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Chat.Context;
using Gs2.Unity.UiKit.Gs2Chat.Fetcher;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace Gs2.Unity.UiKit.Gs2Chat.Localization.Editor
{
    [CustomEditor(typeof(Gs2ChatCategoryModelLocalizationVariables))]
    public class Gs2ChatCategoryModelLocalizationVariablesEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2ChatCategoryModelLocalizationVariables;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2ChatCategoryModelFetcher>() ?? original.GetComponentInParent<Gs2ChatCategoryModelFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2ChatCategoryModelFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2ChatCategoryModelFetcher>();
                }
            }
            else {
                if (fetcher.gameObject.GetComponentInParent<Gs2ChatCategoryModelList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2ChatCategoryModelFetcher), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("CategoryModel is auto assign from Gs2ChatCategoryModelList.", MessageType.Info);
                }
                else {
                    var context = original.GetComponent<Gs2ChatCategoryModelContext>() ?? original.GetComponentInParent<Gs2ChatCategoryModelContext>(true);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2ChatCategoryModelFetcher), false);
                    EditorGUI.indentLevel++;
                    context.CategoryModel = EditorGUILayout.ObjectField("CategoryModel", context.CategoryModel, typeof(CategoryModel), false) as CategoryModel;
                    if (context.CategoryModel != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.CategoryModel?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("Category", context.CategoryModel?.Category.ToString());
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