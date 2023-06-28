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
using Gs2.Unity.UiKit.Gs2Stamina.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Stamina
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Stamina/Stamina/View/Enabler/Properties/RecoverIntervalMinutes/Gs2StaminaStaminaRecoverIntervalMinutesEnabler")]
    public partial class Gs2StaminaStaminaRecoverIntervalMinutesEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Stamina != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableRecoverIntervalMinuteses.Contains(_fetcher.Stamina.RecoverIntervalMinutes));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableRecoverIntervalMinuteses.Contains(_fetcher.Stamina.RecoverIntervalMinutes));
                        break;
                    case Expression.Less:
                        target.SetActive(enableRecoverIntervalMinutes > _fetcher.Stamina.RecoverIntervalMinutes);
                        break;
                    case Expression.LessEqual:
                        target.SetActive(enableRecoverIntervalMinutes >= _fetcher.Stamina.RecoverIntervalMinutes);
                        break;
                    case Expression.Greater:
                        target.SetActive(enableRecoverIntervalMinutes < _fetcher.Stamina.RecoverIntervalMinutes);
                        break;
                    case Expression.GreaterEqual:
                        target.SetActive(enableRecoverIntervalMinutes <= _fetcher.Stamina.RecoverIntervalMinutes);
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

    public partial class Gs2StaminaStaminaRecoverIntervalMinutesEnabler
    {
        private Gs2StaminaOwnStaminaFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2StaminaOwnStaminaFetcher>() ?? GetComponentInParent<Gs2StaminaOwnStaminaFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2StaminaOwnStaminaFetcher.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2StaminaStaminaRecoverIntervalMinutesEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2StaminaStaminaRecoverIntervalMinutesEnabler
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

        public List<int> enableRecoverIntervalMinuteses;

        public int enableRecoverIntervalMinutes;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2StaminaStaminaRecoverIntervalMinutesEnabler
    {
        
    }
}