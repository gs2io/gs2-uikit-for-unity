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

using System;
using System.Collections.Generic;
using Gs2.Core.Util;
using Gs2.Unity.Core.Model;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Lottery.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Lottery
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Lottery/Probability/View/Label/Gs2LotteryProbabilityLabel")]
    public partial class Gs2LotteryProbabilityLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Probability != null)
            {
                onUpdate?.Invoke(
                    format.Replace(
                        "{prizeId}", $"{_fetcher?.Probability?.Prize.PrizeId}"
                    ).Replace(
                        "{rate:0-1:f2}", $"{_fetcher?.Probability?.Rate:f2}"
                    ).Replace(
                        "{rate:0-1:f3}", $"{_fetcher?.Probability?.Rate:f3}"
                    ).Replace(
                        "{rate:0-1:f4}", $"{_fetcher?.Probability?.Rate:f4}"
                    ).Replace(
                        "{rate:0-100:f2}", $"{_fetcher?.Probability?.Rate*100:f2}"
                    ).Replace(
                        "{rate:0-100:f3}", $"{_fetcher?.Probability?.Rate*100:f3}"
                    ).Replace(
                        "{rate:0-100:f4}", $"{_fetcher?.Probability?.Rate*100:f4}"
                    )
                );
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2LotteryProbabilityLabel
    {
        private Gs2LotteryOwnProbabilityFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2LotteryOwnProbabilityFetcher>() ?? GetComponentInParent<Gs2LotteryOwnProbabilityFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2LotteryOwnProbabilityFetcher.");
                enabled = false;
            }

            Update();
        }

        public virtual bool HasError()
        {
            _fetcher = GetComponent<Gs2LotteryOwnProbabilityFetcher>() ?? GetComponentInParent<Gs2LotteryOwnProbabilityFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2LotteryProbabilityLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2LotteryProbabilityLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2LotteryProbabilityLabel
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