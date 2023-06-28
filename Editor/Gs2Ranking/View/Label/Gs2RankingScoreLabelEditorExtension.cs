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

using Gs2.Unity.Gs2Ranking.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Ranking.Context;
using Gs2.Unity.UiKit.Gs2Ranking.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Ranking.Editor
{
    [CustomEditor(typeof(Gs2RankingScoreLabel))]
    public class Gs2RankingScoreLabelEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2RankingScoreLabel;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2RankingOwnScoreFetcher>() ?? original.GetComponentInParent<Gs2RankingOwnScoreFetcher>();
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2RankingOwnScoreFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2RankingOwnScoreFetcher>();
                }
            }
            else {
                if (fetcher.transform.parent.GetComponent<Gs2RankingOwnScoreList>() != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2RankingOwnScoreFetcher), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("Score is auto assign from Gs2RankingOwnScoreList.", MessageType.Info);
                }
                else {
                    var context = original.GetComponent<Gs2RankingOwnScoreContext>() ?? original.GetComponentInParent<Gs2RankingOwnScoreContext>();
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2RankingOwnScoreFetcher), false);
                    EditorGUI.indentLevel++;
                    context.Score = EditorGUILayout.ObjectField("Score", context.Score, typeof(OwnScore), false) as OwnScore;
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.Score?.NamespaceName.ToString());
                    EditorGUILayout.TextField("CategoryName", context.Score?.CategoryName.ToString());
                    EditorGUILayout.TextField("ScorerUserId", context.Score?.ScorerUserId.ToString());
                    EditorGUILayout.TextField("UniqueId", context.Score?.UniqueId.ToString());
                    EditorGUI.indentLevel--;
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            original.format = EditorGUILayout.TextField("Format", original.format);

            GUILayout.Label("Add Format Parameter");
            if (GUILayout.Button("CategoryName")) {
                original.format += "{categoryName}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("UserId")) {
                original.format += "{userId}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("UniqueId")) {
                original.format += "{uniqueId}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("ScorerUserId")) {
                original.format += "{scorerUserId}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("Score")) {
                original.format += "{score}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("Metadata")) {
                original.format += "{metadata}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdate"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}