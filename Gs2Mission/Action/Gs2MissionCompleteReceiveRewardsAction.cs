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
using Gs2.Unity.Gs2Mission.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Mission.Context;
using UnityEngine;
using UnityEngine.Events;
using Complete = Gs2.Unity.Gs2Mission.ScriptableObject.OwnComplete;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Mission
{
	[AddComponentMenu("GS2 UIKit/Mission/Complete/Action/Gs2MissionCompleteReceiveRewardsAction")]
    public partial class Gs2MissionCompleteReceiveRewardsAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            yield return new WaitUntil(() => this._clientHolder.Initialized);
            yield return new WaitUntil(() => this._gameSessionHolder.Initialized);
            
            var domain = this._clientHolder.Gs2.Mission.Namespace(
                this._context.Complete.NamespaceName
            ).Me(
                this._gameSessionHolder.GameSession
            ).Complete(
                this._context.Complete.MissionGroupName
            );
            var future = domain.ReceiveRewards(
                MissionTaskName
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
                        this.onReceiveRewardsComplete.Invoke(future.Result.TransactionId);
                    }

                    this.onError.Invoke(future.Error, Retry);
                    yield break;
                }

                this.onError.Invoke(future.Error, null);
                yield break;
            }
            this.onReceiveRewardsComplete.Invoke(future.Result.TransactionId);
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

    public partial class Gs2MissionCompleteReceiveRewardsAction
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2MissionOwnCompleteContext _context;

        public void Awake()
        {
            this._clientHolder = Gs2ClientHolder.Instance;
            this._gameSessionHolder = Gs2GameSessionHolder.Instance;
            this._context = GetComponentInParent<Gs2MissionOwnCompleteContext>();

            if (_context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MissionOwnCompleteContext.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2MissionCompleteReceiveRewardsAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2MissionCompleteReceiveRewardsAction
    {
        public string MissionTaskName;

        public void SetMissionTaskName(string value) {
            MissionTaskName = value;
            this.onChangeMissionTaskName.Invoke(MissionTaskName);
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MissionCompleteReceiveRewardsAction
    {

        [Serializable]
        private class ChangeMissionTaskNameEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ChangeMissionTaskNameEvent onChangeMissionTaskName = new ChangeMissionTaskNameEvent();
        public event UnityAction<string> OnChangeMissionTaskName
        {
            add => this.onChangeMissionTaskName.AddListener(value);
            remove => this.onChangeMissionTaskName.RemoveListener(value);
        }

        [Serializable]
        private class ReceiveRewardsCompleteEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ReceiveRewardsCompleteEvent onReceiveRewardsComplete = new ReceiveRewardsCompleteEvent();
        public event UnityAction<string> OnReceiveRewardsComplete
        {
            add => this.onReceiveRewardsComplete.AddListener(value);
            remove => this.onReceiveRewardsComplete.RemoveListener(value);
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
    public partial class Gs2MissionCompleteReceiveRewardsAction
    {
        [MenuItem("GameObject/Game Server Services/Mission/Complete/Action/ReceiveRewards", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2MissionCompleteReceiveRewardsAction>(
                "Assets/Scripts/Runtime/Sdk/Gs2/UiKit/Gs2Mission/Prefabs/Action/Gs2MissionCompleteReceiveRewardsAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}