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
using Gs2.Gs2Enchant.Request;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Enchant.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Enchant.Label
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Enchant/BalanceParameterStatus/View/Label/Transaction/Gs2EnchantReDrawBalanceParameterStatusByUserIdLabel")]
    public partial class Gs2EnchantReDrawBalanceParameterStatusByUserIdLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Request != null &&
                    _userDataFetcher != null && _userDataFetcher.Fetched && _userDataFetcher.BalanceParameterStatus != null) {
                {
                    onUpdate?.Invoke(
                        format.Replace(
                            "{namespaceName}",
                            $"{_fetcher.Request.NamespaceName}"
                        ).Replace(
                            "{userId}",
                            $"{_fetcher.Request.UserId}"
                        ).Replace(
                            "{parameterName}",
                            $"{_fetcher.Request.ParameterName}"
                        ).Replace(
                            "{propertyId}",
                            $"{_fetcher.Request.PropertyId}"
                        ).Replace(
                            "{fixedParameterNames}",
                            $"{_fetcher.Request.FixedParameterNames}"
                        ).Replace(
                            "{userData:parameterName}",
                            $"{_userDataFetcher.BalanceParameterStatus.ParameterName}"
                        ).Replace(
                            "{userData:propertyId}",
                            $"{_userDataFetcher.BalanceParameterStatus.PropertyId}"
                        ).Replace(
                            "{userData:parameterValues}",
                            $"{_userDataFetcher.BalanceParameterStatus.ParameterValues}"
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
                            "{parameterName}",
                            $"{_fetcher.Request.ParameterName}"
                        ).Replace(
                            "{propertyId}",
                            $"{_fetcher.Request.PropertyId}"
                        ).Replace(
                            "{fixedParameterNames}",
                            $"{_fetcher.Request.FixedParameterNames}"
                        )
                    );
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2EnchantReDrawBalanceParameterStatusByUserIdLabel
    {
        private Gs2EnchantReDrawBalanceParameterStatusByUserIdFetcher _fetcher;
        private Gs2EnchantOwnBalanceParameterStatusFetcher _userDataFetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2EnchantReDrawBalanceParameterStatusByUserIdFetcher>() ?? GetComponentInParent<Gs2EnchantReDrawBalanceParameterStatusByUserIdFetcher>();
            _userDataFetcher = GetComponent<Gs2EnchantOwnBalanceParameterStatusFetcher>() ?? GetComponentInParent<Gs2EnchantOwnBalanceParameterStatusFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2EnchantReDrawBalanceParameterStatusByUserIdFetcher.");
                enabled = false;
            }

            Update();
        }

        public bool HasError()
        {
            _fetcher = GetComponent<Gs2EnchantReDrawBalanceParameterStatusByUserIdFetcher>() ?? GetComponentInParent<Gs2EnchantReDrawBalanceParameterStatusByUserIdFetcher>(true);
            _userDataFetcher = GetComponent<Gs2EnchantOwnBalanceParameterStatusFetcher>() ?? GetComponentInParent<Gs2EnchantOwnBalanceParameterStatusFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2EnchantReDrawBalanceParameterStatusByUserIdLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2EnchantReDrawBalanceParameterStatusByUserIdLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2EnchantReDrawBalanceParameterStatusByUserIdLabel
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