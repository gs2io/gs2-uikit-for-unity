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

using Gs2.Unity.Gs2Mission.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Mission.Context;
using Gs2.Unity.UiKit.Gs2Mission.Fetcher;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace Gs2.Unity.UiKit.Gs2Mission.Localization.Editor
{
    [CustomEditor(typeof(Gs2MissionCounterModelLocalizationVariables))]
    public class Gs2MissionCounterModelLocalizationVariablesEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2MissionCounterModelLocalizationVariables;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2MissionCounterModelFetcher>() ?? original.GetComponentInParent<Gs2MissionCounterModelFetcher>();
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2MissionCounterModelFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2MissionCounterModelFetcher>();
                }
            }
            else {
                if (fetcher.transform.parent.GetComponent<Gs2MissionCounterModelList>() != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2MissionCounterModelFetcher), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("CounterModel is auto assign from Gs2MissionCounterModelList.", MessageType.Info);
                }
                else if (fetcher.transform.parent.GetComponent<Gs2MissionOwnCounterList>() != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2MissionOwnCounterFetcher), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("CounterModel is auto assign from Gs2MissionOwnCounterList.", MessageType.Info);
                }
                else {
                    var context = original.GetComponent<Gs2MissionCounterModelContext>() ?? original.GetComponentInParent<Gs2MissionCounterModelContext>();
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2MissionCounterModelFetcher), false);
                    EditorGUI.indentLevel++;
                    context.CounterModel = EditorGUILayout.ObjectField("CounterModel", context.CounterModel, typeof(CounterModel), false) as CounterModel;
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.CounterModel?.NamespaceName.ToString());
                    EditorGUILayout.TextField("CounterName", context.CounterModel?.CounterName.ToString());
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