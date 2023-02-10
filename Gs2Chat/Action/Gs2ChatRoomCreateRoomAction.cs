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
using Gs2.Unity.Gs2Chat.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Gs2Chat.Context;
using UnityEngine;
using UnityEngine.Events;
using User = Gs2.Unity.Gs2Chat.ScriptableObject.User;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Chat
{
	[AddComponentMenu("GS2 UIKit/Chat/Room/Action/Gs2ChatRoomCreateRoomAction")]
    public partial class Gs2ChatRoomCreateRoomAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            yield return new WaitUntil(() => this._clientHolder.Initialized);
            yield return new WaitUntil(() => this._gameSessionHolder.Initialized);
            
            var domain = this._clientHolder.Gs2.Chat.Namespace(
                this._context.User.NamespaceName
            ).Me(
                this._gameSessionHolder.GameSession
            );
            var future = domain.CreateRoom(
                Name,
                Metadata,
                Password,
                WhiteListUserIds.ToArray()
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

                        this.onCreateRoomComplete.Invoke(future3.Result);
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

            this.onCreateRoomComplete.Invoke(future2.Result);
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

    public partial class Gs2ChatRoomCreateRoomAction
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2ChatUserContext _context;

        public void Awake()
        {
            this._clientHolder = Gs2ClientHolder.Instance;
            this._gameSessionHolder = Gs2GameSessionHolder.Instance;
            this._context = GetComponentInParent<Gs2ChatUserContext>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2ChatRoomCreateRoomAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2ChatRoomCreateRoomAction
    {
        public string Name;
        public string Metadata;
        public string Password;
        public List<string> WhiteListUserIds;

        public void SetName(string value) {
            Name = value;
        }

        public void SetMetadata(string value) {
            Metadata = value;
        }

        public void SetPassword(string value) {
            Password = value;
        }

        public void SetWhiteListUserIds(List<string> value) {
            WhiteListUserIds = value;
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ChatRoomCreateRoomAction
    {
        [Serializable]
        private class CreateRoomCompleteEvent : UnityEvent<EzRoom>
        {

        }

        [SerializeField]
        private CreateRoomCompleteEvent onCreateRoomComplete = new CreateRoomCompleteEvent();
        public event UnityAction<EzRoom> OnCreateRoomComplete
        {
            add => this.onCreateRoomComplete.AddListener(value);
            remove => this.onCreateRoomComplete.RemoveListener(value);
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
    public partial class Gs2ChatRoomCreateRoomAction
    {
        [MenuItem("GameObject/Game Server Services/Chat/Room/Action/CreateRoom", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2ChatRoomCreateRoomAction>(
                "Assets/Scripts/Runtime/Sdk/Gs2/UiKit/Gs2Chat/Prefabs/Action/Gs2ChatRoomCreateRoomAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}