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

using Gs2.Unity.Gs2Version.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Version.Context;
using Gs2.Unity.UiKit.Gs2Version.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Version.Editor
{
    [CustomEditor(typeof(Gs2VersionVersionModelLabel))]
    public class Gs2VersionVersionModelLabelEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2VersionVersionModelLabel;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2VersionVersionModelFetcher>() ?? original.GetComponentInParent<Gs2VersionVersionModelFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2VersionVersionModelFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2VersionVersionModelFetcher>();
                }
            }
            else {
                if (fetcher.gameObject.GetComponentInParent<Gs2VersionVersionModelList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2VersionVersionModelFetcher), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("VersionModel is auto assign from Gs2VersionVersionModelList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2VersionVersionModelFetcher), false);
                    EditorGUI.indentLevel++;
                    if (fetcher.Context != null) {
                        fetcher.Context.VersionModel = EditorGUILayout.ObjectField("VersionModel", fetcher.Context.VersionModel, typeof(VersionModel), false) as VersionModel;
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", fetcher.Context.VersionModel?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("VersionName", fetcher.Context.VersionModel?.VersionName?.ToString());
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
            if (GUILayout.Button("WarningVersion")) {
                original.format += "{warningVersion}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("ErrorVersion")) {
                original.format += "{errorVersion}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("Scope")) {
                original.format += "{scope}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("CurrentVersion")) {
                original.format += "{currentVersion}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("NeedSignature")) {
                original.format += "{needSignature}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdate"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}