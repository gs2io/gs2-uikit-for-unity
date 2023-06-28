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

using Gs2.Unity.Gs2Datastore.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Datastore.Context;
using Gs2.Unity.UiKit.Gs2Datastore.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Datastore.Editor
{
    [CustomEditor(typeof(Gs2DatastoreDataObjectHistoryLabel))]
    public class Gs2DatastoreDataObjectHistoryLabelEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2DatastoreDataObjectHistoryLabel;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2DatastoreOwnDataObjectHistoryFetcher>() ?? original.GetComponentInParent<Gs2DatastoreOwnDataObjectHistoryFetcher>();
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2DatastoreOwnDataObjectHistoryFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2DatastoreOwnDataObjectHistoryFetcher>();
                }
            }
            else {
                if (fetcher.transform.parent.GetComponent<Gs2DatastoreOwnDataObjectHistoryList>() != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2DatastoreOwnDataObjectHistoryFetcher), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("DataObjectHistory is auto assign from Gs2DatastoreOwnDataObjectHistoryList.", MessageType.Info);
                }
                else {
                    var context = original.GetComponent<Gs2DatastoreOwnDataObjectHistoryContext>() ?? original.GetComponentInParent<Gs2DatastoreOwnDataObjectHistoryContext>();
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2DatastoreOwnDataObjectHistoryFetcher), false);
                    EditorGUI.indentLevel++;
                    context.DataObjectHistory = EditorGUILayout.ObjectField("DataObjectHistory", context.DataObjectHistory, typeof(OwnDataObjectHistory), false) as OwnDataObjectHistory;
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.DataObjectHistory?.NamespaceName.ToString());
                    EditorGUILayout.TextField("DataObjectName", context.DataObjectHistory?.DataObjectName.ToString());
                    EditorGUILayout.TextField("Generation", context.DataObjectHistory?.Generation.ToString());
                    EditorGUI.indentLevel--;
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            original.format = EditorGUILayout.TextField("Format", original.format);

            GUILayout.Label("Add Format Parameter");
            if (GUILayout.Button("DataObjectHistoryId")) {
                original.format += "{dataObjectHistoryId}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("Generation")) {
                original.format += "{generation}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("ContentLength")) {
                original.format += "{contentLength}";
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