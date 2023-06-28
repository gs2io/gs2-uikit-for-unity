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

using Gs2.Unity.Gs2Ranking.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Ranking.Context;
using Gs2.Unity.UiKit.Gs2Ranking.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Ranking.Editor
{
    [CustomEditor(typeof(Gs2RankingSubscribeUserUnsubscribeAction))]
    public class Gs2RankingSubscribeUserUnsubscribeActionEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2RankingSubscribeUserUnsubscribeAction;

            if (original == null) return;

            var context = original.GetComponentInParent<Gs2RankingSubscribeUserContext>();
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2RankingSubscribeUserContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2RankingSubscribeUserContext>();
                }
            }
            else {
                if (context.transform.parent.GetComponent<Gs2RankingOwnSubscribeContext>() != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2RankingSubscribeUserContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("SubscribeUser is auto assign from Gs2RankingSubscribeUserList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2RankingSubscribeUserContext), false);
                    EditorGUI.indentLevel++;
                    EditorGUILayout.ObjectField("SubscribeUser", context.SubscribeUser, typeof(OwnSubscribe), false);
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("CategoryName", context.SubscribeUser?.CategoryName.ToString());
                    EditorGUILayout.TextField("TargetUserId", context.SubscribeUser?.TargetUserId.ToString());
                    EditorGUI.indentLevel--;
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUnsubscribeComplete"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onError"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}