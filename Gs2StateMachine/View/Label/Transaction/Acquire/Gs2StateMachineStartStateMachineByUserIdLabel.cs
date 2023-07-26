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
using Gs2.Gs2StateMachine.Request;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2StateMachine.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2StateMachine.Label
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/StateMachine/Status/View/Label/Transaction/Gs2StateMachineStartStateMachineByUserIdLabel")]
    public partial class Gs2StateMachineStartStateMachineByUserIdLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Request != null &&
                    _userDataFetcher != null && _userDataFetcher.Fetched && _userDataFetcher.Status != null) {
                {
                    onUpdate?.Invoke(
                        format.Replace(
                            "{namespaceName}",
                            $"{_fetcher.Request.NamespaceName}"
                        ).Replace(
                            "{userId}",
                            $"{_fetcher.Request.UserId}"
                        ).Replace(
                            "{args}",
                            $"{_fetcher.Request.Args}"
                        ).Replace(
                            "{ttl}",
                            $"{_fetcher.Request.Ttl}"
                        ).Replace(
                            "{userData:statusId}",
                            $"{_userDataFetcher.Status.StatusId}"
                        ).Replace(
                            "{userData:name}",
                            $"{_userDataFetcher.Status.Name}"
                        ).Replace(
                            "{userData:stacks}",
                            $"{_userDataFetcher.Status.Stacks}"
                        ).Replace(
                            "{userData:variables}",
                            $"{_userDataFetcher.Status.Variables}"
                        ).Replace(
                            "{userData:status}",
                            $"{_userDataFetcher.Status.Status}"
                        ).Replace(
                            "{userData:lastError}",
                            $"{_userDataFetcher.Status.LastError}"
                        ).Replace(
                            "{userData:transitionCount}",
                            $"{_userDataFetcher.Status.TransitionCount}"
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
                            "{args}",
                            $"{_fetcher.Request.Args}"
                        ).Replace(
                            "{ttl}",
                            $"{_fetcher.Request.Ttl}"
                        )
                    );
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2StateMachineStartStateMachineByUserIdLabel
    {
        private Gs2StateMachineStartStateMachineByUserIdFetcher _fetcher;
        private Gs2StateMachineOwnStatusFetcher _userDataFetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2StateMachineStartStateMachineByUserIdFetcher>() ?? GetComponentInParent<Gs2StateMachineStartStateMachineByUserIdFetcher>();
            _userDataFetcher = GetComponent<Gs2StateMachineOwnStatusFetcher>() ?? GetComponentInParent<Gs2StateMachineOwnStatusFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2StateMachineStartStateMachineByUserIdFetcher.");
                enabled = false;
            }

            Update();
        }

        public bool HasError()
        {
            _fetcher = GetComponent<Gs2StateMachineStartStateMachineByUserIdFetcher>() ?? GetComponentInParent<Gs2StateMachineStartStateMachineByUserIdFetcher>(true);
            _userDataFetcher = GetComponent<Gs2StateMachineOwnStatusFetcher>() ?? GetComponentInParent<Gs2StateMachineOwnStatusFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2StateMachineStartStateMachineByUserIdLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2StateMachineStartStateMachineByUserIdLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2StateMachineStartStateMachineByUserIdLabel
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