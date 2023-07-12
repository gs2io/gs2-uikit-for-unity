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
using System.Linq;
using Gs2.Gs2Friend.Request;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Friend.Context;
using Gs2.Unity.UiKit.Gs2Friend.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Friend
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Friend/FollowUser/View/Enabler/Gs2FriendFollowStatusEnabler")]
    public partial class Gs2FriendFollowStatusEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (this._followListFetcher.Fetched && this._followListFetcher.FollowUsers != null) {
                if (!this._followListFetcher.FollowUsers.Select(v => v.UserId).Contains(this._context.FollowUser.TargetUserId)) {
                    this.target.SetActive(this.followed);
                }
                else {
                    this.target.SetActive(this.unrelated);
                }
            }
            else
            {
                this.target.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2FriendFollowStatusEnabler
    {
        private Gs2FriendOwnFollowUserContext _context;
        private Gs2FriendOwnFollowUserListFetcher _followListFetcher;

        public void Awake()
        {
            _context = GetComponent<Gs2FriendOwnFollowUserContext>() ?? GetComponentInParent<Gs2FriendOwnFollowUserContext>();
            _followListFetcher = GetComponent<Gs2FriendOwnFollowUserListFetcher>() ?? GetComponentInParent<Gs2FriendOwnFollowUserListFetcher>();

            if (_followListFetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2FriendOwnFollowUserListFetcher.");
                enabled = false;
            }

            Update();
        }

        public bool HasError()
        {
            _followListFetcher = GetComponent<Gs2FriendOwnFollowUserListFetcher>() ?? GetComponentInParent<Gs2FriendOwnFollowUserListFetcher>(true);
            if (_followListFetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2FriendFollowStatusEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2FriendFollowStatusEnabler
    {
        public bool followed;
        public bool unrelated;
        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FriendFollowStatusEnabler
    {

    }
}