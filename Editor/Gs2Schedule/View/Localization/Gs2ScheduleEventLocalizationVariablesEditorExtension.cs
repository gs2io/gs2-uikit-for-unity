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

using Gs2.Unity.Gs2Schedule.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Schedule.Context;
using Gs2.Unity.UiKit.Gs2Schedule.Fetcher;
using UnityEditor;
using UnityEngine;
using Event = Gs2.Unity.Gs2Schedule.ScriptableObject.Event;
using UnityEngine.Localization.Components;

namespace Gs2.Unity.UiKit.Gs2Schedule.Localization.Editor
{
    [CustomEditor(typeof(Gs2ScheduleEventLocalizationVariables))]
    public class Gs2ScheduleEventLocalizationVariablesEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2ScheduleEventLocalizationVariables;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2ScheduleEventFetcher>() ?? original.GetComponentInParent<Gs2ScheduleEventFetcher>();
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2ScheduleEventFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2ScheduleEventFetcher>();
                }
            }
            else {
                var context = original.GetComponent<Gs2ScheduleEventContext>() ?? original.GetComponentInParent<Gs2ScheduleEventContext>();
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2ScheduleEventFetcher), false);
                EditorGUI.indentLevel++;
                context.Event_ = EditorGUILayout.ObjectField("Event", context.Event_, typeof(Event), false) as Event;
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", context.Event_?.NamespaceName.ToString());
                EditorGUILayout.TextField("EventName", context.Event_?.EventName.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.Update();
            original.target = EditorGUILayout.ObjectField("Target", original.target, typeof(LocalizeStringEvent), true) as LocalizeStringEvent;

            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif