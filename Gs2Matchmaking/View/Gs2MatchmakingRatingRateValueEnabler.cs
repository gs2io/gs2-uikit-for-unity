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
using Gs2.Unity.UiKit.Gs2Matchmaking.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Matchmaking
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Matchmaking/Rating/View/Properties/RateValue/Gs2MatchmakingRatingRateValueEnabler")]
    public partial class Gs2MatchmakingRatingRateValueEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Rating != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableRateValues.Contains(_fetcher.Rating.RateValue));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableRateValues.Contains(_fetcher.Rating.RateValue));
                        break;
                    case Expression.Less:
                        target.SetActive(enableRateValue > _fetcher.Rating.RateValue);
                        break;
                    case Expression.LessEqual:
                        target.SetActive(enableRateValue >= _fetcher.Rating.RateValue);
                        break;
                    case Expression.Greater:
                        target.SetActive(enableRateValue < _fetcher.Rating.RateValue);
                        break;
                    case Expression.GreaterEqual:
                        target.SetActive(enableRateValue <= _fetcher.Rating.RateValue);
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

    public partial class Gs2MatchmakingRatingRateValueEnabler
    {
        private Gs2MatchmakingOwnRatingFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2MatchmakingOwnRatingFetcher>() ?? GetComponentInParent<Gs2MatchmakingOwnRatingFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MatchmakingOwnRatingFetcher.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2MatchmakingRatingRateValueEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2MatchmakingRatingRateValueEnabler
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

        public List<float> enableRateValues;

        public float enableRateValue;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MatchmakingRatingRateValueEnabler
    {
        
    }
}