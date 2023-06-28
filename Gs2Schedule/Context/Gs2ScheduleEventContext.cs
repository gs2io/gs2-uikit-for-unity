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

using Gs2.Unity.Gs2Schedule.ScriptableObject;
using UnityEngine;
using Event = Gs2.Unity.Gs2Schedule.ScriptableObject.Event;

namespace Gs2.Unity.UiKit.Gs2Schedule.Context
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Schedule/Event/Context/Gs2ScheduleEventContext")]
    public partial class Gs2ScheduleEventContext : MonoBehaviour
    {
        public void Start() {
            if (Event_ == null) {
                Debug.LogError("Event_ is not set in Gs2ScheduleEventContext.");
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2ScheduleEventContext
    {

    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2ScheduleEventContext
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2ScheduleEventContext
    {
        public Event Event_;

        public void SetEvent(Event Event_) {
            this.Event_ = Event_;
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ScheduleEventContext
    {

    }
}