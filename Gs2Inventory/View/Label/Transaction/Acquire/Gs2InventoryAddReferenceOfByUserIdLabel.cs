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

	[AddComponentMenu("GS2 UIKit/Inventory/ReferenceOf/View/Label/Transaction/Gs2InventoryAddReferenceOfByUserIdLabel")]
    public partial class Gs2InventoryAddReferenceOfByUserIdLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Request != null) {
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
                            "{itemName}",
                            $"{_fetcher.Request.ItemName}"
                        ).Replace(
                            "{itemSetName}",
                            $"{_fetcher.Request.ItemSetName}"
                        ).Replace(
                            "{referenceOf}",
                            $"{_fetcher.Request.ReferenceOf}"
                        )
                    );
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2InventoryAddReferenceOfByUserIdLabel
    {
        private Gs2InventoryAddReferenceOfByUserIdFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2InventoryAddReferenceOfByUserIdFetcher>() ?? GetComponentInParent<Gs2InventoryAddReferenceOfByUserIdFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2InventoryAddReferenceOfByUserIdFetcher.");
                enabled = false;
            }

            Update();
        }

        public virtual bool HasError()
        {
            _fetcher = GetComponent<Gs2InventoryAddReferenceOfByUserIdFetcher>() ?? GetComponentInParent<Gs2InventoryAddReferenceOfByUserIdFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2InventoryAddReferenceOfByUserIdLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2InventoryAddReferenceOfByUserIdLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InventoryAddReferenceOfByUserIdLabel
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