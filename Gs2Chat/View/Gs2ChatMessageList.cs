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

    [AddComponentMenu("GS2 UIKit/Chat/Message/View/Gs2ChatMessageList")]
    public partial class Gs2ChatMessageList : MonoBehaviour
    {
        private List<Gs2ChatMessageContext> _children;

        public void Update() {
            if (_fetcher.Fetched && this._fetcher.Messages != null) {
                for (var i = 0; i < this.maximumItems; i++) {
                    if (i < this._fetcher.Messages.Count) {
                        _children[i].Message.messageName = this._fetcher.Messages[i].Name;
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

    public partial class Gs2ChatMessageList
    {
        private Gs2ChatRoomContext _context;
        private Gs2ChatMessageListFetcher _fetcher;

        public void Awake()
        {
            _context = GetComponentInParent<Gs2ChatRoomContext>();
            _fetcher = GetComponentInParent<Gs2ChatMessageListFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ChatMessageListFetcher.");
                enabled = false;
            }

            _children = new List<Gs2ChatMessageContext>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.Message = Message.New(
                    _context.Room,
                    ""
                );
                node.gameObject.SetActive(false);
                _children.Add(node);
            }
            this.prefab.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2ChatMessageList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2ChatMessageList
    {
        public Gs2ChatMessageContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ChatMessageList
    {

    }
}