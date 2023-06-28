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

    [AddComponentMenu("GS2 UIKit/MegaField/LayerModel/View/Gs2MegaFieldLayerModelList")]
    public partial class Gs2MegaFieldLayerModelList : MonoBehaviour
    {
        private List<Gs2MegaFieldLayerModelContext> _children;

        public void Update() {
            if (_fetcher.Fetched && this._fetcher.LayerModels != null) {
                for (var i = 0; i < this.maximumItems; i++) {
                    if (i < this._fetcher.LayerModels.Count) {
                        _children[i].LayerModel.layerModelName = this._fetcher.LayerModels[i].Name;
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

    public partial class Gs2MegaFieldLayerModelList
    {
        private Gs2MegaFieldAreaModelContext _context;
        private Gs2MegaFieldLayerModelListFetcher _fetcher;

        public void Awake()
        {
            _context = GetComponent<Gs2MegaFieldAreaModelContext>() ?? GetComponentInParent<Gs2MegaFieldAreaModelContext>();
            _fetcher = GetComponent<Gs2MegaFieldLayerModelListFetcher>() ?? GetComponentInParent<Gs2MegaFieldLayerModelListFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MegaFieldLayerModelListFetcher.");
                enabled = false;
            }

            _children = new List<Gs2MegaFieldLayerModelContext>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.LayerModel = LayerModel.New(
                    _context.AreaModel,
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

    public partial class Gs2MegaFieldLayerModelList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2MegaFieldLayerModelList
    {
        public Gs2MegaFieldLayerModelContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MegaFieldLayerModelList
    {

    }
}