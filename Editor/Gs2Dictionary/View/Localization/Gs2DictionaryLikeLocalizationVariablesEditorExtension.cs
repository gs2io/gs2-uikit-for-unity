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

#if GS2_ENABLE_LOCALIZATION

using Gs2.Unity.Gs2Dictionary.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Dictionary.Context;
using Gs2.Unity.UiKit.Gs2Dictionary.Fetcher;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace Gs2.Unity.UiKit.Gs2Dictionary.Localization.Editor
{
    [CustomEditor(typeof(Gs2DictionaryLikeLocalizationVariables))]
    public class Gs2DictionaryLikeLocalizationVariablesEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2DictionaryLikeLocalizationVariables;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2DictionaryOwnLikeFetcher>() ?? original.GetComponentInParent<Gs2DictionaryOwnLikeFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2DictionaryOwnLikeFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2DictionaryOwnLikeFetcher>();
                }
            }
            else {
                if (fetcher.gameObject.GetComponentInParent<Gs2DictionaryOwnLikeList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2DictionaryOwnLikeFetcher), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("Like is auto assign from Gs2DictionaryOwnLikeList.", MessageType.Info);
                }
                else {
                    var context = original.GetComponent<Gs2DictionaryOwnLikeContext>() ?? original.GetComponentInParent<Gs2DictionaryOwnLikeContext>(true);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2DictionaryOwnLikeFetcher), false);
                    EditorGUI.indentLevel++;
                    context.Like = EditorGUILayout.ObjectField("Like", context.Like, typeof(OwnLike), false) as OwnLike;
                    if (context.Like != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.Like?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("EntryModelName", context.Like?.EntryModelName?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            original.target = EditorGUILayout.ObjectField("Target", original.target, typeof(LocalizeStringEvent), true) as LocalizeStringEvent;

            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif