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

using Gs2.Unity.Gs2MegaField.ScriptableObject;
using Gs2.Unity.UiKit.Gs2MegaField.Context;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2MegaField.Editor
{
    [CustomEditor(typeof(Gs2MegaFieldLayerModelMetadataEnabler))]
    public class Gs2MegaFieldLayerModelMetadataEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2MegaFieldLayerModelMetadataEnabler;

            if (original == null) return;

            var context = original.GetComponentInParent<Gs2MegaFieldLayerModelContext>();
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2MegaFieldLayerModelContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2MegaFieldLayerModelContext>();
                }
            }
            else {
                if (context.transform.parent.GetComponent<Gs2MegaFieldLayerModelList>() != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2MegaFieldLayerModelContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("LayerModel is auto assign from Gs2MegaFieldLayerModelList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2MegaFieldLayerModelContext), false);
                    EditorGUI.indentLevel++;
                    EditorGUILayout.ObjectField("LayerModel", context.LayerModel, typeof(LayerModel), false);
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("LayerModelName", context.LayerModel?.LayerModelName.ToString());
                    EditorGUI.indentLevel--;
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2MegaFieldLayerModelMetadataEnabler.Expression.In || original.expression == Gs2MegaFieldLayerModelMetadataEnabler.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableMetadatas"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableMetadata"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}