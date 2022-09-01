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
// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable CheckNamespace

using System;
using System.Collections;
using Gs2.Core.Exception;
using Gs2.Unity.Gs2Account.Model;
using Gs2.Unity.Gs2Auth.Model;
using Gs2.Unity.Gs2Key.ScriptableObject;
using Gs2.Unity.Util;
using UnityEngine;
using UnityEngine.Events;
using Namespace = Gs2.Unity.Gs2Account.ScriptableObject.Namespace;
#if GS2_ENABLE_UNITASK
#endif

namespace Gs2.Unity.UiKit.Core
{
	[AddComponentMenu("GS2 UIKit/Core/Gs2ProfileLoginAction")]
    public partial class Gs2ProfileLoginAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            yield return new WaitUntil(() => _clientHolder.Initialized);
            
            var future = _clientHolder.Profile.LoginFuture(
                new Gs2AccountAuthenticator(
                    session: _clientHolder.Profile.Gs2RestSession,
                    accountNamespaceName: Namespace.namespaceName,
                    keyId: key.Grn,
                    userId: userId,
                    password: password
                )
            );
            yield return future;
            if (future.Error != null)
            {
                if (future.Error is TransactionException e)
                {
                    IEnumerator Retry()
                    {
                        var retryFuture = e.Retry();
                        yield return retryFuture;
                        if (retryFuture.Error != null)
                        {
                            onError.Invoke(future.Error, Retry);
                            yield break;
                        }

                        onLoginComplete.Invoke(EzAccessToken.FromModel(future.Result.AccessToken));
                    }

                    onError.Invoke(future.Error, Retry);
                    yield break;
                }

                onError.Invoke(future.Error, null);
                yield break;
            }
            
            onLoginComplete.Invoke(EzAccessToken.FromModel(future.Result.AccessToken));
        }
        
        public void OnEnable()
        {
            StartCoroutine(nameof(Process));
        }
        
        public void OnDisable()
        {
            StopCoroutine(nameof(Process));
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>
    
    public partial class Gs2ProfileLoginAction
    {
        private Gs2.Unity.Util.Gs2ClientHolder _clientHolder;
        private Gs2.Unity.Util.Gs2GameSessionHolder _gameSessionHolder;

        public void Awake()
        {
            _clientHolder = Gs2.Unity.Util.Gs2ClientHolder.Instance;
            _gameSessionHolder = Gs2.Unity.Util.Gs2GameSessionHolder.Instance;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>
    
    public partial class Gs2ProfileLoginAction
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2ProfileLoginAction
    {
        public Namespace Namespace;
        public Key key;
        public string userId;
        public string password;

        public void Account(EzAccount account)
        {
            userId = account.UserId;
            password = account.Password;
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ProfileLoginAction
    {
        [Serializable]
        private class LoginCompleteEvent : UnityEvent<EzAccessToken>
        {
            
        }
        
        [SerializeField]
        private LoginCompleteEvent onLoginComplete = new LoginCompleteEvent();
        
        public event UnityAction<EzAccessToken> OnLoginComplete
        {
            add => onLoginComplete.AddListener(value);
            remove => onLoginComplete.RemoveListener(value);
        }

        [SerializeField]
        internal ErrorEvent onError = new ErrorEvent();
        
        public event UnityAction<Gs2Exception, Func<IEnumerator>> OnError
        {
            add => onError.AddListener(value);
            remove => onError.RemoveListener(value);
        }
    }
}