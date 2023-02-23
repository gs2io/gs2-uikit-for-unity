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

using System;
using Gs2.Gs2Money.Request;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Money.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Money
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Money/Wallet/View/Transaction/Gs2MoneyWithdrawByUserIdLabel")]
    public partial class Gs2MoneyWithdrawByUserIdLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.ConsumeAction != null && _fetcher.ConsumeAction.Action == "Gs2Money:WithdrawByUserId" &&
                    _userDataFetcher != null && _userDataFetcher.Fetched && _userDataFetcher.Wallet != null) {
                var request = WithdrawByUserIdRequest.FromJson(JsonMapper.ToObject(_fetcher.ConsumeAction.Request));
                {
                    onUpdate?.Invoke(
                        format.Replace(
                            "{namespaceName}",
                            $"{request.NamespaceName}"
                        ).Replace(
                            "{userId}",
                            $"{request.UserId}"
                        ).Replace(
                            "{slot}",
                            $"{request.Slot}"
                        ).Replace(
                            "{count}",
                            $"{request.Count}"
                        ).Replace(
                            "{paidOnly}",
                            $"{request.PaidOnly}"
                        ).Replace(
                            "{userData:slot}",
                            $"{_userDataFetcher.Wallet.Slot}"
                        ).Replace(
                            "{userData:paid}",
                            $"{_userDataFetcher.Wallet.Paid}"
                        ).Replace(
                            "{userData:paid:changed}",
                            $"{_userDataFetcher.Wallet.Paid - request.Count}"
                        ).Replace(
                            "{userData:free}",
                            $"{_userDataFetcher.Wallet.Free}"
                        ).Replace(
                            "{userData:total}",
                            $"{_userDataFetcher.Wallet.Free + _userDataFetcher.Wallet.Paid}"
                        ).Replace(
                            "{userData:total:changed}",
                            $"{_userDataFetcher.Wallet.Free + _userDataFetcher.Wallet.Paid - request.Count}"
                        ).Replace(
                            "{userData:updatedAt}",
                            $"{_userDataFetcher.Wallet.UpdatedAt}"
                        )
                    );
                }
            } else if (_fetcher.Fetched && _fetcher.ConsumeAction != null && _fetcher.ConsumeAction.Action == "Gs2Money:WithdrawByUserId") {
                var request = WithdrawByUserIdRequest.FromJson(JsonMapper.ToObject(_fetcher.ConsumeAction.Request));
                {
                    onUpdate?.Invoke(
                        format.Replace(
                            "{namespaceName}",
                            $"{request.NamespaceName}"
                        ).Replace(
                            "{userId}",
                            $"{request.UserId}"
                        ).Replace(
                            "{slot}",
                            $"{request.Slot}"
                        ).Replace(
                            "{count}",
                            $"{request.Count}"
                        ).Replace(
                            "{paidOnly}",
                            $"{request.PaidOnly}"
                        )
                    );
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2MoneyWithdrawByUserIdLabel
    {
        private Gs2CoreConsumeActionFetcher _fetcher;
        private Gs2MoneyOwnWalletFetcher _userDataFetcher;

        public void Awake()
        {
            _fetcher = GetComponentInParent<Gs2CoreConsumeActionFetcher>();
            _userDataFetcher = GetComponentInParent<Gs2MoneyOwnWalletFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2CoreConsumeActionFetcher.");
                enabled = false;
            }

            Update();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2MoneyWithdrawByUserIdLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2MoneyWithdrawByUserIdLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MoneyWithdrawByUserIdLabel
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