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

using Gs2.Unity.Gs2Showcase.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Showcase.Context;
using Gs2.Unity.UiKit.Gs2Showcase.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Showcase.Editor
{
    [CustomEditor(typeof(Gs2ShowcaseOwnRandomDisplayItemContext))]
    public class Gs2ShowcaseOwnRandomDisplayItemContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2ShowcaseOwnRandomDisplayItemContext;

            if (original == null) return;

            serializedObject.Update();

            if (original.ownRandomDisplayItem == null) {
                EditorGUILayout.HelpBox("RandomDisplayItem not assigned.", MessageType.Error);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("RandomDisplayItem"), true);
            }
            else {
                original.ownRandomDisplayItem = EditorGUILayout.ObjectField("RandomDisplayItem", original.ownRandomDisplayItem, typeof(OwnRandomDisplayItem), false) as OwnRandomDisplayItem;
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", original.ownRandomDisplayItem?.NamespaceName.ToString());
                EditorGUILayout.TextField("ShowcaseName", original.ownRandomDisplayItem?.ShowcaseName.ToString());
                EditorGUILayout.TextField("DisplayItemName", original.ownRandomDisplayItem?.DisplayItemName.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}