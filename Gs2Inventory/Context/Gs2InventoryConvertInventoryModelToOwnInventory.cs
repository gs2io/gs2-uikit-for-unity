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
using Gs2.Unity.Gs2Inventory.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Inventory.Context
{
    [AddComponentMenu("GS2 UIKit/Inventory/Inventory/Context/Convert/Gs2InventoryConvertInventoryModelToOwnInventory")]
    public class Gs2InventoryConvertInventoryModelToOwnInventory : MonoBehaviour
    {
        private Gs2InventoryInventoryModelContext _originalContext;
        private Gs2InventoryOwnInventoryContext _context;

        public void Awake() {
            _originalContext = GetComponent<Gs2InventoryInventoryModelContext>() ?? GetComponentInParent<Gs2InventoryInventoryModelContext>();
            if (_originalContext == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2InventoryInventoryModelContext.");
                enabled = false;
            }
            _context = GetComponent<Gs2InventoryOwnInventoryContext>();
            if (_context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2InventoryOwnInventoryContext.");
                enabled = false;
            }
        }

        public bool HasError()
        {
            _originalContext = GetComponent<Gs2InventoryInventoryModelContext>() ?? GetComponentInParent<Gs2InventoryInventoryModelContext>(true);
            if (_originalContext == null) {
                return true;
            }
            _context = GetComponent<Gs2InventoryOwnInventoryContext>();
            if (_context == null) {
                return true;
            }
            return false;
        }

        public void Start() {
            _context.SetOwnInventory(
                OwnInventory.New(
                    _originalContext.InventoryModel.Namespace,
                    _originalContext.InventoryModel.inventoryName
                )
            );
            enabled = false;
        }
    }
}