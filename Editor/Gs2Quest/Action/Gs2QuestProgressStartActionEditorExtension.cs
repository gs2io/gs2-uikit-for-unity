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
 *
 * deny overwrite
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

using Gs2.Unity.Gs2Quest.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Quest.Context;
using Gs2.Unity.UiKit.Gs2Quest.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Quest.Editor
{
    [CustomEditor(typeof(Gs2QuestProgressStartAction))]
    public class Gs2QuestProgressStartActionEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2QuestProgressStartAction;

            if (original == null) return;

            var context = original.GetComponent<Gs2QuestQuestModelContext>() ?? original.GetComponentInParent<Gs2QuestQuestModelContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2QuestQuestModelContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2QuestQuestModelContext>();
                }
            }
            else {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2QuestQuestModelContext), false);
                EditorGUI.indentLevel++;
                context.QuestModel = EditorGUILayout.ObjectField("QuestModel", context.QuestModel, typeof(QuestModel), false) as QuestModel;
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", context.QuestModel?.NamespaceName.ToString());
                EditorGUILayout.TextField("QuestGroupName", context.QuestModel?.QuestGroupName.ToString());
                EditorGUILayout.TextField("QuestName", context.QuestModel?.QuestName.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Force"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Config"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onChangeQuestGroupName"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onChangeQuestName"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onChangeForce"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onChangeConfig"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onStartComplete"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onError"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}