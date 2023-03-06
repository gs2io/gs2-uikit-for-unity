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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gs2.Core.Exception;
using Gs2.Gs2Limit.Request;
using Gs2.Unity.Core.Exception;
using Gs2.Unity.Gs2Showcase.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Core.Consume;
using Gs2.Unity.UiKit.Gs2Limit.Fetcher;
using Gs2.Unity.UiKit.Gs2Showcase.Fetcher;
using Gs2.Unity.Util;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Showcase.Context
{
    [AddComponentMenu("GS2 UIKit/Showcase/DisplayItem/Convert/Gs2ShowcaseConvertDisplayItemConsumeActionsToConsumeActions")]
    public class Gs2ShowcaseConvertDisplayItemConsumeActionsToConsumeActions : MonoBehaviour
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _sessionHolder;
        private Gs2ShowcaseDisplayItemFetcher _fetcher;

        private int _callbackCount;
        public int count = 1;

        public void Awake() {
            _clientHolder = Gs2ClientHolder.Instance;
            _sessionHolder = Gs2GameSessionHolder.Instance;
            _fetcher = GetComponentInParent<Gs2ShowcaseDisplayItemFetcher>();
            
            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ShowcaseDisplayItemFetcher.");
                enabled = false;
            }
        }

        public IEnumerator Process()
        {
            while (true) {
                if (_fetcher.Fetched && _fetcher.DisplayItem != null && 
                    _callbackCount != count) {
                    if (_fetcher.DisplayItem.Type == "salesItemGroup") {

                        var lastSalesItem = _fetcher.DisplayItem.SalesItemGroup.SalesItems.Last();
                        var lastLimit = lastSalesItem.ConsumeActions.FirstOrDefault(v => v.Action == "Gs2Limit:CountUpByUserId");
                        var lastLimitRequest = lastLimit == null ? null : CountUpByUserIdRequest.FromJson(JsonMapper.ToObject(lastLimit.Request));
                        
                        var purchaseByStepsCount = new Dictionary<int, int>();
                        for (var i = 0; i < this.count;) {
                            for (var j = 0; j < _fetcher.DisplayItem.SalesItemGroup.SalesItems.Count && i < this.count; j++) {
                                var limit = _fetcher.DisplayItem.SalesItemGroup.SalesItems[j].ConsumeActions
                                    .FirstOrDefault(v =>
                                        v.Action == "Gs2Limit:CountUpByUserId");
                                if (limit == null) break;
                                var request = CountUpByUserIdRequest.FromJson(JsonMapper.ToObject(limit.Request));

                                var future = this._clientHolder.Gs2.Limit.Namespace(
                                    request.NamespaceName
                                ).Me(
                                    this._sessionHolder.GameSession
                                ).Counter(
                                    request.LimitName,
                                    request.CounterName
                                ).Model();
                                yield return future;
                                if (future.Error != null) {
                                    this.onError.Invoke(new CanIgnoreException(future.Error), null);
                                }
                                do {
                                    if (future.Result.Count + i >= request.MaxValue) {
                                        break;
                                    }
                                    if (!purchaseByStepsCount.ContainsKey(j)) {
                                        purchaseByStepsCount[j] = 1;
                                    }
                                    else {
                                        purchaseByStepsCount[j] += 1;
                                    }
                                    if (++i >= this.count) {
                                        break;
                                    }
                                } while (true);
                            }
                        }
                        var mergedConsumeActions = new Dictionary<string, ConsumeAction>();
                        foreach (var index in purchaseByStepsCount.Keys)
                        {
                            var _count = purchaseByStepsCount[index];
                            var consumeActions = _fetcher.DisplayItem.SalesItemGroup.SalesItems[index]
                                .ConsumeActions
                                .Select(v => ConsumeAction.New(
                                    v.Action,
                                    v.Request
                                ) * _count);
                            foreach (var consumeAction in consumeActions) {
                                if (consumeAction.Action == "Gs2Limit:CountUpByUserId") {
                                    var request = CountUpByUserIdRequest.FromJson(JsonMapper.ToObject(consumeAction.Request));
                                    request.MaxValue = lastLimitRequest == null ? 99999999 : lastLimitRequest.MaxValue;
                                    consumeAction.request = JsonMapper.ToJson(request.ToJson());
                                }
                                if (mergedConsumeActions.ContainsKey(consumeAction.Id)) {
                                    mergedConsumeActions[consumeAction.Id] += consumeAction;
                                }
                                else {
                                    mergedConsumeActions[consumeAction.Id] = consumeAction;
                                }
                            }
                        }
                        this.onConverted.Invoke(
                            mergedConsumeActions.Values.ToList()
                        );
                        _callbackCount = count;
                    }
                    else {
                        this.onConverted.Invoke(
                            _fetcher.DisplayItem.SalesItem.ConsumeActions.Select(v => ConsumeAction.New(
                                v.Action,
                                v.Request
                            ) * count).ToList()
                        );
                        _callbackCount = count;
                    }
                    yield return new WaitForSeconds(0.1f);
                }
                else {
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }
        
        public void Start() {
            StartCoroutine(nameof(Process));
        }
        
        public void SetCount(int count)
        {
            this.count = count;
        }

        [Serializable]
        private class ConvertEvent : UnityEvent<List<ConsumeAction>>
        {

        }

        [SerializeField]
        private ConvertEvent onConverted = new ConvertEvent();

        public event UnityAction<List<ConsumeAction>> OnConvert
        {
            add => onConverted.AddListener(value);
            remove => onConverted.RemoveListener(value);
        }

        [SerializeField]
        internal ErrorEvent onError = new ErrorEvent();

        public event UnityAction<Gs2Exception, Func<IEnumerator>> OnError
        {
            add => this.onError.AddListener(value);
            remove => this.onError.RemoveListener(value);
        }
    }
}