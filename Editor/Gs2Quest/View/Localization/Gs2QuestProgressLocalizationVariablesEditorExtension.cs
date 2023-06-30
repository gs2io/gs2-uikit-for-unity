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

#if GS2_ENABLE_LOCALIZATION

using Gs2.Unity.Gs2Quest.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Quest.Context;
using Gs2.Unity.UiKit.Gs2Quest.Fetcher;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace Gs2.Unity.UiKit.Gs2Quest.Localization.Editor
{
    [CustomEditor(typeof(Gs2QuestProgressLocalizationVariables))]
    public class Gs2QuestProgressLocalizationVariablesEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2QuestProgressLocalizationVariables;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2QuestQuestGroupModelFetcher>() ?? original.GetComponentInParent<Gs2QuestQuestGroupModelFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2QuestQuestGroupModelFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2QuestQuestGroupModelFetcher>();
                }
            }
            else {
                var context = original.GetComponent<Gs2QuestQuestGroupModelContext>() ?? original.GetComponentInParent<Gs2QuestQuestGroupModelContext>(true);
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2QuestQuestGroupModelFetcher), false);
                EditorGUI.indentLevel++;
                context.QuestGroupModel = EditorGUILayout.ObjectField("QuestGroupModel", context.QuestGroupModel, typeof(QuestGroupModel), false) as QuestGroupModel;
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", context.QuestGroupModel?.NamespaceName.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.Update();
            original.target = EditorGUILayout.ObjectField("Target", original.target, typeof(LocalizeStringEvent), true) as LocalizeStringEvent;

            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif