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

using Gs2.Unity.Gs2Stamina.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Stamina.Editor
{
    [CustomEditor(typeof(Gs2StaminaConsumeStaminaByUserIdEnabler))]
    public class Gs2StaminaConsumeStaminaByUserIdEnablerEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2StaminaConsumeStaminaByUserIdEnabler;

            if (original == null) return;

            var context = original.GetComponent<Gs2CoreConsumeActionFetcher>() ?? original.GetComponentInParent<Gs2CoreConsumeActionFetcher>();
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2CoreConsumeActionFetcher not found.", MessageType.Error);
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2StaminaConsumeStaminaByUserIdEnabler.Expression.In || original.expression == Gs2StaminaConsumeStaminaByUserIdEnabler.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableConsumeValues"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("enableConsumeValue"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("target"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}