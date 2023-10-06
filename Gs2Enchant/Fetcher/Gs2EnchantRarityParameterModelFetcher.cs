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
using Gs2.Unity.Gs2Enchant.Domain.Model;
using Gs2.Unity.Gs2Enchant.Model;
using Gs2.Unity.Gs2Enchant.ScriptableObject;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Core.Model;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Enchant.Context;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Enchant.Fetcher
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Enchant/RarityParameterModel/Fetcher/Gs2EnchantRarityParameterModelFetcher")]
    public partial class Gs2EnchantRarityParameterModelFetcher : MonoBehaviour
    {
        private EzRarityParameterModelDomain _domain;
        private ulong? _callbackId;

        private IEnumerator Fetch()
        {
            var retryWaitSecond = 1;
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);
            yield return new WaitUntil(() => Context != null && this.Context.RarityParameterModel != null);

            this._domain = clientHolder.Gs2.Enchant.Namespace(
                this.Context.RarityParameterModel.NamespaceName
            ).RarityParameterModel(
                this.Context.RarityParameterModel.ParameterName
            );;
            this._callbackId = this._domain.Subscribe(
                item =>
                {
                    RarityParameterModel = item;
                    Fetched = true;
                    this.OnFetched.Invoke();
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
                    RarityParameterModel = future.Result;
                    Fetched = true;
                    this.OnFetched.Invoke();
                    break;
                }
            }
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

        public void SetTemporaryRarityParameterModel(
            Gs2.Unity.Gs2Enchant.Model.EzRarityParameterModel rarityParameterModel
        ) {
            RarityParameterModel = rarityParameterModel;
            this.OnFetched.Invoke();
        }

        public void RollbackTemporaryRarityParameterModel(
        ) {
            OnUpdateContext();
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2EnchantRarityParameterModelFetcher
    {
        public Gs2EnchantRarityParameterModelContext Context { get; private set; }

        public void Awake()
        {
            Context = GetComponent<Gs2EnchantRarityParameterModelContext>() ?? GetComponentInParent<Gs2EnchantRarityParameterModelContext>();
            if (Context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2EnchantRarityParameterModelContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            Context = GetComponent<Gs2EnchantRarityParameterModelContext>() ?? GetComponentInParent<Gs2EnchantRarityParameterModelContext>(true);
            if (Context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2EnchantRarityParameterModelFetcher
    {
        public Gs2.Unity.Gs2Enchant.Model.EzRarityParameterModel RarityParameterModel { get; protected set; }
        public bool Fetched { get; protected set; }
        public UnityEvent OnFetched = new UnityEvent();
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2EnchantRarityParameterModelFetcher
    {

    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2EnchantRarityParameterModelFetcher
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