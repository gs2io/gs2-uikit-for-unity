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

using Gs2.Unity.UiKit.Gs2Inventory.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Inventory.Label.Editor
{
    [CustomEditor(typeof(Gs2InventoryVerifySimpleItemByUserIdLabel))]
    public class Gs2InventoryVerifySimpleItemByUserIdLabelEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2InventoryVerifySimpleItemByUserIdLabel;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2InventoryVerifySimpleItemByUserIdFetcher>() ?? original.GetComponentInParent<Gs2InventoryVerifySimpleItemByUserIdFetcher>(true);
             var userDataFetcher = original.GetComponent<Gs2InventoryOwnSimpleItemFetcher>() ?? original.GetComponentInParent<Gs2InventoryOwnSimpleItemFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2InventoryVerifySimpleItemByUserIdFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2InventoryVerifySimpleItemByUserIdFetcher>();
                }
            }
            if (userDataFetcher == null) {
                EditorGUILayout.HelpBox("Gs2InventoryOwnSimpleItemFetcher not found. Adding a Fetcher allows more values to be used.", MessageType.Warning);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2InventoryOwnSimpleItemFetcher>();
                }
            }

            serializedObject.Update();
            original.format = EditorGUILayout.TextField("Format", original.format);

            GUILayout.Label("Add Format Parameter");
            if (GUILayout.Button("NamespaceName")) {
                original.format += "{namespaceName}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("UserId")) {
                original.format += "{userId}";
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
            if (GUILayout.Button("VerifyType")) {
                original.format += "{verifyType}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("Count")) {
                original.format += "{count}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (userDataFetcher != null) {
                if (GUILayout.Button("UserData:ItemId")) {
                    original.format += "{userData:itemId}";
                    GUI.FocusControl("");
                    EditorUtility.SetDirty(original);
                }
                if (GUILayout.Button("UserData:ItemName")) {
                    original.format += "{userData:itemName}";
                    GUI.FocusControl("");
                    EditorUtility.SetDirty(original);
                }
                if (GUILayout.Button("UserData:Count")) {
                    original.format += "{userData:count}";
                    GUI.FocusControl("");
                    EditorUtility.SetDirty(original);
                }
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdate"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}