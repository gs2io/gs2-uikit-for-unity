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

using Gs2.Unity.Gs2Mission.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Mission.Context;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Mission.Editor
{
    [CustomEditor(typeof(Gs2MissionMissionGroupModelResetDayOfWeekEnabler))]
    public class Gs2MissionMissionGroupModelResetDayOfWeekEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2MissionMissionGroupModelResetDayOfWeekEnabler;

            if (original == null) return;

            var context = original.GetComponentInParent<Gs2MissionMissionGroupModelContext>();
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2MissionMissionGroupModelContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2MissionMissionGroupModelContext>();
                }
            }
            else {
                if (context.transform.parent.GetComponent<Gs2MissionMissionGroupModelList>() != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2MissionMissionGroupModelContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("MissionGroupModel is auto assign from Gs2MissionMissionGroupModelList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2MissionMissionGroupModelContext), false);
                    EditorGUI.indentLevel++;
                    EditorGUILayout.ObjectField("MissionGroupModel", context.MissionGroupModel, typeof(MissionGroupModel), false);
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.MissionGroupModel?.NamespaceName.ToString());
                    EditorGUILayout.TextField("MissionGroupName", context.MissionGroupModel?.MissionGroupName.ToString());
                    EditorGUI.indentLevel--;
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2MissionMissionGroupModelResetDayOfWeekEnabler.Expression.In || original.expression == Gs2MissionMissionGroupModelResetDayOfWeekEnabler.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableResetDayOfWeeks"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableResetDayOfWeek"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}