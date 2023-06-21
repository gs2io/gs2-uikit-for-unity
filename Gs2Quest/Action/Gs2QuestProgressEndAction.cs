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
using Gs2.Unity.Gs2Quest.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Quest.Context;
using UnityEngine;
using UnityEngine.Events;
using Progress = Gs2.Unity.Gs2Quest.ScriptableObject.OwnProgress;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Quest
{
	[AddComponentMenu("GS2 UIKit/Quest/Progress/Action/Gs2QuestProgressEndAction")]
    public partial class Gs2QuestProgressEndAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            yield return new WaitUntil(() => this._clientHolder.Initialized);
            yield return new WaitUntil(() => this._gameSessionHolder.Initialized);
            
            var domain = this._clientHolder.Gs2.Quest.Namespace(
                this._context.Progress.NamespaceName
            ).Me(
                this._gameSessionHolder.GameSession
            ).Progress(
            );
            var future = domain.End(
                IsComplete,
                Rewards.ToArray(),
                Config.ToArray()
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
                        this.onEndComplete.Invoke(future.Result.TransactionId);
                    }

                    this.onError.Invoke(future.Error, Retry);
                    yield break;
                }

                this.onError.Invoke(future.Error, null);
                yield break;
            }
            this.onEndComplete.Invoke(future.Result.TransactionId);
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

    public partial class Gs2QuestProgressEndAction
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2QuestOwnProgressContext _context;

        public void Awake()
        {
            this._clientHolder = Gs2ClientHolder.Instance;
            this._gameSessionHolder = Gs2GameSessionHolder.Instance;
            this._context = GetComponent<Gs2QuestOwnProgressContext>() ?? GetComponentInParent<Gs2QuestOwnProgressContext>();

            if (_context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2QuestOwnProgressContext.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2QuestProgressEndAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2QuestProgressEndAction
    {
        public List<Gs2.Unity.Gs2Quest.Model.EzReward> Rewards;
        public bool IsComplete;
        public List<Gs2.Unity.Gs2Quest.Model.EzConfig> Config;

        public void SetRewards(List<Gs2.Unity.Gs2Quest.Model.EzReward> value) {
            Rewards = value;
            this.onChangeRewards.Invoke(Rewards);
        }

        public void SetIsComplete(bool value) {
            IsComplete = value;
            this.onChangeIsComplete.Invoke(IsComplete);
        }

        public void SetConfig(List<Gs2.Unity.Gs2Quest.Model.EzConfig> value) {
            Config = value;
            this.onChangeConfig.Invoke(Config);
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2QuestProgressEndAction
    {

        [Serializable]
        private class ChangeRewardsEvent : UnityEvent<List<Gs2.Unity.Gs2Quest.Model.EzReward>>
        {

        }

        [SerializeField]
        private ChangeRewardsEvent onChangeRewards = new ChangeRewardsEvent();
        public event UnityAction<List<Gs2.Unity.Gs2Quest.Model.EzReward>> OnChangeRewards
        {
            add => this.onChangeRewards.AddListener(value);
            remove => this.onChangeRewards.RemoveListener(value);
        }

        [Serializable]
        private class ChangeIsCompleteEvent : UnityEvent<bool>
        {

        }

        [SerializeField]
        private ChangeIsCompleteEvent onChangeIsComplete = new ChangeIsCompleteEvent();
        public event UnityAction<bool> OnChangeIsComplete
        {
            add => this.onChangeIsComplete.AddListener(value);
            remove => this.onChangeIsComplete.RemoveListener(value);
        }

        [Serializable]
        private class ChangeConfigEvent : UnityEvent<List<Gs2.Unity.Gs2Quest.Model.EzConfig>>
        {

        }

        [SerializeField]
        private ChangeConfigEvent onChangeConfig = new ChangeConfigEvent();
        public event UnityAction<List<Gs2.Unity.Gs2Quest.Model.EzConfig>> OnChangeConfig
        {
            add => this.onChangeConfig.AddListener(value);
            remove => this.onChangeConfig.RemoveListener(value);
        }

        [Serializable]
        private class EndCompleteEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private EndCompleteEvent onEndComplete = new EndCompleteEvent();
        public event UnityAction<string> OnEndComplete
        {
            add => this.onEndComplete.AddListener(value);
            remove => this.onEndComplete.RemoveListener(value);
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
    public partial class Gs2QuestProgressEndAction
    {
        [MenuItem("GameObject/Game Server Services/Quest/Progress/Action/End", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2QuestProgressEndAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Quest/Prefabs/Action/Gs2QuestProgressEndAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}