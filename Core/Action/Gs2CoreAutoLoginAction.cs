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
using Gs2.Unity.Util;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Account
{
	[AddComponentMenu("GS2 UIKit/Core/Action/AutoLogin")]
    public partial class Gs2CoreAutoLoginAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            yield return new WaitUntil(() => this._clientHolder.Initialized);

            var createAccountFuture = this._clientHolder.Gs2.Account.Namespace(
                this.Namespace.namespaceName
            ).Create();
            yield return createAccountFuture;
            if (createAccountFuture.Error != null) {
                this.onError.Invoke(createAccountFuture.Error, Process);
                yield break;
            }

            var loadAccountFuture = createAccountFuture.Result.Model();
            yield return loadAccountFuture;
            if (loadAccountFuture.Error != null) {
                this.onError.Invoke(loadAccountFuture.Error, Process);
                yield break;
            }
            var account = loadAccountFuture.Result;

            var loginFuture = this._clientHolder.Profile.LoginFuture(
                new Gs2AccountAuthenticator(
                    this._clientHolder.Profile.Gs2RestSession,
                    this.Namespace.namespaceName,
                    this.Key.Grn,
                    account.UserId,
                    account.Password
                )
            );
            yield return loginFuture;
            if (loginFuture.Error != null) {
                this.onError.Invoke(loginFuture.Error, Process);
                yield break;
            }
            var gameSession = loginFuture.Result;
            
            this._gameSessionHolder.UpdateAccessToken(gameSession);

            this.onAutoLoginComplete.Invoke();
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

    public partial class Gs2CoreAutoLoginAction
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;

        public void Awake()
        {
            this._clientHolder = Gs2ClientHolder.Instance;
            this._gameSessionHolder = Gs2GameSessionHolder.Instance;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2CoreAutoLoginAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2CoreAutoLoginAction
    {
        public Gs2.Unity.Gs2Account.ScriptableObject.Namespace Namespace;
        public Gs2.Unity.Gs2Key.ScriptableObject.Key Key;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2CoreAutoLoginAction
    {
        [Serializable]
        private class AuthenticationCompleteEvent : UnityEvent
        {

        }

        [FormerlySerializedAs("onAuthenticationComplete")] [SerializeField]
        private AuthenticationCompleteEvent onAutoLoginComplete = new AuthenticationCompleteEvent();
        public event UnityAction OnAuthenticationComplete
        {
            add => this.onAutoLoginComplete.AddListener(value);
            remove => this.onAutoLoginComplete.RemoveListener(value);
        }

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
    public partial class Gs2CoreAutoLoginAction
    {
        [MenuItem("GameObject/Game Server Services/Core/Action/AutoLogin", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2CoreAutoLoginAction>(
                "Assets/Scripts/Runtime/Sdk/Gs2/UiKit/Core/Prefabs/Action/Gs2CoreAutoLoginAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}