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
using Gs2.Unity.Gs2Mission.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Mission.Context;
using Gs2.Unity.UiKit.Gs2Mission.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Mission
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Mission/Complete/View/Gs2MissionOwnCompleteList")]
    public partial class Gs2MissionOwnCompleteList : MonoBehaviour
    {
        private List<Gs2MissionOwnCompleteContext> _children;

        public void Update() {
            if (_fetcher.Fetched && this._fetcher.Completes != null) {
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

    public partial class Gs2MissionOwnCompleteList
    {
        private Gs2MissionOwnCompleteListFetcher _fetcher;
        private Gs2MissionNamespaceContext Context => _fetcher.Context;

        public void Awake()
        {
            if (prefab == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MissionOwnCompleteContext Prefab.");
                enabled = false;
                return;
            }

            _fetcher = GetComponent<Gs2MissionOwnCompleteListFetcher>() ?? GetComponentInParent<Gs2MissionOwnCompleteListFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MissionOwnCompleteListFetcher.");
                enabled = false;
            }

            var context = GetComponent<Gs2MissionNamespaceContext>() ?? GetComponentInParent<Gs2MissionNamespaceContext>(true);
            if (context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MissionOwnCompleteListFetcher::Context.");
                enabled = false;
                return;
            }

            _children = new List<Gs2MissionOwnCompleteContext>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.Complete = OwnComplete.New(
                    context.Namespace,
                    ""
                );
                node.gameObject.SetActive(false);
                _children.Add(node);
            }
            this.prefab.gameObject.SetActive(false);
        }

        public virtual bool HasError()
        {
            _fetcher = GetComponent<Gs2MissionOwnCompleteListFetcher>() ?? GetComponentInParent<Gs2MissionOwnCompleteListFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2MissionOwnCompleteList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2MissionOwnCompleteList
    {
        public Gs2MissionOwnCompleteContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MissionOwnCompleteList
    {

    }
}