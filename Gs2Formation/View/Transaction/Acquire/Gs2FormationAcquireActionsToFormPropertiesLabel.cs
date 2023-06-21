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

	[AddComponentMenu("GS2 UIKit/Formation/Form/View/Transaction/Gs2FormationAcquireActionsToFormPropertiesLabel")]
    public partial class Gs2FormationAcquireActionsToFormPropertiesLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.AcquireAction != null && _fetcher.AcquireAction.Action == "Gs2Formation:AcquireActionsToFormProperties" &&
                    _userDataFetcher != null && _userDataFetcher.Fetched && _userDataFetcher.Form != null) {
                var request = AcquireActionsToFormPropertiesRequest.FromJson(JsonMapper.ToObject(_fetcher.AcquireAction.Request));
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
                            "{index}",
                            $"{request.Index}"
                        ).Replace(
                            "{acquireAction}",
                            $"{request.AcquireAction}"
                        ).Replace(
                            "{config}",
                            $"{request.Config}"
                        ).Replace(
                            "{userData:name}",
                            $"{_userDataFetcher.Form.Name}"
                        ).Replace(
                            "{userData:index}",
                            $"{_userDataFetcher.Form.Index}"
                        ).Replace(
                            "{userData:slots}",
                            $"{_userDataFetcher.Form.Slots}"
                        )
                    );
                }
            } else if (_fetcher.Fetched && _fetcher.AcquireAction != null && _fetcher.AcquireAction.Action == "Gs2Formation:AcquireActionsToFormProperties") {
                var request = AcquireActionsToFormPropertiesRequest.FromJson(JsonMapper.ToObject(_fetcher.AcquireAction.Request));
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
                            "{index}",
                            $"{request.Index}"
                        ).Replace(
                            "{acquireAction}",
                            $"{request.AcquireAction}"
                        ).Replace(
                            "{config}",
                            $"{request.Config}"
                        )
                    );
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2FormationAcquireActionsToFormPropertiesLabel
    {
        private Gs2CoreAcquireActionFetcher _fetcher;
        private Gs2FormationOwnFormFetcher _userDataFetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2CoreAcquireActionFetcher>() ?? GetComponentInParent<Gs2CoreAcquireActionFetcher>();
            _userDataFetcher = GetComponent<Gs2FormationOwnFormFetcher>() ?? GetComponentInParent<Gs2FormationOwnFormFetcher>();

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

    public partial class Gs2FormationAcquireActionsToFormPropertiesLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2FormationAcquireActionsToFormPropertiesLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FormationAcquireActionsToFormPropertiesLabel
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