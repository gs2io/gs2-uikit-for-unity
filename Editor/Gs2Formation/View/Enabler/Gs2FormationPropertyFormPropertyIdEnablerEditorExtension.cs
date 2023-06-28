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

namespace Gs2.Unity.UiKit.Gs2Formation.Enabler.Editor
{
    [CustomEditor(typeof(Gs2FormationPropertyFormPropertyIdEnabler))]
    public class Gs2FormationPropertyFormPropertyIdEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2FormationPropertyFormPropertyIdEnabler;

            if (original == null) return;

            var context = original.GetComponent<Gs2FormationOwnPropertyFormContext>() ?? original.GetComponentInParent<Gs2FormationOwnPropertyFormContext>();
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2FormationOwnPropertyFormContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2FormationOwnPropertyFormContext>();
                }
            }
            else {
                if (context.transform.parent.GetComponent<Gs2FormationOwnPropertyFormList>() != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2FormationOwnPropertyFormContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("PropertyForm is auto assign from Gs2FormationOwnPropertyFormList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2FormationOwnPropertyFormContext), false);
                    EditorGUI.indentLevel++;
                    context.PropertyForm = EditorGUILayout.ObjectField("PropertyForm", context.PropertyForm, typeof(OwnPropertyForm), false) as OwnPropertyForm;
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.PropertyForm?.NamespaceName.ToString());
                    EditorGUILayout.TextField("FormModelName", context.PropertyForm?.FormModelName.ToString());
                    EditorGUILayout.TextField("PropertyId", context.PropertyForm?.PropertyId.ToString());
                    EditorGUI.indentLevel--;
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2FormationPropertyFormPropertyIdEnabler.Expression.In || original.expression == Gs2FormationPropertyFormPropertyIdEnabler.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enablePropertyIds"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enablePropertyId"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}