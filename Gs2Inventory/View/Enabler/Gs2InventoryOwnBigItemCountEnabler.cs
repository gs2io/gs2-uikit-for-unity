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
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Inventory.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Inventory
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Inventory/BigItem/View/Enabler/Properties/Count/Gs2InventoryOwnBigItemCountEnabler")]
    public partial class Gs2InventoryOwnBigItemCountEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.BigItem != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableCounts.Contains(_fetcher.BigItem.Count.ToString("0")));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableCounts.Contains(_fetcher.BigItem.Count.ToString("0")));
                        break;
                    case Expression.Less:
                        target.SetActive(decimal.Parse(enableCount) > _fetcher.BigItem.Count);
                        break;
                    case Expression.LessEqual:
                        target.SetActive(decimal.Parse(enableCount) >= _fetcher.BigItem.Count);
                        break;
                    case Expression.Greater:
                        target.SetActive(decimal.Parse(enableCount) < _fetcher.BigItem.Count);
                        break;
                    case Expression.GreaterEqual:
                        target.SetActive(decimal.Parse(enableCount) <= _fetcher.BigItem.Count);
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

    public partial class Gs2InventoryOwnBigItemCountEnabler
    {
        private Gs2InventoryOwnBigItemFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2InventoryOwnBigItemFetcher>() ?? GetComponentInParent<Gs2InventoryOwnBigItemFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2InventoryOwnBigItemFetcher.");
                enabled = false;
            }
            if (target == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: target is not set.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            _fetcher = GetComponent<Gs2InventoryOwnBigItemFetcher>() ?? GetComponentInParent<Gs2InventoryOwnBigItemFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            if (target == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2InventoryOwnBigItemCountEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2InventoryOwnBigItemCountEnabler
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

        public List<string> enableCounts;

        public string enableCount;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InventoryOwnBigItemCountEnabler
    {
        
    }
}