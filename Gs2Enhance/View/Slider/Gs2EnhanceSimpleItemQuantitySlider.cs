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

using System;
using System.Collections;
using Gs2.Core.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Enhance.Fetcher;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Gs2.Unity.UiKit.Gs2Enhance
{
    /// <summary>
    /// Main
    /// </summary>

    [RequireComponent(typeof(Slider))]
	[AddComponentMenu("GS2 UIKit/Enhance/RateModel/View/QuantitySlider/Gs2EnhanceSimpleItemQuantitySlider")]
    public partial class Gs2EnhanceSimpleItemQuantitySlider : MonoBehaviour
    {
        private void OnFetched() {
            this._slider.maxValue = this._fetcher.MaxQuantity;
        }
        
        public void OnValueChanged(float value) {
            if ((int) value != this.quantity) {
                this.onQuantityChanged.Invoke(this._fetcher.TargetId, this.quantity = (long) value);
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2EnhanceSimpleItemQuantitySlider
    {
        private Slider _slider;
        private Gs2EnhanceSimpleItemExperienceValueFetcher _fetcher;

        public void Awake()
        {
            this._slider = GetComponent<Slider>() ?? GetComponentInParent<Slider>();
            if (this._slider == null) {
                Debug.LogWarning($"{gameObject.GetFullPath()}: Couldn't find the Slider.");
                enabled = false;
            }
            this._fetcher = GetComponent<Gs2EnhanceSimpleItemExperienceValueFetcher>() ?? GetComponentInParent<Gs2EnhanceSimpleItemExperienceValueFetcher>();
            if (this._fetcher == null) {
                Debug.LogWarning($"{gameObject.GetFullPath()}: Couldn't find the Gs2EnhanceSimpleItemExperienceValueFetcher.");
                enabled = false;
            }

            this._slider.value = this.quantity;
        }

        public bool HasError()
        {
            this._fetcher = GetComponent<Gs2EnhanceSimpleItemExperienceValueFetcher>() ?? GetComponentInParent<Gs2EnhanceSimpleItemExperienceValueFetcher>(true);
            if (this._fetcher == null) {
                return true;
            }
            return false;
        }

        private void ResetQuantity() {
            this._slider.value = 0;
        }

        private UnityAction _onFetched;

        public void OnEnable()
        {
            this._onFetched = () =>
            {
                OnFetched();
            };
            this._fetcher.OnFetched.AddListener(this._onFetched);
            this._slider.onValueChanged.AddListener(OnValueChanged);
            if (this.action != null) {
                this.action.OnEnhanceComplete += ResetQuantity;
            }
        }

        public void OnDisable()
        {
            if (this._onFetched != null) {
                this._fetcher.OnFetched.RemoveListener(this._onFetched);
                this._onFetched = null;
            }
            this._slider.onValueChanged.RemoveListener(OnValueChanged);
            if (this.action != null) {
                this.action.OnEnhanceComplete -= ResetQuantity;
            }
        }

        public void Add() {
            this._slider.value += 1;
        }

        public void Sub() {
            this._slider.value -= 1;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2EnhanceSimpleItemQuantitySlider
    {
        
    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2EnhanceSimpleItemQuantitySlider
    {
        public enum Type
        {
            Action,
            Value,
        }

        public Type type;
        public long quantity;
        public Gs2EnhanceEnhanceEnhanceAction action;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2EnhanceSimpleItemQuantitySlider
    {
        public UnityEvent<string, long> onQuantityChanged = new UnityEvent<string, long>();
    }
}