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

using System;
using System.Collections.Generic;
using Gs2.Gs2Stamina.Request;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Stamina.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Stamina
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Stamina/Stamina/View/Enabler/Transaction/Gs2StaminaConsumeStaminaByUserIdEnabler")]
    public partial class Gs2StaminaConsumeStaminaByUserIdEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.ConsumeAction != null && _fetcher.ConsumeAction.Action == "Gs2Stamina:ConsumeStaminaByUserId") {
                var request = ConsumeStaminaByUserIdRequest.FromJson(JsonMapper.ToObject(_fetcher.ConsumeAction.Request));
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(request.ConsumeValue != null && enableConsumeValues.Contains(request.ConsumeValue.Value));
                        break;
                    case Expression.NotIn:
                        target.SetActive(request.ConsumeValue != null && !enableConsumeValues.Contains(request.ConsumeValue.Value));
                        break;
                    case Expression.Less:
                        target.SetActive(enableConsumeValue > request.ConsumeValue);
                        break;
                    case Expression.LessEqual:
                        target.SetActive(enableConsumeValue >= request.ConsumeValue);
                        break;
                    case Expression.Greater:
                        target.SetActive(enableConsumeValue < request.ConsumeValue);
                        break;
                    case Expression.GreaterEqual:
                        target.SetActive(enableConsumeValue <= request.ConsumeValue);
                        break;
                }
            }
            else
            {
                target.SetActive(enableConsumeValues.Contains(0));
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2StaminaConsumeStaminaByUserIdEnabler
    {
        private Gs2CoreConsumeActionFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2CoreConsumeActionFetcher>() ?? GetComponentInParent<Gs2CoreConsumeActionFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2CoreConsumeActionFetcher.");
                enabled = false;
            }

            Update();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2StaminaConsumeStaminaByUserIdEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2StaminaConsumeStaminaByUserIdEnabler
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

        public List<int> enableConsumeValues;

        public int enableConsumeValue;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2StaminaConsumeStaminaByUserIdEnabler
    {

    }
}