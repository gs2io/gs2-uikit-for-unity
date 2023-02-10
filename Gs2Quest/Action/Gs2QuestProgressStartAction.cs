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
using Gs2.Unity.UiKit.Gs2Quest.Context;
using UnityEngine;
using UnityEngine.Events;
using Progress = Gs2.Unity.Gs2Quest.ScriptableObject.Progress;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Quest
{
	[AddComponentMenu("GS2 UIKit/Quest/Progress/Action/Gs2QuestProgressStartAction")]
    public partial class Gs2QuestProgressStartAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            yield return new WaitUntil(() => this._clientHolder.Initialized);
            yield return new WaitUntil(() => this._gameSessionHolder.Initialized);
            
            var domain = this._clientHolder.Gs2.Quest.Namespace(
                this._context.Progress.NamespaceName
            ).Me(
                this._gameSessionHolder.GameSession
            );
            var future = domain.Start(
                QuestGroupName,
                QuestName,
                Force,
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
                        this.onStartComplete.Invoke(future.Result.TransactionId);
                    }

                    this.onError.Invoke(future.Error, Retry);
                    yield break;
                }

                this.onError.Invoke(future.Error, null);
                yield break;
            }
            this.onStartComplete.Invoke(future.Result.TransactionId);
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

    public partial class Gs2QuestProgressStartAction
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2QuestProgressContext _context;

        public void Awake()
        {
            this._clientHolder = Gs2ClientHolder.Instance;
            this._gameSessionHolder = Gs2GameSessionHolder.Instance;
            this._context = GetComponentInParent<Gs2QuestProgressContext>();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2QuestProgressStartAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2QuestProgressStartAction
    {
        public string QuestGroupName;
        public string QuestName;
        public bool Force;
        public List<Gs2.Unity.Gs2Quest.Model.EzConfig> Config;

        public void SetQuestGroupName(string value) {
            QuestGroupName = value;
        }

        public void SetQuestName(string value) {
            QuestName = value;
        }

        public void SetForce(bool value) {
            Force = value;
        }

        public void SetConfig(List<Gs2.Unity.Gs2Quest.Model.EzConfig> value) {
            Config = value;
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2QuestProgressStartAction
    {
        [Serializable]
        private class StartCompleteEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private StartCompleteEvent onStartComplete = new StartCompleteEvent();
        public event UnityAction<string> OnStartComplete
        {
            add => this.onStartComplete.AddListener(value);
            remove => this.onStartComplete.RemoveListener(value);
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
    public partial class Gs2QuestProgressStartAction
    {
        [MenuItem("GameObject/Game Server Services/Quest/Progress/Action/Start", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2QuestProgressStartAction>(
                "Assets/Scripts/Runtime/Sdk/Gs2/UiKit/Gs2Quest/Prefabs/Action/Gs2QuestProgressStartAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}