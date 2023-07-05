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

	[AddComponentMenu("GS2 UIKit/Exchange/IncrementalRateModel/View/Label/Gs2ExchangeIncrementalRateModelLabel")]
    public partial class Gs2ExchangeIncrementalRateModelLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.IncrementalRateModel != null)
            {
                onUpdate?.Invoke(
                    format.Replace(
                        "{name}", $"{_fetcher?.IncrementalRateModel?.Name}"
                    ).Replace(
                        "{metadata}", $"{_fetcher?.IncrementalRateModel?.Metadata}"
                    ).Replace(
                        "{calculateType}", $"{_fetcher?.IncrementalRateModel?.CalculateType}"
                    ).Replace(
                        "{consumeAction}", $"{_fetcher?.IncrementalRateModel?.ConsumeAction}"
                    ).Replace(
                        "{baseValue}", $"{_fetcher?.IncrementalRateModel?.BaseValue}"
                    ).Replace(
                        "{coefficientValue}", $"{_fetcher?.IncrementalRateModel?.CoefficientValue}"
                    ).Replace(
                        "{exchangeCountId}", $"{_fetcher?.IncrementalRateModel?.ExchangeCountId}"
                    ).Replace(
                        "{acquireActions}", $"{_fetcher?.IncrementalRateModel?.AcquireActions}"
                    )
                );
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2ExchangeIncrementalRateModelLabel
    {
        private Gs2ExchangeIncrementalRateModelFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2ExchangeIncrementalRateModelFetcher>() ?? GetComponentInParent<Gs2ExchangeIncrementalRateModelFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ExchangeIncrementalRateModelFetcher.");
                enabled = false;
            }

            Update();
        }

        public bool HasError()
        {
            _fetcher = GetComponent<Gs2ExchangeIncrementalRateModelFetcher>() ?? GetComponentInParent<Gs2ExchangeIncrementalRateModelFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2ExchangeIncrementalRateModelLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2ExchangeIncrementalRateModelLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ExchangeIncrementalRateModelLabel
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