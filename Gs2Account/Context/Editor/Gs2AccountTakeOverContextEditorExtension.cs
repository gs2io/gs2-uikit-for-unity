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

using Gs2.Unity.Gs2Account.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Account.Context;
using Gs2.Unity.UiKit.Gs2Account.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Account.Editor
{
    [CustomEditor(typeof(Gs2AccountTakeOverContext))]
    public class Gs2AccountTakeOverContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2AccountTakeOverContext;

            if (original == null) return;

            serializedObject.Update();

            if (original.TakeOver == null) {
                EditorGUILayout.HelpBox("TakeOver not assigned.", MessageType.Error);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("TakeOver"), true);
            }
            else {
                original.TakeOver = EditorGUILayout.ObjectField("TakeOver", original.TakeOver, typeof(TakeOver), false) as TakeOver;
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", original.TakeOver?.NamespaceName.ToString());
                EditorGUILayout.TextField("Type", original.TakeOver?.Type.ToString());
                EditorGUILayout.TextField("UserIdentifier", original.TakeOver?.UserIdentifier.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}