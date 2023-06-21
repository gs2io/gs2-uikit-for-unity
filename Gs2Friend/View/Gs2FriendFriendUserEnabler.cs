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

using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Friend.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Friend
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Friend/FriendUser/Gs2FriendFriendUserEnabler")]
    public partial class Gs2FriendFriendUserEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (!_fetcher.Fetched)
            {
                target.SetActive(loading);
            }
            else
            {
                if (_fetcher.FriendUser == null)
                {
                    target.SetActive(notFound);
                }
                else
                {
                    target.SetActive(loaded);
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2FriendFriendUserEnabler
    {
        private Gs2FriendOwnFriendUserFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2FriendOwnFriendUserFetcher>() ?? GetComponentInParent<Gs2FriendOwnFriendUserFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2FriendOwnFriendUserFetcher.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2FriendFriendUserEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2FriendFriendUserEnabler
    {
        public bool loading;
        public bool notFound;
        public bool loaded;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FriendFriendUserEnabler
    {

    }
}