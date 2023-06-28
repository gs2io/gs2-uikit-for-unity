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
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantAssignment
// ReSharper disable NotAccessedVariable
// ReSharper disable RedundantUsingDirective
// ReSharper disable Unity.NoNullPropagation
// ReSharper disable InconsistentNaming

#pragma warning disable CS0472

using Gs2.Unity.Gs2Formation.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Formation.Context;
using Gs2.Unity.UiKit.Gs2Formation.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Formation.Editor
{
    [CustomEditor(typeof(Gs2FormationMoldModelContext))]
    public class Gs2FormationMoldModelContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2FormationMoldModelContext;

            if (original == null) return;

            serializedObject.Update();

            if (original.MoldModel == null) {
                if (original.transform.parent != null && original.transform.parent.GetComponent<Gs2FormationMoldModelList>() != null) {
                    EditorGUILayout.HelpBox("MoldModel is auto assign from Gs2FormationMoldModelList.", MessageType.Info);
                }
                else {
                    EditorGUILayout.HelpBox("MoldModel not assigned.", MessageType.Error);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("MoldModel"), true);
                }
            }
            else {
                original.MoldModel = EditorGUILayout.ObjectField("MoldModel", original.MoldModel, typeof(MoldModel), false) as MoldModel;
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", original.MoldModel?.NamespaceName.ToString());
                EditorGUILayout.TextField("MoldName", original.MoldModel?.MoldName.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}