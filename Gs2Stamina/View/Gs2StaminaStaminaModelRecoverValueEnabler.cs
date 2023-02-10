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
using Gs2.Unity.UiKit.Gs2Stamina.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Stamina
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Stamina/StaminaModel/View/Properties/RecoverValue/Gs2StaminaStaminaModelRecoverValueEnabler")]
    public partial class Gs2StaminaStaminaModelRecoverValueEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableRecoverValues.Contains(_fetcher.StaminaModel.RecoverValue));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableRecoverValues.Contains(_fetcher.StaminaModel.RecoverValue));
                        break;
                    case Expression.Less:
                        target.SetActive(enableRecoverValue < _fetcher.StaminaModel.RecoverValue);
                        break;
                    case Expression.LessEqual:
                        target.SetActive(enableRecoverValue <= _fetcher.StaminaModel.RecoverValue);
                        break;
                    case Expression.Greater:
                        target.SetActive(enableRecoverValue > _fetcher.StaminaModel.RecoverValue);
                        break;
                    case Expression.GreaterEqual:
                        target.SetActive(enableRecoverValue >= _fetcher.StaminaModel.RecoverValue);
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
    
    public partial class Gs2StaminaStaminaModelRecoverValueEnabler
    {
        private Gs2StaminaStaminaModelFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponentInParent<Gs2StaminaStaminaModelFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2StaminaStaminaModelRecoverValueEnabler
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2StaminaStaminaModelRecoverValueEnabler
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
    public partial class Gs2StaminaStaminaModelRecoverValueEnabler
    {
        
    }
}