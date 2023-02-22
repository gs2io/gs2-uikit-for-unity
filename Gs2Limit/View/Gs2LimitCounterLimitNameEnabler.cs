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

	[AddComponentMenu("GS2 UIKit/Limit/Counter/View/Properties/LimitName/Gs2LimitCounterLimitNameEnabler")]
    public partial class Gs2LimitCounterLimitNameEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Counter != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableLimitNames.Contains(_fetcher.Counter.LimitName));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableLimitNames.Contains(_fetcher.Counter.LimitName));
                        break;
                    case Expression.StartsWith:
                        target.SetActive(enableLimitName.StartsWith(_fetcher.Counter.LimitName));
                        break;
                    case Expression.EndsWith:
                        target.SetActive(enableLimitName.EndsWith(_fetcher.Counter.LimitName));
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

    public partial class Gs2LimitCounterLimitNameEnabler
    {
        private Gs2LimitOwnCounterFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponentInParent<Gs2LimitOwnCounterFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2LimitOwnCounterFetcher.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2LimitCounterLimitNameEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2LimitCounterLimitNameEnabler
    {
        public enum Expression {
            In,
            NotIn,
            StartsWith,
            EndsWith,
        }

        public Expression expression;

        public List<string> enableLimitNames;

        public string enableLimitName;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2LimitCounterLimitNameEnabler
    {
        
    }
}