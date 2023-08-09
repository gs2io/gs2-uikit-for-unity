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
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantAssignment
// ReSharper disable NotAccessedVariable
// ReSharper disable RedundantUsingDirective
// ReSharper disable Unity.NoNullPropagation
// ReSharper disable InconsistentNaming

#pragma warning disable CS0472

using System.Collections.Generic;
using Gs2.Unity.Gs2Matchmaking.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Matchmaking.Context;
using Gs2.Unity.UiKit.Gs2Matchmaking.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Matchmaking
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Matchmaking/Rating/View/Gs2MatchmakingOwnRatingList")]
    public partial class Gs2MatchmakingOwnRatingList : MonoBehaviour
    {
        private List<Gs2MatchmakingOwnRatingContext> _children;

        public void Update() {
            if (_fetcher.Fetched && this._fetcher.Ratings != null) {
                for (var i = 0; i < this.maximumItems; i++) {
                    if (i < this._fetcher.Ratings.Count) {
                        _children[i].RatingModel.ratingName = this._fetcher.Ratings[i].Name;
                        _children[i].Rating.ratingName = this._fetcher.Ratings[i].Name;
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

    public partial class Gs2MatchmakingOwnRatingList
    {
        private Gs2MatchmakingOwnRatingListFetcher _fetcher;
        private Gs2MatchmakingNamespaceContext Context => _fetcher.Context;

        public void Awake()
        {
            if (prefab == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MatchmakingOwnRatingContext Prefab.");
                enabled = false;
                return;
            }

            _fetcher = GetComponent<Gs2MatchmakingOwnRatingListFetcher>() ?? GetComponentInParent<Gs2MatchmakingOwnRatingListFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MatchmakingOwnRatingListFetcher.");
                enabled = false;
            }

            var context = GetComponent<Gs2MatchmakingNamespaceContext>() ?? GetComponentInParent<Gs2MatchmakingNamespaceContext>(true);
            if (context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MatchmakingOwnRatingListFetcher::Context.");
                enabled = false;
                return;
            }

            _children = new List<Gs2MatchmakingOwnRatingContext>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.RatingModel = RatingModel.New(
                    context.Namespace,
                    ""
                );
                node.Rating = OwnRating.New(
                    context.Namespace,
                    ""
                );
                node.gameObject.SetActive(false);
                _children.Add(node);
            }
            this.prefab.gameObject.SetActive(false);
        }

        public virtual bool HasError()
        {
            _fetcher = GetComponent<Gs2MatchmakingOwnRatingListFetcher>() ?? GetComponentInParent<Gs2MatchmakingOwnRatingListFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2MatchmakingOwnRatingList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2MatchmakingOwnRatingList
    {
        public Gs2MatchmakingOwnRatingContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MatchmakingOwnRatingList
    {

    }
}