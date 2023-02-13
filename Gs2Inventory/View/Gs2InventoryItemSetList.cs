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
using Gs2.Unity.Gs2Inventory.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Inventory.Context;
using Gs2.Unity.UiKit.Gs2Inventory.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Inventory
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Inventory/ItemSet/View/Gs2InventoryItemSetList")]
    public partial class Gs2InventoryItemSetList : MonoBehaviour
    {
        private List<Gs2InventoryItemSetContext> _children;

        public void Update() {
            if (_fetcher.Fetched) {
                for (var i = 0; i < this.maximumItems; i++) {
                    if (i < this._fetcher.ItemSets.Count) {
                        _children[i].ItemSet.itemName = this._fetcher.ItemSets[i].ItemName;
                        _children[i].ItemSet.itemSetName = this._fetcher.ItemSets[i].Name;
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

    public partial class Gs2InventoryItemSetList
    {
        private Gs2InventoryInventoryContext _context;
        private Gs2InventoryItemSetListFetcher _fetcher;

        public void Awake()
        {
            _context = GetComponentInParent<Gs2InventoryInventoryContext>();
            _fetcher = GetComponentInParent<Gs2InventoryItemSetListFetcher>();

            _children = new List<Gs2InventoryItemSetContext>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.ItemSet = ItemSet.New(
                    _context.Inventory,
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

    public partial class Gs2InventoryItemSetList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2InventoryItemSetList
    {
        public Gs2InventoryItemSetContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InventoryItemSetList
    {

    }
}