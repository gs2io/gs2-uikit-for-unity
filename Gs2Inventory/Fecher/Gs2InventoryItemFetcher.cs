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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gs2.Core.Exception;
using Gs2.Unity.Core.Exception;
using Gs2.Unity.Gs2Inventory.Model;
using Gs2.Unity.Gs2Inventory.ScriptableObject;
using Gs2.Unity.Util;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Inventory.Fetcher
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Inventory/Gs2InventoryItemFetcher")]
    public partial class Gs2InventoryItemFetcher : MonoBehaviour
    {
        private IEnumerator Fetch()
        {
            Gs2Exception e;
            while (true)
            {
                if (_gameSessionHolder != null && _gameSessionHolder.Initialized && 
                    _clientHolder != null && _clientHolder.Initialized &&
                    item != null)
                {
                    {
                        var future = _clientHolder.Gs2.Inventory.Namespace(
                            item.inventory.Namespace.namespaceName
                        ).InventoryModel(
                            item.inventory.inventoryName
                        ).ItemModel(
                            item.itemName
                        ).Model();
                        yield return future;
                        if (future.Error != null)
                        {
                            if (future.Error is BadRequestException || future.Error is NotFoundException)
                            {
                                onError.Invoke(e = future.Error, null);
                                break;
                            }

                            onError.Invoke(new CanIgnoreException(future.Error), null);
                        }
                        else
                        {
                            Model = future.Result;
                        }
                    }
                    {
                        var future = _clientHolder.Gs2.Inventory.Namespace(
                            item.inventory.Namespace.namespaceName
                        ).Me(
                            _gameSessionHolder.GameSession
                        ).Inventory(
                            item.inventory.inventoryName
                        ).ItemSet(
                            item.itemName,
                            null
                        ).Model();

                        yield return future;

                        if (future.Error != null)
                        {
                            onError.Invoke(e = future.Error, null);
                            break;
                        }

                        ItemSets = future.Result.ToList();
                        if (Model != null && ItemSets != null)
                        {
                            Fetched = true;
                        }
                    }
                }

                yield return new WaitForEndOfFrame();
            }
            
            var transform1 = transform;
            var builder = new StringBuilder(transform1.name);
            var current = transform1.parent;

            while (current != null)
            {
                builder.Insert(0, current.name + "/");
                current = current.parent;
            }
            
            Debug.LogError(e);
            Debug.LogError($"{GetType()} の自動更新が停止されました。 {builder}");
            Debug.LogError($"Automatic update of {GetType()} has been stopped. {builder}");
        }

        public void OnEnable()
        {
            StartCoroutine(nameof(Fetch));
        }

        public void OnDisable()
        {
            StopCoroutine(nameof(Fetch));
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2InventoryItemFetcher
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;

        public void Awake()
        {
            _clientHolder = Gs2ClientHolder.Instance;
            _gameSessionHolder = Gs2GameSessionHolder.Instance;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2InventoryItemFetcher
    {
        public EzItemModel Model { get; private set; }
        public List<EzItemSet> ItemSets { get; private set; }
        public bool Fetched { get; private set; }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2InventoryItemFetcher
    {
        [SerializeField]
        private Item item;
        
        public Item Item
        {
            get => item;
            set
            {
                item = value;
                Fetched = false;
                Model = null;
                ItemSets = null;
            }
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InventoryItemFetcher
    {
        [SerializeField]
        internal ErrorEvent onError = new ErrorEvent();
        
        public event UnityAction<Gs2Exception, Func<IEnumerator>> OnError
        {
            add => onError.AddListener(value);
            remove => onError.RemoveListener(value);
        }
    }
}