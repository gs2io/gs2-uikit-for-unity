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
    [CustomEditor(typeof(Gs2FormationOwnMoldContext))]
    public class Gs2FormationOwnMoldContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2FormationOwnMoldContext;

            if (original == null) return;

            serializedObject.Update();

            if (original.Mold == null) {
                if (original.transform.parent.GetComponent<Gs2FormationOwnMoldList>() != null) {
                    EditorGUILayout.HelpBox("OwnMold is auto assign from Gs2FormationOwnMoldList.", MessageType.Info);
                }
                else {
                    EditorGUILayout.HelpBox("OwnMold not assigned.", MessageType.Error);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("Mold"), true);
                }
            }
            else {
                original.Mold = EditorGUILayout.ObjectField("OwnMold", original.Mold, typeof(OwnMold), false) as OwnMold;
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", original.Mold?.NamespaceName.ToString());
                EditorGUILayout.TextField("MoldName", original.Mold?.MoldName.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}