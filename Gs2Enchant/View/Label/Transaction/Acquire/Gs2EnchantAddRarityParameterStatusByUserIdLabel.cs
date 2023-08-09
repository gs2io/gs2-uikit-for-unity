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

	[AddComponentMenu("GS2 UIKit/Enchant/RarityParameterStatus/View/Label/Transaction/Gs2EnchantAddRarityParameterStatusByUserIdLabel")]
    public partial class Gs2EnchantAddRarityParameterStatusByUserIdLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Request != null &&
                    _userDataFetcher != null && _userDataFetcher.Fetched && _userDataFetcher.RarityParameterStatus != null) {
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
                            "{count}",
                            $"{_fetcher.Request.Count}"
                        ).Replace(
                            "{userData:parameterName}",
                            $"{_userDataFetcher.RarityParameterStatus.ParameterName}"
                        ).Replace(
                            "{userData:propertyId}",
                            $"{_userDataFetcher.RarityParameterStatus.PropertyId}"
                        ).Replace(
                            "{userData:parameterValues}",
                            $"{_userDataFetcher.RarityParameterStatus.ParameterValues}"
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
                            "{count}",
                            $"{_fetcher.Request.Count}"
                        )
                    );
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2EnchantAddRarityParameterStatusByUserIdLabel
    {
        private Gs2EnchantAddRarityParameterStatusByUserIdFetcher _fetcher;
        private Gs2EnchantOwnRarityParameterStatusFetcher _userDataFetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2EnchantAddRarityParameterStatusByUserIdFetcher>() ?? GetComponentInParent<Gs2EnchantAddRarityParameterStatusByUserIdFetcher>();
            _userDataFetcher = GetComponent<Gs2EnchantOwnRarityParameterStatusFetcher>() ?? GetComponentInParent<Gs2EnchantOwnRarityParameterStatusFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2EnchantAddRarityParameterStatusByUserIdFetcher.");
                enabled = false;
            }

            Update();
        }

        public virtual bool HasError()
        {
            _fetcher = GetComponent<Gs2EnchantAddRarityParameterStatusByUserIdFetcher>() ?? GetComponentInParent<Gs2EnchantAddRarityParameterStatusByUserIdFetcher>(true);
            _userDataFetcher = GetComponent<Gs2EnchantOwnRarityParameterStatusFetcher>() ?? GetComponentInParent<Gs2EnchantOwnRarityParameterStatusFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2EnchantAddRarityParameterStatusByUserIdLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2EnchantAddRarityParameterStatusByUserIdLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2EnchantAddRarityParameterStatusByUserIdLabel
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