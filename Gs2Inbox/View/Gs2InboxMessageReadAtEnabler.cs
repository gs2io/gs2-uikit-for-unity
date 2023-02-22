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
using Gs2.Unity.UiKit.Gs2Inbox.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Inbox
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Inbox/Message/View/Properties/ReadAt/Gs2InboxMessageReadAtEnabler")]
    public partial class Gs2InboxMessageReadAtEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Message != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableReadAts.Contains(_fetcher.Message.ReadAt));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableReadAts.Contains(_fetcher.Message.ReadAt));
                        break;
                    case Expression.Less:
                        target.SetActive(enableReadAt > _fetcher.Message.ReadAt);
                        break;
                    case Expression.LessEqual:
                        target.SetActive(enableReadAt >= _fetcher.Message.ReadAt);
                        break;
                    case Expression.Greater:
                        target.SetActive(enableReadAt < _fetcher.Message.ReadAt);
                        break;
                    case Expression.GreaterEqual:
                        target.SetActive(enableReadAt <= _fetcher.Message.ReadAt);
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

    public partial class Gs2InboxMessageReadAtEnabler
    {
        private Gs2InboxOwnMessageFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponentInParent<Gs2InboxOwnMessageFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2InboxOwnMessageFetcher.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2InboxMessageReadAtEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2InboxMessageReadAtEnabler
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

        public List<long> enableReadAts;

        public long enableReadAt;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InboxMessageReadAtEnabler
    {
        
    }
}