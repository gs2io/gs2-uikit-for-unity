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
using System.Text;
using System.Linq;
using Gs2.Core.Exception;
using Gs2.Unity.Core.Exception;
using Gs2.Unity.Gs2Account.Domain.Model;
using Gs2.Unity.Gs2Account.Model;
using Gs2.Unity.Gs2Account.ScriptableObject;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Account.Context;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Account.Fetcher
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Account/PlatformId/Fetcher/Gs2AccountOwnPlatformIdListFetcher")]
    public partial class Gs2AccountOwnPlatformIdListFetcher : MonoBehaviour
    {
        private EzAccountGameSessionDomain _domain;
        private ulong? _callbackId;

        private IEnumerator Load() {
            var retryWaitSecond = 1;
            var it = _domain.PlatformIds();
            var items = new List<Gs2.Unity.Gs2Account.Model.EzPlatformId>();
            while (it.HasNext())
            {
                yield return it.Next();
                if (it.Error != null)
                {
                    if (it.Error is BadRequestException || it.Error is NotFoundException)
                    {
                        onError.Invoke(it.Error, null);
                        Debug.LogError($"{gameObject.GetFullPath()}: {it.Error.Message}");
                        break;
                    }
                    else {
                        onError.Invoke(new CanIgnoreException(it.Error), null);
                    }
                    yield return new WaitForSeconds(retryWaitSecond);
                    retryWaitSecond *= 2;
                }
                else {
                    if (it.Current != null)
                    {
                        items.Add(it.Current);
                    } else {
                        break;
                    }
                }
            }

            retryWaitSecond = 1;
            PlatformIds = items;
            Fetched = true;

            this.OnFetched.Invoke();
        }

        private IEnumerator Fetch()
        {
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);
            yield return new WaitUntil(() => Context != null && Context.Account != null);

            this._domain = clientHolder.Gs2.Account.Namespace(
                this.Context.Account.NamespaceName
            ).Me(
                gameSessionHolder.GameSession
            );
            this._callbackId = this._domain.SubscribePlatformIds(
                items =>
                {
                    PlatformIds = items.ToList();
                    this.OnFetched.Invoke();
                }
            );

            yield return Load();
        }

        public void OnUpdateContext() {
            OnDisable();
            Awake();
            OnEnable();
        }

        public void OnEnable()
        {
            StartCoroutine(nameof(Fetch));
            Context.OnUpdate.AddListener(OnUpdateContext);
        }

        public void OnDisable()
        {
            Context.OnUpdate.RemoveListener(OnUpdateContext);

            if (this._domain == null) {
                return;
            }
            if (!this._callbackId.HasValue) {
                return;
            }
            this._domain.UnsubscribePlatformIds(
                this._callbackId.Value
            );
            this._callbackId = null;
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2AccountOwnPlatformIdListFetcher
    {
        public Gs2AccountOwnAccountContext Context { get; private set; }

        public UnityEvent OnFetched = new UnityEvent();

        public void Awake()
        {
            Context = GetComponent<Gs2AccountOwnAccountContext>() ?? GetComponentInParent<Gs2AccountOwnAccountContext>();
            if (Context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2AccountOwnAccountContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            Context = GetComponent<Gs2AccountOwnAccountContext>() ?? GetComponentInParent<Gs2AccountOwnAccountContext>(true);
            if (Context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2AccountOwnPlatformIdListFetcher
    {
        public List<Gs2.Unity.Gs2Account.Model.EzPlatformId> PlatformIds { get; private set; }
        public bool Fetched { get; private set; }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2AccountOwnPlatformIdListFetcher
    {

    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2AccountOwnPlatformIdListFetcher
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