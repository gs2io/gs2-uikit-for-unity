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

using Gs2.Unity.Gs2Friend.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Friend.Context;
using Gs2.Unity.UiKit.Gs2Friend.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Friend.Editor
{
    [CustomEditor(typeof(Gs2FriendReceiveFriendRequestRejectAction))]
    public class Gs2FriendReceiveFriendRequestRejectActionEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2FriendReceiveFriendRequestRejectAction;

            if (original == null) return;

            var context = original.GetComponentInParent<Gs2FriendOwnReceiveFriendRequestContext>();
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2FriendOwnReceiveFriendRequestContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2FriendOwnReceiveFriendRequestContext>();
                }
            }
            else {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2FriendOwnReceiveFriendRequestContext), false);
                EditorGUI.indentLevel++;
                EditorGUILayout.ObjectField("OwnReceiveFriendRequest", context.ReceiveFriendRequest, typeof(OwnReceiveFriendRequest), false);
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", context.ReceiveFriendRequest.NamespaceName.ToString());
                EditorGUILayout.TextField("FromUserId", context.ReceiveFriendRequest.FromUserId.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onRejectComplete"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onError"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}