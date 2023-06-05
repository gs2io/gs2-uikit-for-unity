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
    [CustomEditor(typeof(Gs2VersionAcceptVersionLabel))]
    public class Gs2VersionAcceptVersionLabelEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2VersionAcceptVersionLabel;

            if (original == null) return;

            var fetcher = original.GetComponentInParent<Gs2VersionOwnAcceptVersionFetcher>();
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2VersionOwnAcceptVersionFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2VersionOwnAcceptVersionFetcher>();
                }
            }
            else {
                if (fetcher.transform.parent.GetComponent<Gs2VersionOwnAcceptVersionList>() != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2VersionOwnAcceptVersionFetcher), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("AcceptVersion is auto assign from Gs2VersionOwnAcceptVersionList.", MessageType.Info);
                }
                else {
                    var context = original.GetComponentInParent<Gs2VersionOwnAcceptVersionContext>();
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2VersionOwnAcceptVersionFetcher), false);
                    EditorGUI.indentLevel++;
                    context.AcceptVersion = EditorGUILayout.ObjectField("AcceptVersion", context.AcceptVersion, typeof(OwnAcceptVersion), false) as OwnAcceptVersion;
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.AcceptVersion?.NamespaceName.ToString());
                    EditorGUILayout.TextField("VersionName", context.AcceptVersion?.VersionName.ToString());
                    EditorGUI.indentLevel--;
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            original.format = EditorGUILayout.TextField("Format", original.format);

            GUILayout.Label("Add Format Parameter");
            if (GUILayout.Button("VersionName")) {
                original.format += "{versionName}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("UserId")) {
                original.format += "{userId}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("Version")) {
                original.format += "{version}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdate"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}