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

    [AddComponentMenu("GS2 UIKit/Friend/ReceiveFriendRequest/View/Gs2FriendReceiveFriendRequestList")]
    public partial class Gs2FriendReceiveFriendRequestList : MonoBehaviour
    {
        private List<Gs2FriendReceiveFriendRequestContext> _children;

        public void Update() {
            if (_fetcher.Fetched) {
                for (var i = 0; i < this.maximumItems; i++) {
                    if (i < this._fetcher.ReceiveFriendRequests.Count) {
                        _children[i].ReceiveFriendRequest.fromUserId = this._fetcher.ReceiveFriendRequests[i].UserId;
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
    
    public partial class Gs2FriendReceiveFriendRequestList
    {
        private Gs2FriendUserContext _context;
        private Gs2FriendReceiveFriendRequestListFetcher _fetcher;

        public void Awake()
        {
            _context = GetComponentInParent<Gs2FriendUserContext>();
            _fetcher = GetComponentInParent<Gs2FriendReceiveFriendRequestListFetcher>();

            _children = new List<Gs2FriendReceiveFriendRequestContext>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.ReceiveFriendRequest = ReceiveFriendRequest.New(
                    _context.User,
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

    public partial class Gs2FriendReceiveFriendRequestList
    {
        
    }
    
    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2FriendReceiveFriendRequestList
    {
        public Gs2FriendReceiveFriendRequestContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FriendReceiveFriendRequestList
    {
        
    }
}