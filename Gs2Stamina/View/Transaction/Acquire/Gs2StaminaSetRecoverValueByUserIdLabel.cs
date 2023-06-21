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
using Gs2.Gs2Stamina.Request;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Stamina.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Stamina
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Stamina/Stamina/View/Transaction/Gs2StaminaSetRecoverValueByUserIdLabel")]
    public partial class Gs2StaminaSetRecoverValueByUserIdLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.AcquireAction != null && _fetcher.AcquireAction.Action == "Gs2Stamina:SetRecoverValueByUserId" &&
                    _userDataFetcher != null && _userDataFetcher.Fetched && _userDataFetcher.Stamina != null) {
                var request = SetRecoverValueByUserIdRequest.FromJson(JsonMapper.ToObject(_fetcher.AcquireAction.Request));
                {
                    onUpdate?.Invoke(
                        format.Replace(
                            "{namespaceName}",
                            $"{request.NamespaceName}"
                        ).Replace(
                            "{staminaName}",
                            $"{request.StaminaName}"
                        ).Replace(
                            "{userId}",
                            $"{request.UserId}"
                        ).Replace(
                            "{recoverValue}",
                            $"{request.RecoverValue}"
                        ).Replace(
                            "{userData:staminaName}",
                            $"{_userDataFetcher.Stamina.StaminaName}"
                        ).Replace(
                            "{userData:value}",
                            $"{_userDataFetcher.Stamina.Value}"
                        ).Replace(
                            "{userData:overflowValue}",
                            $"{_userDataFetcher.Stamina.OverflowValue}"
                        ).Replace(
                            "{userData:maxValue}",
                            $"{_userDataFetcher.Stamina.MaxValue}"
                        ).Replace(
                            "{userData:recoverIntervalMinutes}",
                            $"{_userDataFetcher.Stamina.RecoverIntervalMinutes}"
                        ).Replace(
                            "{userData:recoverValue}",
                            $"{_userDataFetcher.Stamina.RecoverValue}"
                        ).Replace(
                            "{userData:nextRecoverAt}",
                            $"{_userDataFetcher.Stamina.NextRecoverAt}"
                        )
                    );
                }
            } else if (_fetcher.Fetched && _fetcher.AcquireAction != null && _fetcher.AcquireAction.Action == "Gs2Stamina:SetRecoverValueByUserId") {
                var request = SetRecoverValueByUserIdRequest.FromJson(JsonMapper.ToObject(_fetcher.AcquireAction.Request));
                {
                    onUpdate?.Invoke(
                        format.Replace(
                            "{namespaceName}",
                            $"{request.NamespaceName}"
                        ).Replace(
                            "{staminaName}",
                            $"{request.StaminaName}"
                        ).Replace(
                            "{userId}",
                            $"{request.UserId}"
                        ).Replace(
                            "{recoverValue}",
                            $"{request.RecoverValue}"
                        )
                    );
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2StaminaSetRecoverValueByUserIdLabel
    {
        private Gs2CoreAcquireActionFetcher _fetcher;
        private Gs2StaminaOwnStaminaFetcher _userDataFetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2CoreAcquireActionFetcher>() ?? GetComponentInParent<Gs2CoreAcquireActionFetcher>();
            _userDataFetcher = GetComponent<Gs2StaminaOwnStaminaFetcher>() ?? GetComponentInParent<Gs2StaminaOwnStaminaFetcher>();

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

    public partial class Gs2StaminaSetRecoverValueByUserIdLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2StaminaSetRecoverValueByUserIdLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2StaminaSetRecoverValueByUserIdLabel
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