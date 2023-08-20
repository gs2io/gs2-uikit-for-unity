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

using Gs2.Unity.Gs2Inventory.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Inventory.Context
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Inventory/BigInventory/Context/Gs2InventoryOwnBigInventoryContext")]
    public partial class Gs2InventoryOwnBigInventoryContext : Gs2InventoryBigInventoryModelContext
    {
        public new void Start() {
            if (BigInventory == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: BigInventory is not set in Gs2InventoryOwnBigInventoryContext.");
            }
        }
        public override bool HasError() {
            if (!base.HasError()) {
                return false;
            }
            if (BigInventory == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2InventoryOwnBigInventoryContext
    {

    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2InventoryOwnBigInventoryContext
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2InventoryOwnBigInventoryContext
    {
        public OwnBigInventory BigInventory;

        public void SetOwnBigInventory(OwnBigInventory bigInventory) {
            this.BigInventoryModel = BigInventoryModel.New(
                Namespace.New(
                    bigInventory.NamespaceName
                ),
                bigInventory.InventoryName
            );
            this.BigInventory = bigInventory;
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InventoryOwnBigInventoryContext
    {

    }
}