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

using Gs2.Unity.Gs2Idle.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Idle.Context;
using Gs2.Unity.UiKit.Gs2Idle.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Idle.Editor
{
    [CustomEditor(typeof(Gs2IdleCategoryModelContext))]
    public class Gs2IdleCategoryModelContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2IdleCategoryModelContext;

            if (original == null) return;

            serializedObject.Update();

            if (original.CategoryModel == null) {
                if (original.transform.parent.GetComponent<Gs2IdleCategoryModelList>() != null) {
                    EditorGUILayout.HelpBox("CategoryModel is auto assign from Gs2IdleCategoryModelList.", MessageType.Info);
                }
                else {
                    EditorGUILayout.HelpBox("CategoryModel not assigned.", MessageType.Error);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("CategoryModel"), true);
                }
            }
            else {
                original.CategoryModel = EditorGUILayout.ObjectField("CategoryModel", original.CategoryModel, typeof(CategoryModel), false) as CategoryModel;
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", original.CategoryModel?.NamespaceName.ToString());
                EditorGUILayout.TextField("CategoryName", original.CategoryModel?.CategoryName.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}