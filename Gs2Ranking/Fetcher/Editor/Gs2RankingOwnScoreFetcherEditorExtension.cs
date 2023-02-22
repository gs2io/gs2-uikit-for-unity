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
    [CustomEditor(typeof(Gs2RankingOwnScoreFetcher))]
    public class Gs2RankingOwnScoreFetcherEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2RankingOwnScoreFetcher;

            if (original == null) return;

            var context = original.GetComponentInParent<Gs2RankingOwnScoreContext>();
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2RankingOwnScoreContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2RankingOwnScoreContext>();
                }
            }
            else {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2RankingOwnScoreContext), false);
                EditorGUI.indentLevel++;
                EditorGUILayout.ObjectField("OwnScore", context.Score, typeof(OwnScore), false);
                EditorGUI.indentLevel++;
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