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

#if GS2_ENABLE_LOCALIZATION

using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Inventory.Fetcher;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

namespace Gs2.Unity.UiKit.Gs2Inventory.Localization
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Inventory/ItemSet/View/Gs2InventoryItemSetLocalizationVariables")]
    public partial class Gs2InventoryItemSetLocalizationVariables : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched) {
                target.StringReference["itemSetId"] = new StringVariable {
                    Value = _fetcher?.ItemSet?[index].ItemSetId ?? "",
                };
                target.StringReference["name"] = new StringVariable {
                    Value = _fetcher?.ItemSet?[index].Name ?? "",
                };
                target.StringReference["inventoryName"] = new StringVariable {
                    Value = _fetcher?.ItemSet?[index].InventoryName ?? "",
                };
                target.StringReference["itemName"] = new StringVariable {
                    Value = _fetcher?.ItemSet?[index].ItemName ?? "",
                };
                target.StringReference["count"] = new LongVariable {
                    Value = _fetcher?.ItemSet?[index].Count ?? 0,
                };
                target.StringReference["sortValue"] = new IntVariable {
                    Value = _fetcher?.ItemSet?[index].SortValue ?? 0,
                };
                target.StringReference["expiresAt"] = new LongVariable {
                    Value = _fetcher?.ItemSet?[index].ExpiresAt ?? 0,
                };
                enabled = false;
                target.enabled = true;
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2InventoryItemSetLocalizationVariables
    {
        private Gs2InventoryOwnItemSetFetcher _fetcher;

        public void Awake() {
            target.enabled = false;
            _fetcher = GetComponent<Gs2InventoryOwnItemSetFetcher>() ?? GetComponentInParent<Gs2InventoryOwnItemSetFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2InventoryItemSetFetcher.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2InventoryItemSetLocalizationVariables
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2InventoryItemSetLocalizationVariables
    {
        public LocalizeStringEvent target;

        public int index;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InventoryItemSetLocalizationVariables
    {

    }
}

#endif