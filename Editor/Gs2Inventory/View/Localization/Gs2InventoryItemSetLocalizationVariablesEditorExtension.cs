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

#if GS2_ENABLE_LOCALIZATION

using Gs2.Unity.Gs2Inventory.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Inventory.Context;
using Gs2.Unity.UiKit.Gs2Inventory.Fetcher;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace Gs2.Unity.UiKit.Gs2Inventory.Localization.Editor
{
    [CustomEditor(typeof(Gs2InventoryItemSetLocalizationVariables))]
    public class Gs2InventoryItemSetLocalizationVariablesEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2InventoryItemSetLocalizationVariables;

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
            original.target = EditorGUILayout.ObjectField("Target", original.target, typeof(LocalizeStringEvent), true) as LocalizeStringEvent;

            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif