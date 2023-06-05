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

using Gs2.Unity.Gs2Friend.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Friend.Context;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Friend.Editor
{
    [CustomEditor(typeof(Gs2FriendPublicProfileUserIdEnabler))]
    public class Gs2FriendPublicProfileUserIdEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2FriendPublicProfileUserIdEnabler;

            if (original == null) return;

            var context = original.GetComponentInParent<Gs2FriendPublicProfileContext>();
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2FriendPublicProfileContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2FriendPublicProfileContext>();
                }
            }
            else {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2FriendPublicProfileContext), false);
                EditorGUI.indentLevel++;
                context.PublicProfile = EditorGUILayout.ObjectField("PublicProfile", context.PublicProfile, typeof(PublicProfile), false) as PublicProfile;
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", context.PublicProfile?.NamespaceName.ToString());
                EditorGUILayout.TextField("UserId", context.PublicProfile?.UserId.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2FriendPublicProfileUserIdEnabler.Expression.In || original.expression == Gs2FriendPublicProfileUserIdEnabler.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableUserIds"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableUserId"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}