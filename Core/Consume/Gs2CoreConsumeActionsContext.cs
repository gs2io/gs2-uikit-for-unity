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

namespace Gs2.Unity.UiKit.Core.Consume
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Core/ConsumeAction/Gs2CoreConsumeActionsContext")]
    public partial class Gs2CoreConsumeActionsContext : MonoBehaviour
    {
        public void Start() {
            if (ConsumeActions == null) {
                Debug.LogError("ConsumeActions is not set in Gs2CoreConsumeActionContext.");
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2CoreConsumeActionsContext
    {

    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2CoreConsumeActionsContext
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2CoreConsumeActionsContext
    {
        public List<ConsumeAction> ConsumeActions;

        public void SetConsumeActions(List<ConsumeAction> ConsumeActions) {
            this.ConsumeActions = ConsumeActions;
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2CoreConsumeActionsContext
    {

    }
}