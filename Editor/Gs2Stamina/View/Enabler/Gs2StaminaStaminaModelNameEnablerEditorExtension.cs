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
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Stamina.Enabler.Editor
{
    [CustomEditor(typeof(Gs2StaminaStaminaModelNameEnabler))]
    public class Gs2StaminaStaminaModelNameEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2StaminaStaminaModelNameEnabler;

            if (original == null) return;

            var context = original.GetComponent<Gs2StaminaStaminaModelContext>() ?? original.GetComponentInParent<Gs2StaminaStaminaModelContext>();
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2StaminaStaminaModelContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2StaminaStaminaModelContext>();
                }
            }
            else {
                if (context.transform.parent.GetComponent<Gs2StaminaStaminaModelList>() != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2StaminaStaminaModelContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("StaminaModel is auto assign from Gs2StaminaStaminaModelList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2StaminaStaminaModelContext), false);
                    EditorGUI.indentLevel++;
                    context.StaminaModel = EditorGUILayout.ObjectField("StaminaModel", context.StaminaModel, typeof(StaminaModel), false) as StaminaModel;
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.StaminaModel?.NamespaceName.ToString());
                    EditorGUILayout.TextField("StaminaName", context.StaminaModel?.StaminaName.ToString());
                    EditorGUI.indentLevel--;
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2StaminaStaminaModelNameEnabler.Expression.In || original.expression == Gs2StaminaStaminaModelNameEnabler.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableNames"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableName"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}