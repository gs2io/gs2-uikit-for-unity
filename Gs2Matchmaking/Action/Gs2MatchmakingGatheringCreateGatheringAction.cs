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
using Gs2.Unity.Gs2Matchmaking.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Matchmaking.Context;
using UnityEngine;
using UnityEngine.Events;
using User = Gs2.Unity.Gs2Matchmaking.ScriptableObject.User;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Matchmaking
{
	[AddComponentMenu("GS2 UIKit/Matchmaking/Gathering/Action/Gs2MatchmakingGatheringCreateGatheringAction")]
    public partial class Gs2MatchmakingGatheringCreateGatheringAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            yield return new WaitUntil(() => this._clientHolder.Initialized);
            yield return new WaitUntil(() => this._gameSessionHolder.Initialized);
            
            var domain = this._clientHolder.Gs2.Matchmaking.Namespace(
                this._context.Namespace.NamespaceName
            ).Me(
                this._gameSessionHolder.GameSession
            );
            var future = domain.CreateGathering(
                Player,
                AttributeRanges.ToArray(),
                CapacityOfRoles.ToArray(),
                AllowUserIds.ToArray(),
                ExpiresAt,
                ExpiresAtTimeSpan
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

                        this.onCreateGatheringComplete.Invoke(future3.Result);
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

            this.onCreateGatheringComplete.Invoke(future2.Result);
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

    public partial class Gs2MatchmakingGatheringCreateGatheringAction
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2MatchmakingNamespaceContext _context;

        public void Awake()
        {
            this._clientHolder = Gs2ClientHolder.Instance;
            this._gameSessionHolder = Gs2GameSessionHolder.Instance;
            this._context = GetComponent<Gs2MatchmakingNamespaceContext>() ?? GetComponentInParent<Gs2MatchmakingNamespaceContext>();

            if (_context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MatchmakingNamespaceContext.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2MatchmakingGatheringCreateGatheringAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2MatchmakingGatheringCreateGatheringAction
    {
        public Gs2.Unity.Gs2Matchmaking.Model.EzPlayer Player;
        public List<Gs2.Unity.Gs2Matchmaking.Model.EzAttributeRange> AttributeRanges;
        public List<Gs2.Unity.Gs2Matchmaking.Model.EzCapacityOfRole> CapacityOfRoles;
        public List<string> AllowUserIds;
        public long ExpiresAt;
        public Gs2.Unity.Gs2Matchmaking.Model.EzTimeSpan ExpiresAtTimeSpan;

        public void SetPlayer(Gs2.Unity.Gs2Matchmaking.Model.EzPlayer value) {
            Player = value;
            this.onChangePlayer.Invoke(Player);
        }

        public void SetAttributeRanges(List<Gs2.Unity.Gs2Matchmaking.Model.EzAttributeRange> value) {
            AttributeRanges = value;
            this.onChangeAttributeRanges.Invoke(AttributeRanges);
        }

        public void SetCapacityOfRoles(List<Gs2.Unity.Gs2Matchmaking.Model.EzCapacityOfRole> value) {
            CapacityOfRoles = value;
            this.onChangeCapacityOfRoles.Invoke(CapacityOfRoles);
        }

        public void SetAllowUserIds(List<string> value) {
            AllowUserIds = value;
            this.onChangeAllowUserIds.Invoke(AllowUserIds);
        }

        public void SetExpiresAt(long value) {
            ExpiresAt = value;
            this.onChangeExpiresAt.Invoke(ExpiresAt);
        }

        public void DecreaseExpiresAt() {
            ExpiresAt -= 1;
            this.onChangeExpiresAt.Invoke(ExpiresAt);
        }

        public void IncreaseExpiresAt() {
            ExpiresAt += 1;
            this.onChangeExpiresAt.Invoke(ExpiresAt);
        }

        public void SetExpiresAtTimeSpan(Gs2.Unity.Gs2Matchmaking.Model.EzTimeSpan value) {
            ExpiresAtTimeSpan = value;
            this.onChangeExpiresAtTimeSpan.Invoke(ExpiresAtTimeSpan);
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MatchmakingGatheringCreateGatheringAction
    {

        [Serializable]
        private class ChangePlayerEvent : UnityEvent<Gs2.Unity.Gs2Matchmaking.Model.EzPlayer>
        {

        }

        [SerializeField]
        private ChangePlayerEvent onChangePlayer = new ChangePlayerEvent();
        public event UnityAction<Gs2.Unity.Gs2Matchmaking.Model.EzPlayer> OnChangePlayer
        {
            add => this.onChangePlayer.AddListener(value);
            remove => this.onChangePlayer.RemoveListener(value);
        }

        [Serializable]
        private class ChangeAttributeRangesEvent : UnityEvent<List<Gs2.Unity.Gs2Matchmaking.Model.EzAttributeRange>>
        {

        }

        [SerializeField]
        private ChangeAttributeRangesEvent onChangeAttributeRanges = new ChangeAttributeRangesEvent();
        public event UnityAction<List<Gs2.Unity.Gs2Matchmaking.Model.EzAttributeRange>> OnChangeAttributeRanges
        {
            add => this.onChangeAttributeRanges.AddListener(value);
            remove => this.onChangeAttributeRanges.RemoveListener(value);
        }

        [Serializable]
        private class ChangeCapacityOfRolesEvent : UnityEvent<List<Gs2.Unity.Gs2Matchmaking.Model.EzCapacityOfRole>>
        {

        }

        [SerializeField]
        private ChangeCapacityOfRolesEvent onChangeCapacityOfRoles = new ChangeCapacityOfRolesEvent();
        public event UnityAction<List<Gs2.Unity.Gs2Matchmaking.Model.EzCapacityOfRole>> OnChangeCapacityOfRoles
        {
            add => this.onChangeCapacityOfRoles.AddListener(value);
            remove => this.onChangeCapacityOfRoles.RemoveListener(value);
        }

        [Serializable]
        private class ChangeAllowUserIdsEvent : UnityEvent<List<string>>
        {

        }

        [SerializeField]
        private ChangeAllowUserIdsEvent onChangeAllowUserIds = new ChangeAllowUserIdsEvent();
        public event UnityAction<List<string>> OnChangeAllowUserIds
        {
            add => this.onChangeAllowUserIds.AddListener(value);
            remove => this.onChangeAllowUserIds.RemoveListener(value);
        }

        [Serializable]
        private class ChangeExpiresAtEvent : UnityEvent<long>
        {

        }

        [SerializeField]
        private ChangeExpiresAtEvent onChangeExpiresAt = new ChangeExpiresAtEvent();
        public event UnityAction<long> OnChangeExpiresAt
        {
            add => this.onChangeExpiresAt.AddListener(value);
            remove => this.onChangeExpiresAt.RemoveListener(value);
        }

        [Serializable]
        private class ChangeExpiresAtTimeSpanEvent : UnityEvent<Gs2.Unity.Gs2Matchmaking.Model.EzTimeSpan>
        {

        }

        [SerializeField]
        private ChangeExpiresAtTimeSpanEvent onChangeExpiresAtTimeSpan = new ChangeExpiresAtTimeSpanEvent();
        public event UnityAction<Gs2.Unity.Gs2Matchmaking.Model.EzTimeSpan> OnChangeExpiresAtTimeSpan
        {
            add => this.onChangeExpiresAtTimeSpan.AddListener(value);
            remove => this.onChangeExpiresAtTimeSpan.RemoveListener(value);
        }

        [Serializable]
        private class CreateGatheringCompleteEvent : UnityEvent<EzGathering>
        {

        }

        [SerializeField]
        private CreateGatheringCompleteEvent onCreateGatheringComplete = new CreateGatheringCompleteEvent();
        public event UnityAction<EzGathering> OnCreateGatheringComplete
        {
            add => this.onCreateGatheringComplete.AddListener(value);
            remove => this.onCreateGatheringComplete.RemoveListener(value);
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
    public partial class Gs2MatchmakingGatheringCreateGatheringAction
    {
        [MenuItem("GameObject/Game Server Services/Matchmaking/Gathering/Action/CreateGathering", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2MatchmakingGatheringCreateGatheringAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Matchmaking/Prefabs/Action/Gs2MatchmakingGatheringCreateGatheringAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}