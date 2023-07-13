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

    [AddComponentMenu("GS2 UIKit/LoginReward/BonusModel/View/Gs2LoginRewardBonusModelList")]
    public partial class Gs2LoginRewardBonusModelList : MonoBehaviour
    {
        private List<Gs2LoginRewardBonusModelContext> _children;

        public void Update() {
            if (_fetcher.Fetched && this._fetcher.BonusModels != null) {
                for (var i = 0; i < this.maximumItems; i++) {
                    if (i < this._fetcher.BonusModels.Count) {
                        _children[i].BonusModel.bonusModelName = this._fetcher.BonusModels[i].Name;
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

    public partial class Gs2LoginRewardBonusModelList
    {
        private Gs2LoginRewardBonusModelListFetcher _fetcher;
        public Gs2LoginRewardNamespaceContext Context => _fetcher.Context;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2LoginRewardBonusModelListFetcher>() ?? GetComponentInParent<Gs2LoginRewardBonusModelListFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2LoginRewardBonusModelListFetcher.");
                enabled = false;
            }

            _children = new List<Gs2LoginRewardBonusModelContext>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.BonusModel = BonusModel.New(
                    _fetcher.Context.Namespace,
                    ""
                );
                node.gameObject.SetActive(false);
                _children.Add(node);
            }
            this.prefab.gameObject.SetActive(false);
        }

        public bool HasError()
        {
            _fetcher = GetComponent<Gs2LoginRewardBonusModelListFetcher>() ?? GetComponentInParent<Gs2LoginRewardBonusModelListFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2LoginRewardBonusModelList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2LoginRewardBonusModelList
    {
        public Gs2LoginRewardBonusModelContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2LoginRewardBonusModelList
    {

    }
}