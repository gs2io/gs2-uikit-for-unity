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

	[AddComponentMenu("GS2 UIKit/Stamina/StaminaModel/View/Properties/Metadata/Gs2StaminaStaminaModelMetadataEnabler")]
    public partial class Gs2StaminaStaminaModelMetadataEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.StaminaModel != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableMetadatas.Contains(_fetcher.StaminaModel.Metadata));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableMetadatas.Contains(_fetcher.StaminaModel.Metadata));
                        break;
                    case Expression.StartsWith:
                        target.SetActive(enableMetadata.StartsWith(_fetcher.StaminaModel.Metadata));
                        break;
                    case Expression.EndsWith:
                        target.SetActive(enableMetadata.EndsWith(_fetcher.StaminaModel.Metadata));
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

    public partial class Gs2StaminaStaminaModelMetadataEnabler
    {
        private Gs2StaminaStaminaModelFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponentInParent<Gs2StaminaStaminaModelFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2StaminaStaminaModelFetcher.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2StaminaStaminaModelMetadataEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2StaminaStaminaModelMetadataEnabler
    {
        public enum Expression {
            In,
            NotIn,
            StartsWith,
            EndsWith,
        }

        public Expression expression;

        public List<string> enableMetadatas;

        public string enableMetadata;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2StaminaStaminaModelMetadataEnabler
    {
        
    }
}