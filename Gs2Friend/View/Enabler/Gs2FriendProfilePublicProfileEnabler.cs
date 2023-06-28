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
using Gs2.Unity.UiKit.Gs2Friend.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Friend
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Friend/Profile/View/Enabler/Properties/PublicProfile/Gs2FriendProfilePublicProfileEnabler")]
    public partial class Gs2FriendProfilePublicProfileEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Profile != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enablePublicProfiles.Contains(_fetcher.Profile.PublicProfile));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enablePublicProfiles.Contains(_fetcher.Profile.PublicProfile));
                        break;
                    case Expression.StartsWith:
                        target.SetActive(enablePublicProfile.StartsWith(_fetcher.Profile.PublicProfile));
                        break;
                    case Expression.EndsWith:
                        target.SetActive(enablePublicProfile.EndsWith(_fetcher.Profile.PublicProfile));
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

    public partial class Gs2FriendProfilePublicProfileEnabler
    {
        private Gs2FriendOwnProfileFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2FriendOwnProfileFetcher>() ?? GetComponentInParent<Gs2FriendOwnProfileFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2FriendOwnProfileFetcher.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2FriendProfilePublicProfileEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2FriendProfilePublicProfileEnabler
    {
        public enum Expression {
            In,
            NotIn,
            StartsWith,
            EndsWith,
        }

        public Expression expression;

        public List<string> enablePublicProfiles;

        public string enablePublicProfile;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FriendProfilePublicProfileEnabler
    {
        
    }
}