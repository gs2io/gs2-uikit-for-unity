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
 *
 * deny overwrite
 */
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable CheckNamespace

using System.Collections.Generic;
using Gs2.Unity.UiKit.Gs2Stamina.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Stamina
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Stamina/Stamina/View/Properties/Value/Gs2StaminaStaminaValueEnabler")]
    public partial class Gs2StaminaStaminaValueEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Stamina != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableValues.Contains(_fetcher.Stamina.Value));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableValues.Contains(_fetcher.Stamina.Value));
                        break;
                    case Expression.Less:
                        target.SetActive(enableValue > _fetcher.Stamina.Value);
                        break;
                    case Expression.LessEqual:
                        target.SetActive(enableValue >= _fetcher.Stamina.Value);
                        break;
                    case Expression.Greater:
                        target.SetActive(enableValue < _fetcher.Stamina.Value);
                        break;
                    case Expression.GreaterEqual:
                        target.SetActive(enableValue <= _fetcher.Stamina.Value);
                        break;
                    case Expression.ReachMax:
                        target.SetActive(_fetcher.Stamina.MaxValue <= _fetcher.Stamina.Value);
                        break;
                    case Expression.NotReachMax:
                        target.SetActive(_fetcher.Stamina.MaxValue > _fetcher.Stamina.Value);
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

    public partial class Gs2StaminaStaminaValueEnabler
    {
        private Gs2StaminaOwnStaminaFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponentInParent<Gs2StaminaOwnStaminaFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2StaminaStaminaValueEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2StaminaStaminaValueEnabler
    {
        public enum Expression {
            In,
            NotIn,
            Less,
            LessEqual,
            Greater,
            GreaterEqual,
            ReachMax,
            NotReachMax,
        }

        public Expression expression;

        public List<int> enableValues;

        public int enableValue;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2StaminaStaminaValueEnabler
    {
        
    }
}