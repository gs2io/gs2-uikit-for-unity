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

using Gs2.Unity.Gs2Experience.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Experience.Context;
using Gs2.Unity.UiKit.Gs2Experience.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Experience.Editor
{
    [CustomEditor(typeof(Gs2ExperienceStatusLabel))]
    public class Gs2ExperienceStatusLabelEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2ExperienceStatusLabel;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2ExperienceOwnStatusFetcher>() ?? original.GetComponentInParent<Gs2ExperienceOwnStatusFetcher>();
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2ExperienceOwnStatusFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2ExperienceOwnStatusFetcher>();
                }
            }
            else {
                if (fetcher.transform.parent.GetComponent<Gs2ExperienceOwnStatusList>() != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2ExperienceOwnStatusFetcher), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("Status is auto assign from Gs2ExperienceOwnStatusList.", MessageType.Info);
                }
                else {
                    var context = original.GetComponent<Gs2ExperienceOwnStatusContext>() ?? original.GetComponentInParent<Gs2ExperienceOwnStatusContext>();
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2ExperienceOwnStatusFetcher), false);
                    EditorGUI.indentLevel++;
                    context.Status = EditorGUILayout.ObjectField("Status", context.Status, typeof(OwnStatus), false) as OwnStatus;
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.Status?.NamespaceName.ToString());
                    EditorGUILayout.TextField("ExperienceName", context.Status?.ExperienceName.ToString());
                    EditorGUILayout.TextField("PropertyId", context.Status?.PropertyId.ToString());
                    EditorGUI.indentLevel--;
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            original.format = EditorGUILayout.TextField("Format", original.format);

            GUILayout.Label("Add Format Parameter");
            if (GUILayout.Button("ExperienceName")) {
                original.format += "{experienceName}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("PropertyId")) {
                original.format += "{propertyId}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("ExperienceValue")) {
                original.format += "{experienceValue}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("RankValue")) {
                original.format += "{rankValue}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("RankCapValue")) {
                original.format += "{rankCapValue}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdate"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}