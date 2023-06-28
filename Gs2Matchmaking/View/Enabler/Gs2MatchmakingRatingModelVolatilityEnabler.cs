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

	[AddComponentMenu("GS2 UIKit/Matchmaking/RatingModel/View/Enabler/Properties/Volatility/Gs2MatchmakingRatingModelVolatilityEnabler")]
    public partial class Gs2MatchmakingRatingModelVolatilityEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.RatingModel != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableVolatilities.Contains(_fetcher.RatingModel.Volatility));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableVolatilities.Contains(_fetcher.RatingModel.Volatility));
                        break;
                    case Expression.Less:
                        target.SetActive(enableVolatility > _fetcher.RatingModel.Volatility);
                        break;
                    case Expression.LessEqual:
                        target.SetActive(enableVolatility >= _fetcher.RatingModel.Volatility);
                        break;
                    case Expression.Greater:
                        target.SetActive(enableVolatility < _fetcher.RatingModel.Volatility);
                        break;
                    case Expression.GreaterEqual:
                        target.SetActive(enableVolatility <= _fetcher.RatingModel.Volatility);
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

    public partial class Gs2MatchmakingRatingModelVolatilityEnabler
    {
        private Gs2MatchmakingRatingModelFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2MatchmakingRatingModelFetcher>() ?? GetComponentInParent<Gs2MatchmakingRatingModelFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MatchmakingRatingModelFetcher.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2MatchmakingRatingModelVolatilityEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2MatchmakingRatingModelVolatilityEnabler
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

        public List<int> enableVolatilities;

        public int enableVolatility;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MatchmakingRatingModelVolatilityEnabler
    {
        
    }
}