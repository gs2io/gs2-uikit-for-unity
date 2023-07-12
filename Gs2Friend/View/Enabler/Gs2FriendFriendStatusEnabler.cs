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

	[AddComponentMenu("GS2 UIKit/Friend/FriendUser/View/Enabler/Gs2FriendFriendStatusEnabler")]
    public partial class Gs2FriendFriendStatusEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (this._friendListFetcher.Fetched && this._friendListFetcher.Friends != null &&
                (this._sendFriendRequestListFetcher == null || (this._sendFriendRequestListFetcher.Fetched && this._sendFriendRequestListFetcher.SendFriendRequests != null)) &&
                 (this._receiveFriendRequestListFetcher == null || (this._receiveFriendRequestListFetcher.Fetched && this._receiveFriendRequestListFetcher.ReceiveFriendRequests != null))) {
                if (!this._friendListFetcher.Friends.Select(v => v.UserId).Contains(this._context.FriendUser.TargetUserId)) {
                    this.target.SetActive(this.friend);
                }
                else if (this._sendFriendRequestListFetcher != null && !this._sendFriendRequestListFetcher.SendFriendRequests.Select(v => v.TargetUserId).Contains(this._context.FriendUser.TargetUserId)) {
                    this.target.SetActive(this.sent);
                }
                else if (this._receiveFriendRequestListFetcher != null && !this._receiveFriendRequestListFetcher.ReceiveFriendRequests.Select(v => v.UserId).Contains(this._context.FriendUser.TargetUserId)) {
                    this.target.SetActive(this.received);
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

    public partial class Gs2FriendFriendStatusEnabler
    {
        private Gs2FriendOwnFriendUserContext _context;
        private Gs2FriendOwnFriendListFetcher _friendListFetcher;
        private Gs2FriendOwnSendFriendRequestListFetcher _sendFriendRequestListFetcher;
        private Gs2FriendOwnReceiveFriendRequestListFetcher _receiveFriendRequestListFetcher;

        public void Awake()
        {
            _context = GetComponent<Gs2FriendOwnFriendUserContext>() ?? GetComponentInParent<Gs2FriendOwnFriendUserContext>();
            _friendListFetcher = GetComponent<Gs2FriendOwnFriendListFetcher>() ?? GetComponentInParent<Gs2FriendOwnFriendListFetcher>();
            _sendFriendRequestListFetcher = GetComponent<Gs2FriendOwnSendFriendRequestListFetcher>() ?? GetComponentInParent<Gs2FriendOwnSendFriendRequestListFetcher>();
            _receiveFriendRequestListFetcher = GetComponent<Gs2FriendOwnReceiveFriendRequestListFetcher>() ?? GetComponentInParent<Gs2FriendOwnReceiveFriendRequestListFetcher>();

            if (_friendListFetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2FriendOwnFriendListFetcher.");
                enabled = false;
            }

            Update();
        }

        public bool HasError()
        {
            _friendListFetcher = GetComponent<Gs2FriendOwnFriendListFetcher>() ?? GetComponentInParent<Gs2FriendOwnFriendListFetcher>(true);
            if (_friendListFetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2FriendFriendStatusEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2FriendFriendStatusEnabler
    {
        public bool friend;
        public bool sent;
        public bool received;
        public bool unrelated;
        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FriendFriendStatusEnabler
    {

    }
}