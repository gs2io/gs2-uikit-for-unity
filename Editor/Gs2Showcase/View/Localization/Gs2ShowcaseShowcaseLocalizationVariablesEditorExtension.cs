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

#if GS2_ENABLE_LOCALIZATION

using Gs2.Unity.Gs2Showcase.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Showcase.Context;
using Gs2.Unity.UiKit.Gs2Showcase.Fetcher;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace Gs2.Unity.UiKit.Gs2Showcase.Localization.Editor
{
    [CustomEditor(typeof(Gs2ShowcaseShowcaseLocalizationVariables))]
    public class Gs2ShowcaseShowcaseLocalizationVariablesEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2ShowcaseShowcaseLocalizationVariables;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2ShowcaseShowcaseFetcher>() ?? original.GetComponentInParent<Gs2ShowcaseShowcaseFetcher>();
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2ShowcaseShowcaseFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2ShowcaseShowcaseFetcher>();
                }
            }
            else {
                var context = original.GetComponent<Gs2ShowcaseShowcaseContext>() ?? original.GetComponentInParent<Gs2ShowcaseShowcaseContext>();
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2ShowcaseShowcaseFetcher), false);
                EditorGUI.indentLevel++;
                context.Showcase = EditorGUILayout.ObjectField("Showcase", context.Showcase, typeof(Showcase), false) as Showcase;
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", context.Showcase?.NamespaceName.ToString());
                EditorGUILayout.TextField("ShowcaseName", context.Showcase?.ShowcaseName.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.Update();
            original.target = EditorGUILayout.ObjectField("Target", original.target, typeof(LocalizeStringEvent), true) as LocalizeStringEvent;

            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif