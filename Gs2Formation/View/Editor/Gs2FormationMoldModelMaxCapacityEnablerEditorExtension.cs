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
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Formation.Editor
{
    [CustomEditor(typeof(Gs2FormationMoldModelMaxCapacityEnabler))]
    public class Gs2FormationMoldModelMaxCapacityEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2FormationMoldModelMaxCapacityEnabler;

            if (original == null) return;

            var context = original.GetComponentInParent<Gs2FormationMoldModelContext>();
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2FormationMoldModelContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2FormationMoldModelContext>();
                }
            }
            else {
                if (context.transform.parent.GetComponent<Gs2FormationMoldModelList>() != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2FormationMoldModelContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("MoldModel is auto assign from Gs2FormationMoldModelList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2FormationMoldModelContext), false);
                    EditorGUI.indentLevel++;
                    EditorGUILayout.ObjectField("MoldModel", context.MoldModel, typeof(MoldModel), false);
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.MoldModel?.NamespaceName.ToString());
                    EditorGUILayout.TextField("MoldName", context.MoldModel?.MoldName.ToString());
                    EditorGUI.indentLevel--;
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2FormationMoldModelMaxCapacityEnabler.Expression.In || original.expression == Gs2FormationMoldModelMaxCapacityEnabler.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableMaxCapacities"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableMaxCapacity"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}