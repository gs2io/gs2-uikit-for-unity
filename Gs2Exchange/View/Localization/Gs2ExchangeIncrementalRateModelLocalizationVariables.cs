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

#if GS2_ENABLE_LOCALIZATION

using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Exchange.Fetcher;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

namespace Gs2.Unity.UiKit.Gs2Exchange.Localization
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Exchange/IncrementalRateModel/View/Localization/Gs2ExchangeIncrementalRateModelLocalizationVariables")]
    public partial class Gs2ExchangeIncrementalRateModelLocalizationVariables : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched) {
                target.StringReference["name"] = new StringVariable {
                    Value = _fetcher?.IncrementalRateModel?.Name ?? "",
                };
                target.StringReference["metadata"] = new StringVariable {
                    Value = _fetcher?.IncrementalRateModel?.Metadata ?? "",
                };
                target.StringReference["calculateType"] = new StringVariable {
                    Value = _fetcher?.IncrementalRateModel?.CalculateType ?? "",
                };
                target.StringReference["baseValue"] = new LongVariable {
                    Value = _fetcher?.IncrementalRateModel?.BaseValue ?? 0,
                };
                target.StringReference["coefficientValue"] = new LongVariable {
                    Value = _fetcher?.IncrementalRateModel?.CoefficientValue ?? 0,
                };
                target.StringReference["exchangeCountId"] = new StringVariable {
                    Value = _fetcher?.IncrementalRateModel?.ExchangeCountId ?? "",
                };
                enabled = false;
                target.enabled = true;
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2ExchangeIncrementalRateModelLocalizationVariables
    {
        private Gs2ExchangeIncrementalRateModelFetcher _fetcher;

        public void Awake() {
            target.enabled = false;
            _fetcher = GetComponent<Gs2ExchangeIncrementalRateModelFetcher>() ?? GetComponentInParent<Gs2ExchangeIncrementalRateModelFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ExchangeIncrementalRateModelFetcher.");
                enabled = false;
            }
        }

        public virtual bool HasError()
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

    public partial class Gs2ExchangeIncrementalRateModelLocalizationVariables
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2ExchangeIncrementalRateModelLocalizationVariables
    {
        public LocalizeStringEvent target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ExchangeIncrementalRateModelLocalizationVariables
    {

    }
}

#endif