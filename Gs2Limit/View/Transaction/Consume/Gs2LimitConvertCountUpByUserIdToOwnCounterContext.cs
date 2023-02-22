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
using Gs2.Gs2Limit.Request;
using Gs2.Unity.Gs2Limit.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Limit.Context
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Limit/Convert/Transaction/Gs2LimitConvertConsumeCountUpByUserIdToOwnCounterContext")]
    public partial class Gs2LimitConvertCountUpByUserIdToOwnCounterContext : MonoBehaviour
    {
        private Gs2CoreConsumeActionFetcher _fetcher;
        
        public void Awake() {
            _fetcher = GetComponentInParent<Gs2CoreConsumeActionFetcher>();
            
            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2CoreConsumeActionFetcher.");
                enabled = false;
            }
        }
        
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.ConsumeAction != null && _fetcher.ConsumeAction.Action == "Gs2Limit:CountUpByUserId") {
                var request = CountUpByUserIdRequest.FromJson(JsonMapper.ToObject(_fetcher.ConsumeAction.Request));
                this.onConverted.Invoke(
                    OwnCounter.New(
                        Namespace.New(
                            request.NamespaceName
                        ),
                        request.LimitName,
                        request.CounterName
                    )
                );
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2LimitConvertCountUpByUserIdToOwnCounterContext
    {

    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2LimitConvertCountUpByUserIdToOwnCounterContext
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2LimitConvertCountUpByUserIdToOwnCounterContext
    {
        
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2LimitConvertCountUpByUserIdToOwnCounterContext
    {

        [Serializable]
        private class ConvertEvent : UnityEvent<OwnCounter>
        {

        }

        [SerializeField]
        private ConvertEvent onConverted = new ConvertEvent();

        public event UnityAction<OwnCounter> OnConvert
        {
            add => onConverted.AddListener(value);
            remove => onConverted.RemoveListener(value);
        }
    }
}