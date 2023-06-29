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

using Gs2.Unity.Gs2Mission.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Mission.Context;
using Gs2.Unity.UiKit.Gs2Mission.Fetcher;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace Gs2.Unity.UiKit.Gs2Mission.Localization.Editor
{
    [CustomEditor(typeof(Gs2MissionMissionGroupModelLocalizationVariables))]
    public class Gs2MissionMissionGroupModelLocalizationVariablesEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2MissionMissionGroupModelLocalizationVariables;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2MissionMissionGroupModelFetcher>() ?? original.GetComponentInParent<Gs2MissionMissionGroupModelFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2MissionMissionGroupModelFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2MissionMissionGroupModelFetcher>();
                }
            }
            else {
                if (fetcher.gameObject.GetComponentInParent<Gs2MissionMissionGroupModelList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2MissionMissionGroupModelFetcher), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("MissionGroupModel is auto assign from Gs2MissionMissionGroupModelList.", MessageType.Info);
                }
                else {
                    var context = original.GetComponent<Gs2MissionMissionGroupModelContext>() ?? original.GetComponentInParent<Gs2MissionMissionGroupModelContext>(true);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2MissionMissionGroupModelFetcher), false);
                    EditorGUI.indentLevel++;
                    context.MissionGroupModel = EditorGUILayout.ObjectField("MissionGroupModel", context.MissionGroupModel, typeof(MissionGroupModel), false) as MissionGroupModel;
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.MissionGroupModel?.NamespaceName.ToString());
                    EditorGUILayout.TextField("MissionGroupName", context.MissionGroupModel?.MissionGroupName.ToString());
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