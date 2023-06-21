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
using Gs2.Unity.UiKit.Gs2Realtime.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Realtime
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Realtime/Room/View/Properties/IpAddress/Gs2RealtimeRoomIpAddressEnabler")]
    public partial class Gs2RealtimeRoomIpAddressEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Room != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableIpAddresses.Contains(_fetcher.Room.IpAddress));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableIpAddresses.Contains(_fetcher.Room.IpAddress));
                        break;
                    case Expression.StartsWith:
                        target.SetActive(enableIpAddress.StartsWith(_fetcher.Room.IpAddress));
                        break;
                    case Expression.EndsWith:
                        target.SetActive(enableIpAddress.EndsWith(_fetcher.Room.IpAddress));
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

    public partial class Gs2RealtimeRoomIpAddressEnabler
    {
        private Gs2RealtimeRoomFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2RealtimeRoomFetcher>() ?? GetComponentInParent<Gs2RealtimeRoomFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2RealtimeRoomFetcher.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2RealtimeRoomIpAddressEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2RealtimeRoomIpAddressEnabler
    {
        public enum Expression {
            In,
            NotIn,
            StartsWith,
            EndsWith,
        }

        public Expression expression;

        public List<string> enableIpAddresses;

        public string enableIpAddress;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2RealtimeRoomIpAddressEnabler
    {
        
    }
}