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

using Gs2.Unity.Gs2Inventory.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Inventory.Context;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Inventory.Editor
{
    [CustomEditor(typeof(Gs2InventoryInventoryLabel))]
    public class Gs2InventoryInventoryLabelEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2InventoryInventoryLabel;

            if (original == null) return;

            var context = original.GetComponentInParent<Gs2InventoryOwnInventoryContext>();
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2InventoryOwnInventoryContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2InventoryOwnInventoryContext>();
                }
            }
            else {
                if (context.transform.parent.GetComponent<Gs2InventoryOwnInventoryList>() != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2InventoryOwnInventoryContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("Inventory is auto assign from Gs2InventoryOwnInventoryList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2InventoryOwnInventoryContext), false);
                    EditorGUI.indentLevel++;
                    EditorGUILayout.ObjectField("Inventory", context.Inventory, typeof(OwnInventory), false);
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.Inventory?.NamespaceName.ToString());
                    EditorGUILayout.TextField("InventoryName", context.Inventory?.InventoryName.ToString());
                    EditorGUI.indentLevel--;
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            original.format = EditorGUILayout.TextField("Format", original.format);

            GUILayout.Label("Add Format Parameter");
            if (GUILayout.Button("InventoryId")) {
                original.format += "{inventoryId}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("InventoryName")) {
                original.format += "{inventoryName}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("CurrentInventoryCapacityUsage")) {
                original.format += "{currentInventoryCapacityUsage}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("CurrentInventoryMaxCapacity")) {
                original.format += "{currentInventoryMaxCapacity}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdate"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}