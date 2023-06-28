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
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Ranking.Enabler.Editor
{
    [CustomEditor(typeof(Gs2RankingSubscribeUserTargetUserIdEnabler))]
    public class Gs2RankingSubscribeUserTargetUserIdEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2RankingSubscribeUserTargetUserIdEnabler;

            if (original == null) return;

            var context = original.GetComponent<Gs2RankingSubscribeUserContext>() ?? original.GetComponentInParent<Gs2RankingSubscribeUserContext>();
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2RankingSubscribeUserContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2RankingSubscribeUserContext>();
                }
            }
            else {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2RankingSubscribeUserContext), false);
                EditorGUI.indentLevel++;
                context.SubscribeUser = EditorGUILayout.ObjectField("SubscribeUser", context.SubscribeUser, typeof(SubscribeUser), false) as SubscribeUser;
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("CategoryName", context.SubscribeUser?.CategoryName.ToString());
                EditorGUILayout.TextField("TargetUserId", context.SubscribeUser?.TargetUserId.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2RankingSubscribeUserTargetUserIdEnabler.Expression.In || original.expression == Gs2RankingSubscribeUserTargetUserIdEnabler.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableTargetUserIds"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableTargetUserId"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}