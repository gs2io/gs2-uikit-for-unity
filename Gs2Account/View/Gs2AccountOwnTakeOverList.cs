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
using Gs2.Unity.Gs2Account.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Account.Context;
using Gs2.Unity.UiKit.Gs2Account.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Account
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Account/TakeOver/View/Gs2AccountOwnTakeOverList")]
    public partial class Gs2AccountOwnTakeOverList : MonoBehaviour
    {
        private List<Gs2AccountOwnTakeOverContext> _children;

        public void Update() {
            if (_fetcher.Fetched) {
                for (var i = 0; i < this.maximumItems; i++) {
                    if (i < this._fetcher.TakeOvers.Count) {
                        _children[i].TakeOver.type = this._fetcher.TakeOvers[i].Type;
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

    public partial class Gs2AccountOwnTakeOverList
    {
        private Gs2AccountOwnAccountContext _context;
        private Gs2AccountOwnTakeOverListFetcher _fetcher;

        public void Awake()
        {
            _context = GetComponentInParent<Gs2AccountOwnAccountContext>();
            _fetcher = GetComponentInParent<Gs2AccountOwnTakeOverListFetcher>();

            _children = new List<Gs2AccountOwnTakeOverContext>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.TakeOver = OwnTakeOver.New(
                    OwnAccount.New(
                        _context.Account.Namespace
                    ),
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

    public partial class Gs2AccountOwnTakeOverList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2AccountOwnTakeOverList
    {
        public Gs2AccountOwnTakeOverContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2AccountOwnTakeOverList
    {

    }
}