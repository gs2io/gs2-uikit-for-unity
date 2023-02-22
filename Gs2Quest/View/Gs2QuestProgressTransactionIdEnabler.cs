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
using Gs2.Unity.UiKit.Gs2Quest.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Quest
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Quest/Progress/View/Properties/TransactionId/Gs2QuestProgressTransactionIdEnabler")]
    public partial class Gs2QuestProgressTransactionIdEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Progress != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableTransactionIds.Contains(_fetcher.Progress.TransactionId));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableTransactionIds.Contains(_fetcher.Progress.TransactionId));
                        break;
                    case Expression.StartsWith:
                        target.SetActive(enableTransactionId.StartsWith(_fetcher.Progress.TransactionId));
                        break;
                    case Expression.EndsWith:
                        target.SetActive(enableTransactionId.EndsWith(_fetcher.Progress.TransactionId));
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

    public partial class Gs2QuestProgressTransactionIdEnabler
    {
        private Gs2QuestOwnProgressFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponentInParent<Gs2QuestOwnProgressFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2QuestOwnProgressFetcher.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2QuestProgressTransactionIdEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2QuestProgressTransactionIdEnabler
    {
        public enum Expression {
            In,
            NotIn,
            StartsWith,
            EndsWith,
        }

        public Expression expression;

        public List<string> enableTransactionIds;

        public string enableTransactionId;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2QuestProgressTransactionIdEnabler
    {
        
    }
}