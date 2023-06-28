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
    [CustomEditor(typeof(Gs2GatewayWebSocketSessionSetUserIdAction))]
    public class Gs2GatewayWebSocketSessionSetUserIdActionEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2GatewayWebSocketSessionSetUserIdAction;

            if (original == null) return;

            var context = original.GetComponent<Gs2GatewayOwnWebSocketSessionContext>() ?? original.GetComponentInParent<Gs2GatewayOwnWebSocketSessionContext>();
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2GatewayOwnWebSocketSessionContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2GatewayOwnWebSocketSessionContext>();
                }
            }
            else {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2GatewayOwnWebSocketSessionContext), false);
                EditorGUI.indentLevel++;
                context.WebSocketSession = EditorGUILayout.ObjectField("OwnWebSocketSession", context.WebSocketSession, typeof(OwnWebSocketSession), false) as OwnWebSocketSession;
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", context.WebSocketSession?.NamespaceName.ToString());
                EditorGUILayout.TextField("ConnectionId", context.WebSocketSession?.ConnectionId.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("AllowConcurrentAccess"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onChangeAllowConcurrentAccess"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onSetUserIdComplete"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onError"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}