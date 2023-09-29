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

using Gs2.Unity.Gs2Experience.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Experience.Context;
using Gs2.Unity.UiKit.Gs2Experience.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Experience.Editor
{
    [CustomEditor(typeof(Gs2ExperienceOwnStatusContext))]
    public class Gs2ExperienceOwnStatusContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2ExperienceOwnStatusContext;

            if (original == null) return;

            serializedObject.Update();

            if (original.Status == null) {
                var list = original.GetComponentInParent<Gs2ExperienceOwnStatusList>(true);
                if (list != null) {
                    EditorGUILayout.HelpBox("Status is auto assign from Gs2ExperienceOwnStatusList.", MessageType.Info);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("List", list, typeof(Gs2ExperienceOwnStatusList), false);
                    EditorGUI.EndDisabledGroup();
                }
                else {
                    EditorGUILayout.HelpBox("OwnStatus not assigned.", MessageType.Error);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("_status"), true);
                }
            }
            else {
                original.Status = EditorGUILayout.ObjectField("OwnStatus", original.Status, typeof(OwnStatus), false) as OwnStatus;
                EditorGUI.BeginDisabledGroup(true);
                if (original.Status != null) {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", original.Status?.NamespaceName?.ToString());
                    EditorGUILayout.TextField("ExperienceName", original.Status?.ExperienceName?.ToString());
                    EditorGUILayout.TextField("PropertyId", original.Status?.PropertyId?.ToString());
                    EditorGUI.indentLevel--;
                }
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}