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
using Gs2.Gs2Schedule.Request;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Schedule.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Schedule.Label
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Schedule/Trigger/View/Label/Transaction/Gs2ScheduleDeleteTriggerByUserIdLabel")]
    public partial class Gs2ScheduleDeleteTriggerByUserIdLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Request != null &&
                    _userDataFetcher != null && _userDataFetcher.Fetched && _userDataFetcher.Trigger != null) {
                {
                    onUpdate?.Invoke(
                        format.Replace(
                            "{namespaceName}",
                            $"{_fetcher.Request.NamespaceName}"
                        ).Replace(
                            "{userId}",
                            $"{_fetcher.Request.UserId}"
                        ).Replace(
                            "{triggerName}",
                            $"{_fetcher.Request.TriggerName}"
                        ).Replace(
                            "{userData:triggerId}",
                            $"{_userDataFetcher.Trigger.TriggerId}"
                        ).Replace(
                            "{userData:name}",
                            $"{_userDataFetcher.Trigger.Name}"
                        ).Replace(
                            "{userData:createdAt}",
                            $"{_userDataFetcher.Trigger.CreatedAt}"
                        ).Replace(
                            "{userData:expiresAt}",
                            $"{_userDataFetcher.Trigger.ExpiresAt}"
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
                            "{triggerName}",
                            $"{_fetcher.Request.TriggerName}"
                        )
                    );
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2ScheduleDeleteTriggerByUserIdLabel
    {
        private Gs2ScheduleDeleteTriggerByUserIdFetcher _fetcher;
        private Gs2ScheduleOwnTriggerFetcher _userDataFetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2ScheduleDeleteTriggerByUserIdFetcher>() ?? GetComponentInParent<Gs2ScheduleDeleteTriggerByUserIdFetcher>();
            _userDataFetcher = GetComponent<Gs2ScheduleOwnTriggerFetcher>() ?? GetComponentInParent<Gs2ScheduleOwnTriggerFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ScheduleDeleteTriggerByUserIdFetcher.");
                enabled = false;
            }

            Update();
        }

        public virtual bool HasError()
        {
            _fetcher = GetComponent<Gs2ScheduleDeleteTriggerByUserIdFetcher>() ?? GetComponentInParent<Gs2ScheduleDeleteTriggerByUserIdFetcher>(true);
            _userDataFetcher = GetComponent<Gs2ScheduleOwnTriggerFetcher>() ?? GetComponentInParent<Gs2ScheduleOwnTriggerFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2ScheduleDeleteTriggerByUserIdLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2ScheduleDeleteTriggerByUserIdLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ScheduleDeleteTriggerByUserIdLabel
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