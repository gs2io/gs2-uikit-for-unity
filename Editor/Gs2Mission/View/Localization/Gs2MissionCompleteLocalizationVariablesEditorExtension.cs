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
    [CustomEditor(typeof(Gs2MissionCompleteLocalizationVariables))]
    public class Gs2MissionCompleteLocalizationVariablesEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2MissionCompleteLocalizationVariables;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2MissionOwnCompleteFetcher>() ?? original.GetComponentInParent<Gs2MissionOwnCompleteFetcher>();
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2MissionOwnCompleteFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2MissionOwnCompleteFetcher>();
                }
            }
            else {
                if (fetcher.transform.parent.GetComponent<Gs2MissionOwnCompleteList>() != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2MissionOwnCompleteFetcher), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("Complete is auto assign from Gs2MissionOwnCompleteList.", MessageType.Info);
                }
                else {
                    var context = original.GetComponent<Gs2MissionOwnCompleteContext>() ?? original.GetComponentInParent<Gs2MissionOwnCompleteContext>();
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2MissionOwnCompleteFetcher), false);
                    EditorGUI.indentLevel++;
                    context.Complete = EditorGUILayout.ObjectField("Complete", context.Complete, typeof(OwnComplete), false) as OwnComplete;
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.Complete?.NamespaceName.ToString());
                    EditorGUILayout.TextField("MissionGroupName", context.Complete?.MissionGroupName.ToString());
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