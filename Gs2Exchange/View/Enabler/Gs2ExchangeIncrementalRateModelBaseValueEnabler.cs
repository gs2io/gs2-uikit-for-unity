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

namespace Gs2.Unity.UiKit.Gs2Exchange
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Exchange/IncrementalRateModel/View/Enabler/Properties/BaseValue/Gs2ExchangeIncrementalRateModelBaseValueEnabler")]
    public partial class Gs2ExchangeIncrementalRateModelBaseValueEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.IncrementalRateModel != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableBaseValues.Contains(_fetcher.IncrementalRateModel.BaseValue));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableBaseValues.Contains(_fetcher.IncrementalRateModel.BaseValue));
                        break;
                    case Expression.Less:
                        target.SetActive(enableBaseValue > _fetcher.IncrementalRateModel.BaseValue);
                        break;
                    case Expression.LessEqual:
                        target.SetActive(enableBaseValue >= _fetcher.IncrementalRateModel.BaseValue);
                        break;
                    case Expression.Greater:
                        target.SetActive(enableBaseValue < _fetcher.IncrementalRateModel.BaseValue);
                        break;
                    case Expression.GreaterEqual:
                        target.SetActive(enableBaseValue <= _fetcher.IncrementalRateModel.BaseValue);
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

    public partial class Gs2ExchangeIncrementalRateModelBaseValueEnabler
    {
        private Gs2ExchangeIncrementalRateModelFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2ExchangeIncrementalRateModelFetcher>() ?? GetComponentInParent<Gs2ExchangeIncrementalRateModelFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ExchangeIncrementalRateModelFetcher.");
                enabled = false;
            }
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

    public partial class Gs2ExchangeIncrementalRateModelBaseValueEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2ExchangeIncrementalRateModelBaseValueEnabler
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

        public List<long> enableBaseValues;

        public long enableBaseValue;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ExchangeIncrementalRateModelBaseValueEnabler
    {
        
    }
}