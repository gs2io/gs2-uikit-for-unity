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

using Gs2.Unity.Gs2Account.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Account.Context;
using Gs2.Unity.UiKit.Gs2Account.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Account.Editor
{
    [CustomEditor(typeof(Gs2AccountOwnTakeOverLabel))]
    public class Gs2AccountOwnTakeOverLabelEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2AccountOwnTakeOverLabel;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2AccountOwnTakeOverFetcher>() ?? original.GetComponentInParent<Gs2AccountOwnTakeOverFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2AccountOwnTakeOverFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2AccountOwnTakeOverFetcher>();
                }
            }
            else {
                if (fetcher.gameObject.GetComponentInParent<Gs2AccountOwnTakeOverList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2AccountOwnTakeOverFetcher), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("TakeOver is auto assign from Gs2AccountOwnTakeOverList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2AccountOwnTakeOverFetcher), false);
                    EditorGUI.indentLevel++;
                    if (fetcher.Context != null) {
                        EditorGUILayout.ObjectField("TakeOver", fetcher.Context.TakeOver, typeof(OwnTakeOver), false);
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", fetcher.Context.TakeOver?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("Type", fetcher.Context.TakeOver?.Type.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            original.format = EditorGUILayout.TextField("Format", original.format);

            GUILayout.Label("Add Format Parameter");
            if (GUILayout.Button("UserId")) {
                original.format += "{userId}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("Type")) {
                original.format += "{type}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("UserIdentifier")) {
                original.format += "{userIdentifier}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("CreatedAt(Year:2020)")) {
                original.format += "{createdAt:yyyy}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("CreatedAt(Year:20)")) {
                original.format += "{createdAt:yy}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("CreatedAt(Month:12)")) {
                original.format += "{createdAt:MM}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("CreatedAt(Month:Dec)")) {
                original.format += "{createdAt:MMM}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("CreatedAt(Day:25)")) {
                original.format += "{createdAt:dd}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("CreatedAt(Hour:6)")) {
                original.format += "{createdAt:hh}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("CreatedAt(Hour:18)")) {
                original.format += "{createdAt:HH}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("CreatedAt(AM/PM)")) {
                original.format += "{createdAt:tt}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("CreatedAt(Min:05)")) {
                original.format += "{createdAt:mm}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("CreatedAt(Sec:09)")) {
                original.format += "{createdAt:ss}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdate"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}