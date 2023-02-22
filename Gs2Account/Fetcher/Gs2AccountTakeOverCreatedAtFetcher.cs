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

using System;
using System.Collections.Generic;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Account.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Account
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Account/TakeOver/Fetcher/Properties/CreatedAt/Gs2AccountTakeOverCreatedAtFetcher")]
    public partial class Gs2AccountTakeOverCreatedAtFetcher : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.TakeOver != null)
            {
                onUpdate?.Invoke(
                    _fetcher.TakeOver.CreatedAt
                );
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2AccountTakeOverCreatedAtFetcher
    {
        private Gs2AccountOwnTakeOverFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponentInParent<Gs2AccountOwnTakeOverFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2AccountOwnTakeOverFetcher.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2AccountTakeOverCreatedAtFetcher
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2AccountTakeOverCreatedAtFetcher
    {

    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2AccountTakeOverCreatedAtFetcher
    {
        [Serializable]
        private class UpdateEvent : UnityEvent<long>
        {

        }

        [SerializeField]
        private UpdateEvent onUpdate = new UpdateEvent();

        public event UnityAction<long> OnUpdate
        {
            add => onUpdate.AddListener(value);
            remove => onUpdate.RemoveListener(value);
        }
    }
}