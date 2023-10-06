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
using Gs2.Gs2Money.Request;
using Gs2.Unity.Gs2Money.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Money.Context;
using Gs2.Unity.UiKit.Gs2Money.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Money.Label
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Money/Receipt/View/Label/Transaction/Gs2MoneyRevertRecordReceiptLabel")]
    public partial class Gs2MoneyRevertRecordReceiptLabel : MonoBehaviour
    {
        private void OnFetched()
        {
            if ((!this._fetcher?.Fetched ?? false) || this._fetcher.Request == null) {
                return;
            }
            this.onUpdate?.Invoke(
                this.format.Replace(
                    "{namespaceName}",
                    $"{this._fetcher.Request.NamespaceName}"
                ).Replace(
                    "{userId}",
                    $"{this._fetcher.Request.UserId}"
                ).Replace(
                    "{receipt}",
                    $"{this._fetcher.Request.Receipt}"
                )
            );
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2MoneyRevertRecordReceiptLabel
    {
        private Gs2MoneyRevertRecordReceiptFetcher _fetcher;

        public void Awake()
        {
            this._fetcher = GetComponent<Gs2MoneyRevertRecordReceiptFetcher>() ?? GetComponentInParent<Gs2MoneyRevertRecordReceiptFetcher>();
            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MoneyRevertRecordReceiptFetcher.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._fetcher = GetComponent<Gs2MoneyRevertRecordReceiptFetcher>() ?? GetComponentInParent<Gs2MoneyRevertRecordReceiptFetcher>(true);
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

    public partial class Gs2MoneyRevertRecordReceiptLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2MoneyRevertRecordReceiptLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MoneyRevertRecordReceiptLabel
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