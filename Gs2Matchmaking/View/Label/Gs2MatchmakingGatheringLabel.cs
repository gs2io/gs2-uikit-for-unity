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
using Gs2.Core.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Matchmaking.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Matchmaking
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Matchmaking/Gathering/View/Label/Gs2MatchmakingGatheringLabel")]
    public partial class Gs2MatchmakingGatheringLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Gathering != null)
            {
                var expiresAt = _fetcher.Gathering.ExpiresAt == null ? DateTime.Now : UnixTime.FromUnixTime(_fetcher.Gathering.ExpiresAt).ToLocalTime();
                var createdAt = _fetcher.Gathering.CreatedAt == null ? DateTime.Now : UnixTime.FromUnixTime(_fetcher.Gathering.CreatedAt).ToLocalTime();
                var updatedAt = _fetcher.Gathering.UpdatedAt == null ? DateTime.Now : UnixTime.FromUnixTime(_fetcher.Gathering.UpdatedAt).ToLocalTime();
                onUpdate?.Invoke(
                    format.Replace(
                        "{gatheringId}", $"{_fetcher?.Gathering?.GatheringId}"
                    ).Replace(
                        "{name}", $"{_fetcher?.Gathering?.Name}"
                    ).Replace(
                        "{attributeRanges}", $"{_fetcher?.Gathering?.AttributeRanges}"
                    ).Replace(
                        "{capacityOfRoles}", $"{_fetcher?.Gathering?.CapacityOfRoles}"
                    ).Replace(
                        "{allowUserIds}", $"{_fetcher?.Gathering?.AllowUserIds}"
                    ).Replace(
                        "{metadata}", $"{_fetcher?.Gathering?.Metadata}"
                    ).Replace(
                        "{expiresAt:yyyy}", expiresAt.ToString("yyyy")
                    ).Replace(
                        "{expiresAt:yy}", expiresAt.ToString("yy")
                    ).Replace(
                        "{expiresAt:MM}", expiresAt.ToString("MM")
                    ).Replace(
                        "{expiresAt:MMM}", expiresAt.ToString("MMM")
                    ).Replace(
                        "{expiresAt:dd}", expiresAt.ToString("dd")
                    ).Replace(
                        "{expiresAt:hh}", expiresAt.ToString("hh")
                    ).Replace(
                        "{expiresAt:HH}", expiresAt.ToString("HH")
                    ).Replace(
                        "{expiresAt:tt}", expiresAt.ToString("tt")
                    ).Replace(
                        "{expiresAt:mm}", expiresAt.ToString("mm")
                    ).Replace(
                        "{expiresAt:ss}", expiresAt.ToString("ss")
                    ).Replace(
                        "{createdAt:yyyy}", createdAt.ToString("yyyy")
                    ).Replace(
                        "{createdAt:yy}", createdAt.ToString("yy")
                    ).Replace(
                        "{createdAt:MM}", createdAt.ToString("MM")
                    ).Replace(
                        "{createdAt:MMM}", createdAt.ToString("MMM")
                    ).Replace(
                        "{createdAt:dd}", createdAt.ToString("dd")
                    ).Replace(
                        "{createdAt:hh}", createdAt.ToString("hh")
                    ).Replace(
                        "{createdAt:HH}", createdAt.ToString("HH")
                    ).Replace(
                        "{createdAt:tt}", createdAt.ToString("tt")
                    ).Replace(
                        "{createdAt:mm}", createdAt.ToString("mm")
                    ).Replace(
                        "{createdAt:ss}", createdAt.ToString("ss")
                    ).Replace(
                        "{updatedAt:yyyy}", updatedAt.ToString("yyyy")
                    ).Replace(
                        "{updatedAt:yy}", updatedAt.ToString("yy")
                    ).Replace(
                        "{updatedAt:MM}", updatedAt.ToString("MM")
                    ).Replace(
                        "{updatedAt:MMM}", updatedAt.ToString("MMM")
                    ).Replace(
                        "{updatedAt:dd}", updatedAt.ToString("dd")
                    ).Replace(
                        "{updatedAt:hh}", updatedAt.ToString("hh")
                    ).Replace(
                        "{updatedAt:HH}", updatedAt.ToString("HH")
                    ).Replace(
                        "{updatedAt:tt}", updatedAt.ToString("tt")
                    ).Replace(
                        "{updatedAt:mm}", updatedAt.ToString("mm")
                    ).Replace(
                        "{updatedAt:ss}", updatedAt.ToString("ss")
                    )
                );
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2MatchmakingGatheringLabel
    {
        private Gs2MatchmakingGatheringFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2MatchmakingGatheringFetcher>() ?? GetComponentInParent<Gs2MatchmakingGatheringFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MatchmakingGatheringFetcher.");
                enabled = false;
            }

            Update();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2MatchmakingGatheringLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2MatchmakingGatheringLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MatchmakingGatheringLabel
    {
        [Serializable]
        private class UpdateEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private UpdateEvent onUpdate = new UpdateEvent();

        public event UnityAction<string> OnUpdate
        {
            add => onUpdate.AddListener(value);
            remove => onUpdate.RemoveListener(value);
        }
    }
}