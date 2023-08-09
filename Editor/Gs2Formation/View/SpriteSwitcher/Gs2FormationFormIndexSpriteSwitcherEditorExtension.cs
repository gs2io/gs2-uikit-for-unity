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

using Gs2.Unity.Gs2Formation.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Formation.Context;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Formation.Editor
{
    [CustomEditor(typeof(Gs2FormationFormIndexSpriteSwitcher))]
    public class Gs2FormationFormIndexSpriteSwitcherEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2FormationFormIndexSpriteSwitcher;

            if (original == null) return;

            var context = original.GetComponent<Gs2FormationOwnFormContext>() ?? original.GetComponentInParent<Gs2FormationOwnFormContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2FormationOwnFormContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2FormationOwnFormContext>();
                }
            }
            else {
                if (context.gameObject.GetComponentInParent<Gs2FormationOwnFormList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2FormationOwnFormContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("Form is auto assign from Gs2FormationOwnFormList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2FormationOwnFormContext), false);
                    EditorGUI.indentLevel++;
                    context.Form = EditorGUILayout.ObjectField("Form", context.Form, typeof(OwnForm), false) as OwnForm;
                    if (context.Form != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.Form?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("MoldName", context.Form?.MoldName?.ToString());
                        EditorGUILayout.TextField("Index", context.Form?.Index.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2FormationFormIndexSpriteSwitcher.Expression.In || original.expression == Gs2FormationFormIndexSpriteSwitcher.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("applyIndexes"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("applyIndex"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("sprite"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdate"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}