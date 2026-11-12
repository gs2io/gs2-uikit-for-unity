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
using Gs2.Unity.Gs2Account.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Account.Context;
using UnityEngine;
using UnityEngine.Events;
using Account = Gs2.Unity.Gs2Account.ScriptableObject.OwnAccount;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Account
{
    public partial class Gs2AccountAccountPreviewTakeOverOpenIdConnectAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);

            this.onPreviewTakeOverOpenIdConnectStart.Invoke();

            
            var domain = clientHolder.Gs2.Account.Namespace(
                this._context.Account.NamespaceName
            );
            var future = domain.PreviewTakeOverOpenIdConnectFuture(
                Type,
                IdToken
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

                        this.onPreviewTakeOverOpenIdConnectComplete.Invoke(future3.Result);
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

            this.onPreviewTakeOverOpenIdConnectComplete.Invoke(future2.Result);
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

    public partial class Gs2AccountAccountPreviewTakeOverOpenIdConnectAction
    {
        private Gs2AccountOwnAccountContext _context;

        public void Awake()
        {
            this._context = GetComponent<Gs2AccountOwnAccountContext>() ?? GetComponentInParent<Gs2AccountOwnAccountContext>();
            if (this._context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2AccountOwnAccountContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._context = GetComponent<Gs2AccountOwnAccountContext>() ?? GetComponentInParent<Gs2AccountOwnAccountContext>(true);
            if (this._context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2AccountAccountPreviewTakeOverOpenIdConnectAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2AccountAccountPreviewTakeOverOpenIdConnectAction
    {
        public bool WaitAsyncProcessComplete;
        public int Type;
        public string IdToken;

        public void SetType(int value) {
            this.Type = value;
            this.onChangeType.Invoke(this.Type);
            this.OnChange.Invoke();
        }

        public void DecreaseType() {
            this.Type -= 1;
            this.onChangeType.Invoke(this.Type);
            this.OnChange.Invoke();
        }

        public void IncreaseType() {
            this.Type += 1;
            this.onChangeType.Invoke(this.Type);
            this.OnChange.Invoke();
        }

        public void SetIdToken(string value) {
            this.IdToken = value;
            this.onChangeIdToken.Invoke(this.IdToken);
            this.OnChange.Invoke();
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2AccountAccountPreviewTakeOverOpenIdConnectAction
    {

        [Serializable]
        private class ChangeTypeEvent : UnityEvent<int>
        {

        }

        [SerializeField]
        private ChangeTypeEvent onChangeType = new ChangeTypeEvent();
        public event UnityAction<int> OnChangeType
        {
            add => this.onChangeType.AddListener(value);
            remove => this.onChangeType.RemoveListener(value);
        }

        [Serializable]
        private class ChangeIdTokenEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ChangeIdTokenEvent onChangeIdToken = new ChangeIdTokenEvent();
        public event UnityAction<string> OnChangeIdToken
        {
            add => this.onChangeIdToken.AddListener(value);
            remove => this.onChangeIdToken.RemoveListener(value);
        }

        [Serializable]
        private class PreviewTakeOverOpenIdConnectStartEvent : UnityEvent
        {

        }

        [SerializeField]
        private PreviewTakeOverOpenIdConnectStartEvent onPreviewTakeOverOpenIdConnectStart = new PreviewTakeOverOpenIdConnectStartEvent();

        public event UnityAction OnPreviewTakeOverOpenIdConnectStart
        {
            add => this.onPreviewTakeOverOpenIdConnectStart.AddListener(value);
            remove => this.onPreviewTakeOverOpenIdConnectStart.RemoveListener(value);
        }

        [Serializable]
        private class PreviewTakeOverOpenIdConnectCompleteEvent : UnityEvent<EzAccount>
        {

        }

        [SerializeField]
        private PreviewTakeOverOpenIdConnectCompleteEvent onPreviewTakeOverOpenIdConnectComplete = new PreviewTakeOverOpenIdConnectCompleteEvent();
        public event UnityAction<EzAccount> OnPreviewTakeOverOpenIdConnectComplete
        {
            add => this.onPreviewTakeOverOpenIdConnectComplete.AddListener(value);
            remove => this.onPreviewTakeOverOpenIdConnectComplete.RemoveListener(value);
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
    public partial class Gs2AccountAccountPreviewTakeOverOpenIdConnectAction
    {
        [MenuItem("GameObject/Game Server Services/Account/Account/Action/PreviewTakeOverOpenIdConnect", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2AccountAccountPreviewTakeOverOpenIdConnectAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Account/Prefabs/Action/Gs2AccountAccountPreviewTakeOverOpenIdConnectAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}