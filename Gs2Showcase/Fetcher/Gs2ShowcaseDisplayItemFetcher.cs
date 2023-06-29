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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gs2.Core.Exception;
using Gs2.Unity.Core.Exception;
using Gs2.Unity.Gs2Showcase.Model;
using Gs2.Unity.Gs2Showcase.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Gs2Showcase.Context;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;
using EzAcquireAction = Gs2.Unity.Core.Model.EzAcquireAction;
using EzConsumeAction = Gs2.Unity.Core.Model.EzConsumeAction;

namespace Gs2.Unity.UiKit.Gs2Showcase.Fetcher
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Showcase/DisplayItem/Fetcher/Gs2ShowcaseDisplayItemFetcher")]
    public partial class Gs2ShowcaseDisplayItemFetcher : MonoBehaviour, IAcquireActionsFetcher, IConsumeActionsFetcher
    {
        private IEnumerator Fetch()
        {
            Gs2Exception e;
            while (true)
            {
                if (_gameSessionHolder != null && _gameSessionHolder.Initialized &&
                    _clientHolder != null && _clientHolder.Initialized &&
                    Context != null && this.Context.DisplayItem != null)
                {
                    
                    var domain = this._clientHolder.Gs2.Showcase.Namespace(
                        this.Context.DisplayItem.NamespaceName
                    ).Me(
                        this._gameSessionHolder.GameSession
                    ).Showcase(
                        this.Context.DisplayItem.ShowcaseName
                    );
                    var future = domain.Model();
                    yield return future;
                    if (future.Error != null)
                    {
                        if (future.Error is BadRequestException || future.Error is NotFoundException)
                        {
                            onError.Invoke(e = future.Error, null);
                            break;
                        }

                        onError.Invoke(new CanIgnoreException(future.Error), null);
                    }
                    else
                    {
                        DisplayItem = future.Result.DisplayItems.FirstOrDefault(
                            v => v.DisplayItemId == Context.DisplayItem.DisplayItemId
                        );
                        Fetched = true;
                    }
                }
                else {
                    yield return new WaitForSeconds(1);
                }
            }
        }

        public void OnEnable()
        {
            StartCoroutine(nameof(Fetch));
        }

        public void OnDisable()
        {
            StopCoroutine(nameof(Fetch));
        }

        public List<EzAcquireAction> AcquireActions(string context = "default") {
            if (!Fetched) {
                return new List<Unity.Core.Model.EzAcquireAction>();
            }
            return DisplayItem.SalesItem.AcquireActions;
        }

        public List<EzConsumeAction> ConsumeActions(string context = "default") {
            if (!Fetched) {
                return new List<Unity.Core.Model.EzConsumeAction>();
            }
            return DisplayItem.SalesItem.ConsumeActions;
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2ShowcaseDisplayItemFetcher
    {
        protected Gs2ClientHolder _clientHolder;
        protected Gs2GameSessionHolder _gameSessionHolder;
        public Gs2ShowcaseDisplayItemContext Context { get; protected set; }

        private void ChangeCounter(string namespaceName, string limitName, Gs2.Gs2Limit.Model.Counter counter) {
            if (DisplayItem == null) return;
            var consumeAction = DisplayItem.SalesItem.ConsumeActions.FirstOrDefault(
                v => v.Action == "Gs2Limit:CountUpByUserId" || v.Action == "Gs2Limit:DeleteCounterByUserId"
            );
            if (consumeAction != null) {
                var request = Gs2.Gs2Limit.Request.CountUpByUserIdRequest.FromJson(JsonMapper.ToObject(consumeAction.Request));
                if (request.NamespaceName == namespaceName && 
                    request.LimitName == limitName &&
                    request.CounterName == counter.Name) {
                    
                    this._clientHolder.Gs2.ClearCache<Gs2.Gs2Showcase.Model.Showcase>(
                        Gs2.Gs2Showcase.Domain.Model.UserDomain.CreateCacheParentKey(
                            Context.DisplayItem.NamespaceName,
                            counter.UserId,
                            "Showcase"
                        ),
                        Gs2.Gs2Showcase.Domain.Model.ShowcaseDomain.CreateCacheKey(
                            Context.DisplayItem.ShowcaseName
                        )
                    );
                }
            }
        }
        
        public void Awake()
        {
            _clientHolder = Gs2ClientHolder.Instance;
            _gameSessionHolder = Gs2GameSessionHolder.Instance;
            Context = GetComponentInParent<Gs2ShowcaseDisplayItemContext>();
            
            if (Context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ShowcaseDisplayItemContext.");
                enabled = false;
            }
            
            Gs2.Gs2Limit.Domain.Gs2Limit.ChangeCounter += ChangeCounter;
        }

        public void OnDestroy() {
            Gs2.Gs2Limit.Domain.Gs2Limit.ChangeCounter -= ChangeCounter;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2ShowcaseDisplayItemFetcher
    {
        public EzDisplayItem DisplayItem { get; protected set; }
        public bool Fetched { get; protected set; }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2ShowcaseDisplayItemFetcher
    {

    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ShowcaseDisplayItemFetcher
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