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
using Gs2.Unity.Gs2Limit.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Limit.Context;
using UnityEngine;
using UnityEngine.Events;
using Counter = Gs2.Unity.Gs2Limit.ScriptableObject.OwnCounter;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Limit
{
    public partial class Gs2LimitCounterCountUpAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            yield return new WaitUntil(() => this._clientHolder.Initialized);
            yield return new WaitUntil(() => this._gameSessionHolder.Initialized);
            
            var domain = this._clientHolder.Gs2.Limit.Namespace(
                this._context.Counter.NamespaceName
            ).Me(
                this._gameSessionHolder.GameSession
            ).Counter(
                this._context.Counter.LimitName,
                this._context.Counter.CounterName
            );
            var future = domain.CountUp(
                CountUpValue,
                MaxValue
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

                        this.onCountUpComplete.Invoke(future3.Result);
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

            this.onCountUpComplete.Invoke(future2.Result);
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

    public partial class Gs2LimitCounterCountUpAction
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2LimitOwnCounterContext _context;

        public void Awake()
        {
            this._clientHolder = Gs2ClientHolder.Instance;
            this._gameSessionHolder = Gs2GameSessionHolder.Instance;
            this._context = GetComponent<Gs2LimitOwnCounterContext>() ?? GetComponentInParent<Gs2LimitOwnCounterContext>();

            if (_context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2LimitOwnCounterContext.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2LimitCounterCountUpAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2LimitCounterCountUpAction
    {
        public int CountUpValue;
        public int MaxValue;

        public void SetCountUpValue(int value) {
            CountUpValue = value;
            this.onChangeCountUpValue.Invoke(CountUpValue);
        }

        public void DecreaseCountUpValue() {
            CountUpValue -= 1;
            this.onChangeCountUpValue.Invoke(CountUpValue);
        }

        public void IncreaseCountUpValue() {
            CountUpValue += 1;
            this.onChangeCountUpValue.Invoke(CountUpValue);
        }

        public void SetMaxValue(int value) {
            MaxValue = value;
            this.onChangeMaxValue.Invoke(MaxValue);
        }

        public void DecreaseMaxValue() {
            MaxValue -= 1;
            this.onChangeMaxValue.Invoke(MaxValue);
        }

        public void IncreaseMaxValue() {
            MaxValue += 1;
            this.onChangeMaxValue.Invoke(MaxValue);
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2LimitCounterCountUpAction
    {

        [Serializable]
        private class ChangeCountUpValueEvent : UnityEvent<int>
        {

        }

        [SerializeField]
        private ChangeCountUpValueEvent onChangeCountUpValue = new ChangeCountUpValueEvent();
        public event UnityAction<int> OnChangeCountUpValue
        {
            add => this.onChangeCountUpValue.AddListener(value);
            remove => this.onChangeCountUpValue.RemoveListener(value);
        }

        [Serializable]
        private class ChangeMaxValueEvent : UnityEvent<int>
        {

        }

        [SerializeField]
        private ChangeMaxValueEvent onChangeMaxValue = new ChangeMaxValueEvent();
        public event UnityAction<int> OnChangeMaxValue
        {
            add => this.onChangeMaxValue.AddListener(value);
            remove => this.onChangeMaxValue.RemoveListener(value);
        }

        [Serializable]
        private class CountUpCompleteEvent : UnityEvent<EzCounter>
        {

        }

        [SerializeField]
        private CountUpCompleteEvent onCountUpComplete = new CountUpCompleteEvent();
        public event UnityAction<EzCounter> OnCountUpComplete
        {
            add => this.onCountUpComplete.AddListener(value);
            remove => this.onCountUpComplete.RemoveListener(value);
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
    public partial class Gs2LimitCounterCountUpAction
    {
        [MenuItem("GameObject/Game Server Services/Limit/Counter/Action/CountUp", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2LimitCounterCountUpAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Limit/Prefabs/Action/Gs2LimitCounterCountUpAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}