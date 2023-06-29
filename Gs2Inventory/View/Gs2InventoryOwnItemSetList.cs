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
using Gs2.Unity.Gs2Inventory.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Inventory.Context;
using Gs2.Unity.UiKit.Gs2Inventory.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Inventory
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Inventory/ItemSet/View/Gs2InventoryOwnItemSetList")]
    public partial class Gs2InventoryOwnItemSetList : MonoBehaviour
    {
        private List<Gs2InventoryOwnItemSetContext> _children;

        public void Update() {
            if (_fetcher.Fetched && _fetcher.ItemSets != null) {
                for (var i = 0; i < this.maximumItems; i++) {
                    if (i < this._fetcher.ItemSets.Count) {
                        _children[i].ItemModel.itemName = this._fetcher.ItemSets[i].ItemName;
                        _children[i].ItemModel.itemName = this._fetcher.ItemSets[i].ItemName;
                        _children[i].itemSetName = this._fetcher.ItemSets[i].Name;
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

    public partial class Gs2InventoryOwnItemSetList
    {
        private Gs2InventoryOwnInventoryContext _context;
        private Gs2InventoryOwnItemSetListFetcher _fetcher;

        public void Awake()
        {
            _context = GetComponent<Gs2InventoryOwnInventoryContext>() ?? GetComponentInParent<Gs2InventoryOwnInventoryContext>();
            _fetcher = GetComponent<Gs2InventoryOwnItemSetListFetcher>() ?? GetComponentInParent<Gs2InventoryOwnItemSetListFetcher>();

            if (_context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2InventoryOwnInventoryContext.");
                enabled = false;
            }
            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2InventoryOwnItemSetListFetcher.");
                enabled = false;
            }

            _children = new List<Gs2InventoryOwnItemSetContext>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.ItemModel = ItemModel.New(
                    _context.InventoryModel,
                    ""
                );
                node.gameObject.SetActive(false);
                _children.Add(node);
            }
            this.prefab.gameObject.SetActive(false);
        }

        public bool HasError()
        {
            _context = GetComponent<Gs2InventoryOwnInventoryContext>() ?? GetComponentInParent<Gs2InventoryOwnInventoryContext>(true);
            _fetcher = GetComponent<Gs2InventoryOwnItemSetListFetcher>() ?? GetComponentInParent<Gs2InventoryOwnItemSetListFetcher>(true);
            if (_context == null) {
                return true;
            }
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2InventoryOwnItemSetList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2InventoryOwnItemSetList
    {
        public Gs2InventoryOwnItemSetContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InventoryOwnItemSetList
    {

    }
}