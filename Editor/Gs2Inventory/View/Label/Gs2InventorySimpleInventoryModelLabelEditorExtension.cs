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
 *
 * deny overwrite
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
    [CustomEditor(typeof(Gs2InventorySimpleInventoryModelLabel))]
    public class Gs2InventorySimpleInventoryModelLabelEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2InventorySimpleInventoryModelLabel;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2InventorySimpleInventoryModelFetcher>() ?? original.GetComponentInParent<Gs2InventorySimpleInventoryModelFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2InventorySimpleInventoryModelFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2InventorySimpleInventoryModelFetcher>();
                }
            }
            else {
                if (fetcher.gameObject.GetComponentInParent<Gs2InventorySimpleInventoryModelList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2InventorySimpleInventoryModelFetcher), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("SimpleInventoryModel is auto assign from Gs2InventorySimpleInventoryModelList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2InventorySimpleInventoryModelFetcher), false);
                    EditorGUI.indentLevel++;
                    if (fetcher.Context != null) {
                        fetcher.Context.SimpleInventoryModel = EditorGUILayout.ObjectField("SimpleInventoryModel", fetcher.Context.SimpleInventoryModel, typeof(SimpleInventoryModel), false) as SimpleInventoryModel;
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", fetcher.Context.SimpleInventoryModel?.NamespaceName.ToString());
                        EditorGUILayout.TextField("InventoryName", fetcher.Context.SimpleInventoryModel?.InventoryName.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            original.format = EditorGUILayout.TextField("Format", original.format);

            GUILayout.Label("Add Format Parameter");
            if (GUILayout.Button("Name")) {
                original.format += "{name}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("Metadata")) {
                original.format += "{metadata}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdate"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}