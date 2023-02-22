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
using Gs2.Gs2Limit.Request;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Limit.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Limit
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Limit/Counter/View/Transaction/Gs2LimitDeleteCounterByUserIdLabel")]
    public partial class Gs2LimitDeleteCounterByUserIdLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.AcquireAction != null && _fetcher.AcquireAction.Action == "Gs2Limit:DeleteCounterByUserId" &&
                    _userDataFetcher != null && _userDataFetcher.Fetched && _userDataFetcher.Counter != null) {
                var request = DeleteCounterByUserIdRequest.FromJson(JsonMapper.ToObject(_fetcher.AcquireAction.Request));
                {
                    onUpdate?.Invoke(
                        format.Replace(
                            "{namespaceName}",
                            $"{request.NamespaceName}"
                        ).Replace(
                            "{limitName}",
                            $"{request.LimitName}"
                        ).Replace(
                            "{userId}",
                            $"{request.UserId}"
                        ).Replace(
                            "{counterName}",
                            $"{request.CounterName}"
                        ).Replace(
                            "{userData:counterId}",
                            $"{_userDataFetcher.Counter.CounterId}"
                        ).Replace(
                            "{userData:limitName}",
                            $"{_userDataFetcher.Counter.LimitName}"
                        ).Replace(
                            "{userData:name}",
                            $"{_userDataFetcher.Counter.Name}"
                        ).Replace(
                            "{userData:count}",
                            $"{_userDataFetcher.Counter.Count}"
                        ).Replace(
                            "{userData:createdAt}",
                            $"{_userDataFetcher.Counter.CreatedAt}"
                        ).Replace(
                            "{userData:updatedAt}",
                            $"{_userDataFetcher.Counter.UpdatedAt}"
                        )
                    );
                }
            } else if (_fetcher.Fetched && _fetcher.AcquireAction != null && _fetcher.AcquireAction.Action == "Gs2Limit:DeleteCounterByUserId") {
                var request = DeleteCounterByUserIdRequest.FromJson(JsonMapper.ToObject(_fetcher.AcquireAction.Request));
                {
                    onUpdate?.Invoke(
                        format.Replace(
                            "{namespaceName}",
                            $"{request.NamespaceName}"
                        ).Replace(
                            "{limitName}",
                            $"{request.LimitName}"
                        ).Replace(
                            "{userId}",
                            $"{request.UserId}"
                        ).Replace(
                            "{counterName}",
                            $"{request.CounterName}"
                        )
                    );
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2LimitDeleteCounterByUserIdLabel
    {
        private Gs2CoreAcquireActionFetcher _fetcher;
        private Gs2LimitOwnCounterFetcher _userDataFetcher;

        public void Awake()
        {
            _fetcher = GetComponentInParent<Gs2CoreAcquireActionFetcher>();
            _userDataFetcher = GetComponentInParent<Gs2LimitOwnCounterFetcher>();

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

    public partial class Gs2LimitDeleteCounterByUserIdLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2LimitDeleteCounterByUserIdLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2LimitDeleteCounterByUserIdLabel
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