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
using Gs2.Unity.Gs2SkillTree.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2SkillTree.Context;
using UnityEngine;
using UnityEngine.Events;
using Status = Gs2.Unity.Gs2SkillTree.ScriptableObject.OwnStatus;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2SkillTree
{
    public partial class Gs2SkillTreeStatusReleaseAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);

            this.onReleaseStart.Invoke();

            
            var domain = clientHolder.Gs2.SkillTree.Namespace(
                this._context.Status.NamespaceName
            ).Me(
                gameSessionHolder.GameSession
            ).Status(
            );
            var future = domain.ReleaseFuture(
                NodeModelNames.ToArray()
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
                        this.onReleaseComplete.Invoke(future.Result?.TransactionId);
                    }

                    this.onError.Invoke(future.Error, Retry);
                    yield break;
                }

                this.onError.Invoke(future.Error, null);
                yield break;
            }
            if (this.WaitAsyncProcessComplete && future.Result != null) {
                var transaction = future.Result;
                var future2 = transaction.WaitFuture();
                yield return future2;
            }
            this.onReleaseComplete.Invoke(future.Result?.TransactionId);
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

    public partial class Gs2SkillTreeStatusReleaseAction
    {
        private Gs2SkillTreeOwnStatusContext _context;

        public void Awake()
        {
            this._context = GetComponent<Gs2SkillTreeOwnStatusContext>() ?? GetComponentInParent<Gs2SkillTreeOwnStatusContext>();
            if (this._context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2SkillTreeOwnStatusContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._context = GetComponent<Gs2SkillTreeOwnStatusContext>() ?? GetComponentInParent<Gs2SkillTreeOwnStatusContext>(true);
            if (this._context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2SkillTreeStatusReleaseAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2SkillTreeStatusReleaseAction
    {
        public bool WaitAsyncProcessComplete;
        public List<string> NodeModelNames;

        public void SetNodeModelNames(List<string> value) {
            this.NodeModelNames = value;
            this.onChangeNodeModelNames.Invoke(this.NodeModelNames);
            this.OnChange.Invoke();
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2SkillTreeStatusReleaseAction
    {

        [Serializable]
        private class ChangeNodeModelNamesEvent : UnityEvent<List<string>>
        {

        }

        [SerializeField]
        private ChangeNodeModelNamesEvent onChangeNodeModelNames = new ChangeNodeModelNamesEvent();
        public event UnityAction<List<string>> OnChangeNodeModelNames
        {
            add => this.onChangeNodeModelNames.AddListener(value);
            remove => this.onChangeNodeModelNames.RemoveListener(value);
        }

        [Serializable]
        private class ReleaseStartEvent : UnityEvent
        {

        }

        [SerializeField]
        private ReleaseStartEvent onReleaseStart = new ReleaseStartEvent();

        public event UnityAction OnReleaseStart
        {
            add => this.onReleaseStart.AddListener(value);
            remove => this.onReleaseStart.RemoveListener(value);
        }

        [Serializable]
        private class ReleaseCompleteEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private ReleaseCompleteEvent onReleaseComplete = new ReleaseCompleteEvent();
        public event UnityAction<string> OnReleaseComplete
        {
            add => this.onReleaseComplete.AddListener(value);
            remove => this.onReleaseComplete.RemoveListener(value);
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
    public partial class Gs2SkillTreeStatusReleaseAction
    {
        [MenuItem("GameObject/Game Server Services/SkillTree/Status/Action/Release", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2SkillTreeStatusReleaseAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2SkillTree/Prefabs/Action/Gs2SkillTreeStatusReleaseAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}