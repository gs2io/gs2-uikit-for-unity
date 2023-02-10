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
using Gs2.Unity.Gs2Exchange.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Gs2Exchange.Context;
using UnityEngine;
using UnityEngine.Events;
using Await = Gs2.Unity.Gs2Exchange.ScriptableObject.Await;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Exchange
{
	[AddComponentMenu("GS2 UIKit/Exchange/Await/Action/Gs2ExchangeAwaitSkipAction")]
    public partial class Gs2ExchangeAwaitSkipAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            yield return new WaitUntil(() => this._clientHolder.Initialized);
            yield return new WaitUntil(() => this._gameSessionHolder.Initialized);
            
            var domain = this._clientHolder.Gs2.Exchange.Namespace(
                this._context.Await_.NamespaceName
            ).Me(
                this._gameSessionHolder.GameSession
            ).Await(
                this._context.Await_.AwaitName
            );
            var future = domain.Skip(
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
                        this.onSkipComplete.Invoke(future.Result.TransactionId);
                    }

                    this.onError.Invoke(future.Error, Retry);
                    yield break;
                }

                this.onError.Invoke(future.Error, null);
                yield break;
            }
            this.onSkipComplete.Invoke(future.Result.TransactionId);
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

    public partial class Gs2ExchangeAwaitSkipAction
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2ExchangeAwaitContext _context;

        public void Awake()
        {
            this._clientHolder = Gs2ClientHolder.Instance;
            this._gameSessionHolder = Gs2GameSessionHolder.Instance;
            this._context = GetComponentInParent<Gs2ExchangeAwaitContext>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2ExchangeAwaitSkipAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2ExchangeAwaitSkipAction
    {
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ExchangeAwaitSkipAction
    {
        [Serializable]
        private class SkipCompleteEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private SkipCompleteEvent onSkipComplete = new SkipCompleteEvent();
        public event UnityAction<string> OnSkipComplete
        {
            add => this.onSkipComplete.AddListener(value);
            remove => this.onSkipComplete.RemoveListener(value);
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
    public partial class Gs2ExchangeAwaitSkipAction
    {
        [MenuItem("GameObject/Game Server Services/Exchange/Await/Action/Skip", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2ExchangeAwaitSkipAction>(
                "Assets/Scripts/Runtime/Sdk/Gs2/UiKit/Gs2Exchange/Prefabs/Action/Gs2ExchangeAwaitSkipAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}