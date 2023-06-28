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
using Gs2.Unity.UiKit.Gs2MegaField.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2MegaField
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/MegaField/Spatial/View/Enabler/Properties/LayerModelName/Gs2MegaFieldSpatialLayerModelNameEnabler")]
    public partial class Gs2MegaFieldSpatialLayerModelNameEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Spatial != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableLayerModelNames.Contains(_fetcher.Spatial.LayerModelName));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableLayerModelNames.Contains(_fetcher.Spatial.LayerModelName));
                        break;
                    case Expression.StartsWith:
                        target.SetActive(enableLayerModelName.StartsWith(_fetcher.Spatial.LayerModelName));
                        break;
                    case Expression.EndsWith:
                        target.SetActive(enableLayerModelName.EndsWith(_fetcher.Spatial.LayerModelName));
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

    public partial class Gs2MegaFieldSpatialLayerModelNameEnabler
    {
        private Gs2MegaFieldSpatialFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2MegaFieldSpatialFetcher>() ?? GetComponentInParent<Gs2MegaFieldSpatialFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MegaFieldSpatialFetcher.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2MegaFieldSpatialLayerModelNameEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2MegaFieldSpatialLayerModelNameEnabler
    {
        public enum Expression {
            In,
            NotIn,
            StartsWith,
            EndsWith,
        }

        public Expression expression;

        public List<string> enableLayerModelNames;

        public string enableLayerModelName;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MegaFieldSpatialLayerModelNameEnabler
    {
        
    }
}