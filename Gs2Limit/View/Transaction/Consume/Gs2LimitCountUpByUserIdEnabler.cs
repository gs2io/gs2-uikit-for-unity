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

	[AddComponentMenu("GS2 UIKit/Limit/Counter/View/Transaction/Gs2LimitCountUpByUserIdEnabler")]
    public partial class Gs2LimitCountUpByUserIdEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.ConsumeAction != null && _fetcher.ConsumeAction.Action == "Gs2Limit:CountUpByUserId" &&
                    _userDataFetcher.Fetched && _userDataFetcher.Counter != null) {
                var request = CountUpByUserIdRequest.FromJson(JsonMapper.ToObject(_fetcher.ConsumeAction.Request));
                {
                    switch (expression) {
                        case Expression.ReachMax:
                            target.SetActive(_userDataFetcher.Counter.Count >= request.MaxValue);
                            break;
                        case Expression.NotReachMax:
                            target.SetActive(_userDataFetcher.Counter.Count < request.MaxValue);
                            break;
                    }
                }
            } else {
                switch (expression) {
                    case Expression.ReachMax:
                        target.SetActive(false);
                        break;
                    case Expression.NotReachMax:
                        target.SetActive(true);
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2LimitCountUpByUserIdEnabler
    {
        private Gs2CoreConsumeActionFetcher _fetcher;
        private Gs2LimitOwnCounterFetcher _userDataFetcher;

        public void Awake()
        {
            _fetcher = GetComponentInParent<Gs2CoreConsumeActionFetcher>();
            _userDataFetcher = GetComponentInParent<Gs2LimitOwnCounterFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2CoreConsumeActionFetcher.");
                enabled = false;
            }
            if (_userDataFetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2LimitOwnCounterFetcher.");
                enabled = false;
            }

            Update();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2LimitCountUpByUserIdEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2LimitCountUpByUserIdEnabler
    {
        public enum Expression {
            ReachMax,
            NotReachMax,
        }

        public Expression expression;
        
        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2LimitCountUpByUserIdEnabler
    {
        
    }
}