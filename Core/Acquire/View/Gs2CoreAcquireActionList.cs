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

namespace Gs2.Unity.UiKit.Gs2Core.View
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Core/AcquireAction/View/Gs2CoreAcquireActionList")]
    public partial class Gs2CoreAcquireActionList : MonoBehaviour
    {
        private List<Gs2CoreAcquireActionFetcher> _children;

        public void Update() {
            if (_context.AcquireActions != null) {
                for (var i = 0; i < this.maximumItems; i++) {
                    if (i < _context.AcquireActions.Count) {
                        _children[i].AcquireAction.action = _context.AcquireActions[i].action;
                        _children[i].AcquireAction.request = _context.AcquireActions[i].request;
                        _children[i].gameObject.SetActive(true);
                    }
                    else {
                        _children[i].gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2CoreAcquireActionList
    {
        private Gs2CoreAcquireActionsContext _context;

        public void Awake()
        {
            _context = GetComponentInParent<Gs2CoreAcquireActionsContext>();
            
            if (_context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2CoreAcquireActionsContext.");
                enabled = false;
            }

            _children = new List<Gs2CoreAcquireActionFetcher>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.AcquireAction = AcquireAction.New(
                    "",
                    ""
                );
                node.gameObject.SetActive(false);
                _children.Add(node);
            }
            this.prefab.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2CoreAcquireActionList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2CoreAcquireActionList
    {
        public Gs2CoreAcquireActionFetcher prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2CoreAcquireActionList
    {

    }
}