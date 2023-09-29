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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gs2.Core.Exception;
using Gs2.Gs2Limit.Request;
using Gs2.Unity.Gs2Limit.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Limit.Context;
using Gs2.Unity.Util;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Limit.Fetcher
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Limit/Counter/Fetcher/Consume/Gs2LimitCountUpByUserIdFetcher")]
    public partial class Gs2LimitCountUpByUserIdFetcher : Gs2LimitOwnCounterContext
    {
        private void Fetch()
        {
            var action = _fetcher.ConsumeActions().FirstOrDefault(v => v.Action == "Gs2Limit:CountUpByUserId");
            if (action != null) {
                Request = CountUpByUserIdRequest.FromJson(JsonMapper.ToObject(action.Request));
                if (Counter == null || (
                        Counter.NamespaceName == Request.NamespaceName &&
                        Counter.LimitName == Request.LimitName &&
                        Counter.CounterName == Request.CounterName)
                   ) {
                    Counter = OwnCounter.New(
                                Namespace.New(
                                    Request.NamespaceName
                                ),
                                Request.LimitName,
                                Request.CounterName
                            );
                }
            }
            Fetched = true;
            this.OnFetched.Invoke();
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2LimitCountUpByUserIdFetcher
    {
        private IConsumeActionsFetcher _fetcher;

        public new void Start() {

        }

        public void Awake()
        {
            this._fetcher = GetComponent<IConsumeActionsFetcher>() ?? GetComponentInParent<IConsumeActionsFetcher>();
            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the IConsumeActionFetcher.");
                enabled = false;
            }
        }

        public override bool HasError()
        {
            this._fetcher = GetComponent<IConsumeActionsFetcher>() ?? GetComponentInParent<IConsumeActionsFetcher>(true);
            if (this._fetcher == null) {
                return true;
            }
            return false;
        }

        public void OnEnable()
        {
            this._fetcher.OnFetchedEvent().AddListener(Fetch);
            if (this._fetcher.IsFetched()) {
                Fetch();
            }
        }

        public void OnDisable()
        {
            this._fetcher.OnFetchedEvent().RemoveListener(Fetch);
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2LimitCountUpByUserIdFetcher
    {
        public CountUpByUserIdRequest Request { get; protected set; }
        public bool Fetched { get; protected set; }
        public UnityEvent OnFetched = new UnityEvent();
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2LimitCountUpByUserIdFetcher
    {

    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2LimitCountUpByUserIdFetcher
    {

    }
}