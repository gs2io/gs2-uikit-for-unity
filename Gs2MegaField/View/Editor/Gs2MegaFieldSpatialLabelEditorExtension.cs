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
    [CustomEditor(typeof(Gs2MegaFieldSpatialLabel))]
    public class Gs2MegaFieldSpatialLabelEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2MegaFieldSpatialLabel;

            if (original == null) return;

            var fetcher = original.GetComponentInParent<Gs2MegaFieldSpatialFetcher>();
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2MegaFieldSpatialFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2MegaFieldSpatialFetcher>();
                }
            }
            else {
                var context = original.GetComponentInParent<Gs2MegaFieldSpatialContext>();
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2MegaFieldSpatialFetcher), false);
                EditorGUI.indentLevel++;
                context.Spatial = EditorGUILayout.ObjectField("Spatial", context.Spatial, typeof(Spatial), false) as Spatial;
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", context.Spatial?.NamespaceName.ToString());
                EditorGUILayout.TextField("UserId", context.Spatial?.UserId.ToString());
                EditorGUILayout.TextField("AreaModelName", context.Spatial?.AreaModelName.ToString());
                EditorGUILayout.TextField("LayerModelName", context.Spatial?.LayerModelName.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.Update();
            original.format = EditorGUILayout.TextField("Format", original.format);

            GUILayout.Label("Add Format Parameter");
            if (GUILayout.Button("UserId")) {
                original.format += "{userId}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("AreaModelName")) {
                original.format += "{areaModelName}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("LayerModelName")) {
                original.format += "{layerModelName}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("Position")) {
                original.format += "{position}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("Vector")) {
                original.format += "{vector}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdate"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}