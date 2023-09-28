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
using Gs2.Unity.Gs2Exchange.Domain.Model;
using Gs2.Unity.Gs2Exchange.Model;
using Gs2.Unity.Gs2Exchange.ScriptableObject;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Exchange.Context;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Exchange.Fetcher
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Exchange/IncrementalRateModel/Fetcher/Gs2ExchangeIncrementalRateModelFetcher")]
    public partial class Gs2ExchangeIncrementalRateModelFetcher : MonoBehaviour, IAcquireActionsFetcher
    {
        private EzIncrementalRateModelDomain _domain;
        private ulong? _callbackId;

        private IEnumerator Fetch()
        {
            var retryWaitSecond = 1;
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);
            yield return new WaitUntil(() => Context != null && this.Context.IncrementalRateModel != null);

            this._domain = clientHolder.Gs2.Exchange.Namespace(
                this.Context.IncrementalRateModel.NamespaceName
            ).IncrementalRateModel(
                this.Context.IncrementalRateModel.RateName
            );;
            this._callbackId = this._domain.Subscribe(
                item =>
                {
                    IncrementalRateModel = item;
                    Fetched = true;
                }
            );

            while (true) {
                var future = this._domain.Model();
                yield return future;
                if (future.Error != null) {
                    yield return new WaitForSeconds(retryWaitSecond);
                    retryWaitSecond *= 2;
                }
                else {
                    IncrementalRateModel = future.Result;
                    Fetched = true;
                    break;
                }
            }

            this.OnFetched.Invoke();
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

        public List<Unity.Core.Model.EzAcquireAction> AcquireActions(string context = "default") {
            if (!Fetched) {
                return new List<Unity.Core.Model.EzAcquireAction>();
            }
            return IncrementalRateModel.AcquireActions;
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2ExchangeIncrementalRateModelFetcher
    {
        public Gs2ExchangeIncrementalRateModelContext Context { get; private set; }

        public UnityEvent OnFetched = new UnityEvent();

        public void Awake()
        {
            Context = GetComponent<Gs2ExchangeIncrementalRateModelContext>() ?? GetComponentInParent<Gs2ExchangeIncrementalRateModelContext>();
            if (Context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ExchangeIncrementalRateModelContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            Context = GetComponent<Gs2ExchangeIncrementalRateModelContext>() ?? GetComponentInParent<Gs2ExchangeIncrementalRateModelContext>(true);
            if (Context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2ExchangeIncrementalRateModelFetcher
    {
        public Gs2.Unity.Gs2Exchange.Model.EzIncrementalRateModel IncrementalRateModel { get; protected set; }
        public bool Fetched { get; protected set; }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2ExchangeIncrementalRateModelFetcher
    {

    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ExchangeIncrementalRateModelFetcher
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