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
 *
 * deny overwrite
 */
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable CheckNamespace

using Gs2.Unity.Gs2Account.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Account.Context;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Account.Editor
{
    [CustomEditor(typeof(Gs2AccountTakeOverUserIdEnabler))]
    public class Gs2AccountTakeOverUserIdEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2AccountTakeOverUserIdEnabler;

            if (original == null) return;

            var context = original.GetComponentInParent<Gs2AccountOwnTakeOverContext>();
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2AccountOwnTakeOverContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2AccountOwnTakeOverContext>();
                }
            }
            else {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2AccountOwnTakeOverContext), false);
                EditorGUI.indentLevel++;
                EditorGUILayout.ObjectField("TakeOver", context.TakeOver, typeof(OwnTakeOver), false);
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", context.TakeOver?.NamespaceName.ToString());
                EditorGUILayout.TextField("Type", context.TakeOver?.Type.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2AccountTakeOverUserIdEnabler.Expression.In || original.expression == Gs2AccountTakeOverUserIdEnabler.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableUserIds"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableUserId"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}