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
using Gs2.Unity.UiKit.Gs2Mission.Fetcher;
using UnityEngine;
using System.Linq;

namespace Gs2.Unity.UiKit.Gs2Mission
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Mission/Counter/View/Properties/Value/Gs2MissionCounterMonthlyValueEnabler")]
    public partial class Gs2MissionOwnCounterMonthlyValueEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Counter != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableMonthlyValues.Contains(this._fetcher.Counter?.Values?.FirstOrDefault(v => v.ResetType == "notReset")?.Value ?? 0));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableMonthlyValues.Contains(this._fetcher.Counter?.Values?.FirstOrDefault(v => v.ResetType == "notReset")?.Value ?? 0));
                        break;
                    case Expression.Less:
                        target.SetActive(enableMonthlyValue > (this._fetcher.Counter?.Values?.FirstOrDefault(v => v.ResetType == "notReset")?.Value ?? 0));
                        break;
                    case Expression.LessEqual:
                        target.SetActive(enableMonthlyValue >= (this._fetcher.Counter?.Values?.FirstOrDefault(v => v.ResetType == "notReset")?.Value ?? 0));
                        break;
                    case Expression.Greater:
                        target.SetActive(enableMonthlyValue < (this._fetcher.Counter?.Values?.FirstOrDefault(v => v.ResetType == "notReset")?.Value ?? 0));
                        break;
                    case Expression.GreaterEqual:
                        target.SetActive(enableMonthlyValue <= (this._fetcher.Counter?.Values?.FirstOrDefault(v => v.ResetType == "notReset")?.Value ?? 0));
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

    public partial class Gs2MissionOwnCounterMonthlyValueEnabler
    {
        private Gs2MissionOwnCounterFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2MissionOwnCounterFetcher>() ?? GetComponentInParent<Gs2MissionOwnCounterFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MissionOwnCounterFetcher.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2MissionOwnCounterMonthlyValueEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2MissionOwnCounterMonthlyValueEnabler
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

        public List<long> enableMonthlyValues;

        public long enableMonthlyValue;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MissionOwnCounterMonthlyValueEnabler
    {
        
    }
}