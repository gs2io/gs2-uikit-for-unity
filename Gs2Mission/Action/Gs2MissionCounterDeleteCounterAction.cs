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
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantAssignment
// ReSharper disable NotAccessedVariable
// ReSharper disable RedundantUsingDirective
// ReSharper disable Unity.NoNullPropagation
// ReSharper disable InconsistentNaming

#pragma warning disable CS0472

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
using Counter = Gs2.Unity.Gs2Mission.ScriptableObject.OwnCounter;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Mission
{
    public partial class Gs2MissionCounterDeleteCounterAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);

            this.onDeleteCounterStart.Invoke();

            
            var domain = clientHolder.Gs2.Mission.Namespace(
                this._context.Counter.NamespaceName
            ).Me(
                gameSessionHolder.GameSession
            ).Counter(
                this._context.Counter.CounterName
            );
            var future = domain.DeleteCounterFuture(
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
                        var future3 = future.Result.ModelFuture();
                        yield return future3;
                        if (future3.Error != null)
                        {
                            this.onError.Invoke(future3.Error, null);
                            yield break;
                        }

                        this.onDeleteCounterComplete.Invoke(future3.Result);
                    }

                    this.onError.Invoke(future.Error, Retry);
                    yield break;
                }

                this.onError.Invoke(future.Error, null);
                yield break;
            }
            var future2 = future.Result.ModelFuture();
            yield return future2;
            if (future2.Error != null)
            {
                this.onError.Invoke(future2.Error, null);
                yield break;
            }

            this.onDeleteCounterComplete.Invoke(future2.Result);
        }

        public void OnEnable()
        {
            Gs2ClientHolder.Instance.StartCoroutine(Process());
        }

        public void OnDisable()
        {

        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2MissionCounterDeleteCounterAction
    {
        private Gs2MissionOwnCounterContext _context;

        public void Awake()
        {
            this._context = GetComponent<Gs2MissionOwnCounterContext>() ?? GetComponentInParent<Gs2MissionOwnCounterContext>();
            if (this._context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MissionOwnCounterContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._context = GetComponent<Gs2MissionOwnCounterContext>() ?? GetComponentInParent<Gs2MissionOwnCounterContext>(true);
            if (this._context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2MissionCounterDeleteCounterAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2MissionCounterDeleteCounterAction
    {
        public bool WaitAsyncProcessComplete;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MissionCounterDeleteCounterAction
    {

        [Serializable]
        private class DeleteCounterStartEvent : UnityEvent
        {

        }

        [SerializeField]
        private DeleteCounterStartEvent onDeleteCounterStart = new DeleteCounterStartEvent();

        public event UnityAction OnDeleteCounterStart
        {
            add => this.onDeleteCounterStart.AddListener(value);
            remove => this.onDeleteCounterStart.RemoveListener(value);
        }

        [Serializable]
        private class DeleteCounterCompleteEvent : UnityEvent<EzCounter>
        {

        }

        [SerializeField]
        private DeleteCounterCompleteEvent onDeleteCounterComplete = new DeleteCounterCompleteEvent();
        public event UnityAction<EzCounter> OnDeleteCounterComplete
        {
            add => this.onDeleteCounterComplete.AddListener(value);
            remove => this.onDeleteCounterComplete.RemoveListener(value);
        }

        public UnityEvent OnChange = new UnityEvent();

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
    public partial class Gs2MissionCounterDeleteCounterAction
    {
        [MenuItem("GameObject/Game Server Services/Mission/Counter/Action/DeleteCounter", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2MissionCounterDeleteCounterAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Mission/Prefabs/Action/Gs2MissionCounterDeleteCounterAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}