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
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Chat.Enabler.Editor
{
    [CustomEditor(typeof(Gs2ChatSubscribeRoomNameEnabler))]
    public class Gs2ChatSubscribeRoomNameEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2ChatSubscribeRoomNameEnabler;

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
                    EditorGUILayout.HelpBox("Subscribe is auto assign from Gs2ChatOwnSubscribeList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2ChatOwnSubscribeContext), false);
                    EditorGUI.indentLevel++;
                    context.Subscribe = EditorGUILayout.ObjectField("Subscribe", context.Subscribe, typeof(OwnSubscribe), false) as OwnSubscribe;
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.Subscribe?.NamespaceName.ToString());
                    EditorGUILayout.TextField("RoomName", context.Subscribe?.RoomName.ToString());
                    EditorGUI.indentLevel--;
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2ChatSubscribeRoomNameEnabler.Expression.In || original.expression == Gs2ChatSubscribeRoomNameEnabler.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableRoomNames"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableRoomName"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}