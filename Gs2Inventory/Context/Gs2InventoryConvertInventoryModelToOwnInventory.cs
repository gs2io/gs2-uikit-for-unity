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
using Gs2.Unity.Gs2Inventory.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Inventory.Context
{
    [AddComponentMenu("GS2 UIKit/Inventory/Inventory/Context/Convert/Gs2InventoryConvertInventoryModelToOwnInventory")]
    public class Gs2InventoryConvertInventoryModelToOwnInventory : MonoBehaviour
    {
        private Gs2InventoryInventoryModelContext _context;
        
        public void Awake() {
            _context = GetComponent<Gs2InventoryInventoryModelContext>() ?? GetComponentInParent<Gs2InventoryInventoryModelContext>();

            if (_context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2InventoryInventoryModelContext.");
                enabled = false;
            }
        }
        
        public void Start() {
            this.onConverted.Invoke(
                OwnInventory.New(
                    _context.InventoryModel.Namespace,
                    _context.InventoryModel.inventoryName
                )
            );
            enabled = false;
        }
        
        [Serializable]
        private class ConvertEvent : UnityEvent<OwnInventory>
        {

        }

        [SerializeField]
        private ConvertEvent onConverted = new ConvertEvent();

        public event UnityAction<OwnInventory> OnConvert
        {
            add => onConverted.AddListener(value);
            remove => onConverted.RemoveListener(value);
        }
    }
}