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
    [CustomEditor(typeof(Gs2FriendPublicProfileLabel))]
    public class Gs2FriendPublicProfileLabelEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2FriendPublicProfileLabel;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2FriendPublicProfileFetcher>() ?? original.GetComponentInParent<Gs2FriendPublicProfileFetcher>();
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2FriendPublicProfileFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2FriendPublicProfileFetcher>();
                }
            }
            else {
                var context = original.GetComponent<Gs2FriendPublicProfileContext>() ?? original.GetComponentInParent<Gs2FriendPublicProfileContext>();
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2FriendPublicProfileFetcher), false);
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
            original.format = EditorGUILayout.TextField("Format", original.format);

            GUILayout.Label("Add Format Parameter");
            if (GUILayout.Button("UserId")) {
                original.format += "{userId}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("PublicProfile")) {
                original.format += "{publicProfile}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdate"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}