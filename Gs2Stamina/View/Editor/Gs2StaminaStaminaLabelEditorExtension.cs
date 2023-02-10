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

namespace Gs2.Unity.UiKit.Gs2Stamina.Editor
{
    [CustomEditor(typeof(Gs2StaminaStaminaLabel))]
    public class Gs2StaminaStaminaLabelEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2StaminaStaminaLabel;

            if (original == null) return;

            original.format = EditorGUILayout.TextField("Format", original.format);

            GUILayout.Label("Add Format Parameter");
            if (GUILayout.Button("StaminaName")) {
                original.format += "{staminaName}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("Value")) {
                original.format += "{value}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("OverflowValue")) {
                original.format += "{overflowValue}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("MaxValue")) {
                original.format += "{maxValue}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("RecoverIntervalMinutes")) {
                original.format += "{recoverIntervalMinutes}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("RecoverValue")) {
                original.format += "{recoverValue}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("NextRecoverAt(Year:2020)")) {
                original.format += "{nextRecoverAt:yyyy}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("NextRecoverAt(Year:20)")) {
                original.format += "{nextRecoverAt:yy}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("NextRecoverAt(Month:12)")) {
                original.format += "{nextRecoverAt:MM}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("NextRecoverAt(Month:Dec)")) {
                original.format += "{nextRecoverAt:MMM}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("NextRecoverAt(Day:25)")) {
                original.format += "{nextRecoverAt:dd}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("NextRecoverAt(Hour:6)")) {
                original.format += "{nextRecoverAt:hh}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("NextRecoverAt(Hour:18)")) {
                original.format += "{nextRecoverAt:HH}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("NextRecoverAt(AM/PM)")) {
                original.format += "{nextRecoverAt:tt}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("NextRecoverAt(Min:05)")) {
                original.format += "{nextRecoverAt:mm}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("NextRecoverAt(Sec:09)")) {
                original.format += "{nextRecoverAt:ss}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
        }
    }
}