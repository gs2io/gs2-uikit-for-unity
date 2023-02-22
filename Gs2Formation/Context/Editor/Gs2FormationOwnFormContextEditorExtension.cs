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

using Gs2.Unity.Gs2Formation.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Formation.Context;
using Gs2.Unity.UiKit.Gs2Formation.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Formation.Editor
{
    [CustomEditor(typeof(Gs2FormationOwnFormContext))]
    public class Gs2FormationOwnFormContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2FormationOwnFormContext;

            if (original == null) return;

            if (original.Form == null) {
                if (original.transform.parent.GetComponent<Gs2FormationOwnFormList>() != null) {
                    EditorGUILayout.HelpBox("OwnForm is auto assign from Gs2FormationOwnFormList.", MessageType.Info);
                }
                else {
                    EditorGUILayout.HelpBox("OwnForm not assigned.", MessageType.Error);
                    EditorGUILayout.ObjectField("OwnForm", original.Form, typeof(OwnForm), false);
                }
            }
            else {
                EditorGUILayout.ObjectField("OwnForm", original.Form, typeof(OwnForm), false);
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", original.Form?.NamespaceName.ToString());
                EditorGUILayout.TextField("MoldName", original.Form?.MoldName.ToString());
                EditorGUILayout.TextField("Index", original.Form?.Index.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }
            
            serializedObject.Update();
            serializedObject.ApplyModifiedProperties();
        }
    }
}