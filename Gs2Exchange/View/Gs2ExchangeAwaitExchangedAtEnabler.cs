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

using System.Collections.Generic;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Exchange.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Exchange
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Exchange/Await/View/Properties/ExchangedAt/Gs2ExchangeAwaitExchangedAtEnabler")]
    public partial class Gs2ExchangeAwaitExchangedAtEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Await != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableExchangedAts.Contains(_fetcher.Await.ExchangedAt));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableExchangedAts.Contains(_fetcher.Await.ExchangedAt));
                        break;
                    case Expression.Less:
                        target.SetActive(enableExchangedAt > _fetcher.Await.ExchangedAt);
                        break;
                    case Expression.LessEqual:
                        target.SetActive(enableExchangedAt >= _fetcher.Await.ExchangedAt);
                        break;
                    case Expression.Greater:
                        target.SetActive(enableExchangedAt < _fetcher.Await.ExchangedAt);
                        break;
                    case Expression.GreaterEqual:
                        target.SetActive(enableExchangedAt <= _fetcher.Await.ExchangedAt);
                        break;
                }
            }
            else
            {
                target.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2ExchangeAwaitExchangedAtEnabler
    {
        private Gs2ExchangeOwnAwaitFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2ExchangeOwnAwaitFetcher>() ?? GetComponentInParent<Gs2ExchangeOwnAwaitFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ExchangeOwnAwaitFetcher.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2ExchangeAwaitExchangedAtEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2ExchangeAwaitExchangedAtEnabler
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

        public List<long> enableExchangedAts;

        public long enableExchangedAt;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ExchangeAwaitExchangedAtEnabler
    {
        
    }
}