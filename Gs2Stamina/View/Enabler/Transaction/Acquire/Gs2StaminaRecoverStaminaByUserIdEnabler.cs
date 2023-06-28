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

	[AddComponentMenu("GS2 UIKit/Stamina/Stamina/View/Enabler/Transaction/Gs2StaminaRecoverStaminaByUserIdEnabler")]
    public partial class Gs2StaminaRecoverStaminaByUserIdEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Request != null) {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(_fetcher.Request.RecoverValue != null && enableRecoverValues.Contains(_fetcher.Request.RecoverValue.Value));
                        break;
                    case Expression.NotIn:
                        target.SetActive(_fetcher.Request.RecoverValue != null && !enableRecoverValues.Contains(_fetcher.Request.RecoverValue.Value));
                        break;
                    case Expression.Less:
                        target.SetActive(enableRecoverValue > _fetcher.Request.RecoverValue);
                        break;
                    case Expression.LessEqual:
                        target.SetActive(enableRecoverValue >= _fetcher.Request.RecoverValue);
                        break;
                    case Expression.Greater:
                        target.SetActive(enableRecoverValue < _fetcher.Request.RecoverValue);
                        break;
                    case Expression.GreaterEqual:
                        target.SetActive(enableRecoverValue <= _fetcher.Request.RecoverValue);
                        break;
                }
            }
            else
            {
                target.SetActive(enableRecoverValues.Contains(0));
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2StaminaRecoverStaminaByUserIdEnabler
    {
        private Gs2StaminaRecoverStaminaByUserIdFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2StaminaRecoverStaminaByUserIdFetcher>() ?? GetComponentInParent<Gs2StaminaRecoverStaminaByUserIdFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2StaminaRecoverStaminaByUserIdFetcher.");
                enabled = false;
            }

            Update();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2StaminaRecoverStaminaByUserIdEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2StaminaRecoverStaminaByUserIdEnabler
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

        public List<int> enableRecoverValues;

        public int enableRecoverValue;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2StaminaRecoverStaminaByUserIdEnabler
    {

    }
}