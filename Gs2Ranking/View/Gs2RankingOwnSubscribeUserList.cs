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
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantAssignment
// ReSharper disable NotAccessedVariable
// ReSharper disable RedundantUsingDirective
// ReSharper disable Unity.NoNullPropagation
// ReSharper disable InconsistentNaming

#pragma warning disable CS0472

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

    [AddComponentMenu("GS2 UIKit/Ranking/SubscribeUser/View/Gs2RankingOwnSubscribeUserList")]
    public partial class Gs2RankingOwnSubscribeUserList : MonoBehaviour
    {
        private List<Gs2RankingOwnSubscribeUserContext> _children;

        public void Update() {
            if (_fetcher.Fetched && this._fetcher.SubscribeUsers != null) {
                for (var i = 0; i < this.maximumItems; i++) {
                    if (i < this._fetcher.SubscribeUsers.Count) {
                        _children[i].SubscribeUser.categoryName = this._fetcher.Context.CategoryModel.CategoryName;
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

    public partial class Gs2RankingOwnSubscribeUserList
    {
        private Gs2RankingOwnSubscribeUserListFetcher _fetcher;
        private Gs2RankingCategoryModelContext Context => _fetcher.Context;

        public void Awake()
        {
            if (prefab == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2RankingOwnSubscribeUserContext Prefab.");
                enabled = false;
                return;
            }

            _fetcher = GetComponent<Gs2RankingOwnSubscribeUserListFetcher>() ?? GetComponentInParent<Gs2RankingOwnSubscribeUserListFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2RankingOwnSubscribeUserListFetcher.");
                enabled = false;
            }

            var context = GetComponent<Gs2RankingCategoryModelContext>() ?? GetComponentInParent<Gs2RankingCategoryModelContext>(true);
            if (context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2RankingOwnSubscribeUserListFetcher::Context.");
                enabled = false;
                return;
            }

            _children = new List<Gs2RankingOwnSubscribeUserContext>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.SubscribeUser = OwnSubscribeUser.New(
                    context.CategoryModel.Namespace,
                    context.CategoryModel.CategoryName,
                    ""
                );
                node.gameObject.SetActive(false);
                _children.Add(node);
            }
            this.prefab.gameObject.SetActive(false);
        }

        public bool HasError()
        {
            _fetcher = GetComponent<Gs2RankingOwnSubscribeUserListFetcher>() ?? GetComponentInParent<Gs2RankingOwnSubscribeUserListFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2RankingOwnSubscribeUserList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2RankingOwnSubscribeUserList
    {
        public Gs2RankingOwnSubscribeUserContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2RankingOwnSubscribeUserList
    {

    }
}