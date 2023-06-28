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
using Gs2.Unity.Gs2MegaField.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2MegaField.Context;
using Gs2.Unity.UiKit.Gs2MegaField.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2MegaField
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/MegaField/AreaModel/View/Gs2MegaFieldAreaModelList")]
    public partial class Gs2MegaFieldAreaModelList : MonoBehaviour
    {
        private List<Gs2MegaFieldAreaModelContext> _children;

        public void Update() {
            if (_fetcher.Fetched && this._fetcher.AreaModels != null) {
                for (var i = 0; i < this.maximumItems; i++) {
                    if (i < this._fetcher.AreaModels.Count) {
                        _children[i].AreaModel.areaModelName = this._fetcher.AreaModels[i].Name;
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

    public partial class Gs2MegaFieldAreaModelList
    {
        private Gs2MegaFieldNamespaceContext _context;
        private Gs2MegaFieldAreaModelListFetcher _fetcher;

        public void Awake()
        {
            _context = GetComponent<Gs2MegaFieldNamespaceContext>() ?? GetComponentInParent<Gs2MegaFieldNamespaceContext>();
            _fetcher = GetComponent<Gs2MegaFieldAreaModelListFetcher>() ?? GetComponentInParent<Gs2MegaFieldAreaModelListFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MegaFieldAreaModelListFetcher.");
                enabled = false;
            }

            _children = new List<Gs2MegaFieldAreaModelContext>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.AreaModel = AreaModel.New(
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
            _context = GetComponent<Gs2MegaFieldNamespaceContext>() ?? GetComponentInParent<Gs2MegaFieldNamespaceContext>(true);
            _fetcher = GetComponent<Gs2MegaFieldAreaModelListFetcher>() ?? GetComponentInParent<Gs2MegaFieldAreaModelListFetcher>(true);
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

    public partial class Gs2MegaFieldAreaModelList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2MegaFieldAreaModelList
    {
        public Gs2MegaFieldAreaModelContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MegaFieldAreaModelList
    {

    }
}