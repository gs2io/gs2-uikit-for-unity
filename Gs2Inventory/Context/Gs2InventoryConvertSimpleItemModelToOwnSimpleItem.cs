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
    [AddComponentMenu("GS2 UIKit/Inventory/SimpleItem/Context/Convert/Gs2InventoryConvertSimpleItemModelToOwnSimpleItem")]
    public class Gs2InventoryConvertSimpleItemModelToOwnSimpleItem : MonoBehaviour
    {
        private Gs2InventorySimpleItemModelContext _originalContext;
        private Gs2InventoryOwnSimpleItemContext _context;

        public void Awake() {
            _originalContext = GetComponent<Gs2InventorySimpleItemModelContext>() ?? GetComponentInParent<Gs2InventorySimpleItemModelContext>();
            if (_originalContext == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2InventorySimpleItemModelContext.");
                enabled = false;
            }
            _context = GetComponent<Gs2InventoryOwnSimpleItemContext>();
            if (_context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2InventoryOwnSimpleItemContext.");
                enabled = false;
            }
        }

        public bool HasError()
        {
            _originalContext = GetComponent<Gs2InventorySimpleItemModelContext>() ?? GetComponentInParent<Gs2InventorySimpleItemModelContext>(true);
            if (_originalContext == null) {
                return true;
            }
            _context = GetComponent<Gs2InventoryOwnSimpleItemContext>();
            if (_context == null) {
                return true;
            }
            return false;
        }

        public void Start() {
            _context.SetOwnSimpleItem(
                OwnSimpleItem.New(
                    _originalContext.SimpleItemModel.SimpleInventoryModel,
                    _originalContext.SimpleItemModel.itemName
                )
            );
            enabled = false;
        }
    }
}