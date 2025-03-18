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

using Gs2.Unity.Gs2Dictionary.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Dictionary.Context;
using Gs2.Unity.UiKit.Gs2Dictionary.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Dictionary.Editor
{
    [CustomEditor(typeof(Gs2DictionaryOwnLikeFetcher))]
    public class Gs2DictionaryOwnLikeFetcherEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2DictionaryOwnLikeFetcher;

            if (original == null) return;

            var context = original.GetComponent<Gs2DictionaryOwnLikeContext>() ?? original.GetComponentInParent<Gs2DictionaryOwnLikeContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2DictionaryOwnLikeContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2DictionaryOwnLikeContext>();
                }
            }
            else {
                if (context.gameObject.GetComponentInParent<Gs2DictionaryOwnLikeList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2DictionaryOwnLikeContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("Like is auto assign from Gs2DictionaryOwnLikeList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2DictionaryOwnLikeContext), false);
                    EditorGUI.indentLevel++;
                    EditorGUILayout.ObjectField("Like", context.Like, typeof(OwnLike), false);
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
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onError"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}