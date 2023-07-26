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
using Gs2.Core.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2StateMachine.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2StateMachine
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/StateMachine/Status/View/Label/Gs2StateMachineStatusLabel")]
    public partial class Gs2StateMachineStatusLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Status != null)
            {
                onUpdate?.Invoke(
                    format.Replace(
                        "{statusId}", $"{_fetcher?.Status?.StatusId}"
                    ).Replace(
                        "{name}", $"{_fetcher?.Status?.Name}"
                    ).Replace(
                        "{stacks}", $"{_fetcher?.Status?.Stacks}"
                    ).Replace(
                        "{variables}", $"{_fetcher?.Status?.Variables}"
                    ).Replace(
                        "{status}", $"{_fetcher?.Status?.Status}"
                    ).Replace(
                        "{lastError}", $"{_fetcher?.Status?.LastError}"
                    ).Replace(
                        "{transitionCount}", $"{_fetcher?.Status?.TransitionCount}"
                    )
                );
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2StateMachineStatusLabel
    {
        private Gs2StateMachineOwnStatusFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2StateMachineOwnStatusFetcher>() ?? GetComponentInParent<Gs2StateMachineOwnStatusFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2StateMachineOwnStatusFetcher.");
                enabled = false;
            }

            Update();
        }

        public bool HasError()
        {
            _fetcher = GetComponent<Gs2StateMachineOwnStatusFetcher>() ?? GetComponentInParent<Gs2StateMachineOwnStatusFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2StateMachineStatusLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2StateMachineStatusLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2StateMachineStatusLabel
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