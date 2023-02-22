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
using Gs2.Unity.Gs2Mission.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Mission.Context
{
    [AddComponentMenu("GS2 UIKit/Mission/Counter/Convert/Gs2MissionConvertCounterModelToOwnCounter")]
    public class Gs2MissionConvertCounterModelToOwnCounter : MonoBehaviour
    {
        private Gs2MissionCounterModelContext _context;
        
        public void Awake() {
            _context = GetComponentInParent<Gs2MissionCounterModelContext>();

            if (_context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MissionCounterModelContext.");
                enabled = false;
            }
        }
        
        public void Start() {
            this.onConverted.Invoke(
                OwnCounter.New(
                    _context.CounterModel.Namespace,
                    _context.CounterModel.counterName
                )
            );
            enabled = false;
        }
        
        [Serializable]
        private class ConvertEvent : UnityEvent<OwnCounter>
        {

        }

        [SerializeField]
        private ConvertEvent onConverted = new ConvertEvent();

        public event UnityAction<OwnCounter> OnConvert
        {
            add => onConverted.AddListener(value);
            remove => onConverted.RemoveListener(value);
        }
    }
}