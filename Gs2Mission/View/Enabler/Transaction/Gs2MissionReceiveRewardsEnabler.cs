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
using System.Linq;
using Gs2.Gs2Mission.Request;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Mission.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Mission
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Mission/Complete/View/Enabler/Transaction/Gs2MissionReceiveRewardsEnabler")]
    public partial class Gs2MissionReceiveRewardsEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (this._fetcher.Fetched && this._fetcher.MissionTaskModel != null &&
                this._userDataFetcher.Fetched && this._userDataFetcher.Complete != null) {
                if (!_userDataFetcher.Complete.CompletedMissionTaskNames.Contains(this._fetcher.MissionTaskModel.Name)) {
                    this.target.SetActive(this.notCompleted);
                }
                else if (!_userDataFetcher.Complete.ReceivedMissionTaskNames.Contains(this._fetcher.MissionTaskModel.Name)) {
                    this.target.SetActive(this.completed);
                }
                else {
                    this.target.SetActive(this.received);
                }
            }
            else
            {
                this.target.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2MissionReceiveRewardsEnabler
    {
        private Gs2MissionMissionTaskModelFetcher _fetcher;
        private Gs2MissionOwnCompleteFetcher _userDataFetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2MissionMissionTaskModelFetcher>() ?? GetComponentInParent<Gs2MissionMissionTaskModelFetcher>();
            _userDataFetcher = GetComponent<Gs2MissionOwnCompleteFetcher>() ?? GetComponentInParent<Gs2MissionOwnCompleteFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MissionMissionTaskModelFetcher.");
                enabled = false;
            }
            if (_userDataFetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MissionOwnCompleteFetcher.");
                enabled = false;
            }

            Update();
        }

        public bool HasError()
        {
            _fetcher = GetComponent<Gs2MissionMissionTaskModelFetcher>() ?? GetComponentInParent<Gs2MissionMissionTaskModelFetcher>(true);
            _userDataFetcher = GetComponent<Gs2MissionOwnCompleteFetcher>() ?? GetComponentInParent<Gs2MissionOwnCompleteFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            if (_userDataFetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2MissionReceiveRewardsEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2MissionReceiveRewardsEnabler
    {
        public bool notCompleted;
        public bool completed;
        public bool received;
        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MissionReceiveRewardsEnabler
    {

    }
}