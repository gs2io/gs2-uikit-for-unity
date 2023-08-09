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
using Gs2.Unity.Gs2Idle.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Idle.Context;
using Gs2.Unity.UiKit.Gs2Idle.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Idle
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Idle/Status/View/Gs2IdleOwnStatusList")]
    public partial class Gs2IdleOwnStatusList : MonoBehaviour
    {
        private List<Gs2IdleOwnStatusContext> _children;

        public void Update() {
            if (_fetcher.Fetched && this._fetcher.Statuses != null) {
                for (var i = 0; i < this.maximumItems; i++) {
                    if (i < this._fetcher.Statuses.Count) {
                        _children[i].Status.categoryName = this._fetcher.Statuses[i].CategoryName;
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

    public partial class Gs2IdleOwnStatusList
    {
        private Gs2IdleOwnStatusListFetcher _fetcher;
        private Gs2IdleNamespaceContext Context => _fetcher.Context;

        public void Awake()
        {
            if (prefab == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2IdleOwnStatusContext Prefab.");
                enabled = false;
                return;
            }

            _fetcher = GetComponent<Gs2IdleOwnStatusListFetcher>() ?? GetComponentInParent<Gs2IdleOwnStatusListFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2IdleOwnStatusListFetcher.");
                enabled = false;
            }

            var context = GetComponent<Gs2IdleNamespaceContext>() ?? GetComponentInParent<Gs2IdleNamespaceContext>(true);
            if (context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2IdleOwnStatusListFetcher::Context.");
                enabled = false;
                return;
            }

            _children = new List<Gs2IdleOwnStatusContext>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.Status = OwnStatus.New(
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
            _fetcher = GetComponent<Gs2IdleOwnStatusListFetcher>() ?? GetComponentInParent<Gs2IdleOwnStatusListFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2IdleOwnStatusList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2IdleOwnStatusList
    {
        public Gs2IdleOwnStatusContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2IdleOwnStatusList
    {

    }
}