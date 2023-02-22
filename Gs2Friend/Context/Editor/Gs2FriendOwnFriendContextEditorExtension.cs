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
using Gs2.Unity.UiKit.Gs2Friend.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Friend.Editor
{
    [CustomEditor(typeof(Gs2FriendOwnFriendContext))]
    public class Gs2FriendOwnFriendContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2FriendOwnFriendContext;

            if (original == null) return;

            if (original.Friend == null) {
                if (original.transform.parent.GetComponent<Gs2FriendOwnFriendList>() != null) {
                    EditorGUILayout.HelpBox("OwnFriend is auto assign from Gs2FriendOwnFriendList.", MessageType.Info);
                }
                else {
                    EditorGUILayout.HelpBox("OwnFriend not assigned.", MessageType.Error);
                    EditorGUILayout.ObjectField("OwnFriend", original.Friend, typeof(OwnFriend), false);
                }
            }
            else {
                EditorGUILayout.ObjectField("OwnFriend", original.Friend, typeof(OwnFriend), false);
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", original.Friend?.NamespaceName.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }
            
            serializedObject.Update();
            serializedObject.ApplyModifiedProperties();
        }
    }
}