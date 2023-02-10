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
using Gs2.Unity.UiKit.Gs2Limit.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Limit
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Limit/Counter/View/Properties/Count/Gs2LimitCounterCountEnabler")]
    public partial class Gs2LimitCounterCountEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableCounts.Contains(_fetcher.Counter.Count));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableCounts.Contains(_fetcher.Counter.Count));
                        break;
                    case Expression.Less:
                        target.SetActive(enableCount < _fetcher.Counter.Count);
                        break;
                    case Expression.LessEqual:
                        target.SetActive(enableCount <= _fetcher.Counter.Count);
                        break;
                    case Expression.Greater:
                        target.SetActive(enableCount > _fetcher.Counter.Count);
                        break;
                    case Expression.GreaterEqual:
                        target.SetActive(enableCount >= _fetcher.Counter.Count);
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
    
    public partial class Gs2LimitCounterCountEnabler
    {
        private Gs2LimitCounterFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponentInParent<Gs2LimitCounterFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2LimitCounterCountEnabler
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2LimitCounterCountEnabler
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

        public List<int> enableCounts;

        public int enableCount;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2LimitCounterCountEnabler
    {
        
    }
}