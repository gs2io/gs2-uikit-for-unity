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
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Limit.Context;
using Gs2.Unity.UiKit.Gs2Limit.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Limit
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Limit/Counter/View/Gs2LimitOwnCounterList")]
    public partial class Gs2LimitOwnCounterList : MonoBehaviour
    {
        private List<Gs2LimitOwnCounterContext> _children;

        public void Update() {
            if (_fetcher.Fetched && _fetcher.Counters != null) {
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

    public partial class Gs2LimitOwnCounterList
    {
        private Gs2LimitNamespaceContext _context;
        private Gs2LimitOwnCounterListFetcher _fetcher;

        public void Awake()
        {
            _context = GetComponentInParent<Gs2LimitNamespaceContext>();
            _fetcher = GetComponentInParent<Gs2LimitOwnCounterListFetcher>();

            if (_context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2LimitNamespaceContext.");
                enabled = false;
            }
            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2LimitOwnCounterListFetcher.");
                enabled = false;
            }

            _children = new List<Gs2LimitOwnCounterContext>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.Counter = OwnCounter.New(
                    _context.Namespace,
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

    public partial class Gs2LimitOwnCounterList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2LimitOwnCounterList
    {
        public Gs2LimitOwnCounterContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2LimitOwnCounterList
    {

    }
}