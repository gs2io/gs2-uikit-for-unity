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
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Ranking.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Ranking.Enabler
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Ranking/SubscribeUser/View/Enabler/Properties/TargetUserId/Gs2RankingSubscribeUserTargetUserIdEnabler")]
    public partial class Gs2RankingSubscribeUserTargetUserIdEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.SubscribeUser != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableTargetUserIds.Contains(_fetcher.SubscribeUser.TargetUserId));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableTargetUserIds.Contains(_fetcher.SubscribeUser.TargetUserId));
                        break;
                    case Expression.StartsWith:
                        target.SetActive(enableTargetUserId.StartsWith(_fetcher.SubscribeUser.TargetUserId));
                        break;
                    case Expression.EndsWith:
                        target.SetActive(enableTargetUserId.EndsWith(_fetcher.SubscribeUser.TargetUserId));
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

    public partial class Gs2RankingSubscribeUserTargetUserIdEnabler
    {
        private Gs2RankingSubscribeUserFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2RankingSubscribeUserFetcher>() ?? GetComponentInParent<Gs2RankingSubscribeUserFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2RankingSubscribeUserFetcher.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2RankingSubscribeUserTargetUserIdEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2RankingSubscribeUserTargetUserIdEnabler
    {
        public enum Expression {
            In,
            NotIn,
            StartsWith,
            EndsWith,
        }

        public Expression expression;

        public List<string> enableTargetUserIds;

        public string enableTargetUserId;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2RankingSubscribeUserTargetUserIdEnabler
    {
        
    }
}