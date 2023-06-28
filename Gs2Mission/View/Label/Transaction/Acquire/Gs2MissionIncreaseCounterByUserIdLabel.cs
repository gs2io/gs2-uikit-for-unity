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
 *
 * deny overwrite
 */
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable CheckNamespace

using System;
using System.Linq;
using Gs2.Gs2Mission.Request;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Mission.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Mission
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Mission/Counter/View/Transaction/Gs2MissionIncreaseCounterByUserIdLabel")]
    public partial class Gs2MissionIncreaseCounterByUserIdLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.AcquireAction != null && _fetcher.AcquireAction.Action == "Gs2Mission:IncreaseCounterByUserId" &&
                    _userDataFetcher != null && _userDataFetcher.Fetched && _userDataFetcher.Counter != null) {
                var request = IncreaseCounterByUserIdRequest.FromJson(JsonMapper.ToObject(_fetcher.AcquireAction.Request));
                {
                    onUpdate?.Invoke(
                        format.Replace(
                            "{namespaceName}",
                            $"{request.NamespaceName}"
                        ).Replace(
                            "{counterName}",
                            $"{request.CounterName}"
                        ).Replace(
                            "{userId}",
                            $"{request.UserId}"
                        ).Replace(
                            "{value}",
                            $"{request.Value}"
                        ).Replace(
                            "{userData:name}",
                            $"{_userDataFetcher.Counter.Name}"
                        ).Replace(
                            "{userData:values:notReset}",
                            $"{_userDataFetcher.Counter.Values.FirstOrDefault(v => v.ResetType == "notReset")?.Value}"
                        ).Replace(
                            "{userData:values:daily}",
                            $"{_userDataFetcher.Counter.Values.FirstOrDefault(v => v.ResetType == "daily")?.Value}"
                        ).Replace(
                            "{userData:values:weekly}",
                            $"{_userDataFetcher.Counter.Values.FirstOrDefault(v => v.ResetType == "weekly")?.Value}"
                        ).Replace(
                            "{userData:values:monthly}",
                            $"{_userDataFetcher.Counter.Values.FirstOrDefault(v => v.ResetType == "monthly")?.Value}"
                        ).Replace(
                            "{userData:values:notReset:changed}",
                            $"{(_userDataFetcher.Counter.Values.FirstOrDefault(v => v.ResetType == "notReset")?.Value ?? 0) + request.Value}"
                        ).Replace(
                            "{userData:values:daily:changed}",
                            $"{(_userDataFetcher.Counter.Values.FirstOrDefault(v => v.ResetType == "daily")?.Value ?? 0) + request.Value}"
                        ).Replace(
                            "{userData:values:weekly:changed}",
                            $"{(_userDataFetcher.Counter.Values.FirstOrDefault(v => v.ResetType == "weekly")?.Value ?? 0) + request.Value}"
                        ).Replace(
                            "{userData:values:monthly:changed}",
                            $"{(_userDataFetcher.Counter.Values.FirstOrDefault(v => v.ResetType == "monthly")?.Value ?? 0) + request.Value}"
                        )
                    );
                }
            } else if (_fetcher.Fetched && _fetcher.AcquireAction != null && _fetcher.AcquireAction.Action == "Gs2Mission:IncreaseCounterByUserId") {
                var request = IncreaseCounterByUserIdRequest.FromJson(JsonMapper.ToObject(_fetcher.AcquireAction.Request));
                {
                    onUpdate?.Invoke(
                        format.Replace(
                            "{namespaceName}",
                            $"{request.NamespaceName}"
                        ).Replace(
                            "{counterName}",
                            $"{request.CounterName}"
                        ).Replace(
                            "{userId}",
                            $"{request.UserId}"
                        ).Replace(
                            "{value}",
                            $"{request.Value}"
                        )
                    );
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2MissionIncreaseCounterByUserIdLabel
    {
        private Gs2CoreAcquireActionFetcher _fetcher;
        private Gs2MissionOwnCounterFetcher _userDataFetcher;

        public void Awake()
        {
            _fetcher = GetComponentInParent<Gs2CoreAcquireActionFetcher>();
            _userDataFetcher = GetComponentInParent<Gs2MissionOwnCounterFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2CoreAcquireActionFetcher.");
                enabled = false;
            }

            Update();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2MissionIncreaseCounterByUserIdLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2MissionIncreaseCounterByUserIdLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MissionIncreaseCounterByUserIdLabel
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