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
using System.Collections;
using System.Linq;
using Gs2.Core.Exception;
using Gs2.Gs2Inventory.Request;
using Gs2.Unity.Core.Exception;
using Gs2.Unity.Gs2Inventory.Model;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.Util;
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
        private EzItemSet[] _itemSets;
        
        public IEnumerator Process() {
            while (true) {
                yield return new WaitForSeconds(0.1f);

                if (_fetcher.Fetched && _fetcher.AcquireAction != null &&
                    _fetcher.AcquireAction.Action == "Gs2Inventory:AcquireItemSetByUserId") {
                    var request =
                        AcquireItemSetByUserIdRequest.FromJson(
                            JsonMapper.ToObject(_fetcher.AcquireAction.Request));
                    var future = this._clientHolder.Gs2.Inventory.Namespace(
                        request.NamespaceName
                    ).Me(
                        this._sessionHolder.GameSession
                    ).Inventory(
                        request.InventoryName
                    ).ItemSet(
                        request.ItemName,
                        null
                    ).Model();
                    yield return future;
                    if (future.Error != null) {
                        this.onError.Invoke(new CanIgnoreException(future.Error), null);
                    }
                    _itemSets = future.Result;
                }
            }
        }

        public void OnEnable() {
            StartCoroutine(nameof(Process));
        }
        
        public void Update()
        {
            if (_itemSets != null) {
                {
                    var request =
                        AcquireItemSetByUserIdRequest.FromJson(
                            JsonMapper.ToObject(_fetcher.AcquireAction.Request));
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
                            _itemSets.Length == 0 ? "" : $"{_itemSets[0].ItemSetId}"
                        ).Replace(
                            "{userData:name}",
                            _itemSets.Length == 0 ? "" : $"{_itemSets[0].Name}"
                        ).Replace(
                            "{userData:inventoryName}",
                            _itemSets.Length == 0 ? "" : $"{_itemSets[0].InventoryName}"
                        ).Replace(
                            "{userData:itemName}",
                            _itemSets.Length == 0 ? "" : $"{_itemSets[0].ItemName}"
                        ).Replace(
                            "{userData:count}",
                            _itemSets.Length == 0 ? "0" : $"{_itemSets.Sum(v => v.Count)}"
                        ).Replace(
                            "{userData:sortValue}",
                            _itemSets.Length == 0 ? "0" : $"{_itemSets[0].SortValue}"
                        ).Replace(
                            "{userData:expiresAt}",
                            _itemSets.Length == 0 ? "0" : $"{_itemSets[0].ExpiresAt}"
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
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _sessionHolder;
        private Gs2CoreAcquireActionFetcher _fetcher;

        public void Awake()
        {
            _clientHolder = Gs2ClientHolder.Instance;
            _sessionHolder = Gs2GameSessionHolder.Instance;
            _fetcher = GetComponentInParent<Gs2CoreAcquireActionFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2CoreAcquireActionFetcher.");
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

        [SerializeField]
        internal ErrorEvent onError = new ErrorEvent();

        public event UnityAction<Gs2Exception, Func<IEnumerator>> OnError
        {
            add => this.onError.AddListener(value);
            remove => this.onError.RemoveListener(value);
        }
    }
}