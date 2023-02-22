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

using Gs2.Unity.Gs2SerialKey.ScriptableObject;
using Gs2.Unity.UiKit.Gs2SerialKey.Context;
using Gs2.Unity.UiKit.Gs2SerialKey.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2SerialKey.Editor
{
    [CustomEditor(typeof(Gs2SerialKeyCampaignModelContext))]
    public class Gs2SerialKeyCampaignModelContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2SerialKeyCampaignModelContext;

            if (original == null) return;

            if (original.CampaignModel == null) {
                EditorGUILayout.HelpBox("CampaignModel not assigned.", MessageType.Error);
                EditorGUILayout.ObjectField("CampaignModel", original.CampaignModel, typeof(CampaignModel), false);
            }
            else {
                EditorGUILayout.ObjectField("CampaignModel", original.CampaignModel, typeof(CampaignModel), false);
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", original.CampaignModel?.NamespaceName.ToString());
                EditorGUILayout.TextField("CampaignModelName", original.CampaignModel?.CampaignModelName.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }
            
            serializedObject.Update();
            serializedObject.ApplyModifiedProperties();
        }
    }
}