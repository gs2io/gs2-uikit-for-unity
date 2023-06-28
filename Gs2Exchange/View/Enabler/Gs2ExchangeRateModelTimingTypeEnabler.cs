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

using System.Collections.Generic;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Exchange.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Exchange.Enabler
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Exchange/RateModel/View/Enabler/Properties/TimingType/Gs2ExchangeRateModelTimingTypeEnabler")]
    public partial class Gs2ExchangeRateModelTimingTypeEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.RateModel != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableTimingTypes.Contains(_fetcher.RateModel.TimingType));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableTimingTypes.Contains(_fetcher.RateModel.TimingType));
                        break;
                    case Expression.StartsWith:
                        target.SetActive(enableTimingType.StartsWith(_fetcher.RateModel.TimingType));
                        break;
                    case Expression.EndsWith:
                        target.SetActive(enableTimingType.EndsWith(_fetcher.RateModel.TimingType));
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

    public partial class Gs2ExchangeRateModelTimingTypeEnabler
    {
        private Gs2ExchangeRateModelFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2ExchangeRateModelFetcher>() ?? GetComponentInParent<Gs2ExchangeRateModelFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ExchangeRateModelFetcher.");
                enabled = false;
            }
            if (target == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: target is not set.");
                enabled = false;
            }
        }

        public bool HasError()
        {
            _fetcher = GetComponent<Gs2ExchangeRateModelFetcher>() ?? GetComponentInParent<Gs2ExchangeRateModelFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            if (target == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2ExchangeRateModelTimingTypeEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2ExchangeRateModelTimingTypeEnabler
    {
        public enum Expression {
            In,
            NotIn,
            StartsWith,
            EndsWith,
        }

        public Expression expression;

        public List<string> enableTimingTypes;

        public string enableTimingType;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ExchangeRateModelTimingTypeEnabler
    {
        
    }
}