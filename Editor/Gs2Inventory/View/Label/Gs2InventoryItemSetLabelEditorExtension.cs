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
    [CustomEditor(typeof(Gs2InventoryItemSetLabel))]
    public class Gs2InventoryItemSetLabelEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2InventoryItemSetLabel;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2InventoryOwnItemSetFetcher>() ?? original.GetComponentInParent<Gs2InventoryOwnItemSetFetcher>();
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2InventoryOwnItemSetFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2InventoryOwnItemSetFetcher>();
                }
            }
            else {
                if (fetcher.transform.parent.GetComponent<Gs2InventoryOwnItemSetList>() != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2InventoryOwnItemSetFetcher), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("ItemSet is auto assign from Gs2InventoryOwnItemSetList.", MessageType.Info);
                }
                else {
                    var context = original.GetComponent<Gs2InventoryOwnItemSetContext>() ?? original.GetComponentInParent<Gs2InventoryOwnItemSetContext>();
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2InventoryOwnItemSetFetcher), false);
                    EditorGUI.indentLevel++;
                    context.ItemSet = EditorGUILayout.ObjectField("ItemSet", context.ItemSet, typeof(OwnItemSet), false) as OwnItemSet;
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.ItemSet?.NamespaceName.ToString());
                    EditorGUILayout.TextField("InventoryName", context.ItemSet?.InventoryName.ToString());
                    EditorGUILayout.TextField("ItemName", context.ItemSet?.ItemName.ToString());
                    EditorGUILayout.TextField("ItemSetName", context.ItemSet?.ItemSetName.ToString());
                    EditorGUI.indentLevel--;
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            original.format = EditorGUILayout.TextField("Format", original.format);

            GUILayout.Label("Add Format Parameter");
            if (GUILayout.Button("ItemSetId")) {
                original.format += "{itemSetId}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("Name")) {
                original.format += "{name}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("InventoryName")) {
                original.format += "{inventoryName}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("ItemName")) {
                original.format += "{itemName}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("Count")) {
                original.format += "{count}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("SortValue")) {
                original.format += "{sortValue}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("ExpiresAt(Year:2020)")) {
                original.format += "{expiresAt:yyyy}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("ExpiresAt(Year:20)")) {
                original.format += "{expiresAt:yy}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("ExpiresAt(Month:12)")) {
                original.format += "{expiresAt:MM}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("ExpiresAt(Month:Dec)")) {
                original.format += "{expiresAt:MMM}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("ExpiresAt(Day:25)")) {
                original.format += "{expiresAt:dd}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("ExpiresAt(Hour:6)")) {
                original.format += "{expiresAt:hh}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("ExpiresAt(Hour:18)")) {
                original.format += "{expiresAt:HH}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("ExpiresAt(AM/PM)")) {
                original.format += "{expiresAt:tt}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("ExpiresAt(Min:05)")) {
                original.format += "{expiresAt:mm}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("ExpiresAt(Sec:09)")) {
                original.format += "{expiresAt:ss}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdate"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}