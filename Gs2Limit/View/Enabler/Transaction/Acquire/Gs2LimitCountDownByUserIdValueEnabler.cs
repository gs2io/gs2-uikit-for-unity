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
using System.Collections.Generic;
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

	[AddComponentMenu("GS2 UIKit/Limit/Counter/View/Enabler/Transaction/Gs2LimitCountDownByUserIdValueEnabler")]
    public partial class Gs2LimitCountDownByUserIdValueEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Request != null) {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(_fetcher.Request.CountDownValue != null && enableCountDownValues.Contains(_fetcher.Request.CountDownValue.Value));
                        break;
                    case Expression.NotIn:
                        target.SetActive(_fetcher.Request.CountDownValue != null && !enableCountDownValues.Contains(_fetcher.Request.CountDownValue.Value));
                        break;
                    case Expression.Less:
                        target.SetActive(enableCountDownValue > _fetcher.Request.CountDownValue);
                        break;
                    case Expression.LessEqual:
                        target.SetActive(enableCountDownValue >= _fetcher.Request.CountDownValue);
                        break;
                    case Expression.Greater:
                        target.SetActive(enableCountDownValue < _fetcher.Request.CountDownValue);
                        break;
                    case Expression.GreaterEqual:
                        target.SetActive(enableCountDownValue <= _fetcher.Request.CountDownValue);
                        break;
                }
            }
            else
            {
                target.SetActive(enableCountDownValues.Contains(0));
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2LimitCountDownByUserIdValueEnabler
    {
        private Gs2LimitCountDownByUserIdFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2LimitCountDownByUserIdFetcher>() ?? GetComponentInParent<Gs2LimitCountDownByUserIdFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2LimitCountDownByUserIdFetcher.");
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
            _fetcher = GetComponent<Gs2LimitCountDownByUserIdFetcher>() ?? GetComponentInParent<Gs2LimitCountDownByUserIdFetcher>(true);
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

    public partial class Gs2LimitCountDownByUserIdValueEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2LimitCountDownByUserIdValueEnabler
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

        public List<int> enableCountDownValues;

        public int enableCountDownValue;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2LimitCountDownByUserIdValueEnabler
    {

    }
}