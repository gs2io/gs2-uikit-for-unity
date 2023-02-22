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
using Gs2.Unity.Gs2Lottery.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Lottery.Context;
using Gs2.Unity.UiKit.Gs2Lottery.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Lottery
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Lottery/Probability/View/Gs2LotteryProbabilityList")]
    public partial class Gs2LotteryProbabilityList : MonoBehaviour
    {
        private List<Gs2LotteryProbabilityContext> _children;

        public void Update() {
            if (_fetcher.Fetched && this._fetcher.Probabilities != null) {
                for (var i = 0; i < this.maximumItems; i++) {
                    if (i < this._fetcher.Probabilities.Count) {
                        _children[i].Probability.prizeId = this._fetcher.Probabilities[i].Prize.PrizeId;
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

    public partial class Gs2LotteryProbabilityList
    {
        private Gs2LotteryLotteryModelContext _context;
        private Gs2LotteryProbabilityListFetcher _fetcher;

        public void Awake()
        {
            _context = GetComponentInParent<Gs2LotteryLotteryModelContext>();
            _fetcher = GetComponentInParent<Gs2LotteryProbabilityListFetcher>();

            _children = new List<Gs2LotteryProbabilityContext>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.Probability = Probability.New(
                    _context.LotteryModel,
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

    public partial class Gs2LotteryProbabilityList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2LotteryProbabilityList
    {
        public Gs2LotteryProbabilityContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2LotteryProbabilityList
    {

    }
}