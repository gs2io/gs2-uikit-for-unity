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
using System.Linq;
using Gs2.Core.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Mission.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Mission
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Mission/Counter/View/Gs2MissionMissionTaskProgress")]
    public partial class Gs2MissionMissionTaskProgress : MonoBehaviour
    {
        public void Update()
        {
            if (_missionTaskModelFetcher.Fetched && _missionTaskModelFetcher.MissionTaskModel != null &&
                _counterFetcher.Fetched && _counterFetcher.Counter != null)
            {
                switch (resetType) {
                    case ResetType.NotReset:
                        onUpdate?.Invoke(
                            Math.Min((float)(_counterFetcher.Counter.Values.FirstOrDefault(v => v.ResetType == "notReset")?.Value ?? 0) / _missionTaskModelFetcher.MissionTaskModel.TargetValue, 1)
                        );
                        break;
                    case ResetType.Daily:
                        onUpdate?.Invoke(
                            Math.Min((float)(_counterFetcher.Counter.Values.FirstOrDefault(v => v.ResetType == "daily")?.Value ?? 0) / _missionTaskModelFetcher.MissionTaskModel.TargetValue, 1)
                        );
                        break;
                    case ResetType.Weekly:
                        onUpdate?.Invoke(
                            Math.Min((float)(_counterFetcher.Counter.Values.FirstOrDefault(v => v.ResetType == "weekly")?.Value ?? 0) / _missionTaskModelFetcher.MissionTaskModel.TargetValue, 1)
                        );
                        break;
                    case ResetType.Monthly:
                        onUpdate?.Invoke(
                            Math.Min((float)(_counterFetcher.Counter.Values.FirstOrDefault(v => v.ResetType == "monthly")?.Value ?? 0) / _missionTaskModelFetcher.MissionTaskModel.TargetValue, 1)
                        );
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2MissionMissionTaskProgress
    {
        private Gs2MissionMissionTaskModelFetcher _missionTaskModelFetcher;
        private Gs2MissionOwnCounterFetcher _counterFetcher;

        public void Awake()
        {
            _missionTaskModelFetcher = GetComponentInParent<Gs2MissionMissionTaskModelFetcher>();
            _counterFetcher = GetComponentInParent<Gs2MissionOwnCounterFetcher>();
            
            if (_missionTaskModelFetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MissionMissionTaskModelFetcher.");
                enabled = false;
            }
            if (_counterFetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MissionOwnCounterFetcher.");
                enabled = false;
            }

            Update();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2MissionMissionTaskProgress
    {
        public enum ResetType
        {
            NotReset,
            Daily,
            Weekly,
            Monthly
        }

        public ResetType resetType;
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2MissionMissionTaskProgress
    {
        
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MissionMissionTaskProgress
    {
        [Serializable]
        private class UpdateEvent : UnityEvent<float>
        {

        }

        [SerializeField]
        private UpdateEvent onUpdate = new UpdateEvent();

        public event UnityAction<float> OnUpdate
        {
            add => onUpdate.AddListener(value);
            remove => onUpdate.RemoveListener(value);
        }
    }
}