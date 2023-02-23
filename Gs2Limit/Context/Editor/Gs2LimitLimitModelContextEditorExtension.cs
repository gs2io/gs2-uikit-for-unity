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
    [CustomEditor(typeof(Gs2LimitLimitModelContext))]
    public class Gs2LimitLimitModelContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2LimitLimitModelContext;

            if (original == null) return;

            serializedObject.Update();

            if (original.LimitModel == null) {
                if (original.transform.parent.GetComponent<Gs2LimitLimitModelList>() != null) {
                    EditorGUILayout.HelpBox("LimitModel is auto assign from Gs2LimitLimitModelList.", MessageType.Info);
                }
                else {
                    EditorGUILayout.HelpBox("LimitModel not assigned.", MessageType.Error);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("LimitModel"), true);
                }
            }
            else {
                EditorGUILayout.ObjectField("LimitModel", original.LimitModel, typeof(LimitModel), false);
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", original.LimitModel?.NamespaceName.ToString());
                EditorGUILayout.TextField("LimitName", original.LimitModel?.LimitName.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}