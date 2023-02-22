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
using Gs2.Gs2Enhance.Request;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Enhance.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Enhance
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Enhance/Progress/View/Transaction/Gs2EnhanceCreateProgressByUserIdLabel")]
    public partial class Gs2EnhanceCreateProgressByUserIdLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.AcquireAction != null && _fetcher.AcquireAction.Action == "Gs2Enhance:CreateProgressByUserId" &&
                    _userDataFetcher != null && _userDataFetcher.Fetched && _userDataFetcher.Progress != null) {
                var request = CreateProgressByUserIdRequest.FromJson(JsonMapper.ToObject(_fetcher.AcquireAction.Request));
                {
                    onUpdate?.Invoke(
                        format.Replace(
                            "{namespaceName}",
                            $"{request.NamespaceName}"
                        ).Replace(
                            "{userId}",
                            $"{request.UserId}"
                        ).Replace(
                            "{rateName}",
                            $"{request.RateName}"
                        ).Replace(
                            "{targetItemSetId}",
                            $"{request.TargetItemSetId}"
                        ).Replace(
                            "{materials}",
                            $"{request.Materials}"
                        ).Replace(
                            "{force}",
                            $"{request.Force}"
                        ).Replace(
                            "{userData:name}",
                            $"{_userDataFetcher.Progress.Name}"
                        ).Replace(
                            "{userData:rateName}",
                            $"{_userDataFetcher.Progress.RateName}"
                        ).Replace(
                            "{userData:propertyId}",
                            $"{_userDataFetcher.Progress.PropertyId}"
                        ).Replace(
                            "{userData:experienceValue}",
                            $"{_userDataFetcher.Progress.ExperienceValue}"
                        ).Replace(
                            "{userData:rate}",
                            $"{_userDataFetcher.Progress.Rate}"
                        )
                    );
                }
            } else if (_fetcher.Fetched && _fetcher.AcquireAction != null && _fetcher.AcquireAction.Action == "Gs2Enhance:CreateProgressByUserId") {
                var request = CreateProgressByUserIdRequest.FromJson(JsonMapper.ToObject(_fetcher.AcquireAction.Request));
                {
                    onUpdate?.Invoke(
                        format.Replace(
                            "{namespaceName}",
                            $"{request.NamespaceName}"
                        ).Replace(
                            "{userId}",
                            $"{request.UserId}"
                        ).Replace(
                            "{rateName}",
                            $"{request.RateName}"
                        ).Replace(
                            "{targetItemSetId}",
                            $"{request.TargetItemSetId}"
                        ).Replace(
                            "{materials}",
                            $"{request.Materials}"
                        ).Replace(
                            "{force}",
                            $"{request.Force}"
                        )
                    );
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2EnhanceCreateProgressByUserIdLabel
    {
        private Gs2CoreAcquireActionFetcher _fetcher;
        private Gs2EnhanceOwnProgressFetcher _userDataFetcher;

        public void Awake()
        {
            _fetcher = GetComponentInParent<Gs2CoreAcquireActionFetcher>();
            _userDataFetcher = GetComponentInParent<Gs2EnhanceOwnProgressFetcher>();

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

    public partial class Gs2EnhanceCreateProgressByUserIdLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2EnhanceCreateProgressByUserIdLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2EnhanceCreateProgressByUserIdLabel
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