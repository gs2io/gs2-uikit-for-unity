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
 *
 * deny overwrite
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
using Gs2.Unity.Gs2Lottery.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Lottery.Context;
using Gs2.Unity.UiKit.Gs2Lottery.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Lottery
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Lottery/Probability/View/Gs2LotteryOwnProbabilityList")]
    public partial class Gs2LotteryOwnProbabilityList : MonoBehaviour
    {
        private List<Gs2LotteryOwnProbabilityContext> _children;

        public void Update() {
            if (_fetcher.Fetched && this._fetcher.Probabilities != null) {
                for (var i = 0; i < this.maximumItems; i++) {
                    if (i < this._fetcher.Probabilities.Count) {
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

    public partial class Gs2LotteryOwnProbabilityList
    {
        private Gs2LotteryOwnProbabilityListFetcher _fetcher;
        private Gs2LotteryLotteryModelContext Context => _fetcher.Context;

        public void Awake()
        {
            if (prefab == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2LotteryOwnProbabilityContext Prefab.");
                enabled = false;
                return;
            }

            _fetcher = GetComponent<Gs2LotteryOwnProbabilityListFetcher>() ?? GetComponentInParent<Gs2LotteryOwnProbabilityListFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2LotteryOwnProbabilityListFetcher.");
                enabled = false;
            }

            var context = GetComponent<Gs2LotteryLotteryModelContext>() ?? GetComponentInParent<Gs2LotteryLotteryModelContext>(true);
            if (context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2LotteryOwnProbabilityListFetcher::Context.");
                enabled = false;
                return;
            }

            _children = new List<Gs2LotteryOwnProbabilityContext>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.Probability = OwnProbability.New(
                    context.LotteryModel
                );
                node.gameObject.SetActive(false);
                _children.Add(node);
            }
            this.prefab.gameObject.SetActive(false);
        }

        public bool HasError()
        {
            _fetcher = GetComponent<Gs2LotteryOwnProbabilityListFetcher>() ?? GetComponentInParent<Gs2LotteryOwnProbabilityListFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2LotteryOwnProbabilityList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2LotteryOwnProbabilityList
    {
        public Gs2LotteryOwnProbabilityContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2LotteryOwnProbabilityList
    {

    }
}