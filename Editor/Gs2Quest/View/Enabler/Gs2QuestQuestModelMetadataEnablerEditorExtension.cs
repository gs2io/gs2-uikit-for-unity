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
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantAssignment
// ReSharper disable NotAccessedVariable
// ReSharper disable RedundantUsingDirective
// ReSharper disable Unity.NoNullPropagation
// ReSharper disable InconsistentNaming

#pragma warning disable CS0472

using Gs2.Unity.Gs2Quest.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Quest.Context;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Quest.Enabler.Editor
{
    [CustomEditor(typeof(Gs2QuestQuestModelMetadataEnabler))]
    public class Gs2QuestQuestModelMetadataEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2QuestQuestModelMetadataEnabler;

            if (original == null) return;

            var context = original.GetComponent<Gs2QuestQuestModelContext>() ?? original.GetComponentInParent<Gs2QuestQuestModelContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2QuestQuestModelContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2QuestQuestModelContext>();
                }
            }
            else {
                if (context.transform.parent.GetComponent<Gs2QuestQuestModelList>() != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2QuestQuestModelContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("QuestModel is auto assign from Gs2QuestQuestModelList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2QuestQuestModelContext), false);
                    EditorGUI.indentLevel++;
                    context.QuestModel = EditorGUILayout.ObjectField("QuestModel", context.QuestModel, typeof(QuestModel), false) as QuestModel;
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("QuestName", context.QuestModel?.QuestName.ToString());
                    EditorGUI.indentLevel--;
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2QuestQuestModelMetadataEnabler.Expression.In || original.expression == Gs2QuestQuestModelMetadataEnabler.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableMetadatas"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableMetadata"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}