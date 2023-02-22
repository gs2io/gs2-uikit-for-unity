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

using Gs2.Unity.Gs2Realtime.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Realtime.Context;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Realtime.Editor
{
    [CustomEditor(typeof(Gs2RealtimeRoomLabel))]
    public class Gs2RealtimeRoomLabelEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2RealtimeRoomLabel;

            if (original == null) return;

            var context = original.GetComponentInParent<Gs2RealtimeRoomContext>();
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2RealtimeRoomContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2RealtimeRoomContext>();
                }
            }
            else {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2RealtimeRoomContext), false);
                EditorGUI.indentLevel++;
                EditorGUILayout.ObjectField("Room", context.Room, typeof(Room), false);
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
            if (GUILayout.Button("IpAddress")) {
                original.format += "{ipAddress}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("Port")) {
                original.format += "{port}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("EncryptionKey")) {
                original.format += "{encryptionKey}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdate"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}