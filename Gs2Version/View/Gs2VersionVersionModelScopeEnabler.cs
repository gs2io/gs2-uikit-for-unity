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

using System.Collections.Generic;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Version.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Version
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Version/VersionModel/View/Properties/Scope/Gs2VersionVersionModelScopeEnabler")]
    public partial class Gs2VersionVersionModelScopeEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.VersionModel != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableScopes.Contains(_fetcher.VersionModel.Scope));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableScopes.Contains(_fetcher.VersionModel.Scope));
                        break;
                    case Expression.StartsWith:
                        target.SetActive(enableScope.StartsWith(_fetcher.VersionModel.Scope));
                        break;
                    case Expression.EndsWith:
                        target.SetActive(enableScope.EndsWith(_fetcher.VersionModel.Scope));
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

    public partial class Gs2VersionVersionModelScopeEnabler
    {
        private Gs2VersionVersionModelFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2VersionVersionModelFetcher>() ?? GetComponentInParent<Gs2VersionVersionModelFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2VersionVersionModelFetcher.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2VersionVersionModelScopeEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2VersionVersionModelScopeEnabler
    {
        public enum Expression {
            In,
            NotIn,
            StartsWith,
            EndsWith,
        }

        public Expression expression;

        public List<string> enableScopes;

        public string enableScope;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2VersionVersionModelScopeEnabler
    {
        
    }
}