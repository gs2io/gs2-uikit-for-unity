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

using Gs2.Unity.Gs2Schedule.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Schedule.Context;
using UnityEditor;
using UnityEngine;
using Event = Gs2.Unity.Gs2Schedule.ScriptableObject.Event;

namespace Gs2.Unity.UiKit.Gs2Schedule.Enabler.Editor
{
    [CustomEditor(typeof(Gs2ScheduleTriggerNameEnabler))]
    public class Gs2ScheduleTriggerNameEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2ScheduleTriggerNameEnabler;

            if (original == null) return;

            var context = original.GetComponent<Gs2ScheduleOwnTriggerContext>() ?? original.GetComponentInParent<Gs2ScheduleOwnTriggerContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2ScheduleOwnTriggerContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2ScheduleOwnTriggerContext>();
                }
            }
            else {
                if (context.transform.parent.GetComponent<Gs2ScheduleOwnTriggerList>() != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2ScheduleOwnTriggerContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("Trigger is auto assign from Gs2ScheduleOwnTriggerList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2ScheduleOwnTriggerContext), false);
                    EditorGUI.indentLevel++;
                    context.Trigger = EditorGUILayout.ObjectField("Trigger", context.Trigger, typeof(OwnTrigger), false) as OwnTrigger;
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.Trigger?.NamespaceName.ToString());
                    EditorGUILayout.TextField("TriggerName", context.Trigger?.TriggerName.ToString());
                    EditorGUI.indentLevel--;
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2ScheduleTriggerNameEnabler.Expression.In || original.expression == Gs2ScheduleTriggerNameEnabler.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableNames"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableName"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}