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
using Gs2.Unity.UiKit.Gs2Enchant.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Enchant
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Enchant/RarityParameterModel/View/Label/Gs2EnchantRarityParameterModelLabel")]
    public partial class Gs2EnchantRarityParameterModelLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.RarityParameterModel != null)
            {
                onUpdate?.Invoke(
                    format.Replace(
                        "{name}", $"{_fetcher?.RarityParameterModel?.Name}"
                    ).Replace(
                        "{metadata}", $"{_fetcher?.RarityParameterModel?.Metadata}"
                    ).Replace(
                        "{maximumParameterCount}", $"{_fetcher?.RarityParameterModel?.MaximumParameterCount}"
                    ).Replace(
                        "{parameterCounts}", $"{_fetcher?.RarityParameterModel?.ParameterCounts}"
                    ).Replace(
                        "{parameters}", $"{_fetcher?.RarityParameterModel?.Parameters}"
                    )
                );
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2EnchantRarityParameterModelLabel
    {
        private Gs2EnchantRarityParameterModelFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2EnchantRarityParameterModelFetcher>() ?? GetComponentInParent<Gs2EnchantRarityParameterModelFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2EnchantRarityParameterModelFetcher.");
                enabled = false;
            }

            Update();
        }

        public virtual bool HasError()
        {
            _fetcher = GetComponent<Gs2EnchantRarityParameterModelFetcher>() ?? GetComponentInParent<Gs2EnchantRarityParameterModelFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2EnchantRarityParameterModelLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2EnchantRarityParameterModelLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2EnchantRarityParameterModelLabel
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