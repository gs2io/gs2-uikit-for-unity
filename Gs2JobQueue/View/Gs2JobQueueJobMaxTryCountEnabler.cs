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
using Gs2.Unity.UiKit.Gs2JobQueue.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2JobQueue
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/JobQueue/Job/View/Properties/MaxTryCount/Gs2JobQueueJobMaxTryCountEnabler")]
    public partial class Gs2JobQueueJobMaxTryCountEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableMaxTryCounts.Contains(_fetcher.Job.MaxTryCount));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableMaxTryCounts.Contains(_fetcher.Job.MaxTryCount));
                        break;
                    case Expression.Less:
                        target.SetActive(enableMaxTryCount < _fetcher.Job.MaxTryCount);
                        break;
                    case Expression.LessEqual:
                        target.SetActive(enableMaxTryCount <= _fetcher.Job.MaxTryCount);
                        break;
                    case Expression.Greater:
                        target.SetActive(enableMaxTryCount > _fetcher.Job.MaxTryCount);
                        break;
                    case Expression.GreaterEqual:
                        target.SetActive(enableMaxTryCount >= _fetcher.Job.MaxTryCount);
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
    
    public partial class Gs2JobQueueJobMaxTryCountEnabler
    {
        private Gs2JobQueueJobFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponentInParent<Gs2JobQueueJobFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2JobQueueJobMaxTryCountEnabler
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2JobQueueJobMaxTryCountEnabler
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

        public List<int> enableMaxTryCounts;

        public int enableMaxTryCount;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2JobQueueJobMaxTryCountEnabler
    {
        
    }
}