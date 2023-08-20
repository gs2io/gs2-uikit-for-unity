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

    [AddComponentMenu("GS2 UIKit/Inventory/BigItem/View/Gs2InventoryOwnBigItemList")]
    public partial class Gs2InventoryOwnBigItemList : MonoBehaviour
    {
        private List<Gs2InventoryOwnBigItemContext> _children;

        public void Update() {
            if (_fetcher.Fetched && this._fetcher.BigItems != null) {
                for (var i = 0; i < this.maximumItems; i++) {
                    if (i < this._fetcher.BigItems.Count) {
                        _children[i].SetOwnBigItem(
                            OwnBigItem.New(
                                OwnBigInventory.New(
                                    Namespace.New(
                                        this._fetcher.Context.BigInventoryModel.NamespaceName
                                    ),
                                    this._fetcher.Context.BigInventoryModel.InventoryName
                                ),
                                this._fetcher.BigItems[i].ItemName
                            )
                        );
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

    public partial class Gs2InventoryOwnBigItemList
    {
        private Gs2InventoryOwnBigItemListFetcher _fetcher;
        private Gs2InventoryBigInventoryModelContext Context => _fetcher.Context;

        public void Awake()
        {
            if (prefab == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2InventoryOwnBigItemContext Prefab.");
                enabled = false;
                return;
            }

            _fetcher = GetComponent<Gs2InventoryOwnBigItemListFetcher>() ?? GetComponentInParent<Gs2InventoryOwnBigItemListFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2InventoryOwnBigItemListFetcher.");
                enabled = false;
            }

            var context = GetComponent<Gs2InventoryBigInventoryModelContext>() ?? GetComponentInParent<Gs2InventoryBigInventoryModelContext>(true);
            if (context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2InventoryOwnBigItemListFetcher::Context.");
                enabled = false;
                return;
            }

            _children = new List<Gs2InventoryOwnBigItemContext>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.BigItemModel = BigItemModel.New(
                    context.BigInventoryModel,
                    ""
                );
                node.BigItem = OwnBigItem.New(
                    context.BigInventoryModel,
                    ""
                );
                node.gameObject.SetActive(false);
                _children.Add(node);
            }
            this.prefab.gameObject.SetActive(false);
        }

        public virtual bool HasError()
        {
            _fetcher = GetComponent<Gs2InventoryOwnBigItemListFetcher>() ?? GetComponentInParent<Gs2InventoryOwnBigItemListFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2InventoryOwnBigItemList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2InventoryOwnBigItemList
    {
        public Gs2InventoryOwnBigItemContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InventoryOwnBigItemList
    {

    }
}