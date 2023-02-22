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

using Gs2.Unity.Gs2Lottery.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Lottery.Context;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Lottery.Editor
{
    [CustomEditor(typeof(Gs2LotteryBoxItemsLabel))]
    public class Gs2LotteryBoxItemsLabelEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2LotteryBoxItemsLabel;

            if (original == null) return;

            var context = original.GetComponentInParent<Gs2LotteryOwnBoxItemsContext>();
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2LotteryOwnBoxItemsContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2LotteryOwnBoxItemsContext>();
                }
            }
            else {
                if (context.transform.parent.GetComponent<Gs2LotteryOwnBoxItemsList>() != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2LotteryOwnBoxItemsContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("BoxItems is auto assign from Gs2LotteryOwnBoxItemsList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2LotteryOwnBoxItemsContext), false);
                    EditorGUI.indentLevel++;
                    EditorGUILayout.ObjectField("BoxItems", context.BoxItems, typeof(OwnBoxItems), false);
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.BoxItems?.NamespaceName.ToString());
                    EditorGUILayout.TextField("PrizeTableName", context.BoxItems?.PrizeTableName.ToString());
                    EditorGUI.indentLevel--;
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            original.format = EditorGUILayout.TextField("Format", original.format);

            GUILayout.Label("Add Format Parameter");
            if (GUILayout.Button("BoxId")) {
                original.format += "{boxId}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("PrizeTableName")) {
                original.format += "{prizeTableName}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("Items")) {
                original.format += "{items}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdate"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}