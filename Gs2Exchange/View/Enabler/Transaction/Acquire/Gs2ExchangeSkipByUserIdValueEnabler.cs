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
using System.Collections.Generic;
using Gs2.Gs2Exchange.Request;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Exchange.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Exchange
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Exchange/Await/View/Enabler/Transaction/Gs2ExchangeSkipByUserIdValueEnabler")]
    public partial class Gs2ExchangeSkipByUserIdValueEnabler : MonoBehaviour
    {
        private void OnFetched()
        {
            if (!this._fetcher.Fetched || this._fetcher.Request == null) {
                return;
            }
            switch(this.expression)
            {
                case Expression.In:
                    this.target.SetActive(this._fetcher.Request.Minutes != null && this.enableMinuteses.Contains(this._fetcher.Request.Minutes.Value));
                    break;
                case Expression.NotIn:
                    this.target.SetActive(this._fetcher.Request.Minutes != null && !this.enableMinuteses.Contains(this._fetcher.Request.Minutes.Value));
                    break;
                case Expression.Less:
                    this.target.SetActive(this.enableMinutes > this._fetcher.Request.Minutes);
                    break;
                case Expression.LessEqual:
                    this.target.SetActive(this.enableMinutes >= this._fetcher.Request.Minutes);
                    break;
                case Expression.Greater:
                    this.target.SetActive(this.enableMinutes < this._fetcher.Request.Minutes);
                    break;
                case Expression.GreaterEqual:
                    this.target.SetActive(this.enableMinutes <= this._fetcher.Request.Minutes);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2ExchangeSkipByUserIdValueEnabler
    {
        private Gs2ExchangeSkipByUserIdFetcher _fetcher;

        public void Awake()
        {
            this._fetcher = GetComponent<Gs2ExchangeSkipByUserIdFetcher>() ?? GetComponentInParent<Gs2ExchangeSkipByUserIdFetcher>();
            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ExchangeSkipByUserIdFetcher.");
                enabled = false;
            }
            if (this.target == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: target is not set.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._fetcher = GetComponent<Gs2ExchangeSkipByUserIdFetcher>() ?? GetComponentInParent<Gs2ExchangeSkipByUserIdFetcher>(true);
            if (this._fetcher == null) {
                return true;
            }
            if (this.target == null) {
                return true;
            }
            return false;
        }

        private UnityAction _onFetched;

        public void OnEnable()
        {
            this._onFetched = () =>
            {
                OnFetched();
            };
            this._fetcher.OnFetched.AddListener(this._onFetched);

            if (this._fetcher.Fetched) {
                OnFetched();
            }
        }

        public void OnDisable()
        {
            if (this._onFetched != null) {
                this._fetcher.OnFetched.RemoveListener(this._onFetched);
                this._onFetched = null;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2ExchangeSkipByUserIdValueEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2ExchangeSkipByUserIdValueEnabler
    {
        public enum Expression {
            In,
            NotIn,
            Less,
            LessEqual,
            Greater,
            GreaterEqual,
        }

        public Expression expression;

        public List<int> enableMinuteses;

        public int enableMinutes;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ExchangeSkipByUserIdValueEnabler
    {

    }
}