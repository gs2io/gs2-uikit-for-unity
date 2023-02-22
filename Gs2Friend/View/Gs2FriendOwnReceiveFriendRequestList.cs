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

using System.Collections.Generic;
using Gs2.Unity.Gs2Friend.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Friend.Context;
using Gs2.Unity.UiKit.Gs2Friend.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Friend
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Friend/ReceiveFriendRequest/View/Gs2FriendOwnReceiveFriendRequestList")]
    public partial class Gs2FriendOwnReceiveFriendRequestList : MonoBehaviour
    {
        private List<Gs2FriendOwnReceiveFriendRequestContext> _children;

        public void Update() {
            if (_fetcher.Fetched) {
                for (var i = 0; i < this.maximumItems; i++) {
                    if (i < this._fetcher.ReceiveFriendRequests.Count) {
                        _children[i].ReceiveFriendRequest.fromUserId = this._fetcher.ReceiveFriendRequests[i].TargetUserId;
                        _children[i].gameObject.SetActive(true);
                    }
                    else {
                        _children[i].gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2FriendOwnReceiveFriendRequestList
    {
        private Gs2FriendNamespaceContext _context;
        private Gs2FriendOwnReceiveFriendRequestListFetcher _fetcher;

        public void Awake()
        {
            _context = GetComponentInParent<Gs2FriendNamespaceContext>();
            _fetcher = GetComponentInParent<Gs2FriendOwnReceiveFriendRequestListFetcher>();

            _children = new List<Gs2FriendOwnReceiveFriendRequestContext>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.ReceiveFriendRequest = OwnReceiveFriendRequest.New(
                    _context.Namespace,
                    ""
                );
                node.gameObject.SetActive(false);
                _children.Add(node);
            }
            this.prefab.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2FriendOwnReceiveFriendRequestList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2FriendOwnReceiveFriendRequestList
    {
        public Gs2FriendOwnReceiveFriendRequestContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FriendOwnReceiveFriendRequestList
    {

    }
}