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
using Gs2.Unity.UiKit.Gs2Inventory.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Inventory
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Inventory/ItemModel/View/Properties/SortValue/Gs2InventoryItemModelSortValueEnabler")]
    public partial class Gs2InventoryItemModelSortValueEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableSortValues.Contains(_fetcher.ItemModel.SortValue));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableSortValues.Contains(_fetcher.ItemModel.SortValue));
                        break;
                    case Expression.Less:
                        target.SetActive(enableSortValue < _fetcher.ItemModel.SortValue);
                        break;
                    case Expression.LessEqual:
                        target.SetActive(enableSortValue <= _fetcher.ItemModel.SortValue);
                        break;
                    case Expression.Greater:
                        target.SetActive(enableSortValue > _fetcher.ItemModel.SortValue);
                        break;
                    case Expression.GreaterEqual:
                        target.SetActive(enableSortValue >= _fetcher.ItemModel.SortValue);
                        break;
                }
            }
            else 
            {
                target.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2InventoryItemModelSortValueEnabler
    {
        private Gs2InventoryItemModelFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponentInParent<Gs2InventoryItemModelFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2InventoryItemModelSortValueEnabler
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2InventoryItemModelSortValueEnabler
    {
        public enum Expression {
            In,
            NotIn,
            Less,
            LessEqual,
            Greater,
            GreaterEqual,
        }

        public Expression expression;

        public List<int> enableSortValues;

        public int enableSortValue;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InventoryItemModelSortValueEnabler
    {
        
    }
}