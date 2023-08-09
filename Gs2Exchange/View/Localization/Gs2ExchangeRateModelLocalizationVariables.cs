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

    [AddComponentMenu("GS2 UIKit/Exchange/RateModel/View/Localization/Gs2ExchangeRateModelLocalizationVariables")]
    public partial class Gs2ExchangeRateModelLocalizationVariables : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched) {
                target.StringReference["name"] = new StringVariable {
                    Value = _fetcher?.RateModel?.Name ?? "",
                };
                target.StringReference["metadata"] = new StringVariable {
                    Value = _fetcher?.RateModel?.Metadata ?? "",
                };
                target.StringReference["timingType"] = new StringVariable {
                    Value = _fetcher?.RateModel?.TimingType ?? "",
                };
                target.StringReference["lockTime"] = new IntVariable {
                    Value = _fetcher?.RateModel?.LockTime ?? 0,
                };
                enabled = false;
                target.enabled = true;
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2ExchangeRateModelLocalizationVariables
    {
        private Gs2ExchangeRateModelFetcher _fetcher;

        public void Awake() {
            target.enabled = false;
            _fetcher = GetComponent<Gs2ExchangeRateModelFetcher>() ?? GetComponentInParent<Gs2ExchangeRateModelFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ExchangeRateModelFetcher.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            _fetcher = GetComponent<Gs2ExchangeRateModelFetcher>() ?? GetComponentInParent<Gs2ExchangeRateModelFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2ExchangeRateModelLocalizationVariables
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2ExchangeRateModelLocalizationVariables
    {
        public LocalizeStringEvent target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ExchangeRateModelLocalizationVariables
    {

    }
}

#endif