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
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Gs2.Core.Exception;
using Gs2.Unity.Core.Exception;
using Gs2.Unity.Gs2Friend.Model;
using Gs2.Unity.Gs2Friend.ScriptableObject;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Friend.Context;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Friend.Fetcher
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Friend/PublicProfile/Fetcher/Gs2FriendOwnPublicProfileFetcher")]
    public partial class Gs2FriendOwnPublicProfileFetcher : MonoBehaviour
    {
        private IEnumerator Fetch()
        {
            var retryWaitSecond = 1;
            Gs2Exception e;
            while (true)
            {
                if (_gameSessionHolder != null && _gameSessionHolder.Initialized &&
                    _clientHolder != null && _clientHolder.Initialized &&
                    Context != null && this.Context.PublicProfile != null)
                {
                    
                    var domain = this._clientHolder.Gs2.Friend.Namespace(
                        this.Context.PublicProfile.NamespaceName
                    ).Me(
                        _gameSessionHolder.GameSession
                    ).Profile(
                    );
                    var future = domain.Model();
                    yield return future;
                    if (future.Error != null)
                    {
                        if (future.Error is BadRequestException || future.Error is NotFoundException)
                        {
                            onError.Invoke(e = future.Error, null);
                            Debug.LogError($"{gameObject.GetFullPath()}: {future.Error.Message}");
                            break;
                        }
                        else {
                            onError.Invoke(new CanIgnoreException(future.Error), null);
                        }
                        yield return new WaitForSeconds(retryWaitSecond);
                        retryWaitSecond *= 2;
                    }
                    else
                    {
                        retryWaitSecond = 1;
                        PublicProfile = new EzPublicProfile {
                            UserId = future.Result.UserId,
                            PublicProfile = future.Result.PublicProfile,
                        };
                        Fetched = true;
                    }
                }
                else {
                    yield return new WaitForSeconds(0.1f);
                }
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

    public partial class Gs2FriendOwnPublicProfileFetcher
    {
        protected Gs2ClientHolder _clientHolder;
        protected Gs2GameSessionHolder _gameSessionHolder;
        public Gs2FriendOwnPublicProfileContext Context { get; private set; }

        public void Awake()
        {
            _clientHolder = Gs2ClientHolder.Instance;
            _gameSessionHolder = Gs2GameSessionHolder.Instance;
            Context = GetComponent<Gs2FriendOwnPublicProfileContext>() ?? GetComponentInParent<Gs2FriendOwnPublicProfileContext>();

            if (Context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2FriendOwnPublicProfileContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            Context = GetComponent<Gs2FriendOwnPublicProfileContext>() ?? GetComponentInParent<Gs2FriendOwnPublicProfileContext>(true);
            if (Context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2FriendOwnPublicProfileFetcher
    {
        public Gs2.Unity.Gs2Friend.Model.EzPublicProfile PublicProfile { get; protected set; }
        public bool Fetched { get; protected set; }
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2FriendOwnPublicProfileFetcher
    {

    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FriendOwnPublicProfileFetcher
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