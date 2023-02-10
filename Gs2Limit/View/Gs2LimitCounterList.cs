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

using System.Collections.Generic;
using Gs2.Unity.Gs2Limit.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Limit.Context;
using Gs2.Unity.UiKit.Gs2Limit.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Limit
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Limit/Counter/View/Gs2LimitCounterList")]
    public partial class Gs2LimitCounterList : MonoBehaviour
    {
        private List<Gs2LimitCounterContext> _children;

        public void Update() {
            if (_fetcher.Fetched) {
                for (var i = 0; i < this.maximumItems; i++) {
                    if (i < this._fetcher.Counters.Count) {
                        _children[i].Counter.limitName = this._fetcher.Counters[i].LimitName;
                        _children[i].Counter.counterName = this._fetcher.Counters[i].Name;
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

    public partial class Gs2LimitCounterList
    {
        private Gs2LimitUserContext _context;
        private Gs2LimitCounterListFetcher _fetcher;

        public void Awake()
        {
            _context = GetComponentInParent<Gs2LimitUserContext>();
            _fetcher = GetComponentInParent<Gs2LimitCounterListFetcher>();

            _children = new List<Gs2LimitCounterContext>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.Counter = Counter.New(
                    _context.User,
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

    public partial class Gs2LimitCounterList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2LimitCounterList
    {
        public Gs2LimitCounterContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2LimitCounterList
    {

    }
}