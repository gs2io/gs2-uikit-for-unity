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

    [AddComponentMenu("GS2 UIKit/Lottery/BoxItem/View/Gs2LotteryOwnBoxItemList")]
    public partial class Gs2LotteryOwnBoxItemList : MonoBehaviour
    {
        private List<Gs2LotteryOwnBoxItemContext> _children;

        public void Update() {
            if (_fetcher.Fetched && this._fetcher.BoxItemes != null) {
                for (var i = 0; i < this.maximumItems; i++) {
                    if (i < this._fetcher.BoxItemes.Count) {
                        _children[i].BoxItem.Namespace = this._fetcher.Context.BoxItems.Namespace;
                        _children[i].BoxItem.prizeTableName = this._fetcher.Context.BoxItems.PrizeTableName;
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

    public partial class Gs2LotteryOwnBoxItemList
    {
        private Gs2LotteryOwnBoxItemListFetcher _fetcher;
        private Gs2LotteryOwnBoxItemsContext Context => _fetcher.Context;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2LotteryOwnBoxItemListFetcher>() ?? GetComponentInParent<Gs2LotteryOwnBoxItemListFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2LotteryOwnBoxItemListFetcher.");
                enabled = false;
            }

            _children = new List<Gs2LotteryOwnBoxItemContext>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.BoxItem = OwnBoxItem.New(
                    _fetcher.Context.BoxItems.Namespace,
                    _fetcher.Context.BoxItems.PrizeTableName,
                    i
                );
                node.gameObject.SetActive(false);
                _children.Add(node);
            }
            this.prefab.gameObject.SetActive(false);
        }

        public bool HasError()
        {
            _fetcher = GetComponent<Gs2LotteryOwnBoxItemListFetcher>() ?? GetComponentInParent<Gs2LotteryOwnBoxItemListFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2LotteryOwnBoxItemList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2LotteryOwnBoxItemList
    {
        public Gs2LotteryOwnBoxItemContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2LotteryOwnBoxItemList
    {

    }
}