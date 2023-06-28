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
using Gs2.Unity.Gs2Money.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Money.Context;
using UnityEngine;
using UnityEngine.Events;
using Wallet = Gs2.Unity.Gs2Money.ScriptableObject.OwnWallet;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Money
{
    public partial class Gs2MoneyWalletWithdrawAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            yield return new WaitUntil(() => this._clientHolder.Initialized);
            yield return new WaitUntil(() => this._gameSessionHolder.Initialized);
            
            var domain = this._clientHolder.Gs2.Money.Namespace(
                this._context.Wallet.NamespaceName
            ).Me(
                this._gameSessionHolder.GameSession
            ).Wallet(
                this._context.Wallet.Slot
            );
            var future = domain.Withdraw(
                Count,
                PaidOnly
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
                        var future3 = future.Result.Model();
                        yield return future3;
                        if (future3.Error != null)
                        {
                            this.onError.Invoke(future3.Error, null);
                            yield break;
                        }

                        this.onWithdrawComplete.Invoke(future3.Result);
                    }

                    this.onError.Invoke(future.Error, Retry);
                    yield break;
                }

                this.onError.Invoke(future.Error, null);
                yield break;
            }
            var future2 = future.Result.Model();
            yield return future2;
            if (future2.Error != null)
            {
                this.onError.Invoke(future2.Error, null);
                yield break;
            }

            this.onWithdrawComplete.Invoke(future2.Result);
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

    public partial class Gs2MoneyWalletWithdrawAction
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2MoneyOwnWalletContext _context;

        public void Awake()
        {
            this._clientHolder = Gs2ClientHolder.Instance;
            this._gameSessionHolder = Gs2GameSessionHolder.Instance;
            this._context = GetComponent<Gs2MoneyOwnWalletContext>() ?? GetComponentInParent<Gs2MoneyOwnWalletContext>();

            if (_context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MoneyOwnWalletContext.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2MoneyWalletWithdrawAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2MoneyWalletWithdrawAction
    {
        public int Count;
        public bool PaidOnly;

        public void SetCount(int value) {
            Count = value;
            this.onChangeCount.Invoke(Count);
        }

        public void DecreaseCount() {
            Count -= 1;
            this.onChangeCount.Invoke(Count);
        }

        public void IncreaseCount() {
            Count += 1;
            this.onChangeCount.Invoke(Count);
        }

        public void SetPaidOnly(bool value) {
            PaidOnly = value;
            this.onChangePaidOnly.Invoke(PaidOnly);
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MoneyWalletWithdrawAction
    {

        [Serializable]
        private class ChangeCountEvent : UnityEvent<int>
        {

        }

        [SerializeField]
        private ChangeCountEvent onChangeCount = new ChangeCountEvent();
        public event UnityAction<int> OnChangeCount
        {
            add => this.onChangeCount.AddListener(value);
            remove => this.onChangeCount.RemoveListener(value);
        }

        [Serializable]
        private class ChangePaidOnlyEvent : UnityEvent<bool>
        {

        }

        [SerializeField]
        private ChangePaidOnlyEvent onChangePaidOnly = new ChangePaidOnlyEvent();
        public event UnityAction<bool> OnChangePaidOnly
        {
            add => this.onChangePaidOnly.AddListener(value);
            remove => this.onChangePaidOnly.RemoveListener(value);
        }

        [Serializable]
        private class WithdrawCompleteEvent : UnityEvent<EzWallet>
        {

        }

        [SerializeField]
        private WithdrawCompleteEvent onWithdrawComplete = new WithdrawCompleteEvent();
        public event UnityAction<EzWallet> OnWithdrawComplete
        {
            add => this.onWithdrawComplete.AddListener(value);
            remove => this.onWithdrawComplete.RemoveListener(value);
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
    public partial class Gs2MoneyWalletWithdrawAction
    {
        [MenuItem("GameObject/Game Server Services/Money/Wallet/Action/Withdraw", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2MoneyWalletWithdrawAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Money/Prefabs/Action/Gs2MoneyWalletWithdrawAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}