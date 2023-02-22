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
using System.Linq;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Core.Acquire;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Core
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Core/AcquireAction/View/Gs2CoreAcquireActionsEnabler")]
    public partial class Gs2CoreAcquireActionsEnabler : MonoBehaviour
    {
        public void Update()
        {
            switch(expression)
            {
                case Expression.Include:
                    target.SetActive((this._context.AcquireActions == null ? new string[]{} : this._context.AcquireActions.Select(v => v.Action)).Contains(action));
                    break;
                case Expression.NotInclude:
                    target.SetActive(!(this._context.AcquireActions == null ? new string[]{} : this._context.AcquireActions.Select(v => v.Action)).Contains(action));
                    break;
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2CoreAcquireActionsEnabler
    {
        private Gs2CoreAcquireActionsContext _context;

        public void Awake()
        {
            _context = GetComponentInParent<Gs2CoreAcquireActionsContext>();
            
            if (_context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2CoreAcquireActionsContext.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2CoreAcquireActionsEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2CoreAcquireActionsEnabler
    {
        public enum Expression {
            Include,
            NotInclude,
        }

        public Expression expression;

        public string action;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2CoreAcquireActionsEnabler
    {
        
    }
}