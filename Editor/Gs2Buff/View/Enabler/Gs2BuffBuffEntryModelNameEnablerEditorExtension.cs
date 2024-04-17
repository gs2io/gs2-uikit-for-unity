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
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Buff.Enabler.Editor
{
    [CustomEditor(typeof(Gs2BuffBuffEntryModelNameEnabler))]
    public class Gs2BuffBuffEntryModelNameEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2BuffBuffEntryModelNameEnabler;

            if (original == null) return;

            var context = original.GetComponent<Gs2BuffBuffEntryModelContext>() ?? original.GetComponentInParent<Gs2BuffBuffEntryModelContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2BuffBuffEntryModelContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2BuffBuffEntryModelContext>();
                }
            }
            else {
                if (context.gameObject.GetComponentInParent<Gs2BuffBuffEntryModelList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2BuffBuffEntryModelContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("BuffEntryModel is auto assign from Gs2BuffBuffEntryModelList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2BuffBuffEntryModelContext), false);
                    EditorGUI.indentLevel++;
                    context.BuffEntryModel = EditorGUILayout.ObjectField("BuffEntryModel", context.BuffEntryModel, typeof(BuffEntryModel), false) as BuffEntryModel;
                    if (context.BuffEntryModel != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.BuffEntryModel?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("BuffEntryName", context.BuffEntryModel?.BuffEntryName?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2BuffBuffEntryModelNameEnabler.Expression.In || original.expression == Gs2BuffBuffEntryModelNameEnabler.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableNames"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableName"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}