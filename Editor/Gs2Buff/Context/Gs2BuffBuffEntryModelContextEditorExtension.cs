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

using Gs2.Unity.Gs2Buff.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Buff.Context;
using Gs2.Unity.UiKit.Gs2Buff.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Buff.Editor
{
    [CustomEditor(typeof(Gs2BuffBuffEntryModelContext))]
    public class Gs2BuffBuffEntryModelContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2BuffBuffEntryModelContext;

            if (original == null) return;

            serializedObject.Update();

            if (original.BuffEntryModel == null) {
                var list = original.GetComponentInParent<Gs2BuffBuffEntryModelList>(true);
                if (list != null) {
                    EditorGUILayout.HelpBox("BuffEntryModel is auto assign from Gs2BuffBuffEntryModelList.", MessageType.Info);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("List", list, typeof(Gs2BuffBuffEntryModelList), false);
                    EditorGUI.EndDisabledGroup();
                }
                else {
                    EditorGUILayout.HelpBox("BuffEntryModel not assigned.", MessageType.Error);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("_buffEntryModel"), true);
                }
            }
            else {
                original.BuffEntryModel = EditorGUILayout.ObjectField("BuffEntryModel", original.BuffEntryModel, typeof(BuffEntryModel), false) as BuffEntryModel;
                EditorGUI.BeginDisabledGroup(true);
                if (original.BuffEntryModel != null) {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", original.BuffEntryModel?.NamespaceName?.ToString());
                    EditorGUILayout.TextField("BuffEntryName", original.BuffEntryModel?.BuffEntryName?.ToString());
                    EditorGUI.indentLevel--;
                }
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}