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

using Gs2.Unity.Gs2Ranking.ScriptableObject;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Ranking.Context
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Ranking/Score/Gs2RankingOwnScoreContext")]
    public partial class Gs2RankingOwnScoreContext : MonoBehaviour
    {
        public void Start() {
            if (Score == null) {
                Debug.LogError("Score is not set in Gs2RankingOwnScoreContext.");
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2RankingOwnScoreContext
    {

    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2RankingOwnScoreContext
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2RankingOwnScoreContext
    {
        public OwnScore Score;

        public void SetOwnScore(OwnScore Score) {
            this.Score = Score;
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2RankingOwnScoreContext
    {

    }
}