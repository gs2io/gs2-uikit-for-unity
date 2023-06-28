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

#if GS2_ENABLE_LOCALIZATION

using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Limit.Fetcher;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

namespace Gs2.Unity.UiKit.Gs2Limit.Localization
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Limit/LimitModel/View/Localization/Gs2LimitLimitModelLocalizationVariables")]
    public partial class Gs2LimitLimitModelLocalizationVariables : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched) {
                target.StringReference["limitModelId"] = new StringVariable {
                    Value = _fetcher?.LimitModel?.LimitModelId ?? "",
                };
                target.StringReference["name"] = new StringVariable {
                    Value = _fetcher?.LimitModel?.Name ?? "",
                };
                target.StringReference["metadata"] = new StringVariable {
                    Value = _fetcher?.LimitModel?.Metadata ?? "",
                };
                target.StringReference["resetType"] = new StringVariable {
                    Value = _fetcher?.LimitModel?.ResetType ?? "",
                };
                target.StringReference["resetDayOfMonth"] = new IntVariable {
                    Value = _fetcher?.LimitModel?.ResetDayOfMonth ?? 0,
                };
                target.StringReference["resetDayOfWeek"] = new StringVariable {
                    Value = _fetcher?.LimitModel?.ResetDayOfWeek ?? "",
                };
                target.StringReference["resetHour"] = new IntVariable {
                    Value = _fetcher?.LimitModel?.ResetHour ?? 0,
                };
                enabled = false;
                target.enabled = true;
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2LimitLimitModelLocalizationVariables
    {
        private Gs2LimitLimitModelFetcher _fetcher;

        public void Awake() {
            target.enabled = false;
            _fetcher = GetComponent<Gs2LimitLimitModelFetcher>() ?? GetComponentInParent<Gs2LimitLimitModelFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2LimitLimitModelFetcher.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2LimitLimitModelLocalizationVariables
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2LimitLimitModelLocalizationVariables
    {
        public LocalizeStringEvent target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2LimitLimitModelLocalizationVariables
    {

    }
}

#endif