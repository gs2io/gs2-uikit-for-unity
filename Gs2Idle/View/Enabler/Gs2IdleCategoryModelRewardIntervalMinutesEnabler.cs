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
using Gs2.Unity.UiKit.Gs2Idle.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Idle
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Idle/CategoryModel/View/Enabler/Properties/RewardIntervalMinutes/Gs2IdleCategoryModelRewardIntervalMinutesEnabler")]
    public partial class Gs2IdleCategoryModelRewardIntervalMinutesEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.CategoryModel != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableRewardIntervalMinuteses.Contains(_fetcher.CategoryModel.RewardIntervalMinutes));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableRewardIntervalMinuteses.Contains(_fetcher.CategoryModel.RewardIntervalMinutes));
                        break;
                    case Expression.Less:
                        target.SetActive(enableRewardIntervalMinutes > _fetcher.CategoryModel.RewardIntervalMinutes);
                        break;
                    case Expression.LessEqual:
                        target.SetActive(enableRewardIntervalMinutes >= _fetcher.CategoryModel.RewardIntervalMinutes);
                        break;
                    case Expression.Greater:
                        target.SetActive(enableRewardIntervalMinutes < _fetcher.CategoryModel.RewardIntervalMinutes);
                        break;
                    case Expression.GreaterEqual:
                        target.SetActive(enableRewardIntervalMinutes <= _fetcher.CategoryModel.RewardIntervalMinutes);
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

    public partial class Gs2IdleCategoryModelRewardIntervalMinutesEnabler
    {
        private Gs2IdleCategoryModelFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2IdleCategoryModelFetcher>() ?? GetComponentInParent<Gs2IdleCategoryModelFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2IdleCategoryModelFetcher.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2IdleCategoryModelRewardIntervalMinutesEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2IdleCategoryModelRewardIntervalMinutesEnabler
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

        public List<int> enableRewardIntervalMinuteses;

        public int enableRewardIntervalMinutes;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2IdleCategoryModelRewardIntervalMinutesEnabler
    {
        
    }
}