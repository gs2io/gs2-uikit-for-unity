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
using System.Linq;
using System.Collections.Generic;
using Gs2.Gs2Enhance.Request;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Enhance.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Enhance
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Enhance/Progress/View/Enabler/Transaction/Gs2EnhanceCreateProgressByUserIdEnabler")]
    public partial class Gs2EnhanceCreateProgressByUserIdEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (this._fetcher.AcquireActions().Count(v => v.Action == "Gs2Enhance:CreateProgressByUserId") == 0) {
                target.SetActive(this.notIncludeAcquireActions);
            }
            else {
                target.SetActive(this.includeAcquireActions);
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2EnhanceCreateProgressByUserIdEnabler
    {
        private IAcquireActionsFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<IAcquireActionsFetcher>() ?? GetComponentInParent<IAcquireActionsFetcher>();
            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the IAcquireActionsFetcher.");
                enabled = false;
            }
            if (target == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: target is not set.");
                enabled = false;
            }

            Update();
        }

        public virtual bool HasError()
        {
            _fetcher = GetComponent<IAcquireActionsFetcher>() ?? GetComponentInParent<IAcquireActionsFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            if (target == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2EnhanceCreateProgressByUserIdEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2EnhanceCreateProgressByUserIdEnabler
    {
        public bool includeAcquireActions;
        public bool notIncludeAcquireActions;
        
        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2EnhanceCreateProgressByUserIdEnabler
    {

    }
}