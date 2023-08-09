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

using System;
using System.Collections.Generic;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Inventory.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Inventory
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Inventory/ItemSet/View/SpriteSwitcher/Properties/SortValue/Gs2InventoryItemSetSortValueSpriteSwitcher")]
    public partial class Gs2InventoryItemSetSortValueSpriteSwitcher : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.ItemSet != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        if (applySortValues.Contains(_fetcher.ItemSet[index].SortValue)) {
                            this.onUpdate.Invoke(this.sprite);
                        }
                        break;
                    case Expression.NotIn:
                        if (!applySortValues.Contains(_fetcher.ItemSet[index].SortValue)) {
                            this.onUpdate.Invoke(this.sprite);
                        }
                        break;
                    case Expression.Less:
                        if (applySortValue > _fetcher.ItemSet[index].SortValue) {
                            this.onUpdate.Invoke(this.sprite);
                        }
                        break;
                    case Expression.LessEqual:
                        if (applySortValue >= _fetcher.ItemSet[index].SortValue) {
                            this.onUpdate.Invoke(this.sprite);
                        }
                        break;
                    case Expression.Greater:
                        if (applySortValue < _fetcher.ItemSet[index].SortValue) {
                            this.onUpdate.Invoke(this.sprite);
                        }
                        break;
                    case Expression.GreaterEqual:
                        if (applySortValue <= _fetcher.ItemSet[index].SortValue) {
                            this.onUpdate.Invoke(this.sprite);
                        }
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2InventoryItemSetSortValueSpriteSwitcher
    {
        private Gs2InventoryOwnItemSetFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2InventoryOwnItemSetFetcher>() ?? GetComponentInParent<Gs2InventoryOwnItemSetFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2InventoryOwnItemSetFetcher.");
                enabled = false;
            }
            if (sprite == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: sprite is not set.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            _fetcher = GetComponent<Gs2InventoryOwnItemSetFetcher>() ?? GetComponentInParent<Gs2InventoryOwnItemSetFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            if (sprite == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2InventoryItemSetSortValueSpriteSwitcher
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2InventoryItemSetSortValueSpriteSwitcher
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

        public List<int> applySortValues;

        public int applySortValue;

        public int index;

        public Sprite sprite;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InventoryItemSetSortValueSpriteSwitcher
    {
        [Serializable]
        private class UpdateEvent : UnityEvent<Sprite>
        {

        }

        [SerializeField]
        private UpdateEvent onUpdate = new UpdateEvent();

        public event UnityAction<Sprite> OnUpdate
        {
            add => onUpdate.AddListener(value);
            remove => onUpdate.RemoveListener(value);
        }
    }
}