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
using Gs2.Unity.UiKit.Gs2Matchmaking.Fetcher;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

namespace Gs2.Unity.UiKit.Gs2Matchmaking.Localization
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Matchmaking/Gathering/View/Localization/Gs2MatchmakingGatheringLocalizationVariables")]
    public partial class Gs2MatchmakingGatheringLocalizationVariables : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched) {
                target.StringReference["gatheringId"] = new StringVariable {
                    Value = _fetcher?.Gathering?.GatheringId ?? "",
                };
                target.StringReference["name"] = new StringVariable {
                    Value = _fetcher?.Gathering?.Name ?? "",
                };
                target.StringReference["metadata"] = new StringVariable {
                    Value = _fetcher?.Gathering?.Metadata ?? "",
                };
                target.StringReference["expiresAt"] = new LongVariable {
                    Value = _fetcher?.Gathering?.ExpiresAt ?? 0,
                };
                target.StringReference["createdAt"] = new LongVariable {
                    Value = _fetcher?.Gathering?.CreatedAt ?? 0,
                };
                target.StringReference["updatedAt"] = new LongVariable {
                    Value = _fetcher?.Gathering?.UpdatedAt ?? 0,
                };
                enabled = false;
                target.enabled = true;
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2MatchmakingGatheringLocalizationVariables
    {
        private Gs2MatchmakingGatheringFetcher _fetcher;

        public void Awake() {
            target.enabled = false;
            _fetcher = GetComponent<Gs2MatchmakingGatheringFetcher>() ?? GetComponentInParent<Gs2MatchmakingGatheringFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MatchmakingGatheringFetcher.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            _fetcher = GetComponent<Gs2MatchmakingGatheringFetcher>() ?? GetComponentInParent<Gs2MatchmakingGatheringFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2MatchmakingGatheringLocalizationVariables
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2MatchmakingGatheringLocalizationVariables
    {
        public LocalizeStringEvent target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MatchmakingGatheringLocalizationVariables
    {

    }
}

#endif