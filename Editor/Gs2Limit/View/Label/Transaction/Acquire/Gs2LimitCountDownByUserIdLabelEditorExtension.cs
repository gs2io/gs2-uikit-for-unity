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

using Gs2.Unity.UiKit.Gs2Limit.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Limit.Label.Editor
{
    [CustomEditor(typeof(Gs2LimitCountDownByUserIdLabel))]
    public class Gs2LimitCountDownByUserIdLabelEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2LimitCountDownByUserIdLabel;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2LimitCountDownByUserIdFetcher>() ?? original.GetComponentInParent<Gs2LimitCountDownByUserIdFetcher>(true);
             var userDataFetcher = original.GetComponent<Gs2LimitOwnCounterFetcher>() ?? original.GetComponentInParent<Gs2LimitOwnCounterFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2LimitCountDownByUserIdFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2LimitCountDownByUserIdFetcher>();
                }
            }
            if (userDataFetcher == null) {
                EditorGUILayout.HelpBox("Gs2LimitOwnCounterFetcher not found. Adding a Fetcher allows more values to be used.", MessageType.Warning);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2LimitOwnCounterFetcher>();
                }
            }

            serializedObject.Update();
            original.format = EditorGUILayout.TextField("Format", original.format);

            GUILayout.Label("Add Format Parameter");
            if (GUILayout.Button("NamespaceName")) {
                original.format += "{namespaceName}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("LimitName")) {
                original.format += "{limitName}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("CounterName")) {
                original.format += "{counterName}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("UserId")) {
                original.format += "{userId}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("CountDownValue")) {
                original.format += "{countDownValue}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (userDataFetcher != null) {
                if (GUILayout.Button("UserData:CounterId")) {
                    original.format += "{userData:counterId}";
                    GUI.FocusControl("");
                    EditorUtility.SetDirty(original);
                }
                if (GUILayout.Button("UserData:LimitName")) {
                    original.format += "{userData:limitName}";
                    GUI.FocusControl("");
                    EditorUtility.SetDirty(original);
                }
                if (GUILayout.Button("UserData:Name")) {
                    original.format += "{userData:name}";
                    GUI.FocusControl("");
                    EditorUtility.SetDirty(original);
                }
                if (GUILayout.Button("UserData:Count")) {
                    original.format += "{userData:count}";
                    GUI.FocusControl("");
                    EditorUtility.SetDirty(original);
                }
                if (GUILayout.Button("UserData:Count:Changed")) {
                    original.format += "{userData:count:changed}";
                    GUI.FocusControl("");
                    EditorUtility.SetDirty(original);
                }
                if (GUILayout.Button("UserData:CreatedAt")) {
                    original.format += "{userData:createdAt}";
                    GUI.FocusControl("");
                    EditorUtility.SetDirty(original);
                }
                if (GUILayout.Button("UserData:UpdatedAt")) {
                    original.format += "{userData:updatedAt}";
                    GUI.FocusControl("");
                    EditorUtility.SetDirty(original);
                }
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdate"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}