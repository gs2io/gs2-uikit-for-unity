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

using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Schedule.Editor
{
    [CustomEditor(typeof(Gs2ScheduleTriggerLabel))]
    public class Gs2ScheduleTriggerLabelEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2ScheduleTriggerLabel;

            if (original == null) return;

            original.format = EditorGUILayout.TextField("Format", original.format);

            GUILayout.Label("Add Format Parameter");
            if (GUILayout.Button("TriggerId")) {
                original.format += "{triggerId}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("Name")) {
                original.format += "{name}";
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
            if (GUILayout.Button("ExpiresAt(Year:2020)")) {
                original.format += "{expiresAt:yyyy}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("ExpiresAt(Year:20)")) {
                original.format += "{expiresAt:yy}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("ExpiresAt(Month:12)")) {
                original.format += "{expiresAt:MM}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("ExpiresAt(Month:Dec)")) {
                original.format += "{expiresAt:MMM}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("ExpiresAt(Day:25)")) {
                original.format += "{expiresAt:dd}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("ExpiresAt(Hour:6)")) {
                original.format += "{expiresAt:hh}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("ExpiresAt(Hour:18)")) {
                original.format += "{expiresAt:HH}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("ExpiresAt(AM/PM)")) {
                original.format += "{expiresAt:tt}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("ExpiresAt(Min:05)")) {
                original.format += "{expiresAt:mm}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("ExpiresAt(Sec:09)")) {
                original.format += "{expiresAt:ss}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
        }
    }
}