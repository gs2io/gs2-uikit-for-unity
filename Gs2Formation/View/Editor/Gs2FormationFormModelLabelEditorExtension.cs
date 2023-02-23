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

using Gs2.Unity.Gs2Formation.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Formation.Context;
using Gs2.Unity.UiKit.Gs2Formation.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Formation.Editor
{
    [CustomEditor(typeof(Gs2FormationFormModelLabel))]
    public class Gs2FormationFormModelLabelEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2FormationFormModelLabel;

            if (original == null) return;

            var fetcher = original.GetComponentInParent<Gs2FormationFormModelFetcher>();
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2FormationFormModelFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2FormationFormModelFetcher>();
                }
            }
            else {
                if (fetcher.transform.parent.GetComponent<Gs2FormationFormModelList>() != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2FormationFormModelFetcher), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("FormModel is auto assign from Gs2FormationFormModelList.", MessageType.Info);
                }
                else if (fetcher.transform.parent.GetComponent<Gs2FormationOwnFormList>() != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2FormationOwnFormFetcher), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("FormModel is auto assign from Gs2FormationOwnFormList.", MessageType.Info);
                }
                else {
                    var context = original.GetComponentInParent<Gs2FormationFormModelContext>();
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2FormationFormModelFetcher), false);
                    EditorGUI.indentLevel++;
                    EditorGUILayout.ObjectField("FormModel", context.FormModel, typeof(FormModel), false);
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.FormModel?.NamespaceName.ToString());
                    EditorGUILayout.TextField("FormModelName", context.FormModel?.FormModelName.ToString());
                    EditorGUI.indentLevel--;
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
            if (GUILayout.Button("Slots")) {
                original.format += "{slots}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdate"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}