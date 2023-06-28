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
using Gs2.Core.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Experience.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Experience
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Experience/Status/View/Progress/Gs2ExperienceStatusProgress")]
    public partial class Gs2ExperienceStatusProgress : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Status != null && _modelFetcher.Fetched && _modelFetcher.ExperienceModel != null) {
                if (this._fetcher.Status.RankValue == this._fetcher.Status.RankCapValue) {
                    onUpdate?.Invoke(1);
                }
                else {
                    var before = 0L;
                    if (this._fetcher.Status.RankValue > 1) {
                        before = _modelFetcher.ExperienceModel.RankThreshold.Values[
                            (int) (this._fetcher.Status.RankValue - 2)
                        ];
                    }
                    var next = _modelFetcher.ExperienceModel.RankThreshold.Values[
                        (int)this._fetcher.Status.RankValue - 1
                    ];
                    var value = _fetcher.Status.ExperienceValue - before;
                    var span = next - before;
                    onUpdate?.Invoke(
                        Math.Min((float)value / span, 1)
                    );
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2ExperienceStatusProgress
    {
        private Gs2ExperienceExperienceModelFetcher _modelFetcher;
        private Gs2ExperienceOwnStatusFetcher _fetcher;

        public void Awake()
        {
            _modelFetcher = GetComponentInParent<Gs2ExperienceExperienceModelFetcher>();
            _fetcher = GetComponentInParent<Gs2ExperienceOwnStatusFetcher>();
            Update();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2ExperienceStatusProgress
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2ExperienceStatusProgress
    {
        
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ExperienceStatusProgress
    {
        [Serializable]
        private class UpdateEvent : UnityEvent<float>
        {

        }

        [SerializeField]
        private UpdateEvent onUpdate = new UpdateEvent();

        public event UnityAction<float> OnUpdate
        {
            add => onUpdate.AddListener(value);
            remove => onUpdate.RemoveListener(value);
        }
    }
}