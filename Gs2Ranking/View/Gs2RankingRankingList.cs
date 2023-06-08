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
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Ranking.Context;
using Gs2.Unity.UiKit.Gs2Ranking.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Ranking
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Ranking/Ranking/View/Gs2RankingRankingList")]
    public partial class Gs2RankingRankingList : MonoBehaviour
    {
        private List<Gs2RankingRankingContext> _children;

        public void Update() {
            if (_fetcher.Fetched && this._fetcher.Rankings != null) {
                for (var i = 0; i < this.maximumItems; i++) {
                    if (i < this._fetcher.Rankings.Count) {
                        _children[i].Ranking.index = this._fetcher.Rankings[i].Index;
                        _children[i].Ranking.User.userId = this._fetcher.Rankings[i].UserId;
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

    public partial class Gs2RankingRankingList
    {
        private Gs2RankingNamespaceContext _context;
        private Gs2RankingCategoryModelContext _categoryModelContext;
        private Gs2RankingUserContext _userContext;
        private Gs2RankingRankingListFetcher _fetcher;

        public void Awake()
        {
            _context = GetComponentInParent<Gs2RankingNamespaceContext>();
            _categoryModelContext = GetComponentInParent<Gs2RankingCategoryModelContext>();
            _userContext = GetComponentInParent<Gs2RankingUserContext>();
            _fetcher = GetComponentInParent<Gs2RankingRankingListFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2RankingRankingListFetcher.");
                enabled = false;
            }

            _children = new List<Gs2RankingRankingContext>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.Ranking = Ranking.New(
                    User.New(_context.Namespace, ""),
                    this._categoryModelContext.CategoryModel,
                    0
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

    public partial class Gs2RankingRankingList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2RankingRankingList
    {
        public Gs2RankingRankingContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2RankingRankingList
    {

    }
}