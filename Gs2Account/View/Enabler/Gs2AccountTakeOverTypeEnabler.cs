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

using System.Collections.Generic;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Account.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Account
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Account/TakeOver/View/Enabler/Properties/Type/Gs2AccountTakeOverTypeEnabler")]
    public partial class Gs2AccountTakeOverTypeEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.TakeOver != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableTypes.Contains(_fetcher.TakeOver.Type));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableTypes.Contains(_fetcher.TakeOver.Type));
                        break;
                    case Expression.Less:
                        target.SetActive(enableType > _fetcher.TakeOver.Type);
                        break;
                    case Expression.LessEqual:
                        target.SetActive(enableType >= _fetcher.TakeOver.Type);
                        break;
                    case Expression.Greater:
                        target.SetActive(enableType < _fetcher.TakeOver.Type);
                        break;
                    case Expression.GreaterEqual:
                        target.SetActive(enableType <= _fetcher.TakeOver.Type);
                        break;
                }
            }
            else
            {
                target.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2AccountTakeOverTypeEnabler
    {
        private Gs2AccountOwnTakeOverFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2AccountOwnTakeOverFetcher>() ?? GetComponentInParent<Gs2AccountOwnTakeOverFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2AccountOwnTakeOverFetcher.");
                enabled = false;
            }
            if (target == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: target is not set.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            _fetcher = GetComponent<Gs2AccountOwnTakeOverFetcher>() ?? GetComponentInParent<Gs2AccountOwnTakeOverFetcher>(true);
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

    public partial class Gs2AccountTakeOverTypeEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2AccountTakeOverTypeEnabler
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

        public List<int> enableTypes;

        public int enableType;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2AccountTakeOverTypeEnabler
    {
        
    }
}