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
using Gs2.Unity.UiKit.Gs2Ranking.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Ranking
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Ranking/Score/View/Properties/ScorerUserId/Gs2RankingScoreScorerUserIdEnabler")]
    public partial class Gs2RankingScoreScorerUserIdEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Score != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableScorerUserIds.Contains(_fetcher.Score.ScorerUserId));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableScorerUserIds.Contains(_fetcher.Score.ScorerUserId));
                        break;
                    case Expression.StartsWith:
                        target.SetActive(enableScorerUserId.StartsWith(_fetcher.Score.ScorerUserId));
                        break;
                    case Expression.EndsWith:
                        target.SetActive(enableScorerUserId.EndsWith(_fetcher.Score.ScorerUserId));
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

    public partial class Gs2RankingScoreScorerUserIdEnabler
    {
        private Gs2RankingOwnScoreFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2RankingOwnScoreFetcher>() ?? GetComponentInParent<Gs2RankingOwnScoreFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2RankingOwnScoreFetcher.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2RankingScoreScorerUserIdEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2RankingScoreScorerUserIdEnabler
    {
        public enum Expression {
            In,
            NotIn,
            StartsWith,
            EndsWith,
        }

        public Expression expression;

        public List<string> enableScorerUserIds;

        public string enableScorerUserId;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2RankingScoreScorerUserIdEnabler
    {
        
    }
}