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

using System;
using System.Linq;
using Gs2.Core.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Mission.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Mission
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Mission/Counter/View/Label/Gs2MissionOwnCounterLabel")]
    public partial class Gs2MissionOwnCounterLabel : MonoBehaviour
    {
        private void OnFetched()
        {
            this.onUpdate?.Invoke(
                this.format.Replace(
                    "{name}", $"{this._fetcher?.Counter?.Name}"
                ).Replace(
                    "{notReset:value}", $"{_fetcher?.Counter?.Values?.FirstOrDefault(v => v.ResetType == "notReset")?.Value ?? 0}"
                ).Replace(
                    "{daily:value}", $"{_fetcher?.Counter?.Values?.FirstOrDefault(v => v.ResetType == "daily")?.Value ?? 0}"
                ).Replace(
                    "{weekly:value}", $"{_fetcher?.Counter?.Values?.FirstOrDefault(v => v.ResetType == "weekly")?.Value ?? 0}"
                ).Replace(
                    "{monthly:value}", $"{_fetcher?.Counter?.Values?.FirstOrDefault(v => v.ResetType == "monthly")?.Value ?? 0}"
                )
            );
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2MissionOwnCounterLabel
    {
        private Gs2MissionOwnCounterFetcher _fetcher;

        public void Awake()
        {
            this._fetcher = GetComponent<Gs2MissionOwnCounterFetcher>() ?? GetComponentInParent<Gs2MissionOwnCounterFetcher>();
            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MissionOwnCounterFetcher.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._fetcher = GetComponent<Gs2MissionOwnCounterFetcher>() ?? GetComponentInParent<Gs2MissionOwnCounterFetcher>(true);
            if (this._fetcher == null) {
                return true;
            }
            return false;
        }

        private UnityAction _onFetched;

        public void OnEnable()
        {
            this._onFetched = () =>
            {
                OnFetched();
            };
            this._fetcher.OnFetched.AddListener(this._onFetched);

            if (this._fetcher.Fetched) {
                OnFetched();
            }
        }

        public void OnDisable()
        {
            if (this._onFetched != null) {
                this._fetcher.OnFetched.RemoveListener(this._onFetched);
                this._onFetched = null;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2MissionOwnCounterLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2MissionOwnCounterLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MissionOwnCounterLabel
    {
        [Serializable]
        private class UpdateEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private UpdateEvent onUpdate = new UpdateEvent();

        public event UnityAction<string> OnUpdate
        {
            add => this.onUpdate.AddListener(value);
            remove => this.onUpdate.RemoveListener(value);
        }
    }
}