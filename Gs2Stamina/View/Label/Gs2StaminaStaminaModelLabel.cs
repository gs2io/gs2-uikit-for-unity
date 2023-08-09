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

	[AddComponentMenu("GS2 UIKit/Stamina/StaminaModel/View/Label/Gs2StaminaStaminaModelLabel")]
    public partial class Gs2StaminaStaminaModelLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.StaminaModel != null)
            {
                onUpdate?.Invoke(
                    format.Replace(
                        "{name}", $"{_fetcher?.StaminaModel?.Name}"
                    ).Replace(
                        "{metadata}", $"{_fetcher?.StaminaModel?.Metadata}"
                    ).Replace(
                        "{recoverIntervalMinutes}", $"{_fetcher?.StaminaModel?.RecoverIntervalMinutes}"
                    ).Replace(
                        "{recoverValue}", $"{_fetcher?.StaminaModel?.RecoverValue}"
                    ).Replace(
                        "{initialCapacity}", $"{_fetcher?.StaminaModel?.InitialCapacity}"
                    ).Replace(
                        "{isOverflow}", $"{_fetcher?.StaminaModel?.IsOverflow}"
                    ).Replace(
                        "{maxCapacity}", $"{_fetcher?.StaminaModel?.MaxCapacity}"
                    ).Replace(
                        "{maxStaminaTable}", $"{_fetcher?.StaminaModel?.MaxStaminaTable}"
                    ).Replace(
                        "{recoverIntervalTable}", $"{_fetcher?.StaminaModel?.RecoverIntervalTable}"
                    ).Replace(
                        "{recoverValueTable}", $"{_fetcher?.StaminaModel?.RecoverValueTable}"
                    )
                );
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2StaminaStaminaModelLabel
    {
        private Gs2StaminaStaminaModelFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2StaminaStaminaModelFetcher>() ?? GetComponentInParent<Gs2StaminaStaminaModelFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2StaminaStaminaModelFetcher.");
                enabled = false;
            }

            Update();
        }

        public virtual bool HasError()
        {
            _fetcher = GetComponent<Gs2StaminaStaminaModelFetcher>() ?? GetComponentInParent<Gs2StaminaStaminaModelFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2StaminaStaminaModelLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2StaminaStaminaModelLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2StaminaStaminaModelLabel
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