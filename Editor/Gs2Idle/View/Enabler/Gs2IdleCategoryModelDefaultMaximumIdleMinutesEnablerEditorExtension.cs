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
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantAssignment
// ReSharper disable NotAccessedVariable
// ReSharper disable RedundantUsingDirective
// ReSharper disable Unity.NoNullPropagation
// ReSharper disable InconsistentNaming

#pragma warning disable CS0472

using Gs2.Unity.Gs2Idle.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Idle.Context;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Idle.Editor
{
    [CustomEditor(typeof(Gs2IdleCategoryModelDefaultMaximumIdleMinutesEnabler))]
    public class Gs2IdleCategoryModelDefaultMaximumIdleMinutesEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2IdleCategoryModelDefaultMaximumIdleMinutesEnabler;

            if (original == null) return;

            var context = original.GetComponent<Gs2IdleCategoryModelContext>() ?? original.GetComponentInParent<Gs2IdleCategoryModelContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2IdleCategoryModelContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2IdleCategoryModelContext>();
                }
            }
            else {
                if (context.transform.parent.GetComponent<Gs2IdleCategoryModelList>() != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2IdleCategoryModelContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("CategoryModel is auto assign from Gs2IdleCategoryModelList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2IdleCategoryModelContext), false);
                    EditorGUI.indentLevel++;
                    context.CategoryModel = EditorGUILayout.ObjectField("CategoryModel", context.CategoryModel, typeof(CategoryModel), false) as CategoryModel;
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.CategoryModel?.NamespaceName.ToString());
                    EditorGUILayout.TextField("CategoryName", context.CategoryModel?.CategoryName.ToString());
                    EditorGUI.indentLevel--;
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2IdleCategoryModelDefaultMaximumIdleMinutesEnabler.Expression.In || original.expression == Gs2IdleCategoryModelDefaultMaximumIdleMinutesEnabler.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableDefaultMaximumIdleMinuteses"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableDefaultMaximumIdleMinutes"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}