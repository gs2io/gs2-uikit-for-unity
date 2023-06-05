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

using Gs2.Unity.Gs2Schedule.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Schedule.Context;
using UnityEditor;
using UnityEngine;
using Event = Gs2.Unity.Gs2Schedule.ScriptableObject.Event;

namespace Gs2.Unity.UiKit.Gs2Schedule.Editor
{
    [CustomEditor(typeof(Gs2ScheduleEventRelativeDurationEnabler))]
    public class Gs2ScheduleEventRelativeDurationEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2ScheduleEventRelativeDurationEnabler;

            if (original == null) return;

            var context = original.GetComponentInParent<Gs2ScheduleEventContext>();
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2ScheduleEventContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2ScheduleEventContext>();
                }
            }
            else {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2ScheduleEventContext), false);
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
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2ScheduleEventRelativeDurationEnabler.Expression.In || original.expression == Gs2ScheduleEventRelativeDurationEnabler.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableRelativeDurations"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableRelativeDuration"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}