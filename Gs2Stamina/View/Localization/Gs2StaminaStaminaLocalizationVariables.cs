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
using Gs2.Unity.UiKit.Gs2Stamina.Fetcher;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

namespace Gs2.Unity.UiKit.Gs2Stamina.Localization
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Stamina/Stamina/View/Localization/Gs2StaminaStaminaLocalizationVariables")]
    public partial class Gs2StaminaStaminaLocalizationVariables : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched) {
                target.StringReference["staminaName"] = new StringVariable {
                    Value = _fetcher?.Stamina?.StaminaName ?? "",
                };
                target.StringReference["value"] = new IntVariable {
                    Value = _fetcher?.Stamina?.Value ?? 0,
                };
                target.StringReference["overflowValue"] = new IntVariable {
                    Value = _fetcher?.Stamina?.OverflowValue ?? 0,
                };
                target.StringReference["maxValue"] = new IntVariable {
                    Value = _fetcher?.Stamina?.MaxValue ?? 0,
                };
                target.StringReference["recoverIntervalMinutes"] = new IntVariable {
                    Value = _fetcher?.Stamina?.RecoverIntervalMinutes ?? 0,
                };
                target.StringReference["recoverValue"] = new IntVariable {
                    Value = _fetcher?.Stamina?.RecoverValue ?? 0,
                };
                target.StringReference["nextRecoverAt"] = new LongVariable {
                    Value = _fetcher?.Stamina?.NextRecoverAt ?? 0,
                };
                enabled = false;
                target.enabled = true;
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2StaminaStaminaLocalizationVariables
    {
        private Gs2StaminaOwnStaminaFetcher _fetcher;

        public void Awake() {
            target.enabled = false;
            _fetcher = GetComponent<Gs2StaminaOwnStaminaFetcher>() ?? GetComponentInParent<Gs2StaminaOwnStaminaFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2StaminaStaminaFetcher.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2StaminaStaminaLocalizationVariables
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2StaminaStaminaLocalizationVariables
    {
        public LocalizeStringEvent target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2StaminaStaminaLocalizationVariables
    {

    }
}

#endif