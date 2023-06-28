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
    [CustomEditor(typeof(Gs2RankingSubscribeUserLabel))]
    public class Gs2RankingSubscribeUserLabelEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2RankingSubscribeUserLabel;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2RankingSubscribeUserFetcher>() ?? original.GetComponentInParent<Gs2RankingSubscribeUserFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2RankingSubscribeUserFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2RankingSubscribeUserFetcher>();
                }
            }
            else {
                var context = original.GetComponent<Gs2RankingSubscribeUserContext>() ?? original.GetComponentInParent<Gs2RankingSubscribeUserContext>(true);
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2RankingSubscribeUserFetcher), false);
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
            original.format = EditorGUILayout.TextField("Format", original.format);

            GUILayout.Label("Add Format Parameter");
            if (GUILayout.Button("UserId")) {
                original.format += "{userId}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("TargetUserId")) {
                original.format += "{targetUserId}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdate"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}