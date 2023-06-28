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
using Gs2.Unity.Gs2Matchmaking.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Gs2Matchmaking.Context;
using UnityEngine;
using UnityEngine.Events;
using Gathering = Gs2.Unity.Gs2Matchmaking.ScriptableObject.Gathering;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Matchmaking
{
    public partial class Gs2MatchmakingGatheringDoMatchmakingAction : MonoBehaviour
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
            var items = domain.DoMatchmaking(
                  Player
            );
            yield return items.Next();
            this.onDoMatchmakingComplete.Invoke(items.Current);
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

    public partial class Gs2MatchmakingGatheringDoMatchmakingAction
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2MatchmakingNamespaceContext _context;

        public void Awake()
        {
            this._clientHolder = Gs2ClientHolder.Instance;
            this._gameSessionHolder = Gs2GameSessionHolder.Instance;
            this._context = GetComponentInParent<Gs2MatchmakingNamespaceContext>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2MatchmakingGatheringDoMatchmakingAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2MatchmakingGatheringDoMatchmakingAction
    {
        public Gs2.Unity.Gs2Matchmaking.Model.EzPlayer Player;
        public string MatchmakingContextToken;

        public void SetPlayer(Gs2.Unity.Gs2Matchmaking.Model.EzPlayer value) {
            Player = value;
        }

        public void SetMatchmakingContextToken(string value) {
            MatchmakingContextToken = value;
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MatchmakingGatheringDoMatchmakingAction
    {
        [Serializable]
        private class DoMatchmakingCompleteEvent : UnityEvent<EzGathering>
        {

        }

        [SerializeField]
        private DoMatchmakingCompleteEvent onDoMatchmakingComplete = new DoMatchmakingCompleteEvent();
        public event UnityAction<EzGathering> OnDoMatchmakingComplete
        {
            add => this.onDoMatchmakingComplete.AddListener(value);
            remove => this.onDoMatchmakingComplete.RemoveListener(value);
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
    public partial class Gs2MatchmakingGatheringDoMatchmakingAction
    {
        [MenuItem("GameObject/Game Server Services/Matchmaking/Gathering/Action/DoMatchmaking", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2MatchmakingGatheringDoMatchmakingAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Matchmaking/Prefabs/Action/Gs2MatchmakingGatheringDoMatchmakingAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}