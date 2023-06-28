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
    [CustomEditor(typeof(Gs2MissionCounterLocalizationVariables))]
    public class Gs2MissionCounterLocalizationVariablesEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2MissionCounterLocalizationVariables;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2MissionOwnCounterFetcher>() ?? original.GetComponentInParent<Gs2MissionOwnCounterFetcher>();
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2MissionOwnCounterFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2MissionOwnCounterFetcher>();
                }
            }
            else {
                if (fetcher.transform.parent.GetComponent<Gs2MissionOwnCounterList>() != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2MissionOwnCounterFetcher), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("Counter is auto assign from Gs2MissionOwnCounterList.", MessageType.Info);
                }
                else {
                    var context = original.GetComponent<Gs2MissionOwnCounterContext>() ?? original.GetComponentInParent<Gs2MissionOwnCounterContext>();
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2MissionOwnCounterFetcher), false);
                    EditorGUI.indentLevel++;
                    context.Counter = EditorGUILayout.ObjectField("Counter", context.Counter, typeof(OwnCounter), false) as OwnCounter;
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.Counter?.NamespaceName.ToString());
                    EditorGUILayout.TextField("CounterName", context.Counter?.CounterName.ToString());
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