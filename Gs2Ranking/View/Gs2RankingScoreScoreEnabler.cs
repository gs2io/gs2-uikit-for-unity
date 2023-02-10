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
using Gs2.Unity.UiKit.Gs2Ranking.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Ranking
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Ranking/Score/View/Properties/Score/Gs2RankingScoreScoreEnabler")]
    public partial class Gs2RankingScoreScoreEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableScores.Contains(_fetcher.Score.Score));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableScores.Contains(_fetcher.Score.Score));
                        break;
                    case Expression.Less:
                        target.SetActive(enableScore < _fetcher.Score.Score);
                        break;
                    case Expression.LessEqual:
                        target.SetActive(enableScore <= _fetcher.Score.Score);
                        break;
                    case Expression.Greater:
                        target.SetActive(enableScore > _fetcher.Score.Score);
                        break;
                    case Expression.GreaterEqual:
                        target.SetActive(enableScore >= _fetcher.Score.Score);
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
    
    public partial class Gs2RankingScoreScoreEnabler
    {
        private Gs2RankingScoreFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponentInParent<Gs2RankingScoreFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2RankingScoreScoreEnabler
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2RankingScoreScoreEnabler
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

        public List<long> enableScores;

        public long enableScore;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2RankingScoreScoreEnabler
    {
        
    }
}