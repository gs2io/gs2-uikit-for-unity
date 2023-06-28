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
 *
 * deny overwrite
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
using Gs2.Gs2Money.Request;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Money.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Money.Label
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Money/Wallet/View/Label/Transaction/Gs2MoneyDepositByUserIdLabel")]
    public partial class Gs2MoneyDepositByUserIdLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Request != null &&
                    _userDataFetcher != null && _userDataFetcher.Fetched && _userDataFetcher.Wallet != null) {
                {
                    onUpdate?.Invoke(
                        format.Replace(
                            "{namespaceName}",
                            $"{_fetcher.Request.NamespaceName}"
                        ).Replace(
                            "{userId}",
                            $"{_fetcher.Request.UserId}"
                        ).Replace(
                            "{slot}",
                            $"{_fetcher.Request.Slot}"
                        ).Replace(
                            "{price}",
                            $"{_fetcher.Request.Price}"
                        ).Replace(
                            "{count}",
                            $"{_fetcher.Request.Count}"
                        ).Replace(
                            "{userData:slot}",
                            $"{_userDataFetcher.Wallet.Slot}"
                        ).Replace(
                            "{userData:paid}",
                            $"{_userDataFetcher.Wallet.Paid}"
                        ).Replace(
                            "{userData:free}",
                            $"{_userDataFetcher.Wallet.Free}"
                        ).Replace(
                            "{userData:total}",
                            $"{_userDataFetcher.Wallet.Free + _userDataFetcher.Wallet.Paid}"
                        ).Replace(
                            "{userData:updatedAt}",
                            $"{_userDataFetcher.Wallet.UpdatedAt}"
                        )
                    );
                }
            } else if (_fetcher.Fetched && _fetcher.Request != null) {
                {
                    onUpdate?.Invoke(
                        format.Replace(
                            "{namespaceName}",
                            $"{_fetcher.Request.NamespaceName}"
                        ).Replace(
                            "{userId}",
                            $"{_fetcher.Request.UserId}"
                        ).Replace(
                            "{slot}",
                            $"{_fetcher.Request.Slot}"
                        ).Replace(
                            "{price}",
                            $"{_fetcher.Request.Price}"
                        ).Replace(
                            "{count}",
                            $"{_fetcher.Request.Count}"
                        )
                    );
                }
            } else {
                onUpdate?.Invoke(
                    format.Replace(
                        "{price}",
                        ""
                    ).Replace(
                        "{count}",
                        "0"
                    )
                );
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2MoneyDepositByUserIdLabel
    {
        private Gs2MoneyDepositByUserIdFetcher _fetcher;
        private Gs2MoneyOwnWalletFetcher _userDataFetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2MoneyDepositByUserIdFetcher>() ?? GetComponentInParent<Gs2MoneyDepositByUserIdFetcher>();
            _userDataFetcher = GetComponent<Gs2MoneyOwnWalletFetcher>() ?? GetComponentInParent<Gs2MoneyOwnWalletFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MoneyDepositByUserIdFetcher.");
                enabled = false;
            }

            Update();
        }

        public bool HasError()
        {
            _fetcher = GetComponent<Gs2MoneyDepositByUserIdFetcher>() ?? GetComponentInParent<Gs2MoneyDepositByUserIdFetcher>(true);
            _userDataFetcher = GetComponent<Gs2MoneyOwnWalletFetcher>() ?? GetComponentInParent<Gs2MoneyOwnWalletFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2MoneyDepositByUserIdLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2MoneyDepositByUserIdLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MoneyDepositByUserIdLabel
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