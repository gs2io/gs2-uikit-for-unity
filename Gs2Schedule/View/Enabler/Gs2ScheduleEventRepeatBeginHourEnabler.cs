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
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Schedule.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Schedule
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Schedule/Event/View/Enabler/Properties/RepeatBeginHour/Gs2ScheduleEventRepeatBeginHourEnabler")]
    public partial class Gs2ScheduleEventRepeatBeginHourEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Event != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableRepeatBeginHours.Contains(_fetcher.Event.RepeatBeginHour));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableRepeatBeginHours.Contains(_fetcher.Event.RepeatBeginHour));
                        break;
                    case Expression.Less:
                        target.SetActive(enableRepeatBeginHour > _fetcher.Event.RepeatBeginHour);
                        break;
                    case Expression.LessEqual:
                        target.SetActive(enableRepeatBeginHour >= _fetcher.Event.RepeatBeginHour);
                        break;
                    case Expression.Greater:
                        target.SetActive(enableRepeatBeginHour < _fetcher.Event.RepeatBeginHour);
                        break;
                    case Expression.GreaterEqual:
                        target.SetActive(enableRepeatBeginHour <= _fetcher.Event.RepeatBeginHour);
                        break;
                }
            }
            else
            {
                target.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2ScheduleEventRepeatBeginHourEnabler
    {
        private Gs2ScheduleEventFetcher _fetcher;

        public void Awake()
        {
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

    public partial class Gs2ScheduleEventRepeatBeginHourEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2ScheduleEventRepeatBeginHourEnabler
    {
        public enum Expression {
            In,
            NotIn,
            Less,
            LessEqual,
            Greater,
            GreaterEqual,
        }

        public Expression expression;

        public List<int> enableRepeatBeginHours;

        public int enableRepeatBeginHour;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ScheduleEventRepeatBeginHourEnabler
    {
        
    }
}