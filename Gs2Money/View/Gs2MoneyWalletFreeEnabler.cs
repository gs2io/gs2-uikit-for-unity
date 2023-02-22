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

	[AddComponentMenu("GS2 UIKit/Money/Wallet/View/Properties/Free/Gs2MoneyWalletFreeEnabler")]
    public partial class Gs2MoneyWalletFreeEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Wallet != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableFrees.Contains(_fetcher.Wallet.Free));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableFrees.Contains(_fetcher.Wallet.Free));
                        break;
                    case Expression.Less:
                        target.SetActive(enableFree > _fetcher.Wallet.Free);
                        break;
                    case Expression.LessEqual:
                        target.SetActive(enableFree >= _fetcher.Wallet.Free);
                        break;
                    case Expression.Greater:
                        target.SetActive(enableFree < _fetcher.Wallet.Free);
                        break;
                    case Expression.GreaterEqual:
                        target.SetActive(enableFree <= _fetcher.Wallet.Free);
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

    public partial class Gs2MoneyWalletFreeEnabler
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

    public partial class Gs2MoneyWalletFreeEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2MoneyWalletFreeEnabler
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

        public List<int> enableFrees;

        public int enableFree;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MoneyWalletFreeEnabler
    {
        
    }
}