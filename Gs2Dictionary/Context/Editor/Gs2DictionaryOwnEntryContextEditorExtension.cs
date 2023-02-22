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
using Gs2.Unity.UiKit.Gs2Dictionary.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Dictionary.Editor
{
    [CustomEditor(typeof(Gs2DictionaryOwnEntryContext))]
    public class Gs2DictionaryOwnEntryContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2DictionaryOwnEntryContext;

            if (original == null) return;

            if (original.Entry == null) {
                if (original.transform.parent.GetComponent<Gs2DictionaryOwnEntryList>() != null) {
                    EditorGUILayout.HelpBox("OwnEntry is auto assign from Gs2DictionaryOwnEntryList.", MessageType.Info);
                }
                else {
                    EditorGUILayout.HelpBox("OwnEntry not assigned.", MessageType.Error);
                    EditorGUILayout.ObjectField("OwnEntry", original.Entry, typeof(OwnEntry), false);
                }
            }
            else {
                EditorGUILayout.ObjectField("OwnEntry", original.Entry, typeof(OwnEntry), false);
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", original.Entry?.NamespaceName.ToString());
                EditorGUILayout.TextField("EntryModelName", original.Entry?.EntryModelName.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }
            
            serializedObject.Update();
            serializedObject.ApplyModifiedProperties();
        }
    }
}