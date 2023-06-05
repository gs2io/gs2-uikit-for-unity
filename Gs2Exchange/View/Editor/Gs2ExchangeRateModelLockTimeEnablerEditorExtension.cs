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

using Gs2.Unity.Gs2Exchange.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Exchange.Context;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Exchange.Editor
{
    [CustomEditor(typeof(Gs2ExchangeRateModelLockTimeEnabler))]
    public class Gs2ExchangeRateModelLockTimeEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2ExchangeRateModelLockTimeEnabler;

            if (original == null) return;

            var context = original.GetComponentInParent<Gs2ExchangeRateModelContext>();
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2ExchangeRateModelContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2ExchangeRateModelContext>();
                }
            }
            else {
                if (context.transform.parent.GetComponent<Gs2ExchangeRateModelList>() != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2ExchangeRateModelContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("RateModel is auto assign from Gs2ExchangeRateModelList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2ExchangeRateModelContext), false);
                    EditorGUI.indentLevel++;
                    context.RateModel = EditorGUILayout.ObjectField("RateModel", context.RateModel, typeof(RateModel), false) as RateModel;
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.RateModel?.NamespaceName.ToString());
                    EditorGUILayout.TextField("RateName", context.RateModel?.RateName.ToString());
                    EditorGUI.indentLevel--;
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2ExchangeRateModelLockTimeEnabler.Expression.In || original.expression == Gs2ExchangeRateModelLockTimeEnabler.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableLockTimes"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableLockTime"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}