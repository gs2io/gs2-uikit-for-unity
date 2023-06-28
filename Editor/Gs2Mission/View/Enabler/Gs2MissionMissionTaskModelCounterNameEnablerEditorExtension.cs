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

namespace Gs2.Unity.UiKit.Gs2Mission.Enabler.Editor
{
    [CustomEditor(typeof(Gs2MissionMissionTaskModelCounterNameEnabler))]
    public class Gs2MissionMissionTaskModelCounterNameEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2MissionMissionTaskModelCounterNameEnabler;

            if (original == null) return;

            var context = original.GetComponent<Gs2MissionMissionTaskModelContext>() ?? original.GetComponentInParent<Gs2MissionMissionTaskModelContext>();
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2MissionMissionTaskModelContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2MissionMissionTaskModelContext>();
                }
            }
            else {
                if (context.transform.parent.GetComponent<Gs2MissionMissionTaskModelList>() != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2MissionMissionTaskModelContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("MissionTaskModel is auto assign from Gs2MissionMissionTaskModelList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2MissionMissionTaskModelContext), false);
                    EditorGUI.indentLevel++;
                    context.MissionTaskModel = EditorGUILayout.ObjectField("MissionTaskModel", context.MissionTaskModel, typeof(MissionTaskModel), false) as MissionTaskModel;
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("MissionTaskName", context.MissionTaskModel?.MissionTaskName.ToString());
                    EditorGUI.indentLevel--;
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2MissionMissionTaskModelCounterNameEnabler.Expression.In || original.expression == Gs2MissionMissionTaskModelCounterNameEnabler.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableCounterNames"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableCounterName"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}