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

	[AddComponentMenu("GS2 UIKit/Schedule/Event/View/Label/Gs2ScheduleEventLabel")]
    public partial class Gs2ScheduleEventLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Event != null)
            {
                var absoluteBegin = _fetcher.Event.AbsoluteBegin == null ? DateTime.Now : UnixTime.FromUnixTime(_fetcher.Event.AbsoluteBegin).ToLocalTime();
                var absoluteEnd = _fetcher.Event.AbsoluteEnd == null ? DateTime.Now : UnixTime.FromUnixTime(_fetcher.Event.AbsoluteEnd).ToLocalTime();
                onUpdate?.Invoke(
                    format.Replace(
                        "{name}", $"{_fetcher?.Event?.Name}"
                    ).Replace(
                        "{metadata}", $"{_fetcher?.Event?.Metadata}"
                    ).Replace(
                        "{scheduleType}", $"{_fetcher?.Event?.ScheduleType}"
                    ).Replace(
                        "{repeatType}", $"{_fetcher?.Event?.RepeatType}"
                    ).Replace(
                        "{absoluteBegin:yyyy}", absoluteBegin.ToString("yyyy")
                    ).Replace(
                        "{absoluteBegin:yy}", absoluteBegin.ToString("yy")
                    ).Replace(
                        "{absoluteBegin:MM}", absoluteBegin.ToString("MM")
                    ).Replace(
                        "{absoluteBegin:MMM}", absoluteBegin.ToString("MMM")
                    ).Replace(
                        "{absoluteBegin:dd}", absoluteBegin.ToString("dd")
                    ).Replace(
                        "{absoluteBegin:hh}", absoluteBegin.ToString("hh")
                    ).Replace(
                        "{absoluteBegin:HH}", absoluteBegin.ToString("HH")
                    ).Replace(
                        "{absoluteBegin:tt}", absoluteBegin.ToString("tt")
                    ).Replace(
                        "{absoluteBegin:mm}", absoluteBegin.ToString("mm")
                    ).Replace(
                        "{absoluteBegin:ss}", absoluteBegin.ToString("ss")
                    ).Replace(
                        "{absoluteEnd:yyyy}", absoluteEnd.ToString("yyyy")
                    ).Replace(
                        "{absoluteEnd:yy}", absoluteEnd.ToString("yy")
                    ).Replace(
                        "{absoluteEnd:MM}", absoluteEnd.ToString("MM")
                    ).Replace(
                        "{absoluteEnd:MMM}", absoluteEnd.ToString("MMM")
                    ).Replace(
                        "{absoluteEnd:dd}", absoluteEnd.ToString("dd")
                    ).Replace(
                        "{absoluteEnd:hh}", absoluteEnd.ToString("hh")
                    ).Replace(
                        "{absoluteEnd:HH}", absoluteEnd.ToString("HH")
                    ).Replace(
                        "{absoluteEnd:tt}", absoluteEnd.ToString("tt")
                    ).Replace(
                        "{absoluteEnd:mm}", absoluteEnd.ToString("mm")
                    ).Replace(
                        "{absoluteEnd:ss}", absoluteEnd.ToString("ss")
                    ).Replace(
                        "{repeatBeginDayOfMonth}", $"{_fetcher?.Event?.RepeatBeginDayOfMonth}"
                    ).Replace(
                        "{repeatEndDayOfMonth}", $"{_fetcher?.Event?.RepeatEndDayOfMonth}"
                    ).Replace(
                        "{repeatBeginDayOfWeek}", $"{_fetcher?.Event?.RepeatBeginDayOfWeek}"
                    ).Replace(
                        "{repeatEndDayOfWeek}", $"{_fetcher?.Event?.RepeatEndDayOfWeek}"
                    ).Replace(
                        "{repeatBeginHour}", $"{_fetcher?.Event?.RepeatBeginHour}"
                    ).Replace(
                        "{repeatEndHour}", $"{_fetcher?.Event?.RepeatEndHour}"
                    ).Replace(
                        "{relativeTriggerName}", $"{_fetcher?.Event?.RelativeTriggerName}"
                    )
                );
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2ScheduleEventLabel
    {
        private Gs2ScheduleEventFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2ScheduleEventFetcher>() ?? GetComponentInParent<Gs2ScheduleEventFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ScheduleEventFetcher.");
                enabled = false;
            }

            Update();
        }

        public bool HasError()
        {
            _fetcher = GetComponent<Gs2ScheduleEventFetcher>() ?? GetComponentInParent<Gs2ScheduleEventFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2ScheduleEventLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2ScheduleEventLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ScheduleEventLabel
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