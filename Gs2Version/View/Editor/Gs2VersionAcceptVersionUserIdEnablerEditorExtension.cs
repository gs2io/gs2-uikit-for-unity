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
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Version.Editor
{
    [CustomEditor(typeof(Gs2VersionAcceptVersionUserIdEnabler))]
    public class Gs2VersionAcceptVersionUserIdEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2VersionAcceptVersionUserIdEnabler;

            if (original == null) return;

            var context = original.GetComponentInParent<Gs2VersionOwnAcceptVersionContext>();
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2VersionOwnAcceptVersionContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2VersionOwnAcceptVersionContext>();
                }
            }
            else {
                if (context.transform.parent.GetComponent<Gs2VersionOwnAcceptVersionList>() != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2VersionOwnAcceptVersionContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("AcceptVersion is auto assign from Gs2VersionOwnAcceptVersionList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2VersionOwnAcceptVersionContext), false);
                    EditorGUI.indentLevel++;
                    EditorGUILayout.ObjectField("AcceptVersion", context.AcceptVersion, typeof(OwnAcceptVersion), false);
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.AcceptVersion?.NamespaceName.ToString());
                    EditorGUILayout.TextField("VersionName", context.AcceptVersion?.VersionName.ToString());
                    EditorGUI.indentLevel--;
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2VersionAcceptVersionUserIdEnabler.Expression.In || original.expression == Gs2VersionAcceptVersionUserIdEnabler.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableUserIds"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableUserId"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}