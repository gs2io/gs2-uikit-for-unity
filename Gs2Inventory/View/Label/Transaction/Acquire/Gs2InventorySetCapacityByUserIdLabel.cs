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

	[AddComponentMenu("GS2 UIKit/Inventory/Inventory/View/Label/Transaction/Gs2InventorySetCapacityByUserIdLabel")]
    public partial class Gs2InventorySetCapacityByUserIdLabel : MonoBehaviour
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
                            "{newCapacityValue}",
                            $"{_fetcher.Request.NewCapacityValue}"
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
                            "{newCapacityValue}",
                            $"{_fetcher.Request.NewCapacityValue}"
                        )
                    );
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2InventorySetCapacityByUserIdLabel
    {
        private Gs2InventorySetCapacityByUserIdFetcher _fetcher;
        private Gs2InventoryOwnInventoryFetcher _userDataFetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2InventorySetCapacityByUserIdFetcher>() ?? GetComponentInParent<Gs2InventorySetCapacityByUserIdFetcher>();
            _userDataFetcher = GetComponent<Gs2InventoryOwnInventoryFetcher>() ?? GetComponentInParent<Gs2InventoryOwnInventoryFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2InventorySetCapacityByUserIdFetcher.");
                enabled = false;
            }

            Update();
        }

        public virtual bool HasError()
        {
            _fetcher = GetComponent<Gs2InventorySetCapacityByUserIdFetcher>() ?? GetComponentInParent<Gs2InventorySetCapacityByUserIdFetcher>(true);
            _userDataFetcher = GetComponent<Gs2InventoryOwnInventoryFetcher>() ?? GetComponentInParent<Gs2InventoryOwnInventoryFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2InventorySetCapacityByUserIdLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2InventorySetCapacityByUserIdLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InventorySetCapacityByUserIdLabel
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