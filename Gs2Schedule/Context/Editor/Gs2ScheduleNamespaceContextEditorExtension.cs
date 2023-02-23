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
    [CustomEditor(typeof(Gs2ScheduleNamespaceContext))]
    public class Gs2ScheduleNamespaceContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2ScheduleNamespaceContext;

            if (original == null) return;

            serializedObject.Update();

            if (original.Namespace == null) {
                EditorGUILayout.HelpBox("Namespace not assigned.", MessageType.Error);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("Namespace"), true);
            }
            else {
                EditorGUILayout.ObjectField("Namespace", original.Namespace, typeof(Namespace), false);
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", original.Namespace?.NamespaceName.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}