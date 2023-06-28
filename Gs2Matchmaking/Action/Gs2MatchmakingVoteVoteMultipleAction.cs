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
using Vote = Gs2.Unity.Gs2Matchmaking.ScriptableObject.Vote;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Matchmaking
{
    public partial class Gs2MatchmakingVoteVoteMultipleAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            yield return new WaitUntil(() => this._clientHolder.Initialized);
            
            var domain = this._clientHolder.Gs2.Matchmaking.Namespace(
                this._context.Vote.NamespaceName
            );
            var future = domain.VoteMultiple(
                KeyId,
                SignedBallots.ToArray(),
                GameResults.ToArray()
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

                        this.onVoteMultipleComplete.Invoke(future3.Result);
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

            this.onVoteMultipleComplete.Invoke(future2.Result);
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

    public partial class Gs2MatchmakingVoteVoteMultipleAction
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2MatchmakingVoteContext _context;

        public void Awake()
        {
            this._clientHolder = Gs2ClientHolder.Instance;
            this._gameSessionHolder = Gs2GameSessionHolder.Instance;
            this._context = GetComponent<Gs2MatchmakingVoteContext>() ?? GetComponentInParent<Gs2MatchmakingVoteContext>();

            if (_context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MatchmakingVoteContext.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2MatchmakingVoteVoteMultipleAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2MatchmakingVoteVoteMultipleAction
    {
        public List<Gs2.Unity.Gs2Matchmaking.Model.EzSignedBallot> SignedBallots;
        public List<Gs2.Unity.Gs2Matchmaking.Model.EzGameResult> GameResults;
        public string KeyId;

        public void SetSignedBallots(List<Gs2.Unity.Gs2Matchmaking.Model.EzSignedBallot> value) {
            SignedBallots = value;
            this.onChangeSignedBallots.Invoke(SignedBallots);
        }

        public void SetGameResults(List<Gs2.Unity.Gs2Matchmaking.Model.EzGameResult> value) {
            GameResults = value;
            this.onChangeGameResults.Invoke(GameResults);
        }

        public void SetKeyId(string value) {
            KeyId = value;
            this.onChangeKeyId.Invoke(KeyId);
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MatchmakingVoteVoteMultipleAction
    {

        [Serializable]
        private class ChangeSignedBallotsEvent : UnityEvent<List<Gs2.Unity.Gs2Matchmaking.Model.EzSignedBallot>>
        {

        }

        [SerializeField]
        private ChangeSignedBallotsEvent onChangeSignedBallots = new ChangeSignedBallotsEvent();
        public event UnityAction<List<Gs2.Unity.Gs2Matchmaking.Model.EzSignedBallot>> OnChangeSignedBallots
        {
            add => this.onChangeSignedBallots.AddListener(value);
            remove => this.onChangeSignedBallots.RemoveListener(value);
        }

        [Serializable]
        private class ChangeGameResultsEvent : UnityEvent<List<Gs2.Unity.Gs2Matchmaking.Model.EzGameResult>>
        {

        }

        [SerializeField]
        private ChangeGameResultsEvent onChangeGameResults = new ChangeGameResultsEvent();
        public event UnityAction<List<Gs2.Unity.Gs2Matchmaking.Model.EzGameResult>> OnChangeGameResults
        {
            add => this.onChangeGameResults.AddListener(value);
            remove => this.onChangeGameResults.RemoveListener(value);
        }

        [Serializable]
        private class ChangeKeyIdEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ChangeKeyIdEvent onChangeKeyId = new ChangeKeyIdEvent();
        public event UnityAction<string> OnChangeKeyId
        {
            add => this.onChangeKeyId.AddListener(value);
            remove => this.onChangeKeyId.RemoveListener(value);
        }

        [Serializable]
        private class VoteMultipleCompleteEvent : UnityEvent<EzBallot>
        {

        }

        [SerializeField]
        private VoteMultipleCompleteEvent onVoteMultipleComplete = new VoteMultipleCompleteEvent();
        public event UnityAction<EzBallot> OnVoteMultipleComplete
        {
            add => this.onVoteMultipleComplete.AddListener(value);
            remove => this.onVoteMultipleComplete.RemoveListener(value);
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
    public partial class Gs2MatchmakingVoteVoteMultipleAction
    {
        [MenuItem("GameObject/Game Server Services/Matchmaking/Vote/Action/VoteMultiple", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2MatchmakingVoteVoteMultipleAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Matchmaking/Prefabs/Action/Gs2MatchmakingVoteVoteMultipleAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}