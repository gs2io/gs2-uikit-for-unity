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
using Gs2.Unity.Gs2Mission.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Mission.Context;
using Gs2.Unity.UiKit.Gs2Mission.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Mission
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Mission/Counter/View/Gs2MissionOwnCounterList")]
    public partial class Gs2MissionOwnCounterList : MonoBehaviour
    {
        private List<Gs2MissionOwnCounterContext> _children;

        public void Update() {
            if (_fetcher.Fetched && _fetcher.Counters != null) {
                for (var i = 0; i < this.maximumItems; i++) {
                    if (i < this._fetcher.Counters.Count) {
                        _children[i].CounterModel.counterName = this._fetcher.Counters[i].Name;
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

    public partial class Gs2MissionOwnCounterList
    {
        private Gs2MissionNamespaceContext _context;
        private Gs2MissionOwnCounterListFetcher _fetcher;

        public void Awake()
        {
            _context = GetComponentInParent<Gs2MissionNamespaceContext>();
            _fetcher = GetComponentInParent<Gs2MissionOwnCounterListFetcher>();

            if (_context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MissionNamespaceContext.");
                enabled = false;
            }
            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MissionOwnCounterListFetcher.");
                enabled = false;
            }

            _children = new List<Gs2MissionOwnCounterContext>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.CounterModel = CounterModel.New(
                    _context.Namespace,
                    ""
                );
                node.Counter = OwnCounter.New(
                    _context.Namespace,
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

    public partial class Gs2MissionOwnCounterList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2MissionOwnCounterList
    {
        public Gs2MissionOwnCounterContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MissionOwnCounterList
    {

    }
}