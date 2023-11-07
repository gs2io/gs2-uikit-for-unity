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
    public partial class Gs2FriendBlackListUnregisterBlackListAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);

            this.onUnregisterBlackListStart.Invoke();

            
            var domain = clientHolder.Gs2.Friend.Namespace(
                this._context.BlackList.NamespaceName
            ).Me(
                gameSessionHolder.GameSession
            ).BlackList(
            );
            var future = domain.UnregisterBlackListFuture(
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
                        var future3 = future.Result.ModelFuture();
                        yield return future3;
                        if (future3.Error != null)
                        {
                            this.onError.Invoke(future3.Error, null);
                            yield break;
                        }

                        this.onUnregisterBlackListComplete.Invoke(future3.Result);
                    }

                    this.onError.Invoke(future.Error, Retry);
                    yield break;
                }

                this.onError.Invoke(future.Error, null);
                yield break;
            }
            var future2 = future.Result.ModelFuture();
            yield return future2;
            if (future2.Error != null)
            {
                this.onError.Invoke(future2.Error, null);
                yield break;
            }

            this.onUnregisterBlackListComplete.Invoke(future2.Result);
        }

        public void OnEnable()
        {
            Gs2ClientHolder.Instance.StartCoroutine(Process());
        }

        public void OnDisable()
        {

        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2FriendBlackListUnregisterBlackListAction
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

    public partial class Gs2FriendBlackListUnregisterBlackListAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2FriendBlackListUnregisterBlackListAction
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
    public partial class Gs2FriendBlackListUnregisterBlackListAction
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
        private class UnregisterBlackListStartEvent : UnityEvent
        {

        }

        [SerializeField]
        private UnregisterBlackListStartEvent onUnregisterBlackListStart = new UnregisterBlackListStartEvent();

        public event UnityAction OnUnregisterBlackListStart
        {
            add => this.onUnregisterBlackListStart.AddListener(value);
            remove => this.onUnregisterBlackListStart.RemoveListener(value);
        }

        [Serializable]
        private class UnregisterBlackListCompleteEvent : UnityEvent<EzBlackList>
        {

        }

        [SerializeField]
        private UnregisterBlackListCompleteEvent onUnregisterBlackListComplete = new UnregisterBlackListCompleteEvent();
        public event UnityAction<EzBlackList> OnUnregisterBlackListComplete
        {
            add => this.onUnregisterBlackListComplete.AddListener(value);
            remove => this.onUnregisterBlackListComplete.RemoveListener(value);
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
    public partial class Gs2FriendBlackListUnregisterBlackListAction
    {
        [MenuItem("GameObject/Game Server Services/Friend/BlackList/Action/UnregisterBlackList", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2FriendBlackListUnregisterBlackListAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Friend/Prefabs/Action/Gs2FriendBlackListUnregisterBlackListAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}