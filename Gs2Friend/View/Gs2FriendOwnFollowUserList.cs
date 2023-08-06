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

    [AddComponentMenu("GS2 UIKit/Friend/FollowUser/View/Gs2FriendOwnFollowUserList")]
    public partial class Gs2FriendOwnFollowUserList : MonoBehaviour
    {
        private List<Gs2FriendOwnFollowUserContext> _children;

        public void Update() {
            if (_fetcher.Fetched && this._fetcher.FollowUsers != null) {
                for (var i = 0; i < this.maximumItems; i++) {
                    if (i < this._fetcher.FollowUsers.Count) {
                        _children[i].FollowUser.targetUserId = this._fetcher.FollowUsers[i].UserId;
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

    public partial class Gs2FriendOwnFollowUserList
    {
        private Gs2FriendOwnFollowUserListFetcher _fetcher;
        private Gs2FriendNamespaceContext Context => _fetcher.Context;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2FriendOwnFollowUserListFetcher>() ?? GetComponentInParent<Gs2FriendOwnFollowUserListFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2FriendOwnFollowUserListFetcher.");
                enabled = false;
            }

            _children = new List<Gs2FriendOwnFollowUserContext>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.FollowUser = OwnFollowUser.New(
                    _fetcher.Context.Namespace,
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
            _fetcher = GetComponent<Gs2FriendOwnFollowUserListFetcher>() ?? GetComponentInParent<Gs2FriendOwnFollowUserListFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2FriendOwnFollowUserList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2FriendOwnFollowUserList
    {
        public Gs2FriendOwnFollowUserContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FriendOwnFollowUserList
    {

    }
}