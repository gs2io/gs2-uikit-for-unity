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
using Gs2.Unity.Gs2Mission.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Mission.Context;
using Gs2.Unity.UiKit.Gs2Mission.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Mission
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Mission/Complete/View/Gs2MissionCompleteList")]
    public partial class Gs2MissionCompleteList : MonoBehaviour
    {
        private List<Gs2MissionCompleteContext> _children;

        public void Update() {
            if (_fetcher.Fetched) {
                for (var i = 0; i < this.maximumItems; i++) {
                    if (i < this._fetcher.Completes.Count) {
                        _children[i].Complete.missionGroupName = this._fetcher.Completes[i].MissionGroupName;
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

    public partial class Gs2MissionCompleteList
    {
        private Gs2MissionUserContext _context;
        private Gs2MissionCompleteListFetcher _fetcher;

        public void Awake()
        {
            _context = GetComponentInParent<Gs2MissionUserContext>();
            _fetcher = GetComponentInParent<Gs2MissionCompleteListFetcher>();

            _children = new List<Gs2MissionCompleteContext>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.Complete = Complete.New(
                    _context.User,
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

    public partial class Gs2MissionCompleteList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2MissionCompleteList
    {
        public Gs2MissionCompleteContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MissionCompleteList
    {

    }
}