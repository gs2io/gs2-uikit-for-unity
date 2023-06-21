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

	[AddComponentMenu("GS2 UIKit/Mission/MissionTaskModel/View/Properties/PremiseMissionTaskName/Gs2MissionMissionTaskModelPremiseMissionTaskNameEnabler")]
    public partial class Gs2MissionMissionTaskModelPremiseMissionTaskNameEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.MissionTaskModel != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enablePremiseMissionTaskNames.Contains(_fetcher.MissionTaskModel.PremiseMissionTaskName));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enablePremiseMissionTaskNames.Contains(_fetcher.MissionTaskModel.PremiseMissionTaskName));
                        break;
                    case Expression.StartsWith:
                        target.SetActive(enablePremiseMissionTaskName.StartsWith(_fetcher.MissionTaskModel.PremiseMissionTaskName));
                        break;
                    case Expression.EndsWith:
                        target.SetActive(enablePremiseMissionTaskName.EndsWith(_fetcher.MissionTaskModel.PremiseMissionTaskName));
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

    public partial class Gs2MissionMissionTaskModelPremiseMissionTaskNameEnabler
    {
        private Gs2MissionMissionTaskModelFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2MissionMissionTaskModelFetcher>() ?? GetComponentInParent<Gs2MissionMissionTaskModelFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MissionMissionTaskModelFetcher.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2MissionMissionTaskModelPremiseMissionTaskNameEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2MissionMissionTaskModelPremiseMissionTaskNameEnabler
    {
        public enum Expression {
            In,
            NotIn,
            StartsWith,
            EndsWith,
        }

        public Expression expression;

        public List<string> enablePremiseMissionTaskNames;

        public string enablePremiseMissionTaskName;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MissionMissionTaskModelPremiseMissionTaskNameEnabler
    {
        
    }
}