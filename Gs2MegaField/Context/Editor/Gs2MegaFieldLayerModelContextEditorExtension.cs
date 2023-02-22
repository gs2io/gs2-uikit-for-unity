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
using Gs2.Unity.UiKit.Gs2MegaField.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2MegaField.Editor
{
    [CustomEditor(typeof(Gs2MegaFieldLayerModelContext))]
    public class Gs2MegaFieldLayerModelContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2MegaFieldLayerModelContext;

            if (original == null) return;

            if (original.LayerModel == null) {
                if (original.transform.parent.GetComponent<Gs2MegaFieldLayerModelList>() != null) {
                    EditorGUILayout.HelpBox("LayerModel is auto assign from Gs2MegaFieldLayerModelList.", MessageType.Info);
                }
                else {
                    EditorGUILayout.HelpBox("LayerModel not assigned.", MessageType.Error);
                    EditorGUILayout.ObjectField("LayerModel", original.LayerModel, typeof(LayerModel), false);
                }
            }
            else {
                EditorGUILayout.ObjectField("LayerModel", original.LayerModel, typeof(LayerModel), false);
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("LayerModelName", original.LayerModel?.LayerModelName.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }
            
            serializedObject.Update();
            serializedObject.ApplyModifiedProperties();
        }
    }
}