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
using Gs2.Gs2Stamina.Request;
using Gs2.Unity.Gs2Stamina.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Stamina.Context;
using Gs2.Unity.Util;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Stamina.Fetcher
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Stamina/Stamina/Fetcher/Acquire/Gs2StaminaSetRecoverIntervalByUserIdFetcher")]
    public partial class Gs2StaminaSetRecoverIntervalByUserIdFetcher : Gs2StaminaOwnStaminaContext
    {
        private void Fetch()
        {
            var action = _fetcher.AcquireActions().FirstOrDefault(v => v.Action == "Gs2Stamina:SetRecoverIntervalByUserId");
            if (action != null) {
                Request = SetRecoverIntervalByUserIdRequest.FromJson(JsonMapper.ToObject(action.Request));
                if (Stamina == null || (
                        Stamina.NamespaceName == Request.NamespaceName &&
                        Stamina.StaminaName == Request.StaminaName)
                   ) {
                    Stamina = OwnStamina.New(
                                Namespace.New(
                                    Request.NamespaceName
                                ),
                                Request.StaminaName
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

    public partial class Gs2StaminaSetRecoverIntervalByUserIdFetcher
    {
        private IAcquireActionsFetcher _fetcher;

        public new void Start() {

        }

        public void Awake()
        {
            this._fetcher = GetComponent<IAcquireActionsFetcher>() ?? GetComponentInParent<IAcquireActionsFetcher>();
            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the IAcquireActionFetcher.");
                enabled = false;
            }
        }

        public override bool HasError()
        {
            this._fetcher = GetComponent<IAcquireActionsFetcher>() ?? GetComponentInParent<IAcquireActionsFetcher>(true);
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

    public partial class Gs2StaminaSetRecoverIntervalByUserIdFetcher
    {
        public SetRecoverIntervalByUserIdRequest Request { get; protected set; }
        public bool Fetched { get; protected set; }
        public UnityEvent OnFetched = new UnityEvent();
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2StaminaSetRecoverIntervalByUserIdFetcher
    {

    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2StaminaSetRecoverIntervalByUserIdFetcher
    {

    }
}