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

#if GS2_ENABLE_LOCALIZATION

using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Mission.Fetcher;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

namespace Gs2.Unity.UiKit.Gs2Mission.Localization
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Mission/MissionTaskModel/View/Localization/Gs2MissionMissionTaskModelLocalizationVariables")]
    public partial class Gs2MissionMissionTaskModelLocalizationVariables : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched) {
                target.StringReference["name"] = new StringVariable {
                    Value = _fetcher?.MissionTaskModel?.Name ?? "",
                };
                target.StringReference["metadata"] = new StringVariable {
                    Value = _fetcher?.MissionTaskModel?.Metadata ?? "",
                };
                target.StringReference["counterName"] = new StringVariable {
                    Value = _fetcher?.MissionTaskModel?.CounterName ?? "",
                };
                target.StringReference["targetResetType"] = new StringVariable {
                    Value = _fetcher?.MissionTaskModel?.TargetResetType ?? "",
                };
                target.StringReference["targetValue"] = new LongVariable {
                    Value = _fetcher?.MissionTaskModel?.TargetValue ?? 0,
                };
                target.StringReference["challengePeriodEventId"] = new StringVariable {
                    Value = _fetcher?.MissionTaskModel?.ChallengePeriodEventId ?? "",
                };
                target.StringReference["premiseMissionTaskName"] = new StringVariable {
                    Value = _fetcher?.MissionTaskModel?.PremiseMissionTaskName ?? "",
                };
                enabled = false;
                target.enabled = true;
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2MissionMissionTaskModelLocalizationVariables
    {
        private Gs2MissionMissionTaskModelFetcher _fetcher;

        public void Awake() {
            target.enabled = false;
            _fetcher = GetComponent<Gs2MissionMissionTaskModelFetcher>() ?? GetComponentInParent<Gs2MissionMissionTaskModelFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MissionMissionTaskModelFetcher.");
                enabled = false;
            }
        }

        public bool HasError()
        {
            _fetcher = GetComponent<Gs2MissionMissionTaskModelFetcher>() ?? GetComponentInParent<Gs2MissionMissionTaskModelFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2MissionMissionTaskModelLocalizationVariables
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2MissionMissionTaskModelLocalizationVariables
    {
        public LocalizeStringEvent target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MissionMissionTaskModelLocalizationVariables
    {

    }
}

#endif