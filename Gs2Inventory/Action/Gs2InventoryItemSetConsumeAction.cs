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
using Gs2.Unity.Gs2Inventory.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Gs2Inventory.Context;
using UnityEngine;
using UnityEngine.Events;
using ItemSet = Gs2.Unity.Gs2Inventory.ScriptableObject.OwnItemSet;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Inventory
{
	[AddComponentMenu("GS2 UIKit/Inventory/ItemSet/Action/Gs2InventoryItemSetConsumeAction")]
    public partial class Gs2InventoryItemSetConsumeAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            yield return new WaitUntil(() => this._clientHolder.Initialized);
            yield return new WaitUntil(() => this._gameSessionHolder.Initialized);
            
            var domain = this._clientHolder.Gs2.Inventory.Namespace(
                this._context.ItemSet.NamespaceName
            ).Me(
                this._gameSessionHolder.GameSession
            ).Inventory(
                this._context.ItemSet.InventoryName
            ).ItemSet(
                this._context.ItemSet.ItemName,
                this._context.ItemSet.ItemSetName
            );
            var future = domain.Consume(
                ConsumeCount
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

                        this.onConsumeComplete.Invoke(future3.Result.ToList());
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

            this.onConsumeComplete.Invoke(future2.Result.ToList());
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

    public partial class Gs2InventoryItemSetConsumeAction
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2InventoryOwnItemSetContext _context;

        public void Awake()
        {
            this._clientHolder = Gs2ClientHolder.Instance;
            this._gameSessionHolder = Gs2GameSessionHolder.Instance;
            this._context = GetComponentInParent<Gs2InventoryOwnItemSetContext>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2InventoryItemSetConsumeAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2InventoryItemSetConsumeAction
    {
        public long ConsumeCount;

        public void SetConsumeCount(long value) {
            ConsumeCount = value;
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InventoryItemSetConsumeAction
    {
        [Serializable]
        private class ConsumeCompleteEvent : UnityEvent<List<EzItemSet>>
        {

        }

        [SerializeField]
        private ConsumeCompleteEvent onConsumeComplete = new ConsumeCompleteEvent();
        public event UnityAction<List<EzItemSet>> OnConsumeComplete
        {
            add => this.onConsumeComplete.AddListener(value);
            remove => this.onConsumeComplete.RemoveListener(value);
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
    public partial class Gs2InventoryItemSetConsumeAction
    {
        [MenuItem("GameObject/Game Server Services/Inventory/ItemSet/Action/Consume", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2InventoryItemSetConsumeAction>(
                "Assets/Scripts/Runtime/Sdk/Gs2/UiKit/Gs2Inventory/Prefabs/Action/Gs2InventoryItemSetConsumeAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}