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
using Gs2.Unity.UiKit.Gs2Limit.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Limit
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Limit/LimitModel/View/Properties/ResetDayOfWeek/Gs2LimitLimitModelResetDayOfWeekEnabler")]
    public partial class Gs2LimitLimitModelResetDayOfWeekEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.LimitModel != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableResetDayOfWeeks.Contains(_fetcher.LimitModel.ResetDayOfWeek));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableResetDayOfWeeks.Contains(_fetcher.LimitModel.ResetDayOfWeek));
                        break;
                    case Expression.StartsWith:
                        target.SetActive(enableResetDayOfWeek.StartsWith(_fetcher.LimitModel.ResetDayOfWeek));
                        break;
                    case Expression.EndsWith:
                        target.SetActive(enableResetDayOfWeek.EndsWith(_fetcher.LimitModel.ResetDayOfWeek));
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

    public partial class Gs2LimitLimitModelResetDayOfWeekEnabler
    {
        private Gs2LimitLimitModelFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponentInParent<Gs2LimitLimitModelFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2LimitLimitModelFetcher.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2LimitLimitModelResetDayOfWeekEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2LimitLimitModelResetDayOfWeekEnabler
    {
        public enum Expression {
            In,
            NotIn,
            StartsWith,
            EndsWith,
        }

        public Expression expression;

        public List<string> enableResetDayOfWeeks;

        public string enableResetDayOfWeek;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2LimitLimitModelResetDayOfWeekEnabler
    {
        
    }
}