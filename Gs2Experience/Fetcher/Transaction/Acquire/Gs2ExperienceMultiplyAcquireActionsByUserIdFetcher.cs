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
using Gs2.Gs2Experience.Request;
using Gs2.Unity.Gs2Experience.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Experience.Context;
using Gs2.Unity.UiKit.Gs2Experience.Fetcher;
using Gs2.Unity.Util;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Experience.Fetcher
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Experience/Status/Fetcher/Acquire/Gs2ExperienceMultiplyAcquireActionsByUserIdFetcher")]
    public partial class Gs2ExperienceMultiplyAcquireActionsByUserIdFetcher : Gs2ExperienceOwnStatusContext
    {
        private IEnumerator Fetch()
        {
            while (true)
            {
                if (_fetcher != null) {
                    var action = _fetcher.AcquireActions().FirstOrDefault(v => v.Action == "Gs2Experience:MultiplyAcquireActionsByUserId");
                    if (action != null) {
                        Request = MultiplyAcquireActionsByUserIdRequest.FromJson(JsonMapper.ToObject(action.Request));
                        if (Status == null || (
                                Status.NamespaceName == Request.NamespaceName &&
                                Status.ExperienceName == Request.ExperienceName &&
                                Status.PropertyId == Request.PropertyId)
                           ) {
                            Status = OwnStatus.New(
                                Namespace.New(
                                    Request.NamespaceName
                                ),
                                Request.ExperienceName,
                                Request.PropertyId
                            );
                        }
                        Fetched = true;
                    }
                }
                yield return new WaitForSeconds(0.1f);
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

    public partial class Gs2ExperienceMultiplyAcquireActionsByUserIdFetcher
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

        public override bool HasError()
        {
            if (!base.HasError()) {
                return false;
            }
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

    public partial class Gs2ExperienceMultiplyAcquireActionsByUserIdFetcher
    {
        public MultiplyAcquireActionsByUserIdRequest Request { get; protected set; }
        public bool Fetched { get; protected set; }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2ExperienceMultiplyAcquireActionsByUserIdFetcher
    {

    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ExperienceMultiplyAcquireActionsByUserIdFetcher
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