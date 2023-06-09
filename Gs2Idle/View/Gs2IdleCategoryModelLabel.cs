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
using Gs2.Unity.UiKit.Gs2Idle.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Idle
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Idle/CategoryModel/View/Gs2IdleCategoryModelLabel")]
    public partial class Gs2IdleCategoryModelLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.CategoryModel != null)
            {
                onUpdate?.Invoke(
                    format.Replace(
                        "{name}", $"{_fetcher?.CategoryModel?.Name}"
                    ).Replace(
                        "{metadata}", $"{_fetcher?.CategoryModel?.Metadata}"
                    ).Replace(
                        "{rewardIntervalMinutes}", $"{_fetcher?.CategoryModel?.RewardIntervalMinutes}"
                    ).Replace(
                        "{idlePeriodScheduleId}", $"{_fetcher?.CategoryModel?.IdlePeriodScheduleId}"
                    ).Replace(
                        "{receivePeriodScheduleId}", $"{_fetcher?.CategoryModel?.ReceivePeriodScheduleId}"
                    )
                );
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2IdleCategoryModelLabel
    {
        private Gs2IdleCategoryModelFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponentInParent<Gs2IdleCategoryModelFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2IdleCategoryModelFetcher.");
                enabled = false;
            }

            Update();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2IdleCategoryModelLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2IdleCategoryModelLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2IdleCategoryModelLabel
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