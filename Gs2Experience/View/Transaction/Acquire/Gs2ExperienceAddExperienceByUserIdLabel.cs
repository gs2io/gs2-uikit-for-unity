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
using Gs2.Gs2Experience.Request;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Experience.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Experience
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Experience/Status/View/Transaction/Gs2ExperienceAddExperienceByUserIdLabel")]
    public partial class Gs2ExperienceAddExperienceByUserIdLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.AcquireAction != null && _fetcher.AcquireAction.Action == "Gs2Experience:AddExperienceByUserId" &&
                    _userDataFetcher != null && _userDataFetcher.Fetched && _userDataFetcher.Status != null) {
                var request = AddExperienceByUserIdRequest.FromJson(JsonMapper.ToObject(_fetcher.AcquireAction.Request));
                {
                    onUpdate?.Invoke(
                        format.Replace(
                            "{namespaceName}",
                            $"{request.NamespaceName}"
                        ).Replace(
                            "{userId}",
                            $"{request.UserId}"
                        ).Replace(
                            "{experienceName}",
                            $"{request.ExperienceName}"
                        ).Replace(
                            "{propertyId}",
                            $"{request.PropertyId}"
                        ).Replace(
                            "{experienceValue}",
                            $"{request.ExperienceValue}"
                        ).Replace(
                            "{userData:experienceName}",
                            $"{_userDataFetcher.Status.ExperienceName}"
                        ).Replace(
                            "{userData:propertyId}",
                            $"{_userDataFetcher.Status.PropertyId}"
                        ).Replace(
                            "{userData:experienceValue}",
                            $"{_userDataFetcher.Status.ExperienceValue}"
                        ).Replace(
                            "{userData:experienceValue:changed}",
                            $"{_userDataFetcher.Status.ExperienceValue + request.ExperienceValue}"
                        ).Replace(
                            "{userData:rankValue}",
                            $"{_userDataFetcher.Status.RankValue}"
                        ).Replace(
                            "{userData:rankCapValue}",
                            $"{_userDataFetcher.Status.RankCapValue}"
                        )
                    );
                }
            } else if (_fetcher.Fetched && _fetcher.AcquireAction != null && _fetcher.AcquireAction.Action == "Gs2Experience:AddExperienceByUserId") {
                var request = AddExperienceByUserIdRequest.FromJson(JsonMapper.ToObject(_fetcher.AcquireAction.Request));
                {
                    onUpdate?.Invoke(
                        format.Replace(
                            "{namespaceName}",
                            $"{request.NamespaceName}"
                        ).Replace(
                            "{userId}",
                            $"{request.UserId}"
                        ).Replace(
                            "{experienceName}",
                            $"{request.ExperienceName}"
                        ).Replace(
                            "{propertyId}",
                            $"{request.PropertyId}"
                        ).Replace(
                            "{experienceValue}",
                            $"{request.ExperienceValue}"
                        )
                    );
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2ExperienceAddExperienceByUserIdLabel
    {
        private Gs2CoreAcquireActionFetcher _fetcher;
        private Gs2ExperienceOwnStatusFetcher _userDataFetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2CoreAcquireActionFetcher>() ?? GetComponentInParent<Gs2CoreAcquireActionFetcher>();
            _userDataFetcher = GetComponent<Gs2ExperienceOwnStatusFetcher>() ?? GetComponentInParent<Gs2ExperienceOwnStatusFetcher>();

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

    public partial class Gs2ExperienceAddExperienceByUserIdLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2ExperienceAddExperienceByUserIdLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ExperienceAddExperienceByUserIdLabel
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