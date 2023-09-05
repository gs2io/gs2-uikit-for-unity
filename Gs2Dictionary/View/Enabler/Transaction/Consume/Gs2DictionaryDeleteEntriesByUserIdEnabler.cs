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
using Gs2.Gs2Dictionary.Request;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Dictionary.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Dictionary
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Dictionary/Entry/View/Enabler/Transaction/Gs2DictionaryDeleteEntriesByUserIdEnabler")]
    public partial class Gs2DictionaryDeleteEntriesByUserIdEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (this._fetcher.ConsumeActions().Count(v => v.Action == "Gs2Dictionary:DeleteEntriesByUserId") == 0) {
                target.SetActive(this.notIncludeConsumeActions);
            }
            else {
                target.SetActive(this.includeConsumeActions);
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2DictionaryDeleteEntriesByUserIdEnabler
    {
        private IConsumeActionsFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<IConsumeActionsFetcher>() ?? GetComponentInParent<IConsumeActionsFetcher>();
            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the IConsumeActionsFetcher.");
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
            _fetcher = GetComponent<IConsumeActionsFetcher>() ?? GetComponentInParent<IConsumeActionsFetcher>(true);
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

    public partial class Gs2DictionaryDeleteEntriesByUserIdEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2DictionaryDeleteEntriesByUserIdEnabler
    {
        public bool includeConsumeActions;
        public bool notIncludeConsumeActions;
        
        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2DictionaryDeleteEntriesByUserIdEnabler
    {

    }
}