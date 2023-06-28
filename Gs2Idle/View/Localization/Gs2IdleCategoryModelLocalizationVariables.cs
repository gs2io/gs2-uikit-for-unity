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
using Gs2.Unity.UiKit.Gs2Idle.Fetcher;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

namespace Gs2.Unity.UiKit.Gs2Idle.Localization
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Idle/CategoryModel/View/Localization/Gs2IdleCategoryModelLocalizationVariables")]
    public partial class Gs2IdleCategoryModelLocalizationVariables : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched) {
                target.StringReference["name"] = new StringVariable {
                    Value = _fetcher?.CategoryModel?.Name ?? "",
                };
                target.StringReference["metadata"] = new StringVariable {
                    Value = _fetcher?.CategoryModel?.Metadata ?? "",
                };
                target.StringReference["rewardIntervalMinutes"] = new IntVariable {
                    Value = _fetcher?.CategoryModel?.RewardIntervalMinutes ?? 0,
                };
                target.StringReference["defaultMaximumIdleMinutes"] = new IntVariable {
                    Value = _fetcher?.CategoryModel?.DefaultMaximumIdleMinutes ?? 0,
                };
                target.StringReference["idlePeriodScheduleId"] = new StringVariable {
                    Value = _fetcher?.CategoryModel?.IdlePeriodScheduleId ?? "",
                };
                target.StringReference["receivePeriodScheduleId"] = new StringVariable {
                    Value = _fetcher?.CategoryModel?.ReceivePeriodScheduleId ?? "",
                };
                enabled = false;
                target.enabled = true;
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2IdleCategoryModelLocalizationVariables
    {
        private Gs2IdleCategoryModelFetcher _fetcher;

        public void Awake() {
            target.enabled = false;
            _fetcher = GetComponent<Gs2IdleCategoryModelFetcher>() ?? GetComponentInParent<Gs2IdleCategoryModelFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2IdleCategoryModelFetcher.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2IdleCategoryModelLocalizationVariables
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2IdleCategoryModelLocalizationVariables
    {
        public LocalizeStringEvent target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2IdleCategoryModelLocalizationVariables
    {

    }
}

#endif