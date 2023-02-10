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
using Gs2.Unity.Gs2Inbox.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Gs2Inbox.Context;
using UnityEngine;
using UnityEngine.Events;
using Message = Gs2.Unity.Gs2Inbox.ScriptableObject.Message;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Inbox
{
	[AddComponentMenu("GS2 UIKit/Inbox/Message/Action/Gs2InboxMessageReceiveGlobalMessageAction")]
    public partial class Gs2InboxMessageReceiveGlobalMessageAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            yield return new WaitUntil(() => this._clientHolder.Initialized);
            yield return new WaitUntil(() => this._gameSessionHolder.Initialized);
            
            var domain = this._clientHolder.Gs2.Inbox.Namespace(
                this._context.Message.NamespaceName
            ).Me(
                this._gameSessionHolder.GameSession
            );
            var future = domain.ReceiveGlobalMessage(
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
                        var items2 = new List<EzMessage>();
                        foreach (var domain_ in future.Result) {
                            var future3 = domain_.Model();
                            yield return future3;
                            if (future3.Error != null)
                            {
                                this.onError.Invoke(future3.Error, null);
                                yield break;
                            }
                            items2.Add(future3.Result);
                        }

                        this.onReceiveGlobalMessageComplete.Invoke(items2);
                    }

                    this.onError.Invoke(future.Error, Retry);
                    yield break;
                }

                this.onError.Invoke(future.Error, null);
                yield break;
            }
            var items = new List<EzMessage>();
            foreach (var domain_ in future.Result) {
                var future2 = domain_.Model();
                yield return future2;
                if (future2.Error != null)
                {
                    this.onError.Invoke(future2.Error, null);
                    yield break;
                }
                items.Add(future2.Result);
            }

            this.onReceiveGlobalMessageComplete.Invoke(items);
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

    public partial class Gs2InboxMessageReceiveGlobalMessageAction
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2InboxMessageContext _context;

        public void Awake()
        {
            this._clientHolder = Gs2ClientHolder.Instance;
            this._gameSessionHolder = Gs2GameSessionHolder.Instance;
            this._context = GetComponentInParent<Gs2InboxMessageContext>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2InboxMessageReceiveGlobalMessageAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2InboxMessageReceiveGlobalMessageAction
    {
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InboxMessageReceiveGlobalMessageAction
    {
        [Serializable]
        private class ReceiveGlobalMessageCompleteEvent : UnityEvent<List<EzMessage>>
        {

        }

        [SerializeField]
        private ReceiveGlobalMessageCompleteEvent onReceiveGlobalMessageComplete = new ReceiveGlobalMessageCompleteEvent();
        public event UnityAction<List<EzMessage>> OnReceiveGlobalMessageComplete
        {
            add => this.onReceiveGlobalMessageComplete.AddListener(value);
            remove => this.onReceiveGlobalMessageComplete.RemoveListener(value);
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
    public partial class Gs2InboxMessageReceiveGlobalMessageAction
    {
        [MenuItem("GameObject/Game Server Services/Inbox/Message/Action/ReceiveGlobalMessage", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2InboxMessageReceiveGlobalMessageAction>(
                "Assets/Scripts/Runtime/Sdk/Gs2/UiKit/Gs2Inbox/Prefabs/Action/Gs2InboxMessageReceiveGlobalMessageAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}