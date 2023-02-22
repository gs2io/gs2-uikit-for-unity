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

using Gs2.Unity.Gs2Mission.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Mission.Context;
using Gs2.Unity.UiKit.Gs2Mission.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Mission.Editor
{
    [CustomEditor(typeof(Gs2MissionUserContext))]
    public class Gs2MissionUserContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2MissionUserContext;

            if (original == null) return;

            if (original.User == null) {
                EditorGUILayout.HelpBox("User not assigned.", MessageType.Error);
                EditorGUILayout.ObjectField("User", original.User, typeof(User), false);
            }
            else {
                EditorGUILayout.ObjectField("User", original.User, typeof(User), false);
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", original.User?.NamespaceName.ToString());
                EditorGUILayout.TextField("UserId", original.User?.UserId.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }
            
            serializedObject.Update();
            serializedObject.ApplyModifiedProperties();
        }
    }
}