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

using Gs2.Unity.Gs2Showcase.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Showcase.Context;
using Gs2.Unity.UiKit.Gs2Showcase.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Showcase.Editor
{
    [CustomEditor(typeof(Gs2ShowcaseOwnDisplayItemFetcher))]
    public class Gs2ShowcaseOwnDisplayItemFetcherEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2ShowcaseOwnDisplayItemFetcher;

            if (original == null) return;

            var context = original.GetComponent<Gs2ShowcaseOwnDisplayItemContext>() ?? original.GetComponentInParent<Gs2ShowcaseOwnDisplayItemContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2ShowcaseOwnDisplayItemContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2ShowcaseOwnDisplayItemContext>();
                }
            }
            else {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2ShowcaseOwnDisplayItemContext), false);
                EditorGUI.indentLevel++;
                EditorGUILayout.ObjectField("DisplayItem", context.DisplayItem, typeof(OwnDisplayItem), false);
                if (context.DisplayItem != null) {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.DisplayItem?.NamespaceName?.ToString());
                    EditorGUILayout.TextField("ShowcaseName", context.DisplayItem?.ShowcaseName?.ToString());
                    EditorGUILayout.TextField("DisplayItemId", context.DisplayItem?.DisplayItemId?.ToString());
                    EditorGUI.indentLevel--;
                }
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }
            
            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onError"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}