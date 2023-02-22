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
using Gs2.Unity.UiKit.Core.Consume;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Core
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Core/ConsumeAction/View/Gs2CoreConsumeActionEnabler")]
    public partial class Gs2CoreConsumeActionEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.ConsumeAction != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableActions.Contains(_fetcher.ConsumeAction.Action));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableActions.Contains(_fetcher.ConsumeAction.Action));
                        break;
                    case Expression.StartsWith:
                        target.SetActive(enableAction.StartsWith(_fetcher.ConsumeAction.Action));
                        break;
                    case Expression.EndsWith:
                        target.SetActive(enableAction.EndsWith(_fetcher.ConsumeAction.Action));
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

    public partial class Gs2CoreConsumeActionEnabler
    {
        private Gs2CoreConsumeActionFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponentInParent<Gs2CoreConsumeActionFetcher>();
            
            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2CoreConsumeActionFetcher.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2CoreConsumeActionEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2CoreConsumeActionEnabler
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
    public partial class Gs2CoreConsumeActionEnabler
    {
        
    }
}