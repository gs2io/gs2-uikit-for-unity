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

	[AddComponentMenu("GS2 UIKit/Exchange/Await/View/Label/Gs2ExchangeAwaitLabel")]
    public partial class Gs2ExchangeAwaitLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Await != null)
            {
                var exchangedAt = _fetcher.Await.ExchangedAt == null ? DateTime.Now : UnixTime.FromUnixTime(_fetcher.Await.ExchangedAt).ToLocalTime();
                onUpdate?.Invoke(
                    format.Replace(
                        "{userId}", $"{_fetcher?.Await?.UserId}"
                    ).Replace(
                        "{rateName}", $"{_fetcher?.Await?.RateName}"
                    ).Replace(
                        "{name}", $"{_fetcher?.Await?.Name}"
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
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2ExchangeAwaitLabel
    {
        private Gs2ExchangeOwnAwaitFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2ExchangeOwnAwaitFetcher>() ?? GetComponentInParent<Gs2ExchangeOwnAwaitFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ExchangeOwnAwaitFetcher.");
                enabled = false;
            }

            Update();
        }

        public bool HasError()
        {
            _fetcher = GetComponent<Gs2ExchangeOwnAwaitFetcher>() ?? GetComponentInParent<Gs2ExchangeOwnAwaitFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2ExchangeAwaitLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2ExchangeAwaitLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ExchangeAwaitLabel
    {
        [Serializable]
        private class UpdateEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private UpdateEvent onUpdate = new UpdateEvent();

        public event UnityAction<string> OnUpdate
        {
            add => onUpdate.AddListener(value);
            remove => onUpdate.RemoveListener(value);
        }
    }
}