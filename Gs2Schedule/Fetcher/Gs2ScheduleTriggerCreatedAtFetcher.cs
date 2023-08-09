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
using System.Collections.Generic;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Schedule.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Schedule
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Schedule/Trigger/Fetcher/Properties/CreatedAt/Gs2ScheduleTriggerCreatedAtFetcher")]
    public partial class Gs2ScheduleTriggerCreatedAtFetcher : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Trigger != null)
            {
                onUpdate?.Invoke(
                    _fetcher.Trigger.CreatedAt
                );
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2ScheduleTriggerCreatedAtFetcher
    {
        private Gs2ScheduleOwnTriggerFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2ScheduleOwnTriggerFetcher>() ?? GetComponentInParent<Gs2ScheduleOwnTriggerFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ScheduleOwnTriggerFetcher.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            _fetcher = GetComponent<Gs2ScheduleOwnTriggerFetcher>() ?? GetComponentInParent<Gs2ScheduleOwnTriggerFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2ScheduleTriggerCreatedAtFetcher
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>
    
    public partial class Gs2ScheduleTriggerCreatedAtFetcher
    {

    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ScheduleTriggerCreatedAtFetcher
    {
        [Serializable]
        private class UpdateEvent : UnityEvent<long>
        {

        }

        [SerializeField]
        private UpdateEvent onUpdate = new UpdateEvent();

        public event UnityAction<long> OnUpdate
        {
            add => onUpdate.AddListener(value);
            remove => onUpdate.RemoveListener(value);
        }
    }
}