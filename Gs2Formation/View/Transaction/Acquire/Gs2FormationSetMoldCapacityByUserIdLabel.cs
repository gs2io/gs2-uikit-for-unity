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
using Gs2.Gs2Formation.Request;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Formation.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Formation
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Formation/Mold/View/Transaction/Gs2FormationSetMoldCapacityByUserIdLabel")]
    public partial class Gs2FormationSetMoldCapacityByUserIdLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.AcquireAction != null && _fetcher.AcquireAction.Action == "Gs2Formation:SetMoldCapacityByUserId" &&
                    _userDataFetcher != null && _userDataFetcher.Fetched && _userDataFetcher.Mold != null) {
                var request = SetMoldCapacityByUserIdRequest.FromJson(JsonMapper.ToObject(_fetcher.AcquireAction.Request));
                {
                    onUpdate?.Invoke(
                        format.Replace(
                            "{namespaceName}",
                            $"{request.NamespaceName}"
                        ).Replace(
                            "{userId}",
                            $"{request.UserId}"
                        ).Replace(
                            "{moldName}",
                            $"{request.MoldName}"
                        ).Replace(
                            "{capacity}",
                            $"{request.Capacity}"
                        ).Replace(
                            "{userData:name}",
                            $"{_userDataFetcher.Mold.Name}"
                        ).Replace(
                            "{userData:userId}",
                            $"{_userDataFetcher.Mold.UserId}"
                        ).Replace(
                            "{userData:capacity}",
                            $"{_userDataFetcher.Mold.Capacity}"
                        )
                    );
                }
            } else if (_fetcher.Fetched && _fetcher.AcquireAction != null && _fetcher.AcquireAction.Action == "Gs2Formation:SetMoldCapacityByUserId") {
                var request = SetMoldCapacityByUserIdRequest.FromJson(JsonMapper.ToObject(_fetcher.AcquireAction.Request));
                {
                    onUpdate?.Invoke(
                        format.Replace(
                            "{namespaceName}",
                            $"{request.NamespaceName}"
                        ).Replace(
                            "{userId}",
                            $"{request.UserId}"
                        ).Replace(
                            "{moldName}",
                            $"{request.MoldName}"
                        ).Replace(
                            "{capacity}",
                            $"{request.Capacity}"
                        )
                    );
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2FormationSetMoldCapacityByUserIdLabel
    {
        private Gs2CoreAcquireActionFetcher _fetcher;
        private Gs2FormationOwnMoldFetcher _userDataFetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2CoreAcquireActionFetcher>() ?? GetComponentInParent<Gs2CoreAcquireActionFetcher>();
            _userDataFetcher = GetComponent<Gs2FormationOwnMoldFetcher>() ?? GetComponentInParent<Gs2FormationOwnMoldFetcher>();

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

    public partial class Gs2FormationSetMoldCapacityByUserIdLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2FormationSetMoldCapacityByUserIdLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FormationSetMoldCapacityByUserIdLabel
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