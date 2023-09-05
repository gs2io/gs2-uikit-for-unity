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

using Gs2.Unity.Gs2Stamina.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Stamina.Context
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Stamina/Stamina/Context/Gs2StaminaOwnStaminaContext")]
    public partial class Gs2StaminaOwnStaminaContext : Gs2StaminaStaminaModelContext
    {
        public new void Start() {
            if (Stamina == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Stamina is not set in Gs2StaminaOwnStaminaContext.");
            }
        }
        public override bool HasError() {
            if (base.HasError()) {
                return true;
            }
            if (Stamina == null) {
                if (GetComponentInParent<Gs2StaminaOwnStaminaList>(true) != null) {
                    return false;
                }
                else {
                    return true;
                }
            }
            return false;
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2StaminaOwnStaminaContext
    {

    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2StaminaOwnStaminaContext
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2StaminaOwnStaminaContext
    {
        public OwnStamina Stamina;

        public void SetOwnStamina(OwnStamina stamina) {
            this.StaminaModel = StaminaModel.New(
                Namespace.New(
                    stamina.NamespaceName
                ),
                stamina.StaminaName
            );
            this.Stamina = stamina;
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2StaminaOwnStaminaContext
    {

    }
}