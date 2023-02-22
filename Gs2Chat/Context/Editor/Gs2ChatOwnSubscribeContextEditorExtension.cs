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
    [CustomEditor(typeof(Gs2ChatOwnSubscribeContext))]
    public class Gs2ChatOwnSubscribeContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2ChatOwnSubscribeContext;

            if (original == null) return;

            if (original.Subscribe == null) {
                if (original.transform.parent.GetComponent<Gs2ChatOwnSubscribeList>() != null) {
                    EditorGUILayout.HelpBox("OwnSubscribe is auto assign from Gs2ChatOwnSubscribeList.", MessageType.Info);
                }
                else {
                    EditorGUILayout.HelpBox("OwnSubscribe not assigned.", MessageType.Error);
                    EditorGUILayout.ObjectField("OwnSubscribe", original.Subscribe, typeof(OwnSubscribe), false);
                }
            }
            else {
                EditorGUILayout.ObjectField("OwnSubscribe", original.Subscribe, typeof(OwnSubscribe), false);
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", original.Subscribe?.NamespaceName.ToString());
                EditorGUILayout.TextField("RoomName", original.Subscribe?.RoomName.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }
            
            serializedObject.Update();
            serializedObject.ApplyModifiedProperties();
        }
    }
}