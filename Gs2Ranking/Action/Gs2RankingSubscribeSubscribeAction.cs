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
using Gs2.Unity.Gs2Ranking.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Ranking.Context;
using UnityEngine;
using UnityEngine.Events;
using Subscribe = Gs2.Unity.Gs2Ranking.ScriptableObject.OwnSubscribe;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Ranking
{
	[AddComponentMenu("GS2 UIKit/Ranking/Subscribe/Action/Gs2RankingSubscribeSubscribeAction")]
    public partial class Gs2RankingSubscribeSubscribeAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            yield return new WaitUntil(() => this._clientHolder.Initialized);
            yield return new WaitUntil(() => this._gameSessionHolder.Initialized);
            
            var domain = this._clientHolder.Gs2.Ranking.Namespace(
                this._context.Subscribe.NamespaceName
            ).Me(
                this._gameSessionHolder.GameSession
            );
            var future = domain.Subscribe(
                this._context.Subscribe.CategoryName,
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

                        this.onSubscribeComplete.Invoke(future3.Result);
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

            this.onSubscribeComplete.Invoke(future2.Result);
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

    public partial class Gs2RankingSubscribeSubscribeAction
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2RankingOwnSubscribeContext _context;

        public void Awake()
        {
            this._clientHolder = Gs2ClientHolder.Instance;
            this._gameSessionHolder = Gs2GameSessionHolder.Instance;
            this._context = GetComponentInParent<Gs2RankingOwnSubscribeContext>();

            if (_context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2RankingOwnSubscribeContext.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2RankingSubscribeSubscribeAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2RankingSubscribeSubscribeAction
    {
        public string TargetUserId;

        public void SetTargetUserId(string value) {
            TargetUserId = value;
            this.onChangeTargetUserId.Invoke(TargetUserId);
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2RankingSubscribeSubscribeAction
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
        private class SubscribeCompleteEvent : UnityEvent<EzSubscribeUser>
        {

        }

        [SerializeField]
        private SubscribeCompleteEvent onSubscribeComplete = new SubscribeCompleteEvent();
        public event UnityAction<EzSubscribeUser> OnSubscribeComplete
        {
            add => this.onSubscribeComplete.AddListener(value);
            remove => this.onSubscribeComplete.RemoveListener(value);
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
    public partial class Gs2RankingSubscribeSubscribeAction
    {
        [MenuItem("GameObject/Game Server Services/Ranking/Subscribe/Action/Subscribe", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2RankingSubscribeSubscribeAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Ranking/Prefabs/Action/Gs2RankingSubscribeSubscribeAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}