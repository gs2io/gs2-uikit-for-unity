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
using Gs2.Core.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Showcase.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Showcase
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Showcase/DisplayItem/View/Gs2ShowcaseDisplayItemLabel")]
    public partial class Gs2ShowcaseDisplayItemLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched)
            {
                onUpdate?.Invoke(
                    format.Replace(
                        "{displayItemId}", $"{_fetcher?.DisplayItem?.DisplayItemId}"
                    ).Replace(
                        "{type}", $"{_fetcher?.DisplayItem?.Type}"
                    ).Replace(
                        "{salesItem}", $"{_fetcher?.DisplayItem?.SalesItem}"
                    ).Replace(
                        "{salesItemGroup}", $"{_fetcher?.DisplayItem?.SalesItemGroup}"
                    )
                );
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2ShowcaseDisplayItemLabel
    {
        private Gs2ShowcaseDisplayItemFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponentInParent<Gs2ShowcaseDisplayItemFetcher>();
            Update();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2ShowcaseDisplayItemLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2ShowcaseDisplayItemLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ShowcaseDisplayItemLabel
    {
        [Serializable]
        private class UpdateEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private UpdateEvent onUpdate = new UpdateEvent();

        public event UnityAction<string> OnUpdate
        {
            add => onUpdate.AddListener(value);
            remove => onUpdate.RemoveListener(value);
        }
    }
}