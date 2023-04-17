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

using Gs2.Unity.Gs2SerialKey.ScriptableObject;
using Gs2.Unity.UiKit.Gs2SerialKey.Context;
using Gs2.Unity.UiKit.Gs2SerialKey.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2SerialKey.Editor
{
    [CustomEditor(typeof(Gs2SerialKeySerialKeyUseSerialCodeAction))]
    public class Gs2SerialKeySerialKeyUseSerialCodeActionEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2SerialKeySerialKeyUseSerialCodeAction;

            if (original == null) return;

            var context = original.GetComponentInParent<Gs2SerialKeySerialKeyContext>();
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2SerialKeySerialKeyContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2SerialKeySerialKeyContext>();
                }
            }
            else {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2SerialKeySerialKeyContext), false);
                EditorGUI.indentLevel++;
                EditorGUILayout.ObjectField("SerialKey", context.SerialKey, typeof(SerialKey), false);
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", context.SerialKey?.NamespaceName.ToString());
                EditorGUILayout.TextField("SerialKeyCode", context.SerialKey?.SerialKeyCode.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Code"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onChangeCode"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUseSerialCodeComplete"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onError"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}