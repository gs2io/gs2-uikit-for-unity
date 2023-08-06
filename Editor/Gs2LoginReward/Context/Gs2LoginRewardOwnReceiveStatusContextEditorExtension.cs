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

using Gs2.Unity.Gs2LoginReward.ScriptableObject;
using Gs2.Unity.UiKit.Gs2LoginReward.Context;
using Gs2.Unity.UiKit.Gs2LoginReward.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2LoginReward.Editor
{
    [CustomEditor(typeof(Gs2LoginRewardOwnReceiveStatusContext))]
    public class Gs2LoginRewardOwnReceiveStatusContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2LoginRewardOwnReceiveStatusContext;

            if (original == null) return;

            serializedObject.Update();

            if (original.ReceiveStatus == null) {
                if (original.GetComponentInParent<Gs2LoginRewardOwnReceiveStatusList>(true) != null) {
                    EditorGUILayout.HelpBox("OwnReceiveStatus is auto assign from Gs2LoginRewardOwnReceiveStatusList.", MessageType.Info);
                }
                else {
                    EditorGUILayout.HelpBox("OwnReceiveStatus not assigned.", MessageType.Error);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("ReceiveStatus"), true);
                }
            }
            else {
                original.ReceiveStatus = EditorGUILayout.ObjectField("OwnReceiveStatus", original.ReceiveStatus, typeof(OwnReceiveStatus), false) as OwnReceiveStatus;
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", original.ReceiveStatus?.NamespaceName?.ToString());
                EditorGUILayout.TextField("BonusModelName", original.ReceiveStatus?.BonusModelName?.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}