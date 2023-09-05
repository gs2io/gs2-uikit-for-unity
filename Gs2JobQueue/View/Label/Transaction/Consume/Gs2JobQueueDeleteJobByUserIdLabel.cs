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
using Gs2.Gs2JobQueue.Request;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2JobQueue.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2JobQueue.Label
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/JobQueue/Job/View/Label/Transaction/Gs2JobQueueDeleteJobByUserIdLabel")]
    public partial class Gs2JobQueueDeleteJobByUserIdLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Request != null &&
                    _userDataFetcher != null && _userDataFetcher.Fetched && _userDataFetcher.Job != null) {
                {
                    onUpdate?.Invoke(
                        format.Replace(
                            "{namespaceName}",
                            $"{_fetcher.Request.NamespaceName}"
                        ).Replace(
                            "{userId}",
                            $"{_fetcher.Request.UserId}"
                        ).Replace(
                            "{jobName}",
                            $"{_fetcher.Request.JobName}"
                        ).Replace(
                            "{userData:jobId}",
                            $"{_userDataFetcher.Job.JobId}"
                        ).Replace(
                            "{userData:scriptId}",
                            $"{_userDataFetcher.Job.ScriptId}"
                        ).Replace(
                            "{userData:args}",
                            $"{_userDataFetcher.Job.Args}"
                        ).Replace(
                            "{userData:currentRetryCount}",
                            $"{_userDataFetcher.Job.CurrentRetryCount}"
                        ).Replace(
                            "{userData:maxTryCount}",
                            $"{_userDataFetcher.Job.MaxTryCount}"
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
                            "{jobName}",
                            $"{_fetcher.Request.JobName}"
                        )
                    );
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2JobQueueDeleteJobByUserIdLabel
    {
        private Gs2JobQueueDeleteJobByUserIdFetcher _fetcher;
        private Gs2JobQueueOwnJobFetcher _userDataFetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2JobQueueDeleteJobByUserIdFetcher>() ?? GetComponentInParent<Gs2JobQueueDeleteJobByUserIdFetcher>();
            _userDataFetcher = GetComponent<Gs2JobQueueOwnJobFetcher>() ?? GetComponentInParent<Gs2JobQueueOwnJobFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2JobQueueDeleteJobByUserIdFetcher.");
                enabled = false;
            }

            Update();
        }

        public virtual bool HasError()
        {
            _fetcher = GetComponent<Gs2JobQueueDeleteJobByUserIdFetcher>() ?? GetComponentInParent<Gs2JobQueueDeleteJobByUserIdFetcher>(true);
            _userDataFetcher = GetComponent<Gs2JobQueueOwnJobFetcher>() ?? GetComponentInParent<Gs2JobQueueOwnJobFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2JobQueueDeleteJobByUserIdLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2JobQueueDeleteJobByUserIdLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2JobQueueDeleteJobByUserIdLabel
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