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

using System.Collections.Generic;
using Gs2.Unity.Gs2LoginReward.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2LoginReward.Context;
using Gs2.Unity.UiKit.Gs2LoginReward.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2LoginReward
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/LoginReward/ReceiveStatus/View/Gs2LoginRewardOwnReceiveStatusList")]
    public partial class Gs2LoginRewardOwnReceiveStatusList : MonoBehaviour
    {
        private List<Gs2LoginRewardOwnReceiveStatusContext> _children;

        public void Update() {
            if (_fetcher.Fetched && this._fetcher.ReceiveStatuses != null) {
                for (var i = 0; i < this.maximumItems; i++) {
                    if (i < this._fetcher.ReceiveStatuses.Count) {
                        _children[i].ReceiveStatus.bonusModelName = this._fetcher.ReceiveStatuses[i].BonusModelName;
                        _children[i].gameObject.SetActive(true);
                    }
                    else {
                        _children[i].gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2LoginRewardOwnReceiveStatusList
    {
        private Gs2LoginRewardOwnReceiveStatusListFetcher _fetcher;
        private Gs2LoginRewardNamespaceContext Context => _fetcher.Context;

        public void Awake()
        {
            if (prefab == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2LoginRewardOwnReceiveStatusContext Prefab.");
                enabled = false;
                return;
            }

            _fetcher = GetComponent<Gs2LoginRewardOwnReceiveStatusListFetcher>() ?? GetComponentInParent<Gs2LoginRewardOwnReceiveStatusListFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2LoginRewardOwnReceiveStatusListFetcher.");
                enabled = false;
            }

            var context = GetComponent<Gs2LoginRewardNamespaceContext>() ?? GetComponentInParent<Gs2LoginRewardNamespaceContext>(true);
            if (context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2LoginRewardOwnReceiveStatusListFetcher::Context.");
                enabled = false;
                return;
            }

            _children = new List<Gs2LoginRewardOwnReceiveStatusContext>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.ReceiveStatus = OwnReceiveStatus.New(
                    context.Namespace,
                    ""
                );
                node.gameObject.SetActive(false);
                _children.Add(node);
            }
            this.prefab.gameObject.SetActive(false);
        }

        public virtual bool HasError()
        {
            _fetcher = GetComponent<Gs2LoginRewardOwnReceiveStatusListFetcher>() ?? GetComponentInParent<Gs2LoginRewardOwnReceiveStatusListFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2LoginRewardOwnReceiveStatusList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2LoginRewardOwnReceiveStatusList
    {
        public Gs2LoginRewardOwnReceiveStatusContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2LoginRewardOwnReceiveStatusList
    {

    }
}