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
// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable CheckNamespace

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gs2.Core.Exception;
using Gs2.Gs2Money.Request;
using Gs2.Unity.Gs2Showcase.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Showcase.Context;
using Gs2.Unity.UiKit.Gs2Showcase.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;
using Showcase = Gs2.Unity.Gs2Showcase.ScriptableObject.Showcase;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Showcase
{
	[AddComponentMenu("GS2 UIKit/Showcase/Showcase/Action/Gs2ShowcaseShowcaseBuyAction")]
    public partial class Gs2ShowcaseShowcaseBuyAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            yield return new WaitUntil(() => this._clientHolder.Initialized);
            yield return new WaitUntil(() => this._gameSessionHolder.Initialized);

            var config = new List<Gs2.Unity.Gs2Showcase.Model.EzConfig>(Config);
            
#if GS2_ENABLE_PURCHASING

            PurchaseParameters purchaseParameters = null;
            var needReceipt = this._fetcher.DisplayItem.SalesItem.ConsumeActions.FirstOrDefault(
                v => v.Action == "Gs2Money:RecordReceipt"
            );
            if (needReceipt != null) {
                var request = RecordReceiptRequest.FromJson(JsonMapper.ToObject(needReceipt.Request));
                var iapFuture = new IAPUtil().BuyFuture(request.ContentsId);
                yield return iapFuture;
                if (iapFuture.Error != null) {
                    this.onError.Invoke(iapFuture.Error, Process);
                    yield break;
                }
                purchaseParameters = iapFuture.Result;
                config.Add(new EzConfig {
                    Key = "receipt",
                    Value = purchaseParameters.receipt,
                });
            }

#endif
            
            var domain = this._clientHolder.Gs2.Showcase.Namespace(
                this._context.DisplayItem.NamespaceName
            ).Me(
                this._gameSessionHolder.GameSession
            ).Showcase(
                this._context.DisplayItem.ShowcaseName
            );
            var future = domain.Buy(
                this._context.DisplayItem.DisplayItemId,
                Quantity,
                config.ToArray()
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
                        
#if GS2_ENABLE_PURCHASING
                        if (purchaseParameters != null) {
                            purchaseParameters.controller.ConfirmPendingPurchase(purchaseParameters.product);
                        }
#endif

                        this.onBuyComplete.Invoke(future.Result.TransactionId);
                    }

                    this.onError.Invoke(future.Error, Retry);
                    yield break;
                }

                this.onError.Invoke(future.Error, null);
                yield break;
            }
            
#if GS2_ENABLE_PURCHASING
            if (purchaseParameters != null) {
                purchaseParameters.controller.ConfirmPendingPurchase(purchaseParameters.product);
            }
#endif

            this.onBuyComplete.Invoke(future.Result.TransactionId);
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

    public partial class Gs2ShowcaseShowcaseBuyAction
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2ShowcaseDisplayItemContext _context;
        private Gs2ShowcaseDisplayItemFetcher _fetcher;

        public void Awake()
        {
            this._clientHolder = Gs2ClientHolder.Instance;
            this._gameSessionHolder = Gs2GameSessionHolder.Instance;
            this._context = GetComponentInParent<Gs2ShowcaseDisplayItemContext>();
            this._fetcher = GetComponentInParent<Gs2ShowcaseDisplayItemFetcher>();

            if (_context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ShowcaseShowcaseContext.");
                enabled = false;
            }
            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ShowcaseDisplayItemFetcher.");
                enabled = false;
            }
            
            this.onChangeQuantity.Invoke(Quantity);
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2ShowcaseShowcaseBuyAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2ShowcaseShowcaseBuyAction
    {
        public int Quantity;
        public List<Gs2.Unity.Gs2Showcase.Model.EzConfig> Config;

        public void SetQuantity(int value) {
            Quantity = value;
            this.onChangeQuantity.Invoke(Quantity);
        }

        public void DecreaseQuantity() {
            Quantity -= 1;
            this.onChangeQuantity.Invoke(Quantity);
        }

        public void IncreaseQuantity() {
            Quantity += 1;
            this.onChangeQuantity.Invoke(Quantity);
        }

        public void SetConfig(List<Gs2.Unity.Gs2Showcase.Model.EzConfig> value) {
            Config = value;
            this.onChangeConfig.Invoke(Config);
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ShowcaseShowcaseBuyAction
    {
        [Serializable]
        private class ChangeQuantityEvent : UnityEvent<int>
        {

        }

        [SerializeField]
        private ChangeQuantityEvent onChangeQuantity = new ChangeQuantityEvent();
        public event UnityAction<int> OnChangeQuantity
        {
            add => this.onChangeQuantity.AddListener(value);
            remove => this.onChangeQuantity.RemoveListener(value);
        }

        [Serializable]
        private class ChangeConfigEvent : UnityEvent<List<Gs2.Unity.Gs2Showcase.Model.EzConfig>>
        {

        }

        [SerializeField]
        private ChangeConfigEvent onChangeConfig = new ChangeConfigEvent();
        public event UnityAction<List<Gs2.Unity.Gs2Showcase.Model.EzConfig>> OnChangeConfig
        {
            add => this.onChangeConfig.AddListener(value);
            remove => this.onChangeConfig.RemoveListener(value);
        }

        [Serializable]
        private class BuyCompleteEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private BuyCompleteEvent onBuyComplete = new BuyCompleteEvent();
        public event UnityAction<string> OnBuyComplete
        {
            add => this.onBuyComplete.AddListener(value);
            remove => this.onBuyComplete.RemoveListener(value);
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
    public partial class Gs2ShowcaseShowcaseBuyAction
    {
        [MenuItem("GameObject/Game Server Services/Showcase/Showcase/Action/Buy", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2ShowcaseShowcaseBuyAction>(
                "Assets/Scripts/Runtime/Sdk/Gs2/UiKit/Gs2Showcase/Prefabs/Action/Gs2ShowcaseShowcaseBuyAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}