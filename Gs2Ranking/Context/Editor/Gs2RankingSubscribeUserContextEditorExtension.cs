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
    [CustomEditor(typeof(Gs2RankingSubscribeUserContext))]
    public class Gs2RankingSubscribeUserContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2RankingSubscribeUserContext;

            if (original == null) return;

            if (original.SubscribeUser == null) {
                EditorGUILayout.HelpBox("SubscribeUser not assigned.", MessageType.Error);
                EditorGUILayout.ObjectField("SubscribeUser", original.SubscribeUser, typeof(SubscribeUser), false);
            }
            else {
                EditorGUILayout.ObjectField("SubscribeUser", original.SubscribeUser, typeof(SubscribeUser), false);
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("CategoryName", original.SubscribeUser?.CategoryName.ToString());
                EditorGUILayout.TextField("TargetUserId", original.SubscribeUser?.TargetUserId.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }
            
            serializedObject.Update();
            serializedObject.ApplyModifiedProperties();
        }
    }
}