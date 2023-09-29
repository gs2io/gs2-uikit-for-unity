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
using Gs2.Unity.UiKit.Gs2Inventory.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Inventory.Editor
{
    [CustomEditor(typeof(Gs2InventoryOwnBigItemContext))]
    public class Gs2InventoryOwnBigItemContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2InventoryOwnBigItemContext;

            if (original == null) return;

            serializedObject.Update();

            if (original.BigItem == null) {
                var list = original.GetComponentInParent<Gs2InventoryOwnBigItemList>(true);
                if (list != null) {
                    EditorGUILayout.HelpBox("BigItem is auto assign from Gs2InventoryOwnBigItemList.", MessageType.Info);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("List", list, typeof(Gs2InventoryOwnBigItemList), false);
                    EditorGUI.EndDisabledGroup();
                }
                else if (original.GetComponentInParent<Gs2InventoryConvertBigItemModelToOwnBigItem>(true) != null) {
                    EditorGUILayout.HelpBox("BigItem is auto assign from Gs2InventoryConvertBigItemModelToOwnBigItem.", MessageType.Info);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Converter", original.GetComponentInParent<Gs2InventoryConvertBigItemModelToOwnBigItem>(true), typeof(Gs2InventoryConvertBigItemModelToOwnBigItem), false);
                    EditorGUI.EndDisabledGroup();
                }
                else {
                    EditorGUILayout.HelpBox("OwnBigItem not assigned.", MessageType.Error);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("_bigItem"), true);
                }
            }
            else {
                original.BigItem = EditorGUILayout.ObjectField("OwnBigItem", original.BigItem, typeof(OwnBigItem), false) as OwnBigItem;
                EditorGUI.BeginDisabledGroup(true);
                if (original.BigItem != null) {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", original.BigItem?.NamespaceName?.ToString());
                    EditorGUILayout.TextField("InventoryName", original.BigItem?.InventoryName?.ToString());
                    EditorGUILayout.TextField("ItemName", original.BigItem?.ItemName?.ToString());
                    EditorGUI.indentLevel--;
                }
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}