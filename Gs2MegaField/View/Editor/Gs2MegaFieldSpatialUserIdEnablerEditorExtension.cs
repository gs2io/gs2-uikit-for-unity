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
    [CustomEditor(typeof(Gs2MegaFieldSpatialUserIdEnabler))]
    public class Gs2MegaFieldSpatialUserIdEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2MegaFieldSpatialUserIdEnabler;

            if (original == null) return;

            var context = original.GetComponentInParent<Gs2MegaFieldSpatialContext>();
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2MegaFieldSpatialContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2MegaFieldSpatialContext>();
                }
            }
            else {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2MegaFieldSpatialContext), false);
                EditorGUI.indentLevel++;
                EditorGUILayout.ObjectField("Spatial", context.Spatial, typeof(Spatial), false);
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
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2MegaFieldSpatialUserIdEnabler.Expression.In || original.expression == Gs2MegaFieldSpatialUserIdEnabler.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableUserIds"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableUserId"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}