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

using Gs2.Unity.Gs2Version.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Version.Context;
using Gs2.Unity.UiKit.Gs2Version.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Version.Editor
{
    [CustomEditor(typeof(Gs2VersionVersionModelContext))]
    public class Gs2VersionVersionModelContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2VersionVersionModelContext;

            if (original == null) return;

            serializedObject.Update();

            if (original.VersionModel == null) {
                if (original.transform.parent.GetComponent<Gs2VersionVersionModelList>() != null) {
                    EditorGUILayout.HelpBox("VersionModel is auto assign from Gs2VersionVersionModelList.", MessageType.Info);
                }
                else {
                    EditorGUILayout.HelpBox("VersionModel not assigned.", MessageType.Error);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("VersionModel"), true);
                }
            }
            else {
                EditorGUILayout.ObjectField("VersionModel", original.VersionModel, typeof(VersionModel), false);
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", original.VersionModel?.NamespaceName.ToString());
                EditorGUILayout.TextField("VersionName", original.VersionModel?.VersionName.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}