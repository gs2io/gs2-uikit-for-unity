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
using System.Collections.Generic;
using Gs2.Gs2Exchange.Request;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Exchange.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Exchange
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Exchange/Await/View/Transaction/Gs2ExchangeCreateAwaitByUserIdEnabler")]
    public partial class Gs2ExchangeCreateAwaitByUserIdEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.AcquireAction != null && _fetcher.AcquireAction.Action == "Gs2Exchange:CreateAwaitByUserId") {
                var request = CreateAwaitByUserIdRequest.FromJson(JsonMapper.ToObject(_fetcher.AcquireAction.Request));
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(request.Count != null && enableCounts.Contains(request.Count.Value));
                        break;
                    case Expression.NotIn:
                        target.SetActive(request.Count != null && !enableCounts.Contains(request.Count.Value));
                        break;
                    case Expression.Less:
                        target.SetActive(enableCount > request.Count);
                        break;
                    case Expression.LessEqual:
                        target.SetActive(enableCount >= request.Count);
                        break;
                    case Expression.Greater:
                        target.SetActive(enableCount < request.Count);
                        break;
                    case Expression.GreaterEqual:
                        target.SetActive(enableCount <= request.Count);
                        break;
                }
            }
            else
            {
                target.SetActive(enableCounts.Contains(0));
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2ExchangeCreateAwaitByUserIdEnabler
    {
        private Gs2CoreAcquireActionFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2CoreAcquireActionFetcher>() ?? GetComponentInParent<Gs2CoreAcquireActionFetcher>();

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

    public partial class Gs2ExchangeCreateAwaitByUserIdEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2ExchangeCreateAwaitByUserIdEnabler
    {
        public enum Expression {
            In,
            NotIn,
            Less,
            LessEqual,
            Greater,
            GreaterEqual,
        }

        public Expression expression;

        public List<int> enableCounts;

        public int enableCount;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ExchangeCreateAwaitByUserIdEnabler
    {

    }
}