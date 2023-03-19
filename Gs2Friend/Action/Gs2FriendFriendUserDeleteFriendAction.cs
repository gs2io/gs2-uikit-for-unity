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
using FriendUser = Gs2.Unity.Gs2Friend.ScriptableObject.OwnFriendUser;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Friend
{
	[AddComponentMenu("GS2 UIKit/Friend/FriendUser/Action/Gs2FriendFriendUserDeleteFriendAction")]
    public partial class Gs2FriendFriendUserDeleteFriendAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            yield return new WaitUntil(() => this._clientHolder.Initialized);
            yield return new WaitUntil(() => this._gameSessionHolder.Initialized);
            
            var domain = this._clientHolder.Gs2.Friend.Namespace(
                this._context.FriendUser.NamespaceName
            ).Me(
                this._gameSessionHolder.GameSession
            ).Friend(
                this._context.FriendUser.WithProfile
            ).FriendUser(
                this._context.FriendUser.TargetUserId
            );
            var future = domain.DeleteFriend(
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

                        this.onDeleteFriendComplete.Invoke(future3.Result);
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

            this.onDeleteFriendComplete.Invoke(future2.Result);
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

    public partial class Gs2FriendFriendUserDeleteFriendAction
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2FriendOwnFriendUserContext _context;

        public void Awake()
        {
            this._clientHolder = Gs2ClientHolder.Instance;
            this._gameSessionHolder = Gs2GameSessionHolder.Instance;
            this._context = GetComponentInParent<Gs2FriendOwnFriendUserContext>();

            if (_context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2FriendOwnFriendUserContext.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2FriendFriendUserDeleteFriendAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2FriendFriendUserDeleteFriendAction
    {
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FriendFriendUserDeleteFriendAction
    {

        [Serializable]
        private class DeleteFriendCompleteEvent : UnityEvent<EzFriendUser>
        {

        }

        [SerializeField]
        private DeleteFriendCompleteEvent onDeleteFriendComplete = new DeleteFriendCompleteEvent();
        public event UnityAction<EzFriendUser> OnDeleteFriendComplete
        {
            add => this.onDeleteFriendComplete.AddListener(value);
            remove => this.onDeleteFriendComplete.RemoveListener(value);
        }

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
    public partial class Gs2FriendFriendUserDeleteFriendAction
    {
        [MenuItem("GameObject/Game Server Services/Friend/FriendUser/Action/DeleteFriend", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2FriendFriendUserDeleteFriendAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Friend/Prefabs/Action/Gs2FriendFriendUserDeleteFriendAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}