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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gs2.Core.Exception;
using Gs2.Gs2Money.Request;
using Gs2.Unity.Gs2Money.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Money.Context;
using Gs2.Unity.Util;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Money.Fetcher
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Money/Receipt/Fetcher/Consume/Gs2MoneyRecordReceiptFetcher")]
    public partial class Gs2MoneyRecordReceiptFetcher : Gs2MoneyNamespaceContext
    {
        private IEnumerator Fetch()
        {
            while (true)
            {
                if (_fetcher != null) {
                    var action = _fetcher.ConsumeActions().FirstOrDefault(v => v.Action == "Gs2Money:RecordReceipt");
                    if (action != null) {
                        Request = RecordReceiptRequest.FromJson(JsonMapper.ToObject(action.Request));
                        if (Namespace == null || (
                                Namespace.NamespaceName == Request.NamespaceName)
                           ) {
                            Namespace = Namespace.New(
                                Request.NamespaceName
                            );
                        }
                        Fetched = true;
                    }
                }
                yield return new WaitForSeconds(1);
            }
            // ReSharper disable once IteratorNeverReturns
        }

        public void OnEnable()
        {
            StartCoroutine(nameof(Fetch));
        }

        public void OnDisable()
        {
            StopCoroutine(nameof(Fetch));
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2MoneyRecordReceiptFetcher
    {
        private IConsumeActionsFetcher _fetcher;

        public new void Start() {

        }
        
        public void Awake()
        {
            _fetcher = GetComponent<IConsumeActionsFetcher>() ?? GetComponentInParent<IConsumeActionsFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the IConsumeActionFetcher.");
                enabled = false;
            }
        }

        public bool HasError()
        {
            _fetcher = GetComponent<IConsumeActionsFetcher>() ?? GetComponentInParent<IConsumeActionsFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2MoneyRecordReceiptFetcher
    {
        public RecordReceiptRequest Request { get; protected set; }
        public bool Fetched { get; protected set; }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2MoneyRecordReceiptFetcher
    {

    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MoneyRecordReceiptFetcher
    {
        [SerializeField]
        internal ErrorEvent onError = new ErrorEvent();

        public event UnityAction<Gs2Exception, Func<IEnumerator>> OnError
        {
            add => onError.AddListener(value);
            remove => onError.RemoveListener(value);
        }
    }
}