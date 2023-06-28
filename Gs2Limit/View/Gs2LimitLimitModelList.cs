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
using Gs2.Unity.Gs2Limit.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Limit.Context;
using Gs2.Unity.UiKit.Gs2Limit.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Limit
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Limit/LimitModel/View/Gs2LimitLimitModelList")]
    public partial class Gs2LimitLimitModelList : MonoBehaviour
    {
        private List<Gs2LimitLimitModelContext> _children;

        public void Update() {
            if (_fetcher.Fetched && this._fetcher.LimitModels != null) {
                for (var i = 0; i < this.maximumItems; i++) {
                    if (i < this._fetcher.LimitModels.Count) {
                        _children[i].LimitModel.limitName = this._fetcher.LimitModels[i].Name;
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

    public partial class Gs2LimitLimitModelList
    {
        private Gs2LimitNamespaceContext _context;
        private Gs2LimitLimitModelListFetcher _fetcher;

        public void Awake()
        {
            _context = GetComponent<Gs2LimitNamespaceContext>() ?? GetComponentInParent<Gs2LimitNamespaceContext>();
            _fetcher = GetComponent<Gs2LimitLimitModelListFetcher>() ?? GetComponentInParent<Gs2LimitLimitModelListFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2LimitLimitModelListFetcher.");
                enabled = false;
            }

            _children = new List<Gs2LimitLimitModelContext>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.LimitModel = LimitModel.New(
                    _context.Namespace,
                    ""
                );
                node.gameObject.SetActive(false);
                _children.Add(node);
            }
            this.prefab.gameObject.SetActive(false);
        }

        public bool HasError()
        {
            _context = GetComponent<Gs2LimitNamespaceContext>() ?? GetComponentInParent<Gs2LimitNamespaceContext>(true);
            _fetcher = GetComponent<Gs2LimitLimitModelListFetcher>() ?? GetComponentInParent<Gs2LimitLimitModelListFetcher>(true);
            if (_context == null) {
                return true;
            }
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2LimitLimitModelList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2LimitLimitModelList
    {
        public Gs2LimitLimitModelContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2LimitLimitModelList
    {

    }
}