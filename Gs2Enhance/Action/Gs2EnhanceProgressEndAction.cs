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
using Gs2.Unity.Gs2Enhance.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Enhance.Context;
using UnityEngine;
using UnityEngine.Events;
using Progress = Gs2.Unity.Gs2Enhance.ScriptableObject.OwnProgress;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Enhance
{
	[AddComponentMenu("GS2 UIKit/Enhance/Progress/Action/Gs2EnhanceProgressEndAction")]
    public partial class Gs2EnhanceProgressEndAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            yield return new WaitUntil(() => this._clientHolder.Initialized);
            yield return new WaitUntil(() => this._gameSessionHolder.Initialized);
            
            var domain = this._clientHolder.Gs2.Enhance.Namespace(
                this._context.Progress.NamespaceName
            ).Me(
                this._gameSessionHolder.GameSession
            ).Progress(
            );
            var future = domain.End(
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

    public partial class Gs2EnhanceProgressEndAction
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2EnhanceOwnProgressContext _context;

        public void Awake()
        {
            this._clientHolder = Gs2ClientHolder.Instance;
            this._gameSessionHolder = Gs2GameSessionHolder.Instance;
            this._context = GetComponent<Gs2EnhanceOwnProgressContext>() ?? GetComponentInParent<Gs2EnhanceOwnProgressContext>();

            if (_context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2EnhanceOwnProgressContext.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2EnhanceProgressEndAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2EnhanceProgressEndAction
    {
        public List<Gs2.Unity.Gs2Enhance.Model.EzConfig> Config;

        public void SetConfig(List<Gs2.Unity.Gs2Enhance.Model.EzConfig> value) {
            Config = value;
            this.onChangeConfig.Invoke(Config);
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2EnhanceProgressEndAction
    {

        [Serializable]
        private class ChangeConfigEvent : UnityEvent<List<Gs2.Unity.Gs2Enhance.Model.EzConfig>>
        {

        }

        [SerializeField]
        private ChangeConfigEvent onChangeConfig = new ChangeConfigEvent();
        public event UnityAction<List<Gs2.Unity.Gs2Enhance.Model.EzConfig>> OnChangeConfig
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
    public partial class Gs2EnhanceProgressEndAction
    {
        [MenuItem("GameObject/Game Server Services/Enhance/Progress/Action/End", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2EnhanceProgressEndAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Enhance/Prefabs/Action/Gs2EnhanceProgressEndAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}