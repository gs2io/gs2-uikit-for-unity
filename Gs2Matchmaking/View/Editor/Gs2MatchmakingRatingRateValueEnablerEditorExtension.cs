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
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Matchmaking.Editor
{
    [CustomEditor(typeof(Gs2MatchmakingRatingRateValueEnabler))]
    public class Gs2MatchmakingRatingRateValueEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2MatchmakingRatingRateValueEnabler;

            if (original == null) return;

            var context = original.GetComponentInParent<Gs2MatchmakingOwnRatingContext>();
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2MatchmakingOwnRatingContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2MatchmakingOwnRatingContext>();
                }
            }
            else {
                if (context.transform.parent.GetComponent<Gs2MatchmakingOwnRatingList>() != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2MatchmakingOwnRatingContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("Rating is auto assign from Gs2MatchmakingOwnRatingList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2MatchmakingOwnRatingContext), false);
                    EditorGUI.indentLevel++;
                    EditorGUILayout.ObjectField("Rating", context.Rating, typeof(OwnRating), false);
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.Rating?.NamespaceName.ToString());
                    EditorGUILayout.TextField("RatingName", context.Rating?.RatingName.ToString());
                    EditorGUI.indentLevel--;
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2MatchmakingRatingRateValueEnabler.Expression.In || original.expression == Gs2MatchmakingRatingRateValueEnabler.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableRateValues"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableRateValue"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}