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
using Gs2.Unity.UiKit.Gs2JobQueue.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2JobQueue
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/JobQueue/Job/View/Gs2JobQueueJobLabel")]
    public partial class Gs2JobQueueJobLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Job != null)
            {
                onUpdate?.Invoke(
                    format.Replace(
                        "{jobId}", $"{_fetcher?.Job?.JobId}"
                    ).Replace(
                        "{scriptId}", $"{_fetcher?.Job?.ScriptId}"
                    ).Replace(
                        "{args}", $"{_fetcher?.Job?.Args}"
                    ).Replace(
                        "{currentRetryCount}", $"{_fetcher?.Job?.CurrentRetryCount}"
                    ).Replace(
                        "{maxTryCount}", $"{_fetcher?.Job?.MaxTryCount}"
                    )
                );
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2JobQueueJobLabel
    {
        private Gs2JobQueueOwnJobFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2JobQueueOwnJobFetcher>() ?? GetComponentInParent<Gs2JobQueueOwnJobFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2JobQueueOwnJobFetcher.");
                enabled = false;
            }

            Update();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2JobQueueJobLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2JobQueueJobLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2JobQueueJobLabel
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