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
using Gs2.Unity.UiKit.Gs2Enhance.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Enhance.Editor
{
    [CustomEditor(typeof(Gs2EnhanceProgressStartAction))]
    public class Gs2EnhanceProgressStartActionEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2EnhanceProgressStartAction;

            if (original == null) return;

            var context = original.GetComponentInParent<Gs2EnhanceOwnProgressContext>();
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
                EditorGUILayout.ObjectField("OwnProgress", context.Progress, typeof(OwnProgress), false);
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", context.Progress?.NamespaceName.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("RateName"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("TargetItemSetId"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Materials"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Force"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Config"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onChangeRateName"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onChangeTargetItemSetId"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onChangeMaterials"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onChangeForce"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onChangeConfig"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onStartComplete"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onError"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}