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
using Gs2.Unity.Gs2Gateway.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Gateway.Context;
using UnityEngine;
using UnityEngine.Events;
using FirebaseToken = Gs2.Unity.Gs2Gateway.ScriptableObject.OwnFirebaseToken;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Gateway
{
    public partial class Gs2GatewayFirebaseTokenSetFirebaseTokenAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);

            this.onSetFirebaseTokenStart.Invoke();

            
            var domain = clientHolder.Gs2.Gateway.Namespace(
                this._context.FirebaseToken.NamespaceName
            ).Me(
                gameSessionHolder.GameSession
            ).FirebaseToken(
            );
            var future = domain.SetFirebaseTokenFuture(
                Token,
                Locale
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
                            this.onError.Invoke(future.Error, Retry);
                            yield break;
                        }
                        var future3 = future.Result.ModelFuture();
                        yield return future3;
                        if (future3.Error != null)
                        {
                            this.onError.Invoke(future3.Error, null);
                            yield break;
                        }

                        this.onSetFirebaseTokenComplete.Invoke(future3.Result);
                    }

                    this.onError.Invoke(future.Error, Retry);
                    yield break;
                }

                this.onError.Invoke(future.Error, null);
                yield break;
            }
            var future2 = future.Result.ModelFuture();
            yield return future2;
            if (future2.Error != null)
            {
                this.onError.Invoke(future2.Error, null);
                yield break;
            }

            this.onSetFirebaseTokenComplete.Invoke(future2.Result);
        }

        public void OnEnable()
        {
            Gs2ClientHolder.Instance.StartCoroutine(Process());
        }

        public void OnDisable()
        {

        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2GatewayFirebaseTokenSetFirebaseTokenAction
    {
        private Gs2GatewayOwnFirebaseTokenContext _context;

        public void Awake()
        {
            this._context = GetComponent<Gs2GatewayOwnFirebaseTokenContext>() ?? GetComponentInParent<Gs2GatewayOwnFirebaseTokenContext>();
            if (this._context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2GatewayOwnFirebaseTokenContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._context = GetComponent<Gs2GatewayOwnFirebaseTokenContext>() ?? GetComponentInParent<Gs2GatewayOwnFirebaseTokenContext>(true);
            if (this._context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2GatewayFirebaseTokenSetFirebaseTokenAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2GatewayFirebaseTokenSetFirebaseTokenAction
    {
        public bool WaitAsyncProcessComplete;
        public string Token;
        public string Locale;

        public void SetToken(string value) {
            this.Token = value;
            this.onChangeToken.Invoke(this.Token);
            this.OnChange.Invoke();
        }

        public void SetLocale(string value) {
            this.Locale = value;
            this.onChangeLocale.Invoke(this.Locale);
            this.OnChange.Invoke();
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2GatewayFirebaseTokenSetFirebaseTokenAction
    {

        [Serializable]
        private class ChangeTokenEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ChangeTokenEvent onChangeToken = new ChangeTokenEvent();
        public event UnityAction<string> OnChangeToken
        {
            add => this.onChangeToken.AddListener(value);
            remove => this.onChangeToken.RemoveListener(value);
        }

        [Serializable]
        private class ChangeLocaleEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ChangeLocaleEvent onChangeLocale = new ChangeLocaleEvent();
        public event UnityAction<string> OnChangeLocale
        {
            add => this.onChangeLocale.AddListener(value);
            remove => this.onChangeLocale.RemoveListener(value);
        }

        [Serializable]
        private class SetFirebaseTokenStartEvent : UnityEvent
        {

        }

        [SerializeField]
        private SetFirebaseTokenStartEvent onSetFirebaseTokenStart = new SetFirebaseTokenStartEvent();

        public event UnityAction OnSetFirebaseTokenStart
        {
            add => this.onSetFirebaseTokenStart.AddListener(value);
            remove => this.onSetFirebaseTokenStart.RemoveListener(value);
        }

        [Serializable]
        private class SetFirebaseTokenCompleteEvent : UnityEvent<EzFirebaseToken>
        {

        }

        [SerializeField]
        private SetFirebaseTokenCompleteEvent onSetFirebaseTokenComplete = new SetFirebaseTokenCompleteEvent();
        public event UnityAction<EzFirebaseToken> OnSetFirebaseTokenComplete
        {
            add => this.onSetFirebaseTokenComplete.AddListener(value);
            remove => this.onSetFirebaseTokenComplete.RemoveListener(value);
        }

        public UnityEvent OnChange = new UnityEvent();

        [SerializeField]
        internal ErrorEvent onError = new ErrorEvent();

        public event UnityAction<Gs2Exception, Func<IEnumerator>> OnError
        {
            add => this.onError.AddListener(value);
            remove => this.onError.RemoveListener(value);
        }
    }

#if UNITY_EDITOR

    /// <summary>
    /// Context Menu
    /// </summary>
    public partial class Gs2GatewayFirebaseTokenSetFirebaseTokenAction
    {
        [MenuItem("GameObject/Game Server Services/Gateway/FirebaseToken/Action/SetFirebaseToken", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2GatewayFirebaseTokenSetFirebaseTokenAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Gateway/Prefabs/Action/Gs2GatewayFirebaseTokenSetFirebaseTokenAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}