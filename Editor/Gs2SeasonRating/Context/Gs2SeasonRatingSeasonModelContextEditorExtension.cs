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

using Gs2.Unity.Gs2SeasonRating.ScriptableObject;
using Gs2.Unity.UiKit.Gs2SeasonRating.Context;
using Gs2.Unity.UiKit.Gs2SeasonRating.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2SeasonRating.Editor
{
    [CustomEditor(typeof(Gs2SeasonRatingSeasonModelContext))]
    public class Gs2SeasonRatingSeasonModelContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2SeasonRatingSeasonModelContext;

            if (original == null) return;

            serializedObject.Update();

            if (original.SeasonModel == null) {
                var list = original.GetComponentInParent<Gs2SeasonRatingSeasonModelList>(true);
                if (list != null) {
                    EditorGUILayout.HelpBox("SeasonModel is auto assign from Gs2SeasonRatingSeasonModelList.", MessageType.Info);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("List", list, typeof(Gs2SeasonRatingSeasonModelList), false);
                    EditorGUI.EndDisabledGroup();
                }
                else {
                    EditorGUILayout.HelpBox("SeasonModel not assigned.", MessageType.Error);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("_seasonModel"), true);
                }
            }
            else {
                original.SeasonModel = EditorGUILayout.ObjectField("SeasonModel", original.SeasonModel, typeof(SeasonModel), false) as SeasonModel;
                EditorGUI.BeginDisabledGroup(true);
                if (original.SeasonModel != null) {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", original.SeasonModel?.NamespaceName?.ToString());
                    EditorGUILayout.TextField("SeasonName", original.SeasonModel?.SeasonName?.ToString());
                    EditorGUI.indentLevel--;
                }
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}