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

using Gs2.Unity.Gs2Friend.ScriptableObject;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Friend.Context
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Friend/BlackList/Context/Gs2FriendOwnBlackListContext")]
    public partial class Gs2FriendOwnBlackListContext : MonoBehaviour
    {
        public void Start() {
            if (BlackList == null) {
                Debug.LogError("BlackList is not set in Gs2FriendOwnBlackListContext.");
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2FriendOwnBlackListContext
    {

    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2FriendOwnBlackListContext
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2FriendOwnBlackListContext
    {
        public OwnBlackList BlackList;

        public void SetOwnBlackList(OwnBlackList BlackList) {
            this.BlackList = BlackList;
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FriendOwnBlackListContext
    {

    }
}