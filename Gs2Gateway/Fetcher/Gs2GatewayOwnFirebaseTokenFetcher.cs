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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Gs2.Core.Exception;
using Gs2.Unity.Core.Exception;
using Gs2.Unity.Gs2Gateway.Domain.Model;
using Gs2.Unity.Gs2Gateway.Model;
using Gs2.Unity.Gs2Gateway.ScriptableObject;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Core.Model;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Gateway.Context;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Gateway.Fetcher
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Gateway/FirebaseToken/Fetcher/Gs2GatewayOwnFirebaseTokenFetcher")]
    public partial class Gs2GatewayOwnFirebaseTokenFetcher : MonoBehaviour
    {
        private EzFirebaseTokenGameSessionDomain _domain;
        private ulong? _callbackId;

        private IEnumerator Fetch()
        {
            var retryWaitSecond = 1;
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);
            yield return new WaitUntil(() => Context != null && this.Context.FirebaseToken != null);

            this._domain = clientHolder.Gs2.Gateway.Namespace(
                this.Context.FirebaseToken.NamespaceName
            ).Me(
                gameSessionHolder.GameSession
            ).FirebaseToken(
            );;
            var future = this._domain.SubscribeWithInitialCallFuture(
                item =>
                {
                    retryWaitSecond = 0;
                    FirebaseToken = item;
                    Fetched = true;
                    this.OnFetched.Invoke();
                }
            );
            yield return future;
            if (future.Error != null) {
                this.onError.Invoke(future.Error, null);
                yield break;
            }
            this._callbackId = future.Result;
        }

        public void OnUpdateContext() {
            OnDisable();
            Awake();
            OnEnable();
        }

        public void OnEnable()
        {
            StartCoroutine(nameof(Fetch));
            Context.OnUpdate.AddListener(OnUpdateContext);
        }

        public void OnDisable()
        {
            Context.OnUpdate.RemoveListener(OnUpdateContext);

            if (this._domain == null) {
                return;
            }
            if (!this._callbackId.HasValue) {
                return;
            }
            this._domain.Unsubscribe(
                this._callbackId.Value
            );
            this._callbackId = null;
        }

        public void SetTemporaryFirebaseToken(
            Gs2.Unity.Gs2Gateway.Model.EzFirebaseToken firebaseToken
        ) {
            FirebaseToken = firebaseToken;
            this.OnFetched.Invoke();
        }

        public void RollbackTemporaryFirebaseToken(
        ) {
            OnUpdateContext();
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2GatewayOwnFirebaseTokenFetcher
    {
        public Gs2GatewayOwnFirebaseTokenContext Context { get; private set; }

        public void Awake()
        {
            Context = GetComponent<Gs2GatewayOwnFirebaseTokenContext>() ?? GetComponentInParent<Gs2GatewayOwnFirebaseTokenContext>();
            if (Context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2GatewayOwnFirebaseTokenContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            Context = GetComponent<Gs2GatewayOwnFirebaseTokenContext>() ?? GetComponentInParent<Gs2GatewayOwnFirebaseTokenContext>(true);
            if (Context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2GatewayOwnFirebaseTokenFetcher
    {
        public Gs2.Unity.Gs2Gateway.Model.EzFirebaseToken FirebaseToken { get; protected set; }
        public bool Fetched { get; protected set; }
        public UnityEvent OnFetched = new UnityEvent();
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2GatewayOwnFirebaseTokenFetcher
    {

    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2GatewayOwnFirebaseTokenFetcher
    {
        [SerializeField]
        internal ErrorEvent onError = new ErrorEvent();

        public event UnityAction<Gs2Exception, Func<IEnumerator>> OnError
        {
            add => onError.AddListener(value);
            remove => onError.RemoveListener(value);
        }
    }
}