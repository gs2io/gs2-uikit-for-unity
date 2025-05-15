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
using Gs2.Unity.Gs2Dictionary.Model;
using Gs2.Unity.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Dictionary.Context;
using UnityEngine;
using UnityEngine.Events;
using Like = Gs2.Unity.Gs2Dictionary.ScriptableObject.OwnLike;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gs2.Unity.UiKit.Gs2Dictionary
{
    public partial class Gs2DictionaryLikeAddLikesAction : MonoBehaviour
    {
        private IEnumerator Process()
        {
            var clientHolder = Gs2ClientHolder.Instance;
            var gameSessionHolder = Gs2GameSessionHolder.Instance;

            yield return new WaitUntil(() => clientHolder.Initialized);
            yield return new WaitUntil(() => gameSessionHolder.Initialized);

            this.onAddLikesStart.Invoke();

            
            var domain = clientHolder.Gs2.Dictionary.Namespace(
                this._context.Like.NamespaceName
            ).Me(
                gameSessionHolder.GameSession
            );
            var future = domain.AddLikesFuture(
                EntryModelNames.ToArray()
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
                        var items = new List<EzLike>();
                        foreach (var d in future.Result) {
                            var future3 = d.ModelFuture();
                            yield return future3;
                            if (future3.Error != null) {
                                this.onError.Invoke(future3.Error, null);
                                yield break;
                            }
                            items.Add(future3.Result);
                        }
                        this.onAddLikesComplete.Invoke(items);
                    }

                    this.onError.Invoke(future.Error, Retry);
                    yield break;
                }

                this.onError.Invoke(future.Error, null);
                yield break;
            }
            var items = new List<EzLike>();
            foreach (var d in future.Result) {
                var future3 = d.ModelFuture();
                yield return future3;
                if (future3.Error != null) {
                    this.onError.Invoke(future3.Error, null);
                    yield break;
                }
                items.Add(future3.Result);
            }
            this.onAddLikesComplete.Invoke(items);
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

    public partial class Gs2DictionaryLikeAddLikesAction
    {
        private Gs2DictionaryOwnLikeContext _context;

        public void Awake()
        {
            this._context = GetComponent<Gs2DictionaryOwnLikeContext>() ?? GetComponentInParent<Gs2DictionaryOwnLikeContext>();
            if (this._context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2DictionaryOwnLikeContext.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._context = GetComponent<Gs2DictionaryOwnLikeContext>() ?? GetComponentInParent<Gs2DictionaryOwnLikeContext>(true);
            if (this._context == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2DictionaryLikeAddLikesAction
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    public partial class Gs2DictionaryLikeAddLikesAction
    {
        public bool WaitAsyncProcessComplete;
        public List<string> EntryModelNames;

        public void SetEntryModelNames(List<string> value) {
            this.EntryModelNames = value;
            this.onChangeEntryModelNames.Invoke(this.EntryModelNames);
            this.OnChange.Invoke();
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2DictionaryLikeAddLikesAction
    {

        [Serializable]
        private class ChangeEntryModelNamesEvent : UnityEvent<List<string>>
        {

        }

        [SerializeField]
        private ChangeEntryModelNamesEvent onChangeEntryModelNames = new ChangeEntryModelNamesEvent();
        public event UnityAction<List<string>> OnChangeEntryModelNames
        {
            add => this.onChangeEntryModelNames.AddListener(value);
            remove => this.onChangeEntryModelNames.RemoveListener(value);
        }

        [Serializable]
        private class AddLikesStartEvent : UnityEvent
        {

        }

        [SerializeField]
        private AddLikesStartEvent onAddLikesStart = new AddLikesStartEvent();

        public event UnityAction OnAddLikesStart
        {
            add => this.onAddLikesStart.AddListener(value);
            remove => this.onAddLikesStart.RemoveListener(value);
        }

        [Serializable]
        private class AddLikesCompleteEvent : UnityEvent<List<EzLike>>
        {

        }

        [SerializeField]
        private AddLikesCompleteEvent onAddLikesComplete = new AddLikesCompleteEvent();
        public event UnityAction<List<EzLike>> OnAddLikesComplete
        {
            add => this.onAddLikesComplete.AddListener(value);
            remove => this.onAddLikesComplete.RemoveListener(value);
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
    public partial class Gs2DictionaryLikeAddLikesAction
    {
        [MenuItem("GameObject/Game Server Services/Dictionary/Like/Action/AddLikes", priority = 0)]
        private static void CreateButton()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<Gs2DictionaryLikeAddLikesAction>(
                "Packages/io.gs2.unity.sdk.uikit/Gs2Dictionary/Prefabs/Action/Gs2DictionaryLikeAddLikesAction.prefab"
            );

            var instance = PrefabUtility.InstantiatePrefab(prefab, Selection.activeTransform);

            Undo.RegisterCreatedObjectUndo(instance, $"Create {instance.name}");
            Selection.activeObject = instance;
        }
    }
#endif
}