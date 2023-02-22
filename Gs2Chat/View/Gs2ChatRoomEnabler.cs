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
using Gs2.Unity.UiKit.Gs2Chat.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Chat
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Chat/Room/Gs2ChatRoomEnabler")]
    public partial class Gs2ChatRoomEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (!_fetcher.Fetched)
            {
                target.SetActive(loading);
            }
            else
            {
                if (_fetcher.Room == null)
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

    public partial class Gs2ChatRoomEnabler
    {
        private Gs2ChatRoomFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponentInParent<Gs2ChatRoomFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ChatRoomFetcher.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2ChatRoomEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2ChatRoomEnabler
    {
        public bool loading;
        public bool notFound;
        public bool loaded;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ChatRoomEnabler
    {

    }
}