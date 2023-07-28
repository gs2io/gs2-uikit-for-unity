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
using Gs2.Unity.UiKit.Gs2Stamina.Fetcher;
using Gs2.Unity.Util;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Stamina.Fetcher
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Stamina/Stamina/Fetcher/Acquire/Gs2StaminaRecoverStaminaByUserIdFetcher")]
    public partial class Gs2StaminaRecoverStaminaByUserIdFetcher : Gs2StaminaOwnStaminaContext
    {
        private IEnumerator Fetch()
        {
            while (true)
            {
                if (_fetcher != null) {
                    var action = _fetcher.AcquireActions().FirstOrDefault(v => v.Action == "Gs2Stamina:RecoverStaminaByUserId");
                    if (action != null) {
                        Request = RecoverStaminaByUserIdRequest.FromJson(JsonMapper.ToObject(action.Request));
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
                        Fetched = true;
                    }
                }
                yield return new WaitForSeconds(1);
            }
            // ReSharper disable once IteratorNeverReturns
        }

        public void OnEnable()
        {
            StartCoroutine(nameof(Fetch));
        }

        public void OnDisable()
        {
            StopCoroutine(nameof(Fetch));
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2StaminaRecoverStaminaByUserIdFetcher
    {
        private IAcquireActionsFetcher _fetcher;

        public new void Start() {

        }

        public void Awake()
        {
            _fetcher = GetComponent<IAcquireActionsFetcher>() ?? GetComponentInParent<IAcquireActionsFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the IAcquireActionFetcher.");
                enabled = false;
            }
        }

        public bool HasError()
        {
            _fetcher = GetComponent<IAcquireActionsFetcher>() ?? GetComponentInParent<IAcquireActionsFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2StaminaRecoverStaminaByUserIdFetcher
    {
        public RecoverStaminaByUserIdRequest Request { get; protected set; }
        public bool Fetched { get; protected set; }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2StaminaRecoverStaminaByUserIdFetcher
    {

    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2StaminaRecoverStaminaByUserIdFetcher
    {
        [SerializeField]
        internal ErrorEvent onError = new ErrorEvent();

        public event UnityAction<Gs2Exception, Func<IEnumerator>> OnError
        {
            add => onError.AddListener(value);
            remove => onError.RemoveListener(value);
        }
    }
}