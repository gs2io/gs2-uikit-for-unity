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
using Gathering = Gs2.Unity.Gs2Matchmaking.ScriptableObject.Gathering;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Matchmaking
{
	[AddComponentMenu("GS2 UIKit/Matchmaking/Gathering/Action/Gs2MatchmakingGatheringUpdateGatheringAction")]
    public partial class Gs2MatchmakingGatheringUpdateGatheringAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            yield return new WaitUntil(() => this._clientHolder.Initialized);
            yield return new WaitUntil(() => this._gameSessionHolder.Initialized);
            
            var domain = this._clientHolder.Gs2.Matchmaking.Namespace(
                this._context.Gathering.NamespaceName
            ).Me(
                this._gameSessionHolder.GameSession
            ).Gathering(
                this._context.Gathering.GatheringName
            );
            var future = domain.UpdateGathering(
                AttributeRanges.ToArray()
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

                        this.onUpdateGatheringComplete.Invoke(future3.Result);
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

            this.onUpdateGatheringComplete.Invoke(future2.Result);
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

    public partial class Gs2MatchmakingGatheringUpdateGatheringAction
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2MatchmakingGatheringContext _context;

        public void Awake()
        {
            this._clientHolder = Gs2ClientHolder.Instance;
            this._gameSessionHolder = Gs2GameSessionHolder.Instance;
            this._context = GetComponentInParent<Gs2MatchmakingGatheringContext>();

            if (_context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MatchmakingGatheringContext.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2MatchmakingGatheringUpdateGatheringAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2MatchmakingGatheringUpdateGatheringAction
    {
        public List<Gs2.Unity.Gs2Matchmaking.Model.EzAttributeRange> AttributeRanges;

        public void SetAttributeRanges(List<Gs2.Unity.Gs2Matchmaking.Model.EzAttributeRange> value) {
            AttributeRanges = value;
            this.onChangeAttributeRanges.Invoke(AttributeRanges);
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MatchmakingGatheringUpdateGatheringAction
    {

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
        private class UpdateGatheringCompleteEvent : UnityEvent<EzGathering>
        {

        }

        [SerializeField]
        private UpdateGatheringCompleteEvent onUpdateGatheringComplete = new UpdateGatheringCompleteEvent();
        public event UnityAction<EzGathering> OnUpdateGatheringComplete
        {
            add => this.onUpdateGatheringComplete.AddListener(value);
            remove => this.onUpdateGatheringComplete.RemoveListener(value);
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
    public partial class Gs2MatchmakingGatheringUpdateGatheringAction
    {
        [MenuItem("GameObject/Game Server Services/Matchmaking/Gathering/Action/UpdateGathering", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2MatchmakingGatheringUpdateGatheringAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Matchmaking/Prefabs/Action/Gs2MatchmakingGatheringUpdateGatheringAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}