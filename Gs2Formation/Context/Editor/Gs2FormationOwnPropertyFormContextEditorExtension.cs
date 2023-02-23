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
    [CustomEditor(typeof(Gs2FormationOwnPropertyFormContext))]
    public class Gs2FormationOwnPropertyFormContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2FormationOwnPropertyFormContext;

            if (original == null) return;

            serializedObject.Update();

            if (original.PropertyForm == null) {
                if (original.transform.parent.GetComponent<Gs2FormationOwnPropertyFormList>() != null) {
                    EditorGUILayout.HelpBox("OwnPropertyForm is auto assign from Gs2FormationOwnPropertyFormList.", MessageType.Info);
                }
                else {
                    EditorGUILayout.HelpBox("OwnPropertyForm not assigned.", MessageType.Error);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("PropertyForm"), true);
                }
            }
            else {
                EditorGUILayout.ObjectField("OwnPropertyForm", original.PropertyForm, typeof(OwnPropertyForm), false);
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", original.PropertyForm?.NamespaceName.ToString());
                EditorGUILayout.TextField("FormModelName", original.PropertyForm?.FormModelName.ToString());
                EditorGUILayout.TextField("PropertyId", original.PropertyForm?.PropertyId.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}