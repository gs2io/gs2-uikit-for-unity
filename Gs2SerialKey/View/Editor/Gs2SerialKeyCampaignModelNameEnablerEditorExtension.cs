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
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2SerialKey.Editor
{
    [CustomEditor(typeof(Gs2SerialKeyCampaignModelNameEnabler))]
    public class Gs2SerialKeyCampaignModelNameEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2SerialKeyCampaignModelNameEnabler;

            if (original == null) return;

            var context = original.GetComponentInParent<Gs2SerialKeyCampaignModelContext>();
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2SerialKeyCampaignModelContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2SerialKeyCampaignModelContext>();
                }
            }
            else {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2SerialKeyCampaignModelContext), false);
                EditorGUI.indentLevel++;
                EditorGUILayout.ObjectField("CampaignModel", context.CampaignModel, typeof(CampaignModel), false);
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", context.CampaignModel?.NamespaceName.ToString());
                EditorGUILayout.TextField("CampaignModelName", context.CampaignModel?.CampaignModelName.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2SerialKeyCampaignModelNameEnabler.Expression.In || original.expression == Gs2SerialKeyCampaignModelNameEnabler.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableNames"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableName"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}