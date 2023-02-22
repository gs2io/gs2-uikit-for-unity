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
    [CustomEditor(typeof(Gs2QuestQuestModelListFetcher))]
    public class Gs2QuestQuestModelListFetcherEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2QuestQuestModelListFetcher;

            if (original == null) return;

            var context = original.GetComponentInParent<Gs2QuestQuestGroupModelContext>();
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2QuestQuestGroupModelContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2QuestQuestGroupModelContext>();
                }
            }
            else {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2QuestQuestGroupModelContext), false);
                EditorGUI.indentLevel++;
                EditorGUILayout.ObjectField("QuestGroupModel", context.QuestGroupModel, typeof(QuestGroupModel), false);
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", context.QuestGroupModel?.NamespaceName.ToString());
                EditorGUILayout.TextField("QuestGroupName", context.QuestGroupModel?.QuestGroupName.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onError"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}