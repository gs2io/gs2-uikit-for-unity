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
    [CustomEditor(typeof(Gs2DictionaryOwnLikeLabel))]
    public class Gs2DictionaryOwnLikeLabelEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2DictionaryOwnLikeLabel;

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
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2DictionaryOwnLikeFetcher), false);
                    EditorGUI.indentLevel++;
                    if (fetcher.Context != null) {
                        EditorGUILayout.ObjectField("Like", fetcher.Context.Like, typeof(OwnLike), false);
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", fetcher.Context.Like?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("EntryModelName", fetcher.Context.Like?.EntryModelName?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            original.format = EditorGUILayout.TextField("Format", original.format);

            GUILayout.Label("Add Format Parameter");
            if (GUILayout.Button("LikeId")) {
                original.format += "{likeId}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("UserId")) {
                original.format += "{userId}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("Name")) {
                original.format += "{name}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdate"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}