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
using Gs2.Unity.UiKit.Gs2Mission.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Mission
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Mission/MissionGroupModel/View/Enabler/Properties/ResetDayOfMonth/Gs2MissionMissionGroupModelResetDayOfMonthEnabler")]
    public partial class Gs2MissionMissionGroupModelResetDayOfMonthEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.MissionGroupModel != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableResetDayOfMonths.Contains(_fetcher.MissionGroupModel.ResetDayOfMonth));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableResetDayOfMonths.Contains(_fetcher.MissionGroupModel.ResetDayOfMonth));
                        break;
                    case Expression.Less:
                        target.SetActive(enableResetDayOfMonth > _fetcher.MissionGroupModel.ResetDayOfMonth);
                        break;
                    case Expression.LessEqual:
                        target.SetActive(enableResetDayOfMonth >= _fetcher.MissionGroupModel.ResetDayOfMonth);
                        break;
                    case Expression.Greater:
                        target.SetActive(enableResetDayOfMonth < _fetcher.MissionGroupModel.ResetDayOfMonth);
                        break;
                    case Expression.GreaterEqual:
                        target.SetActive(enableResetDayOfMonth <= _fetcher.MissionGroupModel.ResetDayOfMonth);
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

    public partial class Gs2MissionMissionGroupModelResetDayOfMonthEnabler
    {
        private Gs2MissionMissionGroupModelFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2MissionMissionGroupModelFetcher>() ?? GetComponentInParent<Gs2MissionMissionGroupModelFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MissionMissionGroupModelFetcher.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2MissionMissionGroupModelResetDayOfMonthEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2MissionMissionGroupModelResetDayOfMonthEnabler
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

        public List<int> enableResetDayOfMonths;

        public int enableResetDayOfMonth;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MissionMissionGroupModelResetDayOfMonthEnabler
    {
        
    }
}