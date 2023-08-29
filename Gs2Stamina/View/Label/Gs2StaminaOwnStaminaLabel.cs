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
using Gs2.Unity.UiKit.Gs2Stamina.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Stamina
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Stamina/Stamina/View/Label/Gs2StaminaOwnStaminaLabel")]
    public partial class Gs2StaminaOwnStaminaLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Stamina != null)
            {
                var nextRecoverAt = _fetcher.Stamina.NextRecoverAt == null ? DateTime.Now : UnixTime.FromUnixTime(_fetcher.Stamina.NextRecoverAt).ToLocalTime();
                onUpdate?.Invoke(
                    format.Replace(
                        "{staminaName}", $"{_fetcher?.Stamina?.StaminaName}"
                    ).Replace(
                        "{value}", $"{_fetcher?.Stamina?.Value}"
                    ).Replace(
                        "{overflowValue}", $"{_fetcher?.Stamina?.OverflowValue}"
                    ).Replace(
                        "{maxValue}", $"{_fetcher?.Stamina?.MaxValue}"
                    ).Replace(
                        "{recoverIntervalMinutes}", $"{_fetcher?.Stamina?.RecoverIntervalMinutes}"
                    ).Replace(
                        "{recoverValue}", $"{_fetcher?.Stamina?.RecoverValue}"
                    ).Replace(
                        "{nextRecoverAt:yyyy}", nextRecoverAt.ToString("yyyy")
                    ).Replace(
                        "{nextRecoverAt:yy}", nextRecoverAt.ToString("yy")
                    ).Replace(
                        "{nextRecoverAt:MM}", nextRecoverAt.ToString("MM")
                    ).Replace(
                        "{nextRecoverAt:MMM}", nextRecoverAt.ToString("MMM")
                    ).Replace(
                        "{nextRecoverAt:dd}", nextRecoverAt.ToString("dd")
                    ).Replace(
                        "{nextRecoverAt:hh}", nextRecoverAt.ToString("hh")
                    ).Replace(
                        "{nextRecoverAt:HH}", nextRecoverAt.ToString("HH")
                    ).Replace(
                        "{nextRecoverAt:tt}", nextRecoverAt.ToString("tt")
                    ).Replace(
                        "{nextRecoverAt:mm}", nextRecoverAt.ToString("mm")
                    ).Replace(
                        "{nextRecoverAt:ss}", nextRecoverAt.ToString("ss")
                    )
                );
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2StaminaOwnStaminaLabel
    {
        private Gs2StaminaOwnStaminaFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2StaminaOwnStaminaFetcher>() ?? GetComponentInParent<Gs2StaminaOwnStaminaFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2StaminaOwnStaminaFetcher.");
                enabled = false;
            }

            Update();
        }

        public virtual bool HasError()
        {
            _fetcher = GetComponent<Gs2StaminaOwnStaminaFetcher>() ?? GetComponentInParent<Gs2StaminaOwnStaminaFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2StaminaOwnStaminaLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2StaminaOwnStaminaLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2StaminaOwnStaminaLabel
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