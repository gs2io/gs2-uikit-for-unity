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
// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable CheckNamespace
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantAssignment
// ReSharper disable NotAccessedVariable
// ReSharper disable RedundantUsingDirective
// ReSharper disable Unity.NoNullPropagation
// ReSharper disable InconsistentNaming

#pragma warning disable CS0472

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gs2.Core.Exception;
using Gs2.Unity.Gs2Friend.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Friend.Context;
using UnityEngine;
using UnityEngine.Events;
using BlackList = Gs2.Unity.Gs2Friend.ScriptableObject.OwnBlackList;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Friend
{
    public partial class Gs2FriendBlackListRegisterBlackListAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);
            
            var domain = clientHolder.Gs2.Friend.Namespace(
                this._context.BlackList.NamespaceName
            ).Me(
                gameSessionHolder.GameSession
            ).BlackList(
            );
            var future = domain.RegisterBlackList(
                TargetUserId
            );
            yield return future;
            if (future.Error != null)
            {
                if (future.Error is TransactionException e)
                {
                    IEnumerator Retry()
                    {
                        var retryFuture = e.Retry();
                        yield return retryFuture;
                        if (retryFuture.Error != null)
                        {
                            this.onError.Invoke(future.Error, Retry);
                            yield break;
                        }
                        var future3 = future.Result.Model();
                        yield return future3;
                        if (future3.Error != null)
                        {
                            this.onError.Invoke(future3.Error, null);
                            yield break;
                        }

                        this.onRegisterBlackListComplete.Invoke(future3.Result);
                    }

                    this.onError.Invoke(future.Error, Retry);
                    yield break;
                }

                this.onError.Invoke(future.Error, null);
                yield break;
            }
            var future2 = future.Result.Model();
            yield return future2;
            if (future2.Error != null)
            {
                this.onError.Invoke(future2.Error, null);
                yield break;
            }

            this.onRegisterBlackListComplete.Invoke(future2.Result);
        }

        public void OnEnable()
        {
            StartCoroutine(nameof(Process));
        }

        public void OnDisable()
        {
            StopCoroutine(nameof(Process));
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2FriendBlackListRegisterBlackListAction
    {
        private Gs2FriendOwnBlackListContext _context;

        public void Awake()
        {
            this._context = GetComponent<Gs2FriendOwnBlackListContext>() ?? GetComponentInParent<Gs2FriendOwnBlackListContext>();
            if (this._context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2FriendOwnBlackListContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._context = GetComponent<Gs2FriendOwnBlackListContext>() ?? GetComponentInParent<Gs2FriendOwnBlackListContext>(true);
            if (this._context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2FriendBlackListRegisterBlackListAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2FriendBlackListRegisterBlackListAction
    {
        public bool WaitAsyncProcessComplete;
        public string TargetUserId;

        public void SetTargetUserId(string value) {
            this.TargetUserId = value;
            this.onChangeTargetUserId.Invoke(this.TargetUserId);
            this.OnChange.Invoke();
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FriendBlackListRegisterBlackListAction
    {

        [Serializable]
        private class ChangeTargetUserIdEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ChangeTargetUserIdEvent onChangeTargetUserId = new ChangeTargetUserIdEvent();
        public event UnityAction<string> OnChangeTargetUserId
        {
            add => this.onChangeTargetUserId.AddListener(value);
            remove => this.onChangeTargetUserId.RemoveListener(value);
        }

        [Serializable]
        private class RegisterBlackListCompleteEvent : UnityEvent<EzBlackList>
        {

        }

        [SerializeField]
        private RegisterBlackListCompleteEvent onRegisterBlackListComplete = new RegisterBlackListCompleteEvent();
        public event UnityAction<EzBlackList> OnRegisterBlackListComplete
        {
            add => this.onRegisterBlackListComplete.AddListener(value);
            remove => this.onRegisterBlackListComplete.RemoveListener(value);
        }

        public UnityEvent OnChange = new UnityEvent();

        [SerializeField]
        internal ErrorEvent onError = new ErrorEvent();

        public event UnityAction<Gs2Exception, Func<IEnumerator>> OnError
        {
            add => this.onError.AddListener(value);
            remove => this.onError.RemoveListener(value);
        }
    }

#if UNITY_EDITOR

    /// <summary>
    /// Context Menu
    /// </summary>
    public partial class Gs2FriendBlackListRegisterBlackListAction
    {
        [MenuItem("GameObject/Game Server Services/Friend/BlackList/Action/RegisterBlackList", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2FriendBlackListRegisterBlackListAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Friend/Prefabs/Action/Gs2FriendBlackListRegisterBlackListAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}