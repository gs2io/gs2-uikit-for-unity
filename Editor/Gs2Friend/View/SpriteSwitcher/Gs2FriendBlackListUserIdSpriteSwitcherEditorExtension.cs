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

using Gs2.Unity.Gs2Friend.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Friend.Context;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Friend.SpriteSwitcher.Editor
{
    [CustomEditor(typeof(Gs2FriendBlackListUserIdSpriteSwitcher))]
    public class Gs2FriendBlackListUserIdSpriteSwitcherEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2FriendBlackListUserIdSpriteSwitcher;

            if (original == null) return;

            var context = original.GetComponent<Gs2FriendOwnBlackListContext>() ?? original.GetComponentInParent<Gs2FriendOwnBlackListContext>(true);
            if (context == null) {
                EditorGUILayout.HelpBox("Gs2FriendOwnBlackListContext not found.", MessageType.Error);
                if (GUILayout.Button("Add Context")) {
                    original.gameObject.AddComponent<Gs2FriendOwnBlackListContext>();
                }
            }
            else {
                if (context.gameObject.GetComponentInParent<Gs2FriendOwnBlackListList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2FriendOwnBlackListContext), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("BlackList is auto assign from Gs2FriendOwnBlackListList.", MessageType.Info);
                }
                else {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Context", context.gameObject, typeof(Gs2FriendOwnBlackListContext), false);
                    EditorGUI.indentLevel++;
                    context.BlackList = EditorGUILayout.ObjectField("BlackList", context.BlackList, typeof(OwnBlackList), false) as OwnBlackList;
                    if (context.BlackList != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.BlackList?.NamespaceName?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("expression"), true);

            if (original.expression == Gs2FriendBlackListUserIdSpriteSwitcher.Expression.In || original.expression == Gs2FriendBlackListUserIdSpriteSwitcher.Expression.NotIn) {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("applyUserIds"), true);
            } else {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("applyUserId"), true);
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty("sprite"), true);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdate"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}