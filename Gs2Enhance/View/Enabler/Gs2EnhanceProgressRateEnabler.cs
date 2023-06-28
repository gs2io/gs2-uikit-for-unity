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
using Gs2.Unity.UiKit.Gs2Enhance.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Enhance
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Enhance/Progress/View/Enabler/Properties/Rate/Gs2EnhanceProgressRateEnabler")]
    public partial class Gs2EnhanceProgressRateEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Progress != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableRates.Contains(_fetcher.Progress.Rate));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableRates.Contains(_fetcher.Progress.Rate));
                        break;
                    case Expression.Less:
                        target.SetActive(enableRate > _fetcher.Progress.Rate);
                        break;
                    case Expression.LessEqual:
                        target.SetActive(enableRate >= _fetcher.Progress.Rate);
                        break;
                    case Expression.Greater:
                        target.SetActive(enableRate < _fetcher.Progress.Rate);
                        break;
                    case Expression.GreaterEqual:
                        target.SetActive(enableRate <= _fetcher.Progress.Rate);
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

    public partial class Gs2EnhanceProgressRateEnabler
    {
        private Gs2EnhanceOwnProgressFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2EnhanceOwnProgressFetcher>() ?? GetComponentInParent<Gs2EnhanceOwnProgressFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2EnhanceOwnProgressFetcher.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2EnhanceProgressRateEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2EnhanceProgressRateEnabler
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

        public List<float> enableRates;

        public float enableRate;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2EnhanceProgressRateEnabler
    {
        
    }
}