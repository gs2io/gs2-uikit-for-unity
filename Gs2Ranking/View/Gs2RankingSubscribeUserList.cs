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

    [AddComponentMenu("GS2 UIKit/Ranking/SubscribeUser/View/Gs2RankingSubscribeUserList")]
    public partial class Gs2RankingSubscribeUserList : MonoBehaviour
    {
        private List<Gs2RankingSubscribeUserContext> _children;

        public void Update() {
            if (_fetcher.Fetched) {
                for (var i = 0; i < this.maximumItems; i++) {
                    if (i < this._fetcher.SubscribeUsers.Count) {
                        _children[i].SubscribeUser.targetUserId = this._fetcher.SubscribeUsers[i].TargetUserId;
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
    
    public partial class Gs2RankingSubscribeUserList
    {
        private Gs2RankingCategoryModelContext _context;
        private Gs2RankingUserContext _userContext;
        private Gs2RankingSubscribeUserListFetcher _fetcher;

        public void Awake()
        {
            _context = GetComponentInParent<Gs2RankingCategoryModelContext>();
            _userContext = GetComponentInParent<Gs2RankingUserContext>();
            _fetcher = GetComponentInParent<Gs2RankingSubscribeUserListFetcher>();

            _children = new List<Gs2RankingSubscribeUserContext>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.SubscribeUser = SubscribeUser.New(
                    _userContext.User,
                    this._context.CategoryModel.categoryName,
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

    public partial class Gs2RankingSubscribeUserList
    {
        
    }
    
    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2RankingSubscribeUserList
    {
        public Gs2RankingSubscribeUserContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2RankingSubscribeUserList
    {
        
    }
}