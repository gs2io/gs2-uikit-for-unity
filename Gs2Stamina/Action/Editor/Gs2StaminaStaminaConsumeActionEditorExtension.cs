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

using Gs2.Unity.Gs2Stamina.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Stamina.Context;
using Gs2.Unity.UiKit.Gs2Stamina.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Stamina.Editor
{
    [CustomEditor(typeof(Gs2StaminaStaminaConsumeAction))]
    public class Gs2StaminaStaminaConsumeActionEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2StaminaStaminaConsumeAction;

            if (original == null) return;

            var context = original.GetComponentInParent<Gs2StaminaOwnStaminaContext>();
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2StaminaOwnStaminaContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2StaminaOwnStaminaContext>();
                }
            }
            else {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2StaminaOwnStaminaContext), false);
                EditorGUI.indentLevel++;
                EditorGUILayout.ObjectField("OwnStamina", context.Stamina, typeof(OwnStamina), false);
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", context.Stamina?.NamespaceName.ToString());
                EditorGUILayout.TextField("StaminaName", context.Stamina?.StaminaName.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("ConsumeValue"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onChangeConsumeValue"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onConsumeComplete"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onError"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}