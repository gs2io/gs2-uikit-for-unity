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
using System.Linq;
using Gs2.Gs2Inventory.Request;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Inventory.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Inventory
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Inventory/ItemSet/View/Transaction/Gs2InventoryAcquireItemSetByUserIdLabel")]
    public partial class Gs2InventoryAcquireItemSetByUserIdLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.AcquireAction != null && _fetcher.AcquireAction.Action == "Gs2Inventory:AcquireItemSetByUserId" &&
                    _userDataFetcher.Fetched && _userDataFetcher.ItemSet != null) {
                var request = AcquireItemSetByUserIdRequest.FromJson(JsonMapper.ToObject(_fetcher.AcquireAction.Request));
                {
                    onUpdate?.Invoke(
                        format.Replace(
                            "{namespaceName}",
                            $"{request.NamespaceName}"
                        ).Replace(
                            "{inventoryName}",
                            $"{request.InventoryName}"
                        ).Replace(
                            "{itemName}",
                            $"{request.ItemName}"
                        ).Replace(
                            "{userId}",
                            $"{request.UserId}"
                        ).Replace(
                            "{acquireCount}",
                            $"{request.AcquireCount}"
                        ).Replace(
                            "{expiresAt}",
                            $"{request.ExpiresAt}"
                        ).Replace(
                            "{createNewItemSet}",
                            $"{request.CreateNewItemSet}"
                        ).Replace(
                            "{itemSetName}",
                            $"{request.ItemSetName}"
                        ).Replace(
                            "{userData:itemSetId}",
                            $"{_userDataFetcher.ItemSet[0].ItemSetId}"
                        ).Replace(
                            "{userData:name}",
                            $"{_userDataFetcher.ItemSet[0].Name}"
                        ).Replace(
                            "{userData:inventoryName}",
                            $"{_userDataFetcher.ItemSet[0].InventoryName}"
                        ).Replace(
                            "{userData:itemName}",
                            $"{_userDataFetcher.ItemSet[0].ItemName}"
                        ).Replace(
                            "{userData:count}",
                            $"{_userDataFetcher.ItemSet.Sum(v => v.Count)}"
                        ).Replace(
                            "{userData:sortValue}",
                            $"{_userDataFetcher.ItemSet[0].SortValue}"
                        ).Replace(
                            "{userData:expiresAt}",
                            $"{_userDataFetcher.ItemSet[0].ExpiresAt}"
                        )
                    );
                }
            } else if (_fetcher.Fetched && _fetcher.AcquireAction != null && _fetcher.AcquireAction.Action == "Gs2Inventory:AcquireItemSetByUserId") {
                var request = AcquireItemSetByUserIdRequest.FromJson(JsonMapper.ToObject(_fetcher.AcquireAction.Request));
                {
                    onUpdate?.Invoke(
                        format.Replace(
                            "{namespaceName}",
                            $"{request.NamespaceName}"
                        ).Replace(
                            "{inventoryName}",
                            $"{request.InventoryName}"
                        ).Replace(
                            "{itemName}",
                            $"{request.ItemName}"
                        ).Replace(
                            "{userId}",
                            $"{request.UserId}"
                        ).Replace(
                            "{acquireCount}",
                            $"{request.AcquireCount}"
                        ).Replace(
                            "{expiresAt}",
                            $"{request.ExpiresAt}"
                        ).Replace(
                            "{createNewItemSet}",
                            $"{request.CreateNewItemSet}"
                        ).Replace(
                            "{itemSetName}",
                            $"{request.ItemSetName}"
                        )
                    );
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2InventoryAcquireItemSetByUserIdLabel
    {
        private Gs2CoreAcquireActionFetcher _fetcher;
        private Gs2InventoryOwnItemSetFetcher _userDataFetcher;

        public void Awake()
        {
            _fetcher = GetComponentInParent<Gs2CoreAcquireActionFetcher>();
            _userDataFetcher = GetComponentInParent<Gs2InventoryOwnItemSetFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2CoreAcquireActionFetcher.");
                enabled = false;
            }
            if (_userDataFetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2InventoryOwnItemSetFetcher.");
                enabled = false;
            }

            Update();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2InventoryAcquireItemSetByUserIdLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2InventoryAcquireItemSetByUserIdLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InventoryAcquireItemSetByUserIdLabel
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