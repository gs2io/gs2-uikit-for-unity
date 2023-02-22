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
using Gs2.Unity.UiKit.Gs2Inventory.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Inventory.Editor
{
    [CustomEditor(typeof(Gs2InventoryOwnItemSetContext))]
    public class Gs2InventoryOwnItemSetContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2InventoryOwnItemSetContext;

            if (original == null) return;

            if (original.ItemSet == null) {
                if (original.transform.parent.GetComponent<Gs2InventoryOwnItemSetList>() != null) {
                    EditorGUILayout.HelpBox("OwnItemSet is auto assign from Gs2InventoryOwnItemSetList.", MessageType.Info);
                }
                else {
                    EditorGUILayout.HelpBox("OwnItemSet not assigned.", MessageType.Error);
                    EditorGUILayout.ObjectField("OwnItemSet", original.ItemSet, typeof(OwnItemSet), false);
                }
            }
            else {
                EditorGUILayout.ObjectField("OwnItemSet", original.ItemSet, typeof(OwnItemSet), false);
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", original.ItemSet?.NamespaceName.ToString());
                EditorGUILayout.TextField("InventoryName", original.ItemSet?.InventoryName.ToString());
                EditorGUILayout.TextField("ItemName", original.ItemSet?.ItemName.ToString());
                EditorGUILayout.TextField("ItemSetName", original.ItemSet?.ItemSetName.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }
            
            serializedObject.Update();
            serializedObject.ApplyModifiedProperties();
        }
    }
}