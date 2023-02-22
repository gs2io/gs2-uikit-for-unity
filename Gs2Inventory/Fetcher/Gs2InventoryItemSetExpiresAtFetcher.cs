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

using System;
using System.Collections.Generic;
using Gs2.Unity.UiKit.Gs2Inventory.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Inventory
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Inventory/ItemSet/Fetcher/Properties/ExpiresAt/Gs2InventoryItemSetExpiresAtFetcher")]
    public partial class Gs2InventoryItemSetExpiresAtFetcher : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.ItemSet != null)
            {
                onUpdate?.Invoke(
                    _fetcher.ItemSet[index].ExpiresAt
                );
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2InventoryItemSetExpiresAtFetcher
    {
        private Gs2InventoryOwnItemSetFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponentInParent<Gs2InventoryOwnItemSetFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2InventoryItemSetExpiresAtFetcher
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2InventoryItemSetExpiresAtFetcher
    {
        public int index;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InventoryItemSetExpiresAtFetcher
    {
        [Serializable]
        private class UpdateEvent : UnityEvent<long>
        {

        }

        [SerializeField]
        private UpdateEvent onUpdate = new UpdateEvent();

        public event UnityAction<long> OnUpdate
        {
            add => onUpdate.AddListener(value);
            remove => onUpdate.RemoveListener(value);
        }
    }
}