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
using System.Collections.Generic;
using System.Linq;
using Gs2.Unity.Core;
using Gs2.Unity.Gs2Mission.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Core.Acquire;
using Gs2.Unity.UiKit.Gs2Mission.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Mission.Context
{
    [AddComponentMenu("GS2 UIKit/Mission/MissionTaskModel/Convert/Gs2MissionConvertMissionTaskModelToCounter")]
    public class Gs2MissionConvertMissionTaskModelToCounter : MonoBehaviour
    {
        private Gs2MissionMissionTaskModelFetcher _fetcher;
        private Gs2MissionMissionTaskModelContext _context;

        public void Awake() {
            _fetcher = GetComponent<Gs2MissionMissionTaskModelFetcher>() ?? GetComponentInParent<Gs2MissionMissionTaskModelFetcher>();
            _context = GetComponent<Gs2MissionMissionTaskModelContext>() ?? GetComponentInParent<Gs2MissionMissionTaskModelContext>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MissionMissionTaskModelFetcher.");
                enabled = false;
            }
            if (_context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MissionMissionTaskModelContext.");
                enabled = false;
            }
        }

        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.MissionTaskModel != null)
            {
                this.onConverted.Invoke(
                    OwnCounter.New(
                        _context.MissionTaskModel.MissionGroupModel.Namespace,
                        _fetcher.MissionTaskModel.CounterName
                    )
                );
                enabled = false;
            }
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