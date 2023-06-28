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

namespace Gs2.Unity.UiKit.Gs2MegaField.Enabler.Editor
{
    [CustomEditor(typeof(Gs2MegaFieldAreaModelNameEnabler))]
    public class Gs2MegaFieldAreaModelNameEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2MegaFieldAreaModelNameEnabler;

            if (original == null) return;

            var context = original.GetComponent<Gs2MegaFieldAreaModelContext>() ?? original.GetComponentInParent<Gs2MegaFieldAreaModelContext>();
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2MegaFieldAreaModelContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2MegaFieldAreaModelContext>();
                }
            }
            else {
                if (context.transform.parent.GetComponent<Gs2MegaFieldAreaModelList>() != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2MegaFieldAreaModelContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("AreaModel is auto assign from Gs2MegaFieldAreaModelList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2MegaFieldAreaModelContext), false);
                    EditorGUI.indentLevel++;
                    context.AreaModel = EditorGUILayout.ObjectField("AreaModel", context.AreaModel, typeof(AreaModel), false) as AreaModel;
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.AreaModel?.NamespaceName.ToString());
                    EditorGUILayout.TextField("AreaModelName", context.AreaModel?.AreaModelName.ToString());
                    EditorGUI.indentLevel--;
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2MegaFieldAreaModelNameEnabler.Expression.In || original.expression == Gs2MegaFieldAreaModelNameEnabler.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableNames"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableName"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}