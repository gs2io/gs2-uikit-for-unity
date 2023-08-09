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

using Gs2.Unity.Gs2StateMachine.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2StateMachine.Context
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/StateMachine/Status/Context/Gs2StateMachineOwnStatusContext")]
    public partial class Gs2StateMachineOwnStatusContext : MonoBehaviour
    {
        public void Start() {
            if (Status == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Status is not set in Gs2StateMachineOwnStatusContext.");
            }
        }
        public virtual bool HasError() {
            if (Status == null) {
                if (GetComponentInParent<Gs2StateMachineOwnStatusList>(true) != null) {
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

    public partial class Gs2StateMachineOwnStatusContext
    {

    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2StateMachineOwnStatusContext
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2StateMachineOwnStatusContext
    {
        public OwnStatus Status;

        public void SetOwnStatus(OwnStatus status) {
            this.Status = status;
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2StateMachineOwnStatusContext
    {

    }
}