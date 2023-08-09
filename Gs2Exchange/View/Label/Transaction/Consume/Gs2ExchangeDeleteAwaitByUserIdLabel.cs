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
using Gs2.Gs2Exchange.Request;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Exchange.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Exchange.Label
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Exchange/Await/View/Label/Transaction/Gs2ExchangeDeleteAwaitByUserIdLabel")]
    public partial class Gs2ExchangeDeleteAwaitByUserIdLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Request != null &&
                    _userDataFetcher != null && _userDataFetcher.Fetched && _userDataFetcher.Await != null) {
                {
                    onUpdate?.Invoke(
                        format.Replace(
                            "{namespaceName}",
                            $"{_fetcher.Request.NamespaceName}"
                        ).Replace(
                            "{userId}",
                            $"{_fetcher.Request.UserId}"
                        ).Replace(
                            "{awaitName}",
                            $"{_fetcher.Request.AwaitName}"
                        ).Replace(
                            "{userData:userId}",
                            $"{_userDataFetcher.Await.UserId}"
                        ).Replace(
                            "{userData:rateName}",
                            $"{_userDataFetcher.Await.RateName}"
                        ).Replace(
                            "{userData:name}",
                            $"{_userDataFetcher.Await.Name}"
                        ).Replace(
                            "{userData:exchangedAt}",
                            $"{_userDataFetcher.Await.ExchangedAt}"
                        )
                    );
                }
            } else if (_fetcher.Fetched && _fetcher.Request != null) {
                {
                    onUpdate?.Invoke(
                        format.Replace(
                            "{namespaceName}",
                            $"{_fetcher.Request.NamespaceName}"
                        ).Replace(
                            "{userId}",
                            $"{_fetcher.Request.UserId}"
                        ).Replace(
                            "{awaitName}",
                            $"{_fetcher.Request.AwaitName}"
                        )
                    );
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2ExchangeDeleteAwaitByUserIdLabel
    {
        private Gs2ExchangeDeleteAwaitByUserIdFetcher _fetcher;
        private Gs2ExchangeOwnAwaitFetcher _userDataFetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2ExchangeDeleteAwaitByUserIdFetcher>() ?? GetComponentInParent<Gs2ExchangeDeleteAwaitByUserIdFetcher>();
            _userDataFetcher = GetComponent<Gs2ExchangeOwnAwaitFetcher>() ?? GetComponentInParent<Gs2ExchangeOwnAwaitFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ExchangeDeleteAwaitByUserIdFetcher.");
                enabled = false;
            }

            Update();
        }

        public virtual bool HasError()
        {
            _fetcher = GetComponent<Gs2ExchangeDeleteAwaitByUserIdFetcher>() ?? GetComponentInParent<Gs2ExchangeDeleteAwaitByUserIdFetcher>(true);
            _userDataFetcher = GetComponent<Gs2ExchangeOwnAwaitFetcher>() ?? GetComponentInParent<Gs2ExchangeOwnAwaitFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2ExchangeDeleteAwaitByUserIdLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2ExchangeDeleteAwaitByUserIdLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ExchangeDeleteAwaitByUserIdLabel
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