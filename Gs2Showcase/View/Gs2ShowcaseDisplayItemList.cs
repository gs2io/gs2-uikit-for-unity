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
using Gs2.Unity.Gs2Showcase.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Showcase.Context;
using Gs2.Unity.UiKit.Gs2Showcase.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Showcase
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Showcase/DisplayItem/View/Gs2ShowcaseDisplayItemList")]
    public partial class Gs2ShowcaseDisplayItemList : MonoBehaviour
    {
        private List<Gs2ShowcaseDisplayItemContext> _children;

        public void Update() {
            if (_fetcher.Fetched && this._fetcher.Showcase.DisplayItems != null) {
                for (var i = 0; i < this.maximumItems; i++) {
                    if (i < this._fetcher.Showcase.DisplayItems.Count) {
                        _children[i].DisplayItem.displayItemId = this._fetcher.Showcase.DisplayItems[i].DisplayItemId;
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

    public partial class Gs2ShowcaseDisplayItemList
    {
        private Gs2ShowcaseShowcaseContext _context;
        private Gs2ShowcaseShowcaseFetcher _fetcher;

        public void Awake()
        {
            _context = GetComponentInParent<Gs2ShowcaseShowcaseContext>();
            _fetcher = GetComponentInParent<Gs2ShowcaseShowcaseFetcher>();

            _children = new List<Gs2ShowcaseDisplayItemContext>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.DisplayItem = DisplayItem.New(
                    _context.Showcase,
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

    public partial class Gs2ShowcaseDisplayItemList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2ShowcaseDisplayItemList
    {
        public Gs2ShowcaseDisplayItemContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ShowcaseDisplayItemList
    {

    }
}