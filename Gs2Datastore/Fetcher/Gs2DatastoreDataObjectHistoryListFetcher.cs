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
using Gs2.Core.Exception;
using Gs2.Unity.Core.Exception;
using Gs2.Unity.Gs2Datastore.Domain.Model;
using Gs2.Unity.Gs2Datastore.Model;
using Gs2.Unity.Gs2Datastore.ScriptableObject;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Datastore.Context;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Datastore.Fetcher
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Datastore/DataObjectHistory/Fetcher/Gs2DatastoreDataObjectHistoryListFetcher")]
    public partial class Gs2DatastoreDataObjectHistoryListFetcher : MonoBehaviour
    {
        private EzDataObjectDomain _domain;
        private ulong? _callbackId;

        private IEnumerator Load() {
            var retryWaitSecond = 1;
            var it = _domain.DataObjectHistories();
            var items = new List<Gs2.Unity.Gs2Datastore.Model.EzDataObjectHistory>();
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
            DataObjectHistories = items;
            Fetched = true;

            this.OnFetched.Invoke();
        }

        private IEnumerator Fetch()
        {
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);
            yield return new WaitUntil(() => Context != null && Context.DataObject != null);

            this._domain = clientHolder.Gs2.Datastore.Namespace(
                this.Context.DataObject.NamespaceName
            ).User(
                this.Context.DataObject.UserId
            ).DataObject(
                this.Context.DataObject.DataObjectName
            );
            this._callbackId = this._domain.SubscribeDataObjectHistories(
                () =>
                {
                    StartCoroutine(nameof(Load));
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
            this._domain.UnsubscribeDataObjectHistories(
                this._callbackId.Value
            );
            this._callbackId = null;
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2DatastoreDataObjectHistoryListFetcher
    {
        public Gs2DatastoreDataObjectContext Context { get; private set; }

        public UnityEvent OnFetched = new UnityEvent();

        public void Awake()
        {
            Context = GetComponent<Gs2DatastoreDataObjectContext>() ?? GetComponentInParent<Gs2DatastoreDataObjectContext>();
            if (Context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2DatastoreDataObjectContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            Context = GetComponent<Gs2DatastoreDataObjectContext>() ?? GetComponentInParent<Gs2DatastoreDataObjectContext>(true);
            if (Context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2DatastoreDataObjectHistoryListFetcher
    {
        public List<Gs2.Unity.Gs2Datastore.Model.EzDataObjectHistory> DataObjectHistories { get; private set; }
        public bool Fetched { get; private set; }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2DatastoreDataObjectHistoryListFetcher
    {

    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2DatastoreDataObjectHistoryListFetcher
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