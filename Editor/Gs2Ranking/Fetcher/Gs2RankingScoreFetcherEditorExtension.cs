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

using Gs2.Unity.Gs2Ranking.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Ranking.Context;
using Gs2.Unity.UiKit.Gs2Ranking.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Ranking.Editor
{
    [CustomEditor(typeof(Gs2RankingScoreFetcher))]
    public class Gs2RankingScoreFetcherEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2RankingScoreFetcher;

            if (original == null) return;

            var context = original.GetComponent<Gs2RankingScoreContext>() ?? original.GetComponentInParent<Gs2RankingScoreContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2RankingScoreContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2RankingScoreContext>();
                }
            }
            else {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2RankingScoreContext), false);
                EditorGUI.indentLevel++;
                context.Score = EditorGUILayout.ObjectField("Score", context.Score, typeof(Score), false) as Score;
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", context.Score?.NamespaceName.ToString());
                EditorGUILayout.TextField("UserId", context.Score?.UserId.ToString());
                EditorGUILayout.TextField("CategoryName", context.Score?.CategoryName.ToString());
                EditorGUILayout.TextField("ScorerUserId", context.Score.User?.UserId?.ToString() ?? "");
                EditorGUILayout.TextField("UniqueId", context.Score?.UniqueId.ToString());
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