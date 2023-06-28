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
using Gs2.Unity.UiKit.Gs2Money.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Money
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Money/Wallet/View/Properties/Total/Gs2MoneyWalletTotalEnabler")]
    public partial class Gs2MoneyWalletTotalEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Wallet != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableTotals.Contains(_fetcher.Wallet.Free + _fetcher.Wallet.Paid));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableTotals.Contains(_fetcher.Wallet.Free + _fetcher.Wallet.Paid));
                        break;
                    case Expression.Less:
                        target.SetActive(enableTotal > _fetcher.Wallet.Free + _fetcher.Wallet.Paid);
                        break;
                    case Expression.LessEqual:
                        target.SetActive(enableTotal >= _fetcher.Wallet.Free + _fetcher.Wallet.Paid);
                        break;
                    case Expression.Greater:
                        target.SetActive(enableTotal < _fetcher.Wallet.Free + _fetcher.Wallet.Paid);
                        break;
                    case Expression.GreaterEqual:
                        target.SetActive(enableTotal <= _fetcher.Wallet.Free + _fetcher.Wallet.Paid);
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

    public partial class Gs2MoneyWalletTotalEnabler
    {
        private Gs2MoneyOwnWalletFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponentInParent<Gs2MoneyOwnWalletFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MoneyOwnWalletFetcher.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2MoneyWalletTotalEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2MoneyWalletTotalEnabler
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

        public List<int> enableTotals;

        public int enableTotal;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MoneyWalletTotalEnabler
    {
        
    }
}