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

#if GS2_ENABLE_LOCALIZATION

using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Schedule.Fetcher;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

namespace Gs2.Unity.UiKit.Gs2Schedule.Localization
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Schedule/Event/View/Localization/Gs2ScheduleEventLocalizationVariables")]
    public partial class Gs2ScheduleEventLocalizationVariables : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched) {
                target.StringReference["name"] = new StringVariable {
                    Value = _fetcher?.Event?.Name ?? "",
                };
                target.StringReference["metadata"] = new StringVariable {
                    Value = _fetcher?.Event?.Metadata ?? "",
                };
                target.StringReference["scheduleType"] = new StringVariable {
                    Value = _fetcher?.Event?.ScheduleType ?? "",
                };
                target.StringReference["repeatType"] = new StringVariable {
                    Value = _fetcher?.Event?.RepeatType ?? "",
                };
                target.StringReference["absoluteBegin"] = new LongVariable {
                    Value = _fetcher?.Event?.AbsoluteBegin ?? 0,
                };
                target.StringReference["absoluteEnd"] = new LongVariable {
                    Value = _fetcher?.Event?.AbsoluteEnd ?? 0,
                };
                target.StringReference["repeatBeginDayOfMonth"] = new IntVariable {
                    Value = _fetcher?.Event?.RepeatBeginDayOfMonth ?? 0,
                };
                target.StringReference["repeatEndDayOfMonth"] = new IntVariable {
                    Value = _fetcher?.Event?.RepeatEndDayOfMonth ?? 0,
                };
                target.StringReference["repeatBeginDayOfWeek"] = new StringVariable {
                    Value = _fetcher?.Event?.RepeatBeginDayOfWeek ?? "",
                };
                target.StringReference["repeatEndDayOfWeek"] = new StringVariable {
                    Value = _fetcher?.Event?.RepeatEndDayOfWeek ?? "",
                };
                target.StringReference["repeatBeginHour"] = new IntVariable {
                    Value = _fetcher?.Event?.RepeatBeginHour ?? 0,
                };
                target.StringReference["repeatEndHour"] = new IntVariable {
                    Value = _fetcher?.Event?.RepeatEndHour ?? 0,
                };
                target.StringReference["relativeTriggerName"] = new StringVariable {
                    Value = _fetcher?.Event?.RelativeTriggerName ?? "",
                };
                enabled = false;
                target.enabled = true;
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2ScheduleEventLocalizationVariables
    {
        private Gs2ScheduleEventFetcher _fetcher;

        public void Awake() {
            target.enabled = false;
            _fetcher = GetComponent<Gs2ScheduleEventFetcher>() ?? GetComponentInParent<Gs2ScheduleEventFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ScheduleEventFetcher.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2ScheduleEventLocalizationVariables
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2ScheduleEventLocalizationVariables
    {
        public LocalizeStringEvent target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ScheduleEventLocalizationVariables
    {

    }
}

#endif