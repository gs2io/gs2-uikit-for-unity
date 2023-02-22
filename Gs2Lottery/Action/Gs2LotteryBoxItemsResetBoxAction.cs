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
using System.Collections.Generic;
using System.Linq;
using Gs2.Core.Exception;
using Gs2.Unity.Gs2Lottery.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Lottery.Context;
using UnityEngine;
using UnityEngine.Events;
using BoxItems = Gs2.Unity.Gs2Lottery.ScriptableObject.OwnBoxItems;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Lottery
{
	[AddComponentMenu("GS2 UIKit/Lottery/BoxItems/Action/Gs2LotteryBoxItemsResetBoxAction")]
    public partial class Gs2LotteryBoxItemsResetBoxAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            yield return new WaitUntil(() => this._clientHolder.Initialized);
            yield return new WaitUntil(() => this._gameSessionHolder.Initialized);
            
            var domain = this._clientHolder.Gs2.Lottery.Namespace(
                this._context.BoxItems.NamespaceName
            ).Me(
                this._gameSessionHolder.GameSession
            ).BoxItems(
                this._context.BoxItems.PrizeTableName
            );
            var future = domain.ResetBox(
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
                        this.onResetBoxComplete.Invoke();
                    }

                    this.onError.Invoke(future.Error, Retry);
                    yield break;
                }

                this.onError.Invoke(future.Error, null);
                yield break;
            }
            this.onResetBoxComplete.Invoke();
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

    public partial class Gs2LotteryBoxItemsResetBoxAction
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2LotteryOwnBoxItemsContext _context;

        public void Awake()
        {
            this._clientHolder = Gs2ClientHolder.Instance;
            this._gameSessionHolder = Gs2GameSessionHolder.Instance;
            this._context = GetComponentInParent<Gs2LotteryOwnBoxItemsContext>();

            if (_context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2LotteryOwnBoxItemsContext.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2LotteryBoxItemsResetBoxAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2LotteryBoxItemsResetBoxAction
    {
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2LotteryBoxItemsResetBoxAction
    {

        [Serializable]
        private class ResetBoxCompleteEvent : UnityEvent
        {

        }

        [SerializeField]
        private ResetBoxCompleteEvent onResetBoxComplete = new ResetBoxCompleteEvent();
        public event UnityAction OnResetBoxComplete
        {
            add => this.onResetBoxComplete.AddListener(value);
            remove => this.onResetBoxComplete.RemoveListener(value);
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
    public partial class Gs2LotteryBoxItemsResetBoxAction
    {
        [MenuItem("GameObject/Game Server Services/Lottery/BoxItems/Action/ResetBox", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2LotteryBoxItemsResetBoxAction>(
                "Assets/Scripts/Runtime/Sdk/Gs2/UiKit/Gs2Lottery/Prefabs/Action/Gs2LotteryBoxItemsResetBoxAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}