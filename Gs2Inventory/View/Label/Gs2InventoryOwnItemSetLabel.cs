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
using Gs2.Core.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Inventory.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Inventory
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Inventory/ItemSet/View/Label/Gs2InventoryOwnItemSetLabel")]
    public partial class Gs2InventoryOwnItemSetLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.ItemSet != null)
            {
                var expiresAt = _fetcher.ItemSet[index].ExpiresAt == null ? DateTime.Now : UnixTime.FromUnixTime(_fetcher.ItemSet[index].ExpiresAt).ToLocalTime();
                onUpdate?.Invoke(
                    format.Replace(
                        "{itemSetId}", $"{_fetcher?.ItemSet?[index].ItemSetId}"
                    ).Replace(
                        "{name}", $"{_fetcher?.ItemSet?[index].Name}"
                    ).Replace(
                        "{inventoryName}", $"{_fetcher?.ItemSet?[index].InventoryName}"
                    ).Replace(
                        "{itemName}", $"{_fetcher?.ItemSet?[index].ItemName}"
                    ).Replace(
                        "{count}", $"{_fetcher?.ItemSet?[index].Count}"
                    ).Replace(
                        "{sortValue}", $"{_fetcher?.ItemSet?[index].SortValue}"
                    ).Replace(
                        "{expiresAt:yyyy}", expiresAt.ToString("yyyy")
                    ).Replace(
                        "{expiresAt:yy}", expiresAt.ToString("yy")
                    ).Replace(
                        "{expiresAt:MM}", expiresAt.ToString("MM")
                    ).Replace(
                        "{expiresAt:MMM}", expiresAt.ToString("MMM")
                    ).Replace(
                        "{expiresAt:dd}", expiresAt.ToString("dd")
                    ).Replace(
                        "{expiresAt:hh}", expiresAt.ToString("hh")
                    ).Replace(
                        "{expiresAt:HH}", expiresAt.ToString("HH")
                    ).Replace(
                        "{expiresAt:tt}", expiresAt.ToString("tt")
                    ).Replace(
                        "{expiresAt:mm}", expiresAt.ToString("mm")
                    ).Replace(
                        "{expiresAt:ss}", expiresAt.ToString("ss")
                    ).Replace(
                        "{referenceOf}", $"{_fetcher?.ItemSet?[index].ReferenceOf}"
                    )
                );
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2InventoryOwnItemSetLabel
    {
        private Gs2InventoryOwnItemSetFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2InventoryOwnItemSetFetcher>() ?? GetComponentInParent<Gs2InventoryOwnItemSetFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2InventoryOwnItemSetFetcher.");
                enabled = false;
            }

            Update();
        }

        public virtual bool HasError()
        {
            _fetcher = GetComponent<Gs2InventoryOwnItemSetFetcher>() ?? GetComponentInParent<Gs2InventoryOwnItemSetFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2InventoryOwnItemSetLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2InventoryOwnItemSetLabel
    {
        public string format;

        public int index;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InventoryOwnItemSetLabel
    {
        [Serializable]
        private class UpdateEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private UpdateEvent onUpdate = new UpdateEvent();

        public event UnityAction<string> OnUpdate
        {
            add => onUpdate.AddListener(value);
            remove => onUpdate.RemoveListener(value);
        }
    }
}