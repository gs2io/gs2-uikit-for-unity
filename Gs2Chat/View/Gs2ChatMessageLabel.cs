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

using System;
using Gs2.Core.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Chat.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Chat
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Chat/Message/View/Gs2ChatMessageLabel")]
    public partial class Gs2ChatMessageLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Message != null)
            {
                var createdAt = _fetcher.Message.CreatedAt == null ? DateTime.Now : UnixTime.FromUnixTime(_fetcher.Message.CreatedAt).ToLocalTime();
                onUpdate?.Invoke(
                    format.Replace(
                        "{name}", $"{_fetcher?.Message?.Name}"
                    ).Replace(
                        "{roomName}", $"{_fetcher?.Message?.RoomName}"
                    ).Replace(
                        "{userId}", $"{_fetcher?.Message?.UserId}"
                    ).Replace(
                        "{category}", $"{_fetcher?.Message?.Category}"
                    ).Replace(
                        "{metadata}", $"{_fetcher?.Message?.Metadata}"
                    ).Replace(
                        "{createdAt:yyyy}", createdAt.ToString("yyyy")
                    ).Replace(
                        "{createdAt:yy}", createdAt.ToString("yy")
                    ).Replace(
                        "{createdAt:MM}", createdAt.ToString("MM")
                    ).Replace(
                        "{createdAt:MMM}", createdAt.ToString("MMM")
                    ).Replace(
                        "{createdAt:dd}", createdAt.ToString("dd")
                    ).Replace(
                        "{createdAt:hh}", createdAt.ToString("hh")
                    ).Replace(
                        "{createdAt:HH}", createdAt.ToString("HH")
                    ).Replace(
                        "{createdAt:tt}", createdAt.ToString("tt")
                    ).Replace(
                        "{createdAt:mm}", createdAt.ToString("mm")
                    ).Replace(
                        "{createdAt:ss}", createdAt.ToString("ss")
                    )
                );
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2ChatMessageLabel
    {
        private Gs2ChatMessageFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2ChatMessageFetcher>() ?? GetComponentInParent<Gs2ChatMessageFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ChatMessageFetcher.");
                enabled = false;
            }

            Update();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2ChatMessageLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2ChatMessageLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ChatMessageLabel
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