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

using Gs2.Unity.Gs2Lottery.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Lottery.Context;
using Gs2.Unity.UiKit.Gs2Lottery.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Lottery.Editor
{
    [CustomEditor(typeof(Gs2LotteryLotteryModelContext))]
    public class Gs2LotteryLotteryModelContextEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2LotteryLotteryModelContext;

            if (original == null) return;

            if (original.LotteryModel == null) {
                if (original.transform.parent.GetComponent<Gs2LotteryLotteryModelList>() != null) {
                    EditorGUILayout.HelpBox("LotteryModel is auto assign from Gs2LotteryLotteryModelList.", MessageType.Info);
                }
                else {
                    EditorGUILayout.HelpBox("LotteryModel not assigned.", MessageType.Error);
                    EditorGUILayout.ObjectField("LotteryModel", original.LotteryModel, typeof(LotteryModel), false);
                }
            }
            else {
                EditorGUILayout.ObjectField("LotteryModel", original.LotteryModel, typeof(LotteryModel), false);
                EditorGUI.BeginDisabledGroup(true);
                EditorGUI.indentLevel++;
                EditorGUILayout.TextField("NamespaceName", original.LotteryModel?.NamespaceName.ToString());
                EditorGUILayout.TextField("LotteryName", original.LotteryModel?.LotteryName.ToString());
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }
            
            serializedObject.Update();
            serializedObject.ApplyModifiedProperties();
        }
    }
}