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

using System;
using Gs2.Gs2Inventory.Request;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Inventory.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Inventory.Label
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Inventory/Inventory/View/Label/Transaction/Gs2InventoryAddCapacityByUserIdLabel")]
    public partial class Gs2InventoryAddCapacityByUserIdLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Request != null &&
                    _userDataFetcher != null && _userDataFetcher.Fetched && _userDataFetcher.Inventory != null) {
                {
                    onUpdate?.Invoke(
                        format.Replace(
                            "{namespaceName}",
                            $"{_fetcher.Request.NamespaceName}"
                        ).Replace(
                            "{inventoryName}",
                            $"{_fetcher.Request.InventoryName}"
                        ).Replace(
                            "{userId}",
                            $"{_fetcher.Request.UserId}"
                        ).Replace(
                            "{addCapacityValue}",
                            $"{_fetcher.Request.AddCapacityValue}"
                        ).Replace(
                            "{userData:inventoryId}",
                            $"{_userDataFetcher.Inventory.InventoryId}"
                        ).Replace(
                            "{userData:inventoryName}",
                            $"{_userDataFetcher.Inventory.InventoryName}"
                        ).Replace(
                            "{userData:currentInventoryCapacityUsage}",
                            $"{_userDataFetcher.Inventory.CurrentInventoryCapacityUsage}"
                        ).Replace(
                            "{userData:currentInventoryMaxCapacity}",
                            $"{_userDataFetcher.Inventory.CurrentInventoryMaxCapacity}"
                        ).Replace(
                            "{userData:currentInventoryMaxCapacity:changed}",
                            $"{_userDataFetcher.Inventory.CurrentInventoryMaxCapacity + _fetcher.Request.AddCapacityValue}"
                        )
                    );
                }
            } else if (_fetcher.Fetched && _fetcher.Request != null) {
                {
                    onUpdate?.Invoke(
                        format.Replace(
                            "{namespaceName}",
                            $"{_fetcher.Request.NamespaceName}"
                        ).Replace(
                            "{inventoryName}",
                            $"{_fetcher.Request.InventoryName}"
                        ).Replace(
                            "{userId}",
                            $"{_fetcher.Request.UserId}"
                        ).Replace(
                            "{addCapacityValue}",
                            $"{_fetcher.Request.AddCapacityValue}"
                        )
                    );
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2InventoryAddCapacityByUserIdLabel
    {
        private Gs2InventoryAddCapacityByUserIdFetcher _fetcher;
        private Gs2InventoryOwnInventoryFetcher _userDataFetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2InventoryAddCapacityByUserIdFetcher>() ?? GetComponentInParent<Gs2InventoryAddCapacityByUserIdFetcher>();
            _userDataFetcher = GetComponent<Gs2InventoryOwnInventoryFetcher>() ?? GetComponentInParent<Gs2InventoryOwnInventoryFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2InventoryAddCapacityByUserIdFetcher.");
                enabled = false;
            }
            if (_userDataFetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2InventoryOwnInventoryFetcher.");
                enabled = false;
            }

            Update();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2InventoryAddCapacityByUserIdLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2InventoryAddCapacityByUserIdLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InventoryAddCapacityByUserIdLabel
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