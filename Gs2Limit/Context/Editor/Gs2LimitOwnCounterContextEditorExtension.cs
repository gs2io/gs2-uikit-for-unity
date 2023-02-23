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

using Gs2.Unity.Gs2Limit.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Limit.Context;
using Gs2.Unity.UiKit.Gs2Limit.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Limit.Editor
{
    [CustomEditor(typeof(Gs2LimitOwnCounterContext))]
    public class Gs2LimitOwnCounterContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2LimitOwnCounterContext;

            if (original == null) return;

            serializedObject.Update();

            if (original.Counter == null) {
                if (original.transform.parent.GetComponent<Gs2LimitOwnCounterList>() != null) {
                    EditorGUILayout.HelpBox("OwnCounter is auto assign from Gs2LimitOwnCounterList.", MessageType.Info);
                }
                else {
                    EditorGUILayout.HelpBox("OwnCounter not assigned.", MessageType.Error);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("Counter"), true);
                }
            }
            else {
                EditorGUILayout.ObjectField("OwnCounter", original.Counter, typeof(OwnCounter), false);
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", original.Counter?.NamespaceName.ToString());
                EditorGUILayout.TextField("LimitName", original.Counter?.LimitName.ToString());
                EditorGUILayout.TextField("CounterName", original.Counter?.CounterName.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}