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

using Gs2.Unity.Gs2MegaField.ScriptableObject;
using Gs2.Unity.UiKit.Gs2MegaField.Context;
using Gs2.Unity.UiKit.Gs2MegaField.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2MegaField.Editor
{
    [CustomEditor(typeof(Gs2MegaFieldAreaModelContext))]
    public class Gs2MegaFieldAreaModelContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2MegaFieldAreaModelContext;

            if (original == null) return;

            if (original.AreaModel == null) {
                if (original.transform.parent.GetComponent<Gs2MegaFieldAreaModelList>() != null) {
                    EditorGUILayout.HelpBox("AreaModel is auto assign from Gs2MegaFieldAreaModelList.", MessageType.Info);
                }
                else {
                    EditorGUILayout.HelpBox("AreaModel not assigned.", MessageType.Error);
                    EditorGUILayout.ObjectField("AreaModel", original.AreaModel, typeof(AreaModel), false);
                }
            }
            else {
                EditorGUILayout.ObjectField("AreaModel", original.AreaModel, typeof(AreaModel), false);
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", original.AreaModel?.NamespaceName.ToString());
                EditorGUILayout.TextField("AreaModelName", original.AreaModel?.AreaModelName.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }
            
            serializedObject.Update();
            serializedObject.ApplyModifiedProperties();
        }
    }
}