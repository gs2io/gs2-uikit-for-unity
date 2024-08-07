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

using Gs2.Unity.Gs2Account.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Account.Context;
using Gs2.Unity.UiKit.Gs2Account.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Account.Editor
{
    [CustomEditor(typeof(Gs2AccountTakeOverDoTakeOverOpenIdConnectAction))]
    public class Gs2AccountTakeOverDoTakeOverOpenIdConnectActionEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2AccountTakeOverDoTakeOverOpenIdConnectAction;

            if (original == null) return;

            var context = original.GetComponent<Gs2AccountOwnTakeOverContext>() ?? original.GetComponentInParent<Gs2AccountOwnTakeOverContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2AccountOwnTakeOverContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2AccountOwnTakeOverContext>();
                }
            }
            else {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2AccountOwnTakeOverContext), false);
                EditorGUI.indentLevel++;
                context.TakeOver = EditorGUILayout.ObjectField("OwnTakeOver", context.TakeOver, typeof(OwnTakeOver), false) as OwnTakeOver;
                if (context.TakeOver != null) {
                    EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.TakeOver?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("Type", context.TakeOver?.Type.ToString());
                    EditorGUI.indentLevel--;
                }
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.Update();
            serializedObject.ApplyModifiedProperties();

            DrawDefaultInspector();
        }
    }
}