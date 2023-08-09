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

using Gs2.Unity.Gs2Ranking.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Ranking.Context;
using Gs2.Unity.UiKit.Gs2Ranking.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Ranking.Editor
{
    [CustomEditor(typeof(Gs2RankingOwnSubscribeUserContext))]
    public class Gs2RankingOwnSubscribeUserContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2RankingOwnSubscribeUserContext;

            if (original == null) return;

            serializedObject.Update();

            if (original.SubscribeUser == null) {
                if (original.GetComponentInParent<Gs2RankingOwnSubscribeUserList>(true) != null) {
                    EditorGUILayout.HelpBox("OwnSubscribeUser is auto assign from Gs2RankingOwnSubscribeUserList.", MessageType.Info);
                }
                else {
                    EditorGUILayout.HelpBox("OwnSubscribeUser not assigned.", MessageType.Error);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("SubscribeUser"), true);
                }
            }
            else {
                original.SubscribeUser = EditorGUILayout.ObjectField("OwnSubscribeUser", original.SubscribeUser, typeof(OwnSubscribeUser), false) as OwnSubscribeUser;
                EditorGUI.BeginDisabledGroup(true);
                if (original.SubscribeUser != null) {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("CategoryName", original.SubscribeUser?.CategoryName?.ToString());
                    EditorGUILayout.TextField("TargetUserId", original.SubscribeUser?.TargetUserId?.ToString());
                    EditorGUI.indentLevel--;
                }
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}