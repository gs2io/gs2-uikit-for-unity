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
using Gs2.Core.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Mission.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Mission
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Mission/Complete/View/Gs2MissionCompleteReceiveRewardsActionLabel")]
    public partial class Gs2MissionCompleteReceiveRewardsActionLabel : MonoBehaviour
    {
        public void Update()
        {
            onUpdate?.Invoke(
                format.Replace(
                    "{missionTaskName}", $"{action?.MissionTaskName}"
                )
            );
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2MissionCompleteReceiveRewardsActionLabel
    {
        
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2MissionCompleteReceiveRewardsActionLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2MissionCompleteReceiveRewardsActionLabel
    {
        public Gs2MissionCompleteReceiveRewardsAction action;
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MissionCompleteReceiveRewardsActionLabel
    {
        [Serializable]
        private class UpdateEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private UpdateEvent onUpdate = new UpdateEvent();

        public event UnityAction<string> OnUpdate
        {
            add => onUpdate.AddListener(value);
            remove => onUpdate.RemoveListener(value);
        }
    }
}