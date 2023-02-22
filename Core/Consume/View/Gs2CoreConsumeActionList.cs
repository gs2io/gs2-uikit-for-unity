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

namespace Gs2.Unity.UiKit.Gs2Core.View
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Core/ConsumeAction/View/Gs2CoreConsumeActionList")]
    public partial class Gs2CoreConsumeActionList : MonoBehaviour
    {
        private List<Gs2CoreConsumeActionFetcher> _children;

        public void Update() {
            if (_context.ConsumeActions != null) {
                for (var i = 0; i < this.maximumItems; i++) {
                    if (i < _context.ConsumeActions.Count) {
                        _children[i].ConsumeAction.action = _context.ConsumeActions[i].action;
                        _children[i].ConsumeAction.request = _context.ConsumeActions[i].request;
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

    public partial class Gs2CoreConsumeActionList
    {
        private Gs2CoreConsumeActionsContext _context;

        public void Awake()
        {
            _context = GetComponentInParent<Gs2CoreConsumeActionsContext>();
            
            if (_context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2CoreConsumeActionsContext.");
                enabled = false;
            }

            _children = new List<Gs2CoreConsumeActionFetcher>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.ConsumeAction = ConsumeAction.New(
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

    public partial class Gs2CoreConsumeActionList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2CoreConsumeActionList
    {
        public Gs2CoreConsumeActionFetcher prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2CoreConsumeActionList
    {

    }
}