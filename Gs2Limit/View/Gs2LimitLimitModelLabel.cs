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
using Gs2.Unity.UiKit.Gs2Limit.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Limit
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Limit/LimitModel/View/Gs2LimitLimitModelLabel")]
    public partial class Gs2LimitLimitModelLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.LimitModel != null)
            {
                onUpdate?.Invoke(
                    format.Replace(
                        "{limitModelId}", $"{_fetcher?.LimitModel?.LimitModelId}"
                    ).Replace(
                        "{name}", $"{_fetcher?.LimitModel?.Name}"
                    ).Replace(
                        "{metadata}", $"{_fetcher?.LimitModel?.Metadata}"
                    ).Replace(
                        "{resetType}", $"{_fetcher?.LimitModel?.ResetType}"
                    ).Replace(
                        "{resetDayOfMonth}", $"{_fetcher?.LimitModel?.ResetDayOfMonth}"
                    ).Replace(
                        "{resetDayOfWeek}", $"{_fetcher?.LimitModel?.ResetDayOfWeek}"
                    ).Replace(
                        "{resetHour}", $"{_fetcher?.LimitModel?.ResetHour}"
                    )
                );
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2LimitLimitModelLabel
    {
        private Gs2LimitLimitModelFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2LimitLimitModelFetcher>() ?? GetComponentInParent<Gs2LimitLimitModelFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2LimitLimitModelFetcher.");
                enabled = false;
            }

            Update();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2LimitLimitModelLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2LimitLimitModelLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2LimitLimitModelLabel
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