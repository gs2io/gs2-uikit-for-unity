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
// ReSharper disable CheckLotteryModel
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantAssignment
// ReSharper disable NotAccessedVariable
// ReSharper disable RedundantUsingDirective
// ReSharper disable Unity.NoNullPropagation
// ReSharper disable InconsistentNaming

#pragma warning disable CS0472

using System.Collections.Generic;
using Gs2.Unity.Gs2Lottery.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Lottery.Context;
using Gs2.Unity.UiKit.Gs2Lottery.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Lottery
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Lottery/DrawnPrize/View/Gs2LotteryOwnDrawnPrizeList")]
    public partial class Gs2LotteryOwnDrawnPrizeList : MonoBehaviour
    {
        private List<Gs2LotteryOwnDrawnPrizeContext> _children;

        public void Update() {
            if (_fetcher.Fetched && this._fetcher.DrawnPrizes != null) {
                for (var i = 0; i < this.maximumItems; i++) {
                    if (i < this._fetcher.DrawnPrizes.Count) {
                        _children[i].DrawnPrize.index = i;
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

    public partial class Gs2LotteryOwnDrawnPrizeList
    {
        private Gs2LotteryOwnDrawnPrizeListFetcher _fetcher;
        private Gs2LotteryLotteryModelContext Context => _fetcher.Context;

        public void Awake()
        {
            if (prefab == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2LotteryOwnDrawnPrizeContext Prefab.");
                enabled = false;
                return;
            }

            _fetcher = GetComponent<Gs2LotteryOwnDrawnPrizeListFetcher>() ?? GetComponentInParent<Gs2LotteryOwnDrawnPrizeListFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2LotteryOwnDrawnPrizeListFetcher.");
                enabled = false;
            }

            var context = GetComponent<Gs2LotteryLotteryModelContext>() ?? GetComponentInParent<Gs2LotteryLotteryModelContext>(true);
            if (context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2LotteryOwnDrawnPrizeListFetcher::Context.");
                enabled = false;
                return;
            }

            _children = new List<Gs2LotteryOwnDrawnPrizeContext>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.DrawnPrize = OwnDrawnPrize.New(
                    context.LotteryModel.Namespace,
                    0
                );
                node.gameObject.SetActive(false);
                _children.Add(node);
            }
            this.prefab.gameObject.SetActive(false);
        }

        public bool HasError()
        {
            _fetcher = GetComponent<Gs2LotteryOwnDrawnPrizeListFetcher>() ?? GetComponentInParent<Gs2LotteryOwnDrawnPrizeListFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2LotteryOwnDrawnPrizeList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2LotteryOwnDrawnPrizeList
    {
        public Gs2LotteryOwnDrawnPrizeContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2LotteryOwnDrawnPrizeList
    {

    }
}