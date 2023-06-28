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
using Gs2.Unity.UiKit.Gs2Lottery.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Lottery.Enabler
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Lottery/LotteryModel/View/Enabler/Properties/Mode/Gs2LotteryLotteryModelModeEnabler")]
    public partial class Gs2LotteryLotteryModelModeEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.LotteryModel != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableModes.Contains(_fetcher.LotteryModel.Mode));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableModes.Contains(_fetcher.LotteryModel.Mode));
                        break;
                    case Expression.StartsWith:
                        target.SetActive(enableMode.StartsWith(_fetcher.LotteryModel.Mode));
                        break;
                    case Expression.EndsWith:
                        target.SetActive(enableMode.EndsWith(_fetcher.LotteryModel.Mode));
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

    public partial class Gs2LotteryLotteryModelModeEnabler
    {
        private Gs2LotteryLotteryModelFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2LotteryLotteryModelFetcher>() ?? GetComponentInParent<Gs2LotteryLotteryModelFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2LotteryLotteryModelFetcher.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2LotteryLotteryModelModeEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2LotteryLotteryModelModeEnabler
    {
        public enum Expression {
            In,
            NotIn,
            StartsWith,
            EndsWith,
        }

        public Expression expression;

        public List<string> enableModes;

        public string enableMode;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2LotteryLotteryModelModeEnabler
    {
        
    }
}