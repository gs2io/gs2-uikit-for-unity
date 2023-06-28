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
using System.Collections;
using System.Linq;
using Gs2.Core.Exception;
using Gs2.Unity.Core.Exception;
using Gs2.Unity.Gs2Mission.Model;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Mission.Fetcher;
using Gs2.Unity.Util;
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
        private EzMissionGroupModel _missionGroupModel;
        
        public IEnumerator Process() {
            while (true) {
                yield return new WaitForSeconds(0.1f);
                
                if (_fetcher.Fetched && _fetcher.MissionTaskModel != null) {
                    var future = this._clientHolder.Gs2.Mission.Namespace(
                        _fetcher.Context.MissionTaskModel.NamespaceName
                    ).MissionGroupModel(
                        _fetcher.Context.MissionTaskModel.MissionGroupName
                    ).Model();
                    yield return future;
                    if (future.Error != null) {
                        this.onError.Invoke(new CanIgnoreException(future.Error), null);
                    }

                    _missionGroupModel = future.Result;
                }
            }
        }

        public void OnEnable() {
            StartCoroutine(nameof(Process));
        }
        
        public void Update()
        {
            if (this._missionGroupModel != null &&
                this._userDataFetcher.Fetched && this._userDataFetcher.Counter != null)
            {
                switch (this._missionGroupModel.ResetType) {
                    case "notReset":
                        onUpdate?.Invoke(
                            Math.Min((float)(this._userDataFetcher.Counter.Values.FirstOrDefault(v => v.ResetType == "notReset")?.Value ?? 0) / this._fetcher.MissionTaskModel.TargetValue, 1)
                        );
                        break;
                    case "daily":
                        onUpdate?.Invoke(
                            Math.Min((float)(this._userDataFetcher.Counter.Values.FirstOrDefault(v => v.ResetType == "daily")?.Value ?? 0) / this._fetcher.MissionTaskModel.TargetValue, 1)
                        );
                        break;
                    case "weekly":
                        onUpdate?.Invoke(
                            Math.Min((float)(this._userDataFetcher.Counter.Values.FirstOrDefault(v => v.ResetType == "weekly")?.Value ?? 0) / this._fetcher.MissionTaskModel.TargetValue, 1)
                        );
                        break;
                    case "monthly":
                        onUpdate?.Invoke(
                            Math.Min((float)(this._userDataFetcher.Counter.Values.FirstOrDefault(v => v.ResetType == "monthly")?.Value ?? 0) / this._fetcher.MissionTaskModel.TargetValue, 1)
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
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _sessionHolder;
        private Gs2MissionMissionTaskModelFetcher _fetcher;
        private Gs2MissionOwnCounterFetcher _userDataFetcher;

        public void Awake()
        {
            _clientHolder = Gs2ClientHolder.Instance;
            _sessionHolder = Gs2GameSessionHolder.Instance;
            this._fetcher = GetComponentInParent<Gs2MissionMissionTaskModelFetcher>();
            this._userDataFetcher = GetComponentInParent<Gs2MissionOwnCounterFetcher>();
            
            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MissionMissionTaskModelFetcher.");
                enabled = false;
            }
            if (this._userDataFetcher == null) {
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
        
        [SerializeField]
        internal ErrorEvent onError = new ErrorEvent();

        public event UnityAction<Gs2Exception, Func<IEnumerator>> OnError
        {
            add => this.onError.AddListener(value);
            remove => this.onError.RemoveListener(value);
        }
    }
}