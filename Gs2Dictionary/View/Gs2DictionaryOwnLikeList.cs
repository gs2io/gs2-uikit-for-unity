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
// ReSharper disable CheckNamespace
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantAssignment
// ReSharper disable NotAccessedVariable
// ReSharper disable RedundantUsingDirective
// ReSharper disable Unity.NoNullPropagation
// ReSharper disable InconsistentNaming

#pragma warning disable CS0472

using System.Collections.Generic;
using Gs2.Unity.Gs2Dictionary.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Dictionary.Context;
using Gs2.Unity.UiKit.Gs2Dictionary.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Dictionary
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Dictionary/Like/View/Gs2DictionaryOwnLikeList")]
    public partial class Gs2DictionaryOwnLikeList : MonoBehaviour
    {
        private List<Gs2DictionaryOwnLikeContext> _children;

        private void OnFetched() {
            for (var i = 0; i < this._children.Count; i++) {
                if (i < this._fetcher.Likes.Count) {
                    this._children[i].SetOwnLike(
                        OwnLike.New(
                            this._fetcher.Context.Namespace,
                            this._fetcher.Likes[i].Name
                        )
                    );
                    this._children[i].gameObject.SetActive(true);
                }
                else {
                    this._children[i].gameObject.SetActive(false);
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2DictionaryOwnLikeList
    {
        private Gs2DictionaryOwnLikeListFetcher _fetcher;

        private void Initialize() {
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.Like = OwnLike.New(
                    this._fetcher.Context.Namespace,
                    ""
                );
                node.gameObject.SetActive(false);
                this._children.Add(node);
            }
        }

        public void Awake()
        {
            if (this.prefab == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2DictionaryOwnLikeContext Prefab.");
                enabled = false;
                return;
            }

            this._fetcher = GetComponent<Gs2DictionaryOwnLikeListFetcher>() ?? GetComponentInParent<Gs2DictionaryOwnLikeListFetcher>();
            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2DictionaryOwnLikeListFetcher.");
                enabled = false;
            }

            this._children = new List<Gs2DictionaryOwnLikeContext>();
            this.prefab.gameObject.SetActive(false);

            Invoke(nameof(Initialize), 0);
        }

        public virtual bool HasError()
        {
            if (this.prefab == null) {
                return true;
            }
            this._fetcher = GetComponent<Gs2DictionaryOwnLikeListFetcher>() ?? GetComponentInParent<Gs2DictionaryOwnLikeListFetcher>(true);
            if (this._fetcher == null) {
                return true;
            }
            return false;
        }

        private UnityAction _onFetched;

        public void OnEnable()
        {
            this._onFetched = () =>
            {
                OnFetched();
            };
            this._fetcher.OnFetched.AddListener(this._onFetched);

            if (this._fetcher.Fetched) {
                OnFetched();
            }
        }

        public void OnDisable()
        {
            if (this._onFetched != null) {
                this._fetcher.OnFetched.RemoveListener(this._onFetched);
                this._onFetched = null;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2DictionaryOwnLikeList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2DictionaryOwnLikeList
    {
        public Gs2DictionaryOwnLikeContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2DictionaryOwnLikeList
    {

    }
}