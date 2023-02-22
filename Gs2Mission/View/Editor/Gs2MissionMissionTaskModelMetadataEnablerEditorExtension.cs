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
    [CustomEditor(typeof(Gs2MissionMissionTaskModelMetadataEnabler))]
    public class Gs2MissionMissionTaskModelMetadataEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2MissionMissionTaskModelMetadataEnabler;

            if (original == null) return;

            var context = original.GetComponentInParent<Gs2MissionMissionTaskModelContext>();
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
                    EditorGUILayout.ObjectField("MissionTaskModel", context.MissionTaskModel, typeof(MissionTaskModel), false);
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("MissionTaskName", context.MissionTaskModel?.MissionTaskName.ToString());
                    EditorGUI.indentLevel--;
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2MissionMissionTaskModelMetadataEnabler.Expression.In || original.expression == Gs2MissionMissionTaskModelMetadataEnabler.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableMetadatas"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableMetadata"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}