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

using System.Collections.Generic;
using Gs2.Unity.Gs2Showcase.ScriptableObject;
using UnityEngine;

namespace Gs2.Unity.UiKit.Core.Acquire
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Core/AcquireAction/Gs2CoreAcquireActionsContext")]
    public partial class Gs2CoreAcquireActionsContext : MonoBehaviour
    {
        public void Start() {
            if (AcquireActions == null) {
                Debug.LogError("AcquireActions is not set in Gs2CoreAcquireActionContext.");
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2CoreAcquireActionsContext
    {

    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2CoreAcquireActionsContext
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2CoreAcquireActionsContext
    {
        public List<AcquireAction> AcquireActions;

        public void SetAcquireActions(List<AcquireAction> AcquireActions) {
            this.AcquireActions = AcquireActions;
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2CoreAcquireActionsContext
    {

    }
}