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
 *
 * deny overwrite
 */
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable CheckNamespace

using System.Collections.Generic;
using Gs2.Unity.Gs2Ranking.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Ranking.Context;
using Gs2.Unity.UiKit.Gs2Ranking.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Ranking
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Ranking/Score/View/Gs2RankingOwnScoreList")]
    public partial class Gs2RankingOwnScoreList : MonoBehaviour
    {
        private List<Gs2RankingOwnScoreContext> _children;

        public void Update() {
            if (_fetcher.Fetched) {
                for (var i = 0; i < this.maximumItems; i++) {
                    if (i < this._fetcher.Scores.Count) {
                        _children[i].Score.scorerUserId = this._fetcher.Scores[i].ScorerUserId;
                        _children[i].Score.uniqueId = this._fetcher.Scores[i].UniqueId;
                        _children[i].gameObject.SetActive(true);
                    }
                    else {
                        _children[i].gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2RankingOwnScoreList
    {
        private Gs2RankingCategoryModelContext _context;
        private Gs2RankingOwnScoreListFetcher _fetcher;

        public void Awake()
        {
            _context = GetComponentInParent<Gs2RankingCategoryModelContext>();
            _fetcher = GetComponentInParent<Gs2RankingOwnScoreListFetcher>();

            _children = new List<Gs2RankingOwnScoreContext>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.Score = OwnScore.New(
                    _context.CategoryModel,
                    "",
                    ""
                );
                node.gameObject.SetActive(false);
                _children.Add(node);
            }
            this.prefab.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2RankingOwnScoreList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2RankingOwnScoreList
    {
        public Gs2RankingOwnScoreContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2RankingOwnScoreList
    {

    }
}