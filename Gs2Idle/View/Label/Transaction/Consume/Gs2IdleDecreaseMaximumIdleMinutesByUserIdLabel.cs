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
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantAssignment
// ReSharper disable NotAccessedVariable
// ReSharper disable RedundantUsingDirective
// ReSharper disable Unity.NoNullPropagation
// ReSharper disable InconsistentNaming

#pragma warning disable CS0472

using System;
using Gs2.Gs2Idle.Request;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Idle.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Idle.Label
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Idle/Status/View/Label/Transaction/Gs2IdleDecreaseMaximumIdleMinutesByUserIdLabel")]
    public partial class Gs2IdleDecreaseMaximumIdleMinutesByUserIdLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Request != null &&
                    _userDataFetcher != null && _userDataFetcher.Fetched && _userDataFetcher.Status != null) {
                {
                    onUpdate?.Invoke(
                        format.Replace(
                            "{namespaceName}",
                            $"{_fetcher.Request.NamespaceName}"
                        ).Replace(
                            "{userId}",
                            $"{_fetcher.Request.UserId}"
                        ).Replace(
                            "{categoryName}",
                            $"{_fetcher.Request.CategoryName}"
                        ).Replace(
                            "{decreaseMinutes}",
                            $"{_fetcher.Request.DecreaseMinutes}"
                        ).Replace(
                            "{userData:categoryName}",
                            $"{_userDataFetcher.Status.CategoryName}"
                        ).Replace(
                            "{userData:randomSeed}",
                            $"{_userDataFetcher.Status.RandomSeed}"
                        ).Replace(
                            "{userData:idleMinutes}",
                            $"{_userDataFetcher.Status.IdleMinutes}"
                        ).Replace(
                            "{userData:maximumIdleMinutes}",
                            $"{_userDataFetcher.Status.MaximumIdleMinutes}"
                        )
                    );
                }
            } else if (_fetcher.Fetched && _fetcher.Request != null) {
                {
                    onUpdate?.Invoke(
                        format.Replace(
                            "{namespaceName}",
                            $"{_fetcher.Request.NamespaceName}"
                        ).Replace(
                            "{userId}",
                            $"{_fetcher.Request.UserId}"
                        ).Replace(
                            "{categoryName}",
                            $"{_fetcher.Request.CategoryName}"
                        ).Replace(
                            "{decreaseMinutes}",
                            $"{_fetcher.Request.DecreaseMinutes}"
                        )
                    );
                }
            } else {
                onUpdate?.Invoke(
                    format.Replace(
                        "{decreaseMinutes}",
                        "0"
                    )
                );
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2IdleDecreaseMaximumIdleMinutesByUserIdLabel
    {
        private Gs2IdleDecreaseMaximumIdleMinutesByUserIdFetcher _fetcher;
        private Gs2IdleOwnStatusFetcher _userDataFetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2IdleDecreaseMaximumIdleMinutesByUserIdFetcher>() ?? GetComponentInParent<Gs2IdleDecreaseMaximumIdleMinutesByUserIdFetcher>();
            _userDataFetcher = GetComponent<Gs2IdleOwnStatusFetcher>() ?? GetComponentInParent<Gs2IdleOwnStatusFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2IdleDecreaseMaximumIdleMinutesByUserIdFetcher.");
                enabled = false;
            }

            Update();
        }

        public virtual bool HasError()
        {
            _fetcher = GetComponent<Gs2IdleDecreaseMaximumIdleMinutesByUserIdFetcher>() ?? GetComponentInParent<Gs2IdleDecreaseMaximumIdleMinutesByUserIdFetcher>(true);
            _userDataFetcher = GetComponent<Gs2IdleOwnStatusFetcher>() ?? GetComponentInParent<Gs2IdleOwnStatusFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2IdleDecreaseMaximumIdleMinutesByUserIdLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2IdleDecreaseMaximumIdleMinutesByUserIdLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2IdleDecreaseMaximumIdleMinutesByUserIdLabel
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