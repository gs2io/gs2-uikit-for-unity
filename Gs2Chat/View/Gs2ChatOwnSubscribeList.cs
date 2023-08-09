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
using Gs2.Unity.Gs2Chat.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Chat.Context;
using Gs2.Unity.UiKit.Gs2Chat.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Chat
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Chat/Subscribe/View/Gs2ChatOwnSubscribeList")]
    public partial class Gs2ChatOwnSubscribeList : MonoBehaviour
    {
        private List<Gs2ChatOwnSubscribeContext> _children;

        public void Update() {
            if (_fetcher.Fetched && this._fetcher.Subscribes != null) {
                for (var i = 0; i < this.maximumItems; i++) {
                    if (i < this._fetcher.Subscribes.Count) {
                        _children[i].SetOwnSubscribe(
                            OwnSubscribe.New(
                                this._fetcher.Context.Namespace,
                                this._fetcher.Subscribes[i].RoomName
                            )
                        );
                        _children[i].gameObject.SetActive(true);
                    }
                    else {
                        _children[i].gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2ChatOwnSubscribeList
    {
        private Gs2ChatOwnSubscribeListFetcher _fetcher;
        private Gs2ChatNamespaceContext Context => _fetcher.Context;

        public void Awake()
        {
            if (prefab == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ChatOwnSubscribeContext Prefab.");
                enabled = false;
                return;
            }

            _fetcher = GetComponent<Gs2ChatOwnSubscribeListFetcher>() ?? GetComponentInParent<Gs2ChatOwnSubscribeListFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ChatOwnSubscribeListFetcher.");
                enabled = false;
            }

            var context = GetComponent<Gs2ChatNamespaceContext>() ?? GetComponentInParent<Gs2ChatNamespaceContext>(true);
            if (context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ChatOwnSubscribeListFetcher::Context.");
                enabled = false;
                return;
            }

            _children = new List<Gs2ChatOwnSubscribeContext>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.Subscribe = OwnSubscribe.New(
                    context.Namespace,
                    ""
                );
                node.gameObject.SetActive(false);
                _children.Add(node);
            }
            this.prefab.gameObject.SetActive(false);
        }

        public virtual bool HasError()
        {
            _fetcher = GetComponent<Gs2ChatOwnSubscribeListFetcher>() ?? GetComponentInParent<Gs2ChatOwnSubscribeListFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2ChatOwnSubscribeList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2ChatOwnSubscribeList
    {
        public Gs2ChatOwnSubscribeContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ChatOwnSubscribeList
    {

    }
}