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
    [CustomEditor(typeof(Gs2MissionOwnCompleteContext))]
    public class Gs2MissionOwnCompleteContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2MissionOwnCompleteContext;

            if (original == null) return;

            if (original.Complete == null) {
                if (original.transform.parent.GetComponent<Gs2MissionOwnCompleteList>() != null) {
                    EditorGUILayout.HelpBox("OwnComplete is auto assign from Gs2MissionOwnCompleteList.", MessageType.Info);
                }
                else {
                    EditorGUILayout.HelpBox("OwnComplete not assigned.", MessageType.Error);
                    EditorGUILayout.ObjectField("OwnComplete", original.Complete, typeof(OwnComplete), false);
                }
            }
            else {
                EditorGUILayout.ObjectField("OwnComplete", original.Complete, typeof(OwnComplete), false);
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", original.Complete?.NamespaceName.ToString());
                EditorGUILayout.TextField("MissionGroupName", original.Complete?.MissionGroupName.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }
            
            serializedObject.Update();
            serializedObject.ApplyModifiedProperties();
        }
    }
}