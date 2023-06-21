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

	[AddComponentMenu("GS2 UIKit/Mission/MissionTaskModel/View/Properties/TargetValue/Gs2MissionMissionTaskModelTargetValueEnabler")]
    public partial class Gs2MissionMissionTaskModelTargetValueEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.MissionTaskModel != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableTargetValues.Contains(_fetcher.MissionTaskModel.TargetValue));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableTargetValues.Contains(_fetcher.MissionTaskModel.TargetValue));
                        break;
                    case Expression.Less:
                        target.SetActive(enableTargetValue > _fetcher.MissionTaskModel.TargetValue);
                        break;
                    case Expression.LessEqual:
                        target.SetActive(enableTargetValue >= _fetcher.MissionTaskModel.TargetValue);
                        break;
                    case Expression.Greater:
                        target.SetActive(enableTargetValue < _fetcher.MissionTaskModel.TargetValue);
                        break;
                    case Expression.GreaterEqual:
                        target.SetActive(enableTargetValue <= _fetcher.MissionTaskModel.TargetValue);
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

    public partial class Gs2MissionMissionTaskModelTargetValueEnabler
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

    public partial class Gs2MissionMissionTaskModelTargetValueEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2MissionMissionTaskModelTargetValueEnabler
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

        public List<long> enableTargetValues;

        public long enableTargetValue;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MissionMissionTaskModelTargetValueEnabler
    {
        
    }
}