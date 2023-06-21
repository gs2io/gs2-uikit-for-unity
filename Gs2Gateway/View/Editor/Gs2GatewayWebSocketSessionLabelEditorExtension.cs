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

using Gs2.Unity.Gs2Gateway.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Gateway.Context;
using Gs2.Unity.UiKit.Gs2Gateway.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Gateway.Editor
{
    [CustomEditor(typeof(Gs2GatewayWebSocketSessionLabel))]
    public class Gs2GatewayWebSocketSessionLabelEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2GatewayWebSocketSessionLabel;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2GatewayOwnWebSocketSessionFetcher>() ?? original.GetComponentInParent<Gs2GatewayOwnWebSocketSessionFetcher>();
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2GatewayOwnWebSocketSessionFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2GatewayOwnWebSocketSessionFetcher>();
                }
            }
            else {
                var context = original.GetComponent<Gs2GatewayOwnWebSocketSessionContext>() ?? original.GetComponentInParent<Gs2GatewayOwnWebSocketSessionContext>();
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2GatewayOwnWebSocketSessionFetcher), false);
                EditorGUI.indentLevel++;
                context.WebSocketSession = EditorGUILayout.ObjectField("WebSocketSession", context.WebSocketSession, typeof(OwnWebSocketSession), false) as OwnWebSocketSession;
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", context.WebSocketSession?.NamespaceName.ToString());
                EditorGUILayout.TextField("ConnectionId", context.WebSocketSession?.ConnectionId.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.Update();
            original.format = EditorGUILayout.TextField("Format", original.format);

            GUILayout.Label("Add Format Parameter");
            if (GUILayout.Button("ConnectionId")) {
                original.format += "{connectionId}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("NamespaceName")) {
                original.format += "{namespaceName}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("UserId")) {
                original.format += "{userId}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdate"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}