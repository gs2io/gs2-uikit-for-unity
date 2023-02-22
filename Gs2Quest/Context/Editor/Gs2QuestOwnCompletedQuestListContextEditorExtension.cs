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

using Gs2.Unity.Gs2Quest.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Quest.Context;
using Gs2.Unity.UiKit.Gs2Quest.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Quest.Editor
{
    [CustomEditor(typeof(Gs2QuestOwnCompletedQuestListContext))]
    public class Gs2QuestOwnCompletedQuestListContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2QuestOwnCompletedQuestListContext;

            if (original == null) return;

            if (original.CompletedQuestList == null) {
                if (original.transform.parent.GetComponent<Gs2QuestOwnCompletedQuestListList>() != null) {
                    EditorGUILayout.HelpBox("OwnCompletedQuestList is auto assign from Gs2QuestOwnCompletedQuestListList.", MessageType.Info);
                }
                else {
                    EditorGUILayout.HelpBox("OwnCompletedQuestList not assigned.", MessageType.Error);
                    EditorGUILayout.ObjectField("OwnCompletedQuestList", original.CompletedQuestList, typeof(OwnCompletedQuestList), false);
                }
            }
            else {
                EditorGUILayout.ObjectField("OwnCompletedQuestList", original.CompletedQuestList, typeof(OwnCompletedQuestList), false);
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", original.CompletedQuestList?.NamespaceName.ToString());
                EditorGUILayout.TextField("QuestGroupName", original.CompletedQuestList?.QuestGroupName.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }
            
            serializedObject.Update();
            serializedObject.ApplyModifiedProperties();
        }
    }
}