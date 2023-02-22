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

using Gs2.Unity.Gs2Dictionary.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Dictionary.Context;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Dictionary.Editor
{
    [CustomEditor(typeof(Gs2DictionaryEntryModelNameEnabler))]
    public class Gs2DictionaryEntryModelNameEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2DictionaryEntryModelNameEnabler;

            if (original == null) return;

            var context = original.GetComponentInParent<Gs2DictionaryEntryModelContext>();
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2DictionaryEntryModelContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2DictionaryEntryModelContext>();
                }
            }
            else {
                if (context.transform.parent.GetComponent<Gs2DictionaryEntryModelList>() != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2DictionaryEntryModelContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("EntryModel is auto assign from Gs2DictionaryEntryModelList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2DictionaryEntryModelContext), false);
                    EditorGUI.indentLevel++;
                    EditorGUILayout.ObjectField("EntryModel", context.EntryModel, typeof(EntryModel), false);
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.EntryModel?.NamespaceName.ToString());
                    EditorGUILayout.TextField("EntryName", context.EntryModel?.EntryName.ToString());
                    EditorGUI.indentLevel--;
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2DictionaryEntryModelNameEnabler.Expression.In || original.expression == Gs2DictionaryEntryModelNameEnabler.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableNames"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableName"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}