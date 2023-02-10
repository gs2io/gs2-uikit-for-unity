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
using Gs2.Unity.UiKit.Gs2Formation.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Formation
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Formation/MoldModel/View/Properties/MaxCapacity/Gs2FormationMoldModelMaxCapacityEnabler")]
    public partial class Gs2FormationMoldModelMaxCapacityEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableMaxCapacities.Contains(_fetcher.MoldModel.MaxCapacity));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableMaxCapacities.Contains(_fetcher.MoldModel.MaxCapacity));
                        break;
                    case Expression.Less:
                        target.SetActive(enableMaxCapacity < _fetcher.MoldModel.MaxCapacity);
                        break;
                    case Expression.LessEqual:
                        target.SetActive(enableMaxCapacity <= _fetcher.MoldModel.MaxCapacity);
                        break;
                    case Expression.Greater:
                        target.SetActive(enableMaxCapacity > _fetcher.MoldModel.MaxCapacity);
                        break;
                    case Expression.GreaterEqual:
                        target.SetActive(enableMaxCapacity >= _fetcher.MoldModel.MaxCapacity);
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
    
    public partial class Gs2FormationMoldModelMaxCapacityEnabler
    {
        private Gs2FormationMoldModelFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponentInParent<Gs2FormationMoldModelFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2FormationMoldModelMaxCapacityEnabler
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2FormationMoldModelMaxCapacityEnabler
    {
        public enum Expression {
            In,
            NotIn,
            Less,
            LessEqual,
            Greater,
            GreaterEqual,
        }

        public Expression expression;

        public List<int> enableMaxCapacities;

        public int enableMaxCapacity;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FormationMoldModelMaxCapacityEnabler
    {
        
    }
}