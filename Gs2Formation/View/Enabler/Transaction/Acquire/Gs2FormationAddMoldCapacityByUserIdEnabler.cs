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

using System;
using System.Collections.Generic;
using Gs2.Gs2Formation.Request;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Formation.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Formation
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Formation/Mold/View/Enabler/Transaction/Gs2FormationAddMoldCapacityByUserIdEnabler")]
    public partial class Gs2FormationAddMoldCapacityByUserIdEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Request != null) {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(_fetcher.Request.Capacity != null && enableCapacities.Contains(_fetcher.Request.Capacity.Value));
                        break;
                    case Expression.NotIn:
                        target.SetActive(_fetcher.Request.Capacity != null && !enableCapacities.Contains(_fetcher.Request.Capacity.Value));
                        break;
                    case Expression.Less:
                        target.SetActive(enableCapacity > _fetcher.Request.Capacity);
                        break;
                    case Expression.LessEqual:
                        target.SetActive(enableCapacity >= _fetcher.Request.Capacity);
                        break;
                    case Expression.Greater:
                        target.SetActive(enableCapacity < _fetcher.Request.Capacity);
                        break;
                    case Expression.GreaterEqual:
                        target.SetActive(enableCapacity <= _fetcher.Request.Capacity);
                        break;
                }
            }
            else
            {
                target.SetActive(enableCapacities.Contains(0));
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2FormationAddMoldCapacityByUserIdEnabler
    {
        private Gs2FormationAddMoldCapacityByUserIdFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2FormationAddMoldCapacityByUserIdFetcher>() ?? GetComponentInParent<Gs2FormationAddMoldCapacityByUserIdFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2FormationAddMoldCapacityByUserIdFetcher.");
                enabled = false;
            }

            Update();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2FormationAddMoldCapacityByUserIdEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2FormationAddMoldCapacityByUserIdEnabler
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

        public List<int> enableCapacities;

        public int enableCapacity;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FormationAddMoldCapacityByUserIdEnabler
    {

    }
}