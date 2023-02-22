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

	[AddComponentMenu("GS2 UIKit/Schedule/Event/View/Properties/RelativeDuration/Gs2ScheduleEventRelativeDurationEnabler")]
    public partial class Gs2ScheduleEventRelativeDurationEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Event != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableRelativeDurations.Contains(_fetcher.Event.RelativeDuration));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableRelativeDurations.Contains(_fetcher.Event.RelativeDuration));
                        break;
                    case Expression.Less:
                        target.SetActive(enableRelativeDuration > _fetcher.Event.RelativeDuration);
                        break;
                    case Expression.LessEqual:
                        target.SetActive(enableRelativeDuration >= _fetcher.Event.RelativeDuration);
                        break;
                    case Expression.Greater:
                        target.SetActive(enableRelativeDuration < _fetcher.Event.RelativeDuration);
                        break;
                    case Expression.GreaterEqual:
                        target.SetActive(enableRelativeDuration <= _fetcher.Event.RelativeDuration);
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

    public partial class Gs2ScheduleEventRelativeDurationEnabler
    {
        private Gs2ScheduleEventFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponentInParent<Gs2ScheduleEventFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ScheduleEventFetcher.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2ScheduleEventRelativeDurationEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2ScheduleEventRelativeDurationEnabler
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

        public List<int> enableRelativeDurations;

        public int enableRelativeDuration;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ScheduleEventRelativeDurationEnabler
    {
        
    }
}