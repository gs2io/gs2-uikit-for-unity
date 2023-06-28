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
using Gs2.Unity.Core;
using Gs2.Unity.Gs2Exchange.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Core.Consume;
using Gs2.Unity.UiKit.Gs2Exchange.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Exchange.Context
{
    [AddComponentMenu("GS2 UIKit/Exchange/RateModel/Convert/Gs2ExchangeConvertRateModelConsumeActionsToConsumeActions")]
    public class Gs2ExchangeConvertRateModelConsumeActionsToConsumeActions : MonoBehaviour
    {
        private Gs2ExchangeRateModelFetcher _fetcher;

        private int _callbackCount;
        public int count = 1;

        public void Awake() {
            _fetcher = GetComponent<Gs2ExchangeRateModelFetcher>() ?? GetComponentInParent<Gs2ExchangeRateModelFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ExchangeRateModelFetcher.");
                enabled = false;
            }
        }

        public bool HasError()
        {
            _fetcher = GetComponent<Gs2ExchangeRateModelFetcher>() ?? GetComponentInParent<Gs2ExchangeRateModelFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }

        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.RateModel != null && _callbackCount != count)
            {
                this.onConverted.Invoke(
                    _fetcher.RateModel.ConsumeActions.Select(v => ConsumeAction.New(
                        v.Action,
                        v.Request
                    ) * count).ToList()
                );
                _callbackCount = count;
            }
        }

        public void SetCount(int count)
        {
            this.count = count;
        }

        [Serializable]
        private class ConvertEvent : UnityEvent<List<ConsumeAction>>
        {

        }

        [SerializeField]
        private ConvertEvent onConverted = new ConvertEvent();

        public event UnityAction<List<ConsumeAction>> OnConvert
        {
            add => onConverted.AddListener(value);
            remove => onConverted.RemoveListener(value);
        }
    }
}