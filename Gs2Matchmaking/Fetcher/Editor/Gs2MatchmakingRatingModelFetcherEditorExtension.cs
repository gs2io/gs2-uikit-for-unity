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

using Gs2.Unity.Gs2Matchmaking.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Matchmaking.Context;
using Gs2.Unity.UiKit.Gs2Matchmaking.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Matchmaking.Editor
{
    [CustomEditor(typeof(Gs2MatchmakingRatingModelFetcher))]
    public class Gs2MatchmakingRatingModelFetcherEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2MatchmakingRatingModelFetcher;

            if (original == null) return;

            var context = original.GetComponentInParent<Gs2MatchmakingRatingModelContext>();
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2MatchmakingRatingModelContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2MatchmakingRatingModelContext>();
                }
            }
            else {
                if (context.transform.parent.GetComponent<Gs2MatchmakingRatingModelList>() != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2MatchmakingRatingModelContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("RatingModel is auto assign from Gs2MatchmakingRatingModelList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2MatchmakingRatingModelContext), false);
                    EditorGUI.indentLevel++;
                    EditorGUILayout.ObjectField("RatingModel", context.RatingModel, typeof(RatingModel), false);
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.RatingModel?.NamespaceName.ToString());
                    EditorGUILayout.TextField("RatingName", context.RatingModel?.RatingName.ToString());
                    EditorGUI.indentLevel--;
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }
            
            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onError"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}