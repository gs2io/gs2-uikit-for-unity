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
using Gs2.Unity.UiKit.Gs2Friend.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Friend.Enabler
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Friend/FriendUser/View/Enabler/Properties/PublicProfile/Gs2FriendOwnFriendUserPublicProfileEnabler")]
    public partial class Gs2FriendOwnFriendUserPublicProfileEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.FriendUser != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enablePublicProfiles.Contains(_fetcher.FriendUser.PublicProfile));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enablePublicProfiles.Contains(_fetcher.FriendUser.PublicProfile));
                        break;
                    case Expression.StartsWith:
                        target.SetActive(_fetcher.FriendUser.PublicProfile.StartsWith(enablePublicProfile));
                        break;
                    case Expression.EndsWith:
                        target.SetActive(_fetcher.FriendUser.PublicProfile.EndsWith(enablePublicProfile));
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

    public partial class Gs2FriendOwnFriendUserPublicProfileEnabler
    {
        private Gs2FriendOwnFriendUserFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2FriendOwnFriendUserFetcher>() ?? GetComponentInParent<Gs2FriendOwnFriendUserFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2FriendOwnFriendUserFetcher.");
                enabled = false;
            }
            if (target == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: target is not set.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            _fetcher = GetComponent<Gs2FriendOwnFriendUserFetcher>() ?? GetComponentInParent<Gs2FriendOwnFriendUserFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            if (target == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2FriendOwnFriendUserPublicProfileEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2FriendOwnFriendUserPublicProfileEnabler
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
    public partial class Gs2FriendOwnFriendUserPublicProfileEnabler
    {
        
    }
}