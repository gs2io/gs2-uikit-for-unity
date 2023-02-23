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
using Gs2.Unity.UiKit.Gs2Gateway.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Gateway
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Gateway/WebSocketSession/View/Properties/ConnectionId/Gs2GatewayWebSocketSessionConnectionIdEnabler")]
    public partial class Gs2GatewayWebSocketSessionConnectionIdEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.WebSocketSession != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableConnectionIds.Contains(_fetcher.WebSocketSession.ConnectionId));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableConnectionIds.Contains(_fetcher.WebSocketSession.ConnectionId));
                        break;
                    case Expression.StartsWith:
                        target.SetActive(enableConnectionId.StartsWith(_fetcher.WebSocketSession.ConnectionId));
                        break;
                    case Expression.EndsWith:
                        target.SetActive(enableConnectionId.EndsWith(_fetcher.WebSocketSession.ConnectionId));
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

    public partial class Gs2GatewayWebSocketSessionConnectionIdEnabler
    {
        private Gs2GatewayOwnWebSocketSessionFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponentInParent<Gs2GatewayOwnWebSocketSessionFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2GatewayOwnWebSocketSessionFetcher.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2GatewayWebSocketSessionConnectionIdEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2GatewayWebSocketSessionConnectionIdEnabler
    {
        public enum Expression {
            In,
            NotIn,
            StartsWith,
            EndsWith,
        }

        public Expression expression;

        public List<string> enableConnectionIds;

        public string enableConnectionId;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2GatewayWebSocketSessionConnectionIdEnabler
    {
        
    }
}