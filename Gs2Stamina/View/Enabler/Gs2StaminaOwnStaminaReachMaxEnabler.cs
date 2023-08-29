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

	[AddComponentMenu("GS2 UIKit/Stamina/Stamina/View/Enabler/Gs2StaminaStaminaReachMaxEnabler")]
    public partial class Gs2StaminaOwnStaminaReachMaxEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Stamina != null)
            {
                if (notReachMax) {
                    this.target.SetActive(this._fetcher.Stamina.Value < this._fetcher.Stamina.MaxValue);
                } else if (reachMax) {
                    this.target.SetActive(this._fetcher.Stamina.Value >= this._fetcher.Stamina.MaxValue);
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

    public partial class Gs2StaminaOwnStaminaReachMaxEnabler
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

    public partial class Gs2StaminaOwnStaminaReachMaxEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2StaminaOwnStaminaReachMaxEnabler
    {
        public bool notReachMax;
        public bool reachMax;
        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2StaminaOwnStaminaReachMaxEnabler
    {
        
    }
}