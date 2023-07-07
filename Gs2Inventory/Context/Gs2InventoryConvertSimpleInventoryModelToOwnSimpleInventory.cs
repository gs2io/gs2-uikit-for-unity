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
    [AddComponentMenu("GS2 UIKit/Inventory/SimpleInventory/Context/Convert/Gs2InventoryConvertSimpleInventoryModelToOwnSimpleInventory")]
    public class Gs2InventoryConvertSimpleInventoryModelToOwnSimpleInventory : MonoBehaviour
    {
        private Gs2InventorySimpleInventoryModelContext _originalContext;
        private Gs2InventoryOwnSimpleInventoryContext _context;

        public void Awake() {
            _originalContext = GetComponent<Gs2InventorySimpleInventoryModelContext>() ?? GetComponentInParent<Gs2InventorySimpleInventoryModelContext>();
            if (_originalContext == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2InventorySimpleInventoryModelContext.");
                enabled = false;
            }
            _context = GetComponent<Gs2InventoryOwnSimpleInventoryContext>();
            if (_context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2InventoryOwnSimpleInventoryContext.");
                enabled = false;
            }
        }

        public bool HasError()
        {
            _originalContext = GetComponent<Gs2InventorySimpleInventoryModelContext>() ?? GetComponentInParent<Gs2InventorySimpleInventoryModelContext>(true);
            if (_originalContext == null) {
                return true;
            }
            _context = GetComponent<Gs2InventoryOwnSimpleInventoryContext>();
            if (_context == null) {
                return true;
            }
            return false;
        }

        public void Start() {
            _context.SetOwnSimpleInventory(
                OwnSimpleInventory.New(
                    _originalContext.SimpleInventoryModel.Namespace,
                    _originalContext.SimpleInventoryModel.inventoryName
                )
            );
            enabled = false;
        }
    }
}