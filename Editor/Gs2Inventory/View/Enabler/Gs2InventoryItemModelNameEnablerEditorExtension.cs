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

using Gs2.Unity.Gs2Inventory.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Inventory.Context;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Inventory.Enabler.Editor
{
    [CustomEditor(typeof(Gs2InventoryItemModelNameEnabler))]
    public class Gs2InventoryItemModelNameEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2InventoryItemModelNameEnabler;

            if (original == null) return;

            var context = original.GetComponent<Gs2InventoryItemModelContext>() ?? original.GetComponentInParent<Gs2InventoryItemModelContext>();
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2InventoryItemModelContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2InventoryItemModelContext>();
                }
            }
            else {
                if (context.transform.parent.GetComponent<Gs2InventoryItemModelList>() != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2InventoryItemModelContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("ItemModel is auto assign from Gs2InventoryItemModelList.", MessageType.Info);
                }
                else if (context.transform.parent.GetComponent<Gs2InventoryOwnItemSetList>() != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2InventoryOwnItemSetContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("ItemModel is auto assign from Gs2InventoryOwnItemSetList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2InventoryItemModelContext), false);
                    EditorGUI.indentLevel++;
                    context.ItemModel = EditorGUILayout.ObjectField("ItemModel", context.ItemModel, typeof(ItemModel), false) as ItemModel;
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("ItemName", context.ItemModel?.ItemName.ToString());
                    EditorGUI.indentLevel--;
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2InventoryItemModelNameEnabler.Expression.In || original.expression == Gs2InventoryItemModelNameEnabler.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableNames"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableName"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}