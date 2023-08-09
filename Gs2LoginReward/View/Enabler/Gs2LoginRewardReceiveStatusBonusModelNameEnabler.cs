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

namespace Gs2.Unity.UiKit.Gs2LoginReward.Enabler
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/LoginReward/ReceiveStatus/View/Enabler/Properties/BonusModelName/Gs2LoginRewardReceiveStatusBonusModelNameEnabler")]
    public partial class Gs2LoginRewardReceiveStatusBonusModelNameEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.ReceiveStatus != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableBonusModelNames.Contains(_fetcher.ReceiveStatus.BonusModelName));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableBonusModelNames.Contains(_fetcher.ReceiveStatus.BonusModelName));
                        break;
                    case Expression.StartsWith:
                        target.SetActive(enableBonusModelName.StartsWith(_fetcher.ReceiveStatus.BonusModelName));
                        break;
                    case Expression.EndsWith:
                        target.SetActive(enableBonusModelName.EndsWith(_fetcher.ReceiveStatus.BonusModelName));
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

    public partial class Gs2LoginRewardReceiveStatusBonusModelNameEnabler
    {
        private Gs2LoginRewardOwnReceiveStatusFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2LoginRewardOwnReceiveStatusFetcher>() ?? GetComponentInParent<Gs2LoginRewardOwnReceiveStatusFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2LoginRewardOwnReceiveStatusFetcher.");
                enabled = false;
            }
            if (target == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: target is not set.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            _fetcher = GetComponent<Gs2LoginRewardOwnReceiveStatusFetcher>() ?? GetComponentInParent<Gs2LoginRewardOwnReceiveStatusFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            if (target == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2LoginRewardReceiveStatusBonusModelNameEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2LoginRewardReceiveStatusBonusModelNameEnabler
    {
        public enum Expression {
            In,
            NotIn,
            StartsWith,
            EndsWith,
        }

        public Expression expression;

        public List<string> enableBonusModelNames;

        public string enableBonusModelName;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2LoginRewardReceiveStatusBonusModelNameEnabler
    {
        
    }
}