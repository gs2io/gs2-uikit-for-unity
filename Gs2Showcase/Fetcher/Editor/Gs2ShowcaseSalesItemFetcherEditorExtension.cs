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
 *
 * deny overwrite
 */
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable CheckNamespace

using Gs2.Unity.Gs2Showcase.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Showcase.Context;
using Gs2.Unity.UiKit.Gs2Showcase.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Showcase.Editor
{
    [CustomEditor(typeof(Gs2ShowcaseSalesItemFetcher))]
    public class Gs2ShowcaseSalesItemFetcherEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2ShowcaseSalesItemFetcher;

            if (original == null) return;

            var context = original.GetComponentInParent<Gs2ShowcaseDisplayItemContext>();
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2ShowcaseDisplayItemContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2ShowcaseDisplayItemContext>();
                }
            }
            else {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2ShowcaseDisplayItemContext), false);
                EditorGUILayout.ObjectField("DisplayItem", context.DisplayItem, typeof(DisplayItem), false);
                EditorGUI.EndDisabledGroup();
            }
            
            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onError"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}