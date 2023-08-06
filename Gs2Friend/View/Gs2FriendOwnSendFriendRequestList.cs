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

using System.Collections.Generic;
using Gs2.Unity.Gs2Friend.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Friend.Context;
using Gs2.Unity.UiKit.Gs2Friend.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Friend
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Friend/SendFriendRequest/View/Gs2FriendOwnSendFriendRequestList")]
    public partial class Gs2FriendOwnSendFriendRequestList : MonoBehaviour
    {
        private List<Gs2FriendOwnSendFriendRequestContext> _children;

        public void Update() {
            if (_fetcher.Fetched && this._fetcher.SendFriendRequests != null) {
                for (var i = 0; i < this.maximumItems; i++) {
                    if (i < this._fetcher.SendFriendRequests.Count) {
                        _children[i].SendFriendRequest.targetUserId = this._fetcher.SendFriendRequests[i].TargetUserId;
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

    public partial class Gs2FriendOwnSendFriendRequestList
    {
        private Gs2FriendOwnSendFriendRequestListFetcher _fetcher;
        private Gs2FriendNamespaceContext Context => _fetcher.Context;

        public void Awake()
        {
            if (prefab == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2FriendOwnSendFriendRequestContext Prefab.");
                enabled = false;
                return;
            }

            _fetcher = GetComponent<Gs2FriendOwnSendFriendRequestListFetcher>() ?? GetComponentInParent<Gs2FriendOwnSendFriendRequestListFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2FriendOwnSendFriendRequestListFetcher.");
                enabled = false;
            }

            var context = GetComponent<Gs2FriendNamespaceContext>() ?? GetComponentInParent<Gs2FriendNamespaceContext>(true);
            if (context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2FriendOwnSendFriendRequestListFetcher::Context.");
                enabled = false;
                return;
            }

            _children = new List<Gs2FriendOwnSendFriendRequestContext>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.SendFriendRequest = OwnSendFriendRequest.New(
                    context.Namespace,
                    "",
                    false
                );
                node.gameObject.SetActive(false);
                _children.Add(node);
            }
            this.prefab.gameObject.SetActive(false);
        }

        public bool HasError()
        {
            _fetcher = GetComponent<Gs2FriendOwnSendFriendRequestListFetcher>() ?? GetComponentInParent<Gs2FriendOwnSendFriendRequestListFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2FriendOwnSendFriendRequestList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2FriendOwnSendFriendRequestList
    {
        public Gs2FriendOwnSendFriendRequestContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FriendOwnSendFriendRequestList
    {

    }
}