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
using Gs2.Unity.UiKit.Gs2Lottery.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Lottery
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Lottery/Probability/View/Label/Gs2LotteryProbabilityLabel")]
    public partial class Gs2LotteryOwnProbabilityLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Probability != null)
            {
                onUpdate?.Invoke(
                    format.Replace(
                        "{prizeId}", $"{_fetcher?.Probability?.Prize.PrizeId}"
                    ).Replace(
                        "{rate:0-1}", $"{_fetcher?.Probability?.Rate}"
                    ).Replace(
                        "{rate:0-100}", $"{(int)((this._fetcher?.Probability?.Rate ?? 0) * 100)}"
                    ).Replace(
                        "{rate:0.000-100.0}", $"{string.Format("{0:f1}", (this._fetcher?.Probability?.Rate ?? 0) * 100)}"
                    ).Replace(
                        "{rate:0.000-100.00}", $"{string.Format("{0:f2}", (this._fetcher?.Probability?.Rate ?? 0) * 100)}"
                    ).Replace(
                        "{rate:0.000-100.000}", $"{string.Format("{0:f3}", (this._fetcher?.Probability?.Rate ?? 0) * 100)}"
                    ).Replace(
                        "{rate:0.0000-100.0000}", $"{string.Format("{0:f4}", (this._fetcher?.Probability?.Rate ?? 0) * 100)}"
                    ).Replace(
                        "{rate:0.00000-100.00000}", $"{string.Format("{0:f5}", (this._fetcher?.Probability?.Rate ?? 0) * 100)}"
                    )
                );
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2LotteryOwnProbabilityLabel
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

        public bool HasError()
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

    public partial class Gs2LotteryOwnProbabilityLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2LotteryOwnProbabilityLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2LotteryOwnProbabilityLabel
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