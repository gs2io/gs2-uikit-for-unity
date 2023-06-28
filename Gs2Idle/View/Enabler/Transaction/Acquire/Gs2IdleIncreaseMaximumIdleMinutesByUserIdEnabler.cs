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
using System.Collections.Generic;
using Gs2.Gs2Idle.Request;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Idle.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Idle
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Idle/Status/View/Enabler/Transaction/Gs2IdleIncreaseMaximumIdleMinutesByUserIdEnabler")]
    public partial class Gs2IdleIncreaseMaximumIdleMinutesByUserIdEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Request != null) {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(_fetcher.Request.IncreaseMinutes != null && enableIncreaseMinuteses.Contains(_fetcher.Request.IncreaseMinutes.Value));
                        break;
                    case Expression.NotIn:
                        target.SetActive(_fetcher.Request.IncreaseMinutes != null && !enableIncreaseMinuteses.Contains(_fetcher.Request.IncreaseMinutes.Value));
                        break;
                    case Expression.Less:
                        target.SetActive(enableIncreaseMinutes > _fetcher.Request.IncreaseMinutes);
                        break;
                    case Expression.LessEqual:
                        target.SetActive(enableIncreaseMinutes >= _fetcher.Request.IncreaseMinutes);
                        break;
                    case Expression.Greater:
                        target.SetActive(enableIncreaseMinutes < _fetcher.Request.IncreaseMinutes);
                        break;
                    case Expression.GreaterEqual:
                        target.SetActive(enableIncreaseMinutes <= _fetcher.Request.IncreaseMinutes);
                        break;
                }
            }
            else
            {
                target.SetActive(enableIncreaseMinuteses.Contains(0));
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2IdleIncreaseMaximumIdleMinutesByUserIdEnabler
    {
        private Gs2IdleIncreaseMaximumIdleMinutesByUserIdFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2IdleIncreaseMaximumIdleMinutesByUserIdFetcher>() ?? GetComponentInParent<Gs2IdleIncreaseMaximumIdleMinutesByUserIdFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2IdleIncreaseMaximumIdleMinutesByUserIdFetcher.");
                enabled = false;
            }

            Update();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2IdleIncreaseMaximumIdleMinutesByUserIdEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2IdleIncreaseMaximumIdleMinutesByUserIdEnabler
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

        public List<int> enableIncreaseMinuteses;

        public int enableIncreaseMinutes;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2IdleIncreaseMaximumIdleMinutesByUserIdEnabler
    {

    }
}