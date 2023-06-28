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

using System;
using Gs2.Gs2Inbox.Request;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Inbox.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Inbox.Label
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Inbox/Message/View/Label/Transaction/Gs2InboxSendMessageByUserIdLabel")]
    public partial class Gs2InboxSendMessageByUserIdLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Request != null &&
                    _userDataFetcher != null && _userDataFetcher.Fetched && _userDataFetcher.Message != null) {
                {
                    onUpdate?.Invoke(
                        format.Replace(
                            "{namespaceName}",
                            $"{_fetcher.Request.NamespaceName}"
                        ).Replace(
                            "{userId}",
                            $"{_fetcher.Request.UserId}"
                        ).Replace(
                            "{metadata}",
                            $"{_fetcher.Request.Metadata}"
                        ).Replace(
                            "{readAcquireActions}",
                            $"{_fetcher.Request.ReadAcquireActions}"
                        ).Replace(
                            "{expiresAt}",
                            $"{_fetcher.Request.ExpiresAt}"
                        ).Replace(
                            "{expiresTimeSpan}",
                            $"{_fetcher.Request.ExpiresTimeSpan}"
                        ).Replace(
                            "{userData:messageId}",
                            $"{_userDataFetcher.Message.MessageId}"
                        ).Replace(
                            "{userData:name}",
                            $"{_userDataFetcher.Message.Name}"
                        ).Replace(
                            "{userData:metadata}",
                            $"{_userDataFetcher.Message.Metadata}"
                        ).Replace(
                            "{userData:isRead}",
                            $"{_userDataFetcher.Message.IsRead}"
                        ).Replace(
                            "{userData:readAcquireActions}",
                            $"{_userDataFetcher.Message.ReadAcquireActions}"
                        ).Replace(
                            "{userData:receivedAt}",
                            $"{_userDataFetcher.Message.ReceivedAt}"
                        ).Replace(
                            "{userData:readAt}",
                            $"{_userDataFetcher.Message.ReadAt}"
                        ).Replace(
                            "{userData:expiresAt}",
                            $"{_userDataFetcher.Message.ExpiresAt}"
                        )
                    );
                }
            } else if (_fetcher.Fetched && _fetcher.Request != null) {
                {
                    onUpdate?.Invoke(
                        format.Replace(
                            "{namespaceName}",
                            $"{_fetcher.Request.NamespaceName}"
                        ).Replace(
                            "{userId}",
                            $"{_fetcher.Request.UserId}"
                        ).Replace(
                            "{metadata}",
                            $"{_fetcher.Request.Metadata}"
                        ).Replace(
                            "{readAcquireActions}",
                            $"{_fetcher.Request.ReadAcquireActions}"
                        ).Replace(
                            "{expiresAt}",
                            $"{_fetcher.Request.ExpiresAt}"
                        ).Replace(
                            "{expiresTimeSpan}",
                            $"{_fetcher.Request.ExpiresTimeSpan}"
                        )
                    );
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2InboxSendMessageByUserIdLabel
    {
        private Gs2InboxSendMessageByUserIdFetcher _fetcher;
        private Gs2InboxOwnMessageFetcher _userDataFetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2InboxSendMessageByUserIdFetcher>() ?? GetComponentInParent<Gs2InboxSendMessageByUserIdFetcher>();
            _userDataFetcher = GetComponent<Gs2InboxOwnMessageFetcher>() ?? GetComponentInParent<Gs2InboxOwnMessageFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2InboxSendMessageByUserIdFetcher.");
                enabled = false;
            }

            Update();
        }

        public bool HasError()
        {
            _fetcher = GetComponent<Gs2InboxSendMessageByUserIdFetcher>() ?? GetComponentInParent<Gs2InboxSendMessageByUserIdFetcher>(true);
            _userDataFetcher = GetComponent<Gs2InboxOwnMessageFetcher>() ?? GetComponentInParent<Gs2InboxOwnMessageFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2InboxSendMessageByUserIdLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2InboxSendMessageByUserIdLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2InboxSendMessageByUserIdLabel
    {
        [Serializable]
        private class UpdateEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private UpdateEvent onUpdate = new UpdateEvent();

        public event UnityAction<string> OnUpdate
        {
            add => onUpdate.AddListener(value);
            remove => onUpdate.RemoveListener(value);
        }
    }
}