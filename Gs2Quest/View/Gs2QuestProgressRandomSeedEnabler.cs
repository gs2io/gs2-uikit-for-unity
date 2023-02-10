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
using Gs2.Unity.UiKit.Gs2Quest.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Quest
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Quest/Progress/View/Properties/RandomSeed/Gs2QuestProgressRandomSeedEnabler")]
    public partial class Gs2QuestProgressRandomSeedEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableRandomSeeds.Contains(_fetcher.Progress.RandomSeed));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableRandomSeeds.Contains(_fetcher.Progress.RandomSeed));
                        break;
                    case Expression.Less:
                        target.SetActive(enableRandomSeed < _fetcher.Progress.RandomSeed);
                        break;
                    case Expression.LessEqual:
                        target.SetActive(enableRandomSeed <= _fetcher.Progress.RandomSeed);
                        break;
                    case Expression.Greater:
                        target.SetActive(enableRandomSeed > _fetcher.Progress.RandomSeed);
                        break;
                    case Expression.GreaterEqual:
                        target.SetActive(enableRandomSeed >= _fetcher.Progress.RandomSeed);
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
    
    public partial class Gs2QuestProgressRandomSeedEnabler
    {
        private Gs2QuestProgressFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponentInParent<Gs2QuestProgressFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2QuestProgressRandomSeedEnabler
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2QuestProgressRandomSeedEnabler
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

        public List<long> enableRandomSeeds;

        public long enableRandomSeed;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2QuestProgressRandomSeedEnabler
    {
        
    }
}