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
using Gs2.Unity.UiKit.Gs2Schedule.Fetcher;
using UnityEditor;
using UnityEngine;
using Event = Gs2.Unity.Gs2Schedule.ScriptableObject.Event;

namespace Gs2.Unity.UiKit.Gs2Schedule.Editor
{
    [CustomEditor(typeof(Gs2ScheduleOwnTriggerContext))]
    public class Gs2ScheduleOwnTriggerContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2ScheduleOwnTriggerContext;

            if (original == null) return;

            if (original.Trigger == null) {
                if (original.transform.parent.GetComponent<Gs2ScheduleOwnTriggerList>() != null) {
                    EditorGUILayout.HelpBox("OwnTrigger is auto assign from Gs2ScheduleOwnTriggerList.", MessageType.Info);
                }
                else {
                    EditorGUILayout.HelpBox("OwnTrigger not assigned.", MessageType.Error);
                    EditorGUILayout.ObjectField("OwnTrigger", original.Trigger, typeof(OwnTrigger), false);
                }
            }
            else {
                EditorGUILayout.ObjectField("OwnTrigger", original.Trigger, typeof(OwnTrigger), false);
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", original.Trigger?.NamespaceName.ToString());
                EditorGUILayout.TextField("TriggerName", original.Trigger?.TriggerName.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }
            
            serializedObject.Update();
            serializedObject.ApplyModifiedProperties();
        }
    }
}