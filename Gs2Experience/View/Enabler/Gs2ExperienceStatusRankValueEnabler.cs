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
 *
 * deny overwrite
 */
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable CheckNamespace

using System.Collections.Generic;
using Gs2.Unity.UiKit.Gs2Experience.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Experience
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Experience/Status/View/Enabler/Properties/RankValue/Gs2ExperienceStatusRankValueEnabler")]
    public partial class Gs2ExperienceStatusRankValueEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Status != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableRankValues.Contains(_fetcher.Status.RankValue));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableRankValues.Contains(_fetcher.Status.RankValue));
                        break;
                    case Expression.Less:
                        target.SetActive(enableRankValue > _fetcher.Status.RankValue);
                        break;
                    case Expression.LessEqual:
                        target.SetActive(enableRankValue >= _fetcher.Status.RankValue);
                        break;
                    case Expression.Greater:
                        target.SetActive(enableRankValue < _fetcher.Status.RankValue);
                        break;
                    case Expression.GreaterEqual:
                        target.SetActive(enableRankValue <= _fetcher.Status.RankValue);
                        break;
                    case Expression.ReachMax:
                        target.SetActive(_fetcher.Status.RankValue == _fetcher.Status.RankCapValue);
                        break;
                    case Expression.NotReachMax:
                        target.SetActive(_fetcher.Status.RankValue != _fetcher.Status.RankCapValue);
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

    public partial class Gs2ExperienceStatusRankValueEnabler
    {
        private Gs2ExperienceOwnStatusFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponentInParent<Gs2ExperienceOwnStatusFetcher>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2ExperienceStatusRankValueEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2ExperienceStatusRankValueEnabler
    {
        public enum Expression {
            In,
            NotIn,
            Less,
            LessEqual,
            Greater,
            GreaterEqual,
            ReachMax,
            NotReachMax,
        }

        public Expression expression;

        public List<long> enableRankValues;

        public long enableRankValue;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ExperienceStatusRankValueEnabler
    {
        
    }
}