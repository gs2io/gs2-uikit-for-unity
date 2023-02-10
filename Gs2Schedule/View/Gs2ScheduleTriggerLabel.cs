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
using Gs2.Unity.UiKit.Gs2Schedule.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Schedule
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Schedule/Trigger/View/Gs2ScheduleTriggerLabel")]
    public partial class Gs2ScheduleTriggerLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched)
            {
                var createdAt = UnixTime.FromUnixTime(_fetcher.Trigger.CreatedAt).ToLocalTime();
                var expiresAt = UnixTime.FromUnixTime(_fetcher.Trigger.ExpiresAt).ToLocalTime();
                onUpdate.Invoke(
                    format.Replace(
                        "{triggerId}", _fetcher.Trigger.TriggerId.ToString()
                    ).Replace(
                        "{name}", _fetcher.Trigger.Name.ToString()
                    ).Replace(
                        "{createdAt:yyyy}", createdAt.ToString("yyyy")
                    ).Replace(
                        "{createdAt:yy}", createdAt.ToString("yy")
                    ).Replace(
                        "{createdAt:MM}", createdAt.ToString("MM")
                    ).Replace(
                        "{createdAt:MMM}", createdAt.ToString("MMM")
                    ).Replace(
                        "{createdAt:dd}", createdAt.ToString("dd")
                    ).Replace(
                        "{createdAt:hh}", createdAt.ToString("hh")
                    ).Replace(
                        "{createdAt:HH}", createdAt.ToString("HH")
                    ).Replace(
                        "{createdAt:tt}", createdAt.ToString("tt")
                    ).Replace(
                        "{createdAt:mm}", createdAt.ToString("mm")
                    ).Replace(
                        "{createdAt:ss}", createdAt.ToString("ss")
                    ).Replace(
                        "{expiresAt:yyyy}", expiresAt.ToString("yyyy")
                    ).Replace(
                        "{expiresAt:yy}", expiresAt.ToString("yy")
                    ).Replace(
                        "{expiresAt:MM}", expiresAt.ToString("MM")
                    ).Replace(
                        "{expiresAt:MMM}", expiresAt.ToString("MMM")
                    ).Replace(
                        "{expiresAt:dd}", expiresAt.ToString("dd")
                    ).Replace(
                        "{expiresAt:hh}", expiresAt.ToString("hh")
                    ).Replace(
                        "{expiresAt:HH}", expiresAt.ToString("HH")
                    ).Replace(
                        "{expiresAt:tt}", expiresAt.ToString("tt")
                    ).Replace(
                        "{expiresAt:mm}", expiresAt.ToString("mm")
                    ).Replace(
                        "{expiresAt:ss}", expiresAt.ToString("ss")
                    )
                );
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2ScheduleTriggerLabel
    {
        private Gs2ScheduleTriggerFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponentInParent<Gs2ScheduleTriggerFetcher>();
            Update();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2ScheduleTriggerLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2ScheduleTriggerLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ScheduleTriggerLabel
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