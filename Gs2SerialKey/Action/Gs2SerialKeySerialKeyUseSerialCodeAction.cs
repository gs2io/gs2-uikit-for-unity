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
using Gs2.Unity.Gs2SerialKey.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2SerialKey.Context;
using UnityEngine;
using UnityEngine.Events;
using SerialKey = Gs2.Unity.Gs2SerialKey.ScriptableObject.SerialKey;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2SerialKey
{
	[AddComponentMenu("GS2 UIKit/SerialKey/SerialKey/Action/Gs2SerialKeySerialKeyUseSerialCodeAction")]
    public partial class Gs2SerialKeySerialKeyUseSerialCodeAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            yield return new WaitUntil(() => this._clientHolder.Initialized);
            yield return new WaitUntil(() => this._gameSessionHolder.Initialized);
            
            var domain = this._clientHolder.Gs2.SerialKey.Namespace(
                this._context.SerialKey.NamespaceName
            ).Me(
                this._gameSessionHolder.GameSession
            ).SerialKey(
                this._context.SerialKey.SerialKeyCode
            );
            var future = domain.UseSerialCode(
                Code
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

                        this.onUseSerialCodeComplete.Invoke(future3.Result);
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

            this.onUseSerialCodeComplete.Invoke(future2.Result);
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

    public partial class Gs2SerialKeySerialKeyUseSerialCodeAction
    {
        private Gs2ClientHolder _clientHolder;
        private Gs2GameSessionHolder _gameSessionHolder;
        private Gs2SerialKeySerialKeyContext _context;

        public void Awake()
        {
            this._clientHolder = Gs2ClientHolder.Instance;
            this._gameSessionHolder = Gs2GameSessionHolder.Instance;
            this._context = GetComponentInParent<Gs2SerialKeySerialKeyContext>();

            if (_context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2SerialKeySerialKeyContext.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2SerialKeySerialKeyUseSerialCodeAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2SerialKeySerialKeyUseSerialCodeAction
    {
        public string Code;

        public void SetCode(string value) {
            Code = value;
            this.onChangeCode.Invoke(Code);
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2SerialKeySerialKeyUseSerialCodeAction
    {

        [Serializable]
        private class ChangeCodeEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ChangeCodeEvent onChangeCode = new ChangeCodeEvent();
        public event UnityAction<string> OnChangeCode
        {
            add => this.onChangeCode.AddListener(value);
            remove => this.onChangeCode.RemoveListener(value);
        }

        [Serializable]
        private class UseSerialCodeCompleteEvent : UnityEvent<EzSerialKey>
        {

        }

        [SerializeField]
        private UseSerialCodeCompleteEvent onUseSerialCodeComplete = new UseSerialCodeCompleteEvent();
        public event UnityAction<EzSerialKey> OnUseSerialCodeComplete
        {
            add => this.onUseSerialCodeComplete.AddListener(value);
            remove => this.onUseSerialCodeComplete.RemoveListener(value);
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
    public partial class Gs2SerialKeySerialKeyUseSerialCodeAction
    {
        [MenuItem("GameObject/Game Server Services/SerialKey/SerialKey/Action/UseSerialCode", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2SerialKeySerialKeyUseSerialCodeAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2SerialKey/Prefabs/Action/Gs2SerialKeySerialKeyUseSerialCodeAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}