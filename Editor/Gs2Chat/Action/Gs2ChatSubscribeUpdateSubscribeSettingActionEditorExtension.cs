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
    [CustomEditor(typeof(Gs2ChatSubscribeUpdateSubscribeSettingAction))]
    public class Gs2ChatSubscribeUpdateSubscribeSettingActionEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2ChatSubscribeUpdateSubscribeSettingAction;

            if (original == null) return;

            var context = original.GetComponent<Gs2ChatOwnSubscribeContext>() ?? original.GetComponentInParent<Gs2ChatOwnSubscribeContext>();
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2ChatOwnSubscribeContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2ChatOwnSubscribeContext>();
                }
            }
            else {
                if (context.transform.parent.GetComponent<Gs2ChatOwnSubscribeList>() != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2ChatOwnSubscribeContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("Subscribe is auto assign from Gs2ChatSubscribeList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2ChatOwnSubscribeContext), false);
                    EditorGUI.indentLevel++;
                    context.Subscribe = EditorGUILayout.ObjectField("OwnSubscribe", context.Subscribe, typeof(OwnSubscribe), false) as OwnSubscribe;
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.Subscribe?.NamespaceName.ToString());
                    EditorGUILayout.TextField("RoomName", context.Subscribe?.RoomName.ToString());
                    EditorGUI.indentLevel--;
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("NotificationTypes"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onChangeNotificationTypes"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdateSubscribeSettingComplete"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onError"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}