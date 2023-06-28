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
using Gs2.Unity.UiKit.Gs2Mission.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Mission.Editor
{
    [CustomEditor(typeof(Gs2MissionMissionTaskModelContext))]
    public class Gs2MissionMissionTaskModelContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2MissionMissionTaskModelContext;

            if (original == null) return;

            serializedObject.Update();

            if (original.MissionTaskModel == null) {
                if (original.transform.parent.GetComponent<Gs2MissionMissionTaskModelList>() != null) {
                    EditorGUILayout.HelpBox("MissionTaskModel is auto assign from Gs2MissionMissionTaskModelList.", MessageType.Info);
                }
                else {
                    EditorGUILayout.HelpBox("MissionTaskModel not assigned.", MessageType.Error);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("MissionTaskModel"), true);
                }
            }
            else {
                original.MissionTaskModel = EditorGUILayout.ObjectField("MissionTaskModel", original.MissionTaskModel, typeof(MissionTaskModel), false) as MissionTaskModel;
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("MissionTaskName", original.MissionTaskModel?.MissionTaskName.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}