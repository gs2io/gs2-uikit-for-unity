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
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Inventory.Context;
using Gs2.Unity.UiKit.Gs2Inventory.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Inventory
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Inventory/InventoryModel/View/Gs2InventoryInventoryModelList")]
    public partial class Gs2InventoryInventoryModelList : MonoBehaviour
    {
        private List<Gs2InventoryInventoryModelContext> _children;

        public void Update() {
            if (_fetcher.Fetched && this._fetcher.InventoryModels != null) {
                for (var i = 0; i < this.maximumItems; i++) {
                    if (i < this._fetcher.InventoryModels.Count) {
                        _children[i].InventoryModel.inventoryName = this._fetcher.InventoryModels[i].Name;
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

    public partial class Gs2InventoryInventoryModelList
    {
        private Gs2InventoryNamespaceContext _context;
        private Gs2InventoryInventoryModelListFetcher _fetcher;

        public void Awake()
        {
            _context = GetComponent<Gs2InventoryNamespaceContext>() ?? GetComponentInParent<Gs2InventoryNamespaceContext>();
            _fetcher = GetComponent<Gs2InventoryInventoryModelListFetcher>() ?? GetComponentInParent<Gs2InventoryInventoryModelListFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2InventoryInventoryModelListFetcher.");
                enabled = false;
            }

            _children = new List<Gs2InventoryInventoryModelContext>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.InventoryModel = InventoryModel.New(
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

    public partial class Gs2InventoryInventoryModelList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2InventoryInventoryModelList
    {
        public Gs2InventoryInventoryModelContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InventoryInventoryModelList
    {

    }
}