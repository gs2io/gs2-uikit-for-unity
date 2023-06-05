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
    [CustomEditor(typeof(Gs2MatchmakingGatheringCreateGatheringAction))]
    public class Gs2MatchmakingGatheringCreateGatheringActionEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2MatchmakingGatheringCreateGatheringAction;

            if (original == null) return;

            var context = original.GetComponentInParent<Gs2MatchmakingNamespaceContext>();
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2MatchmakingNamespaceContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2MatchmakingNamespaceContext>();
                }
            }
            else {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2MatchmakingNamespaceContext), false);
                EditorGUI.indentLevel++;
                context.Namespace = EditorGUILayout.ObjectField("Namespace", context.Namespace, typeof(Namespace), false) as Namespace;
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", context.Namespace?.NamespaceName.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("NamespaceName"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Player"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("AttributeRanges"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("CapacityOfRoles"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("AllowUserIds"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("ExpiresAt"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("ExpiresAtTimeSpan"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onChangeNamespaceName"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onChangePlayer"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onChangeAttributeRanges"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onChangeCapacityOfRoles"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onChangeAllowUserIds"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onChangeExpiresAt"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onChangeExpiresAtTimeSpan"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onCreateGatheringComplete"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onError"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}