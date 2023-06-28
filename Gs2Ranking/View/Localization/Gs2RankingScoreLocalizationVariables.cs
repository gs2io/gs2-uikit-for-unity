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

#if GS2_ENABLE_LOCALIZATION

using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Ranking.Fetcher;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

namespace Gs2.Unity.UiKit.Gs2Ranking.Localization
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Ranking/Score/View/Localization/Gs2RankingScoreLocalizationVariables")]
    public partial class Gs2RankingScoreLocalizationVariables : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched) {
                target.StringReference["categoryName"] = new StringVariable {
                    Value = _fetcher?.Score?.CategoryName ?? "",
                };
                target.StringReference["userId"] = new StringVariable {
                    Value = _fetcher?.Score?.UserId ?? "",
                };
                target.StringReference["uniqueId"] = new StringVariable {
                    Value = _fetcher?.Score?.UniqueId ?? "",
                };
                target.StringReference["scorerUserId"] = new StringVariable {
                    Value = _fetcher?.Score?.ScorerUserId ?? "",
                };
                target.StringReference["score"] = new LongVariable {
                    Value = _fetcher?.Score?.Score ?? 0,
                };
                target.StringReference["metadata"] = new StringVariable {
                    Value = _fetcher?.Score?.Metadata ?? "",
                };
                enabled = false;
                target.enabled = true;
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2RankingScoreLocalizationVariables
    {
        private Gs2RankingOwnScoreFetcher _fetcher;

        public void Awake() {
            target.enabled = false;
            _fetcher = GetComponent<Gs2RankingOwnScoreFetcher>() ?? GetComponentInParent<Gs2RankingOwnScoreFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2RankingScoreFetcher.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2RankingScoreLocalizationVariables
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2RankingScoreLocalizationVariables
    {
        public LocalizeStringEvent target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2RankingScoreLocalizationVariables
    {

    }
}

#endif