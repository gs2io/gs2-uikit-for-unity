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
using Gs2.Unity.UiKit.Gs2Dictionary.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Dictionary
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Dictionary/Entry/View/Gs2DictionaryEntryLabel")]
    public partial class Gs2DictionaryEntryLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched)
            {
                var acquiredAt = UnixTime.FromUnixTime(_fetcher.Entry.AcquiredAt).ToLocalTime();
                onUpdate.Invoke(
                    format.Replace(
                        "{entryId}", _fetcher.Entry.EntryId.ToString()
                    ).Replace(
                        "{userId}", _fetcher.Entry.UserId.ToString()
                    ).Replace(
                        "{name}", _fetcher.Entry.Name.ToString()
                    ).Replace(
                        "{acquiredAt:yyyy}", acquiredAt.ToString("yyyy")
                    ).Replace(
                        "{acquiredAt:yy}", acquiredAt.ToString("yy")
                    ).Replace(
                        "{acquiredAt:MM}", acquiredAt.ToString("MM")
                    ).Replace(
                        "{acquiredAt:MMM}", acquiredAt.ToString("MMM")
                    ).Replace(
                        "{acquiredAt:dd}", acquiredAt.ToString("dd")
                    ).Replace(
                        "{acquiredAt:hh}", acquiredAt.ToString("hh")
                    ).Replace(
                        "{acquiredAt:HH}", acquiredAt.ToString("HH")
                    ).Replace(
                        "{acquiredAt:tt}", acquiredAt.ToString("tt")
                    ).Replace(
                        "{acquiredAt:mm}", acquiredAt.ToString("mm")
                    ).Replace(
                        "{acquiredAt:ss}", acquiredAt.ToString("ss")
                    )
                );
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2DictionaryEntryLabel
    {
        private Gs2DictionaryEntryFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponentInParent<Gs2DictionaryEntryFetcher>();
            Update();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2DictionaryEntryLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2DictionaryEntryLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2DictionaryEntryLabel
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