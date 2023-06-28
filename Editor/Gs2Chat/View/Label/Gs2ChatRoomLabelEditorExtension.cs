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

using Gs2.Unity.Gs2Chat.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Chat.Context;
using Gs2.Unity.UiKit.Gs2Chat.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Chat.Editor
{
    [CustomEditor(typeof(Gs2ChatRoomLabel))]
    public class Gs2ChatRoomLabelEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2ChatRoomLabel;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2ChatRoomFetcher>() ?? original.GetComponentInParent<Gs2ChatRoomFetcher>();
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2ChatRoomFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2ChatRoomFetcher>();
                }
            }
            else {
                var context = original.GetComponent<Gs2ChatRoomContext>() ?? original.GetComponentInParent<Gs2ChatRoomContext>();
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2ChatRoomFetcher), false);
                EditorGUI.indentLevel++;
                context.Room = EditorGUILayout.ObjectField("Room", context.Room, typeof(Room), false) as Room;
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", context.Room?.NamespaceName.ToString());
                EditorGUILayout.TextField("RoomName", context.Room?.RoomName.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.Update();
            original.format = EditorGUILayout.TextField("Format", original.format);

            GUILayout.Label("Add Format Parameter");
            if (GUILayout.Button("Name")) {
                original.format += "{name}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("Metadata")) {
                original.format += "{metadata}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdate"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}