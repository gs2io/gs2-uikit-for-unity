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
using Gs2.Core.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Exchange.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Exchange
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Exchange/Await/View/Label/Gs2ExchangeOwnAwaitLabel")]
    public partial class Gs2ExchangeOwnAwaitLabel : MonoBehaviour
    {
        private void OnFetched()
        {
            var exchangedAt = this._fetcher.Await.ExchangedAt == null ? DateTime.Now : UnixTime.FromUnixTime(_fetcher.Await.ExchangedAt).ToLocalTime();
            this.onUpdate?.Invoke(
                this.format.Replace(
                    "{userId}", $"{this._fetcher?.Await?.UserId}"
                ).Replace(
                    "{rateName}", $"{this._fetcher?.Await?.RateName}"
                ).Replace(
                    "{name}", $"{this._fetcher?.Await?.Name}"
                ).Replace(
                    "{config}", $"{this._fetcher?.Await?.Config}"
                ).Replace(
                    "{exchangedAt:yyyy}", exchangedAt.ToString("yyyy")
                ).Replace(
                    "{exchangedAt:yy}", exchangedAt.ToString("yy")
                ).Replace(
                    "{exchangedAt:MM}", exchangedAt.ToString("MM")
                ).Replace(
                    "{exchangedAt:MMM}", exchangedAt.ToString("MMM")
                ).Replace(
                    "{exchangedAt:dd}", exchangedAt.ToString("dd")
                ).Replace(
                    "{exchangedAt:hh}", exchangedAt.ToString("hh")
                ).Replace(
                    "{exchangedAt:HH}", exchangedAt.ToString("HH")
                ).Replace(
                    "{exchangedAt:tt}", exchangedAt.ToString("tt")
                ).Replace(
                    "{exchangedAt:mm}", exchangedAt.ToString("mm")
                ).Replace(
                    "{exchangedAt:ss}", exchangedAt.ToString("ss")
                )
            );
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2ExchangeOwnAwaitLabel
    {
        private Gs2ExchangeOwnAwaitFetcher _fetcher;

        public void Awake()
        {
            this._fetcher = GetComponent<Gs2ExchangeOwnAwaitFetcher>() ?? GetComponentInParent<Gs2ExchangeOwnAwaitFetcher>();
            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ExchangeOwnAwaitFetcher.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._fetcher = GetComponent<Gs2ExchangeOwnAwaitFetcher>() ?? GetComponentInParent<Gs2ExchangeOwnAwaitFetcher>(true);
            if (this._fetcher == null) {
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

    public partial class Gs2ExchangeOwnAwaitLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2ExchangeOwnAwaitLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ExchangeOwnAwaitLabel
    {
        [Serializable]
        private class UpdateEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private UpdateEvent onUpdate = new UpdateEvent();

        public event UnityAction<string> OnUpdate
        {
            add => this.onUpdate.AddListener(value);
            remove => this.onUpdate.RemoveListener(value);
        }
    }
}