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

using Gs2.Unity.Gs2Enhance.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Enhance.Context;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Enhance.Editor
{
    [CustomEditor(typeof(Gs2EnhanceProgressPropertyIdEnabler))]
    public class Gs2EnhanceProgressPropertyIdEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2EnhanceProgressPropertyIdEnabler;

            if (original == null) return;

            var context = original.GetComponent<Gs2EnhanceOwnProgressContext>() ?? original.GetComponentInParent<Gs2EnhanceOwnProgressContext>();
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2EnhanceOwnProgressContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2EnhanceOwnProgressContext>();
                }
            }
            else {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2EnhanceOwnProgressContext), false);
                EditorGUI.indentLevel++;
                context.Progress = EditorGUILayout.ObjectField("Progress", context.Progress, typeof(OwnProgress), false) as OwnProgress;
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", context.Progress?.NamespaceName.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2EnhanceProgressPropertyIdEnabler.Expression.In || original.expression == Gs2EnhanceProgressPropertyIdEnabler.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enablePropertyIds"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enablePropertyId"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}