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
using Gs2.Unity.UiKit.Core.Acquire;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Core
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Core/AcquireAction/View/Gs2CoreAcquireActionEnabler")]
    public partial class Gs2CoreAcquireActionEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.AcquireAction != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableActions.Contains(_fetcher.AcquireAction.Action));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableActions.Contains(_fetcher.AcquireAction.Action));
                        break;
                    case Expression.StartsWith:
                        target.SetActive(enableAction.StartsWith(_fetcher.AcquireAction.Action));
                        break;
                    case Expression.EndsWith:
                        target.SetActive(enableAction.EndsWith(_fetcher.AcquireAction.Action));
                        break;
                }
            }
            else
            {
                target.SetActive(!enableActions.Contains(_fetcher.AcquireAction.Action));
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2CoreAcquireActionEnabler
    {
        private Gs2CoreAcquireActionFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponentInParent<Gs2CoreAcquireActionFetcher>();
            
            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2CoreAcquireActionFetcher.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2CoreAcquireActionEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2CoreAcquireActionEnabler
    {
        public enum Expression {
            In,
            NotIn,
            StartsWith,
            EndsWith,
        }

        public Expression expression;

        public List<string> enableActions;

        public string enableAction;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2CoreAcquireActionEnabler
    {
        
    }
}