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
using Gs2.Unity.Gs2Account.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Account.Context;
using UnityEngine;
using UnityEngine.Events;
using PlatformId = Gs2.Unity.Gs2Account.ScriptableObject.OwnPlatformId;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Account
{
    public partial class Gs2AccountPlatformIdFindPlatformUserAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);

            this.onFindPlatformUserStart.Invoke();

            
            var domain = clientHolder.Gs2.Account.Namespace(
                this._context.PlatformId.NamespaceName
            ).Me(
                gameSessionHolder.GameSession
            ).PlatformId(
                this._context.PlatformId.Type
            );
            var future = domain.FindPlatformUserFuture(
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

                        this.onFindPlatformUserComplete.Invoke(future.Result);
                    }

                    this.onError.Invoke(future.Error, Retry);
                    yield break;
                }

                this.onError.Invoke(future.Error, null);
                yield break;
            }

            this.onFindPlatformUserComplete.Invoke(future.Result);
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

    public partial class Gs2AccountPlatformIdFindPlatformUserAction
    {
        private Gs2AccountOwnPlatformIdContext _context;

        public void Awake()
        {
            this._context = GetComponent<Gs2AccountOwnPlatformIdContext>() ?? GetComponentInParent<Gs2AccountOwnPlatformIdContext>();
            if (this._context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2AccountOwnPlatformIdContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._context = GetComponent<Gs2AccountOwnPlatformIdContext>() ?? GetComponentInParent<Gs2AccountOwnPlatformIdContext>(true);
            if (this._context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2AccountPlatformIdFindPlatformUserAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2AccountPlatformIdFindPlatformUserAction
    {
        public bool WaitAsyncProcessComplete;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2AccountPlatformIdFindPlatformUserAction
    {

        [Serializable]
        private class FindPlatformUserStartEvent : UnityEvent
        {

        }

        [SerializeField]
        private FindPlatformUserStartEvent onFindPlatformUserStart = new FindPlatformUserStartEvent();

        public event UnityAction OnFindPlatformUserStart
        {
            add => this.onFindPlatformUserStart.AddListener(value);
            remove => this.onFindPlatformUserStart.RemoveListener(value);
        }

        [Serializable]
        private class FindPlatformUserCompleteEvent : UnityEvent<EzPlatformUser>
        {

        }

        [SerializeField]
        private FindPlatformUserCompleteEvent onFindPlatformUserComplete = new FindPlatformUserCompleteEvent();
        public event UnityAction<EzPlatformUser> OnFindPlatformUserComplete
        {
            add => this.onFindPlatformUserComplete.AddListener(value);
            remove => this.onFindPlatformUserComplete.RemoveListener(value);
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
    public partial class Gs2AccountPlatformIdFindPlatformUserAction
    {
        [MenuItem("GameObject/Game Server Services/Account/PlatformId/Action/FindPlatformUser", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2AccountPlatformIdFindPlatformUserAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Account/Prefabs/Action/Gs2AccountPlatformIdFindPlatformUserAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}