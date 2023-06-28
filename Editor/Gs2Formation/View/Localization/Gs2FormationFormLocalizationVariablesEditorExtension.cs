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

#if GS2_ENABLE_LOCALIZATION

using Gs2.Unity.Gs2Formation.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Formation.Context;
using Gs2.Unity.UiKit.Gs2Formation.Fetcher;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace Gs2.Unity.UiKit.Gs2Formation.Localization.Editor
{
    [CustomEditor(typeof(Gs2FormationFormLocalizationVariables))]
    public class Gs2FormationFormLocalizationVariablesEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2FormationFormLocalizationVariables;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2FormationOwnFormFetcher>() ?? original.GetComponentInParent<Gs2FormationOwnFormFetcher>();
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2FormationOwnFormFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2FormationOwnFormFetcher>();
                }
            }
            else {
                if (fetcher.transform.parent.GetComponent<Gs2FormationOwnFormList>() != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2FormationOwnFormFetcher), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("Form is auto assign from Gs2FormationOwnFormList.", MessageType.Info);
                }
                else {
                    var context = original.GetComponent<Gs2FormationOwnFormContext>() ?? original.GetComponentInParent<Gs2FormationOwnFormContext>();
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2FormationOwnFormFetcher), false);
                    EditorGUI.indentLevel++;
                    context.Form = EditorGUILayout.ObjectField("Form", context.Form, typeof(OwnForm), false) as OwnForm;
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.Form?.NamespaceName.ToString());
                    EditorGUILayout.TextField("MoldName", context.Form?.MoldName.ToString());
                    EditorGUILayout.TextField("Index", context.Form?.Index.ToString());
                    EditorGUI.indentLevel--;
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            original.target = EditorGUILayout.ObjectField("Target", original.target, typeof(LocalizeStringEvent), true) as LocalizeStringEvent;

            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif