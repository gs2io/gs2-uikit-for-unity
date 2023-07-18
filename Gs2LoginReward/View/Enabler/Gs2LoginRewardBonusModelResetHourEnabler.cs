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

using System.Collections.Generic;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2LoginReward.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2LoginReward
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/LoginReward/BonusModel/View/Enabler/Properties/ResetHour/Gs2LoginRewardBonusModelResetHourEnabler")]
    public partial class Gs2LoginRewardBonusModelResetHourEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.BonusModel != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableResetHours.Contains(_fetcher.BonusModel.ResetHour));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableResetHours.Contains(_fetcher.BonusModel.ResetHour));
                        break;
                    case Expression.Less:
                        target.SetActive(enableResetHour > _fetcher.BonusModel.ResetHour);
                        break;
                    case Expression.LessEqual:
                        target.SetActive(enableResetHour >= _fetcher.BonusModel.ResetHour);
                        break;
                    case Expression.Greater:
                        target.SetActive(enableResetHour < _fetcher.BonusModel.ResetHour);
                        break;
                    case Expression.GreaterEqual:
                        target.SetActive(enableResetHour <= _fetcher.BonusModel.ResetHour);
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

    public partial class Gs2LoginRewardBonusModelResetHourEnabler
    {
        private Gs2LoginRewardBonusModelFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2LoginRewardBonusModelFetcher>() ?? GetComponentInParent<Gs2LoginRewardBonusModelFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2LoginRewardBonusModelFetcher.");
                enabled = false;
            }
        }

        public bool HasError()
        {
            _fetcher = GetComponent<Gs2LoginRewardBonusModelFetcher>() ?? GetComponentInParent<Gs2LoginRewardBonusModelFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2LoginRewardBonusModelResetHourEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2LoginRewardBonusModelResetHourEnabler
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

        public List<int> enableResetHours;

        public int enableResetHour;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2LoginRewardBonusModelResetHourEnabler
    {
        
    }
}