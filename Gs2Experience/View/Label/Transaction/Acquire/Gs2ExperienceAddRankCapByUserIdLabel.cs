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
using Gs2.Gs2Experience.Request;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Experience.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Experience.Label
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Experience/Status/View/Label/Transaction/Gs2ExperienceAddRankCapByUserIdLabel")]
    public partial class Gs2ExperienceAddRankCapByUserIdLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Request != null &&
                    _userDataFetcher != null && _userDataFetcher.Fetched && _userDataFetcher.Status != null) {
                {
                    onUpdate?.Invoke(
                        format.Replace(
                            "{namespaceName}",
                            $"{_fetcher.Request.NamespaceName}"
                        ).Replace(
                            "{userId}",
                            $"{_fetcher.Request.UserId}"
                        ).Replace(
                            "{experienceName}",
                            $"{_fetcher.Request.ExperienceName}"
                        ).Replace(
                            "{propertyId}",
                            $"{_fetcher.Request.PropertyId}"
                        ).Replace(
                            "{rankCapValue}",
                            $"{_fetcher.Request.RankCapValue}"
                        ).Replace(
                            "{userData:experienceName}",
                            $"{_userDataFetcher.Status.ExperienceName}"
                        ).Replace(
                            "{userData:propertyId}",
                            $"{_userDataFetcher.Status.PropertyId}"
                        ).Replace(
                            "{userData:experienceValue}",
                            $"{_userDataFetcher.Status.ExperienceValue}"
                        ).Replace(
                            "{userData:rankValue}",
                            $"{_userDataFetcher.Status.RankValue}"
                        ).Replace(
                            "{userData:rankCapValue}",
                            $"{_userDataFetcher.Status.RankCapValue}"
                        ).Replace(
                            "{userData:rankCapValue:changed}",
                            $"{_userDataFetcher.Status.RankCapValue + _fetcher.Request.RankCapValue}"
                        ).Replace(
                            "{userData:nextRankUpExperienceValue}",
                            $"{_userDataFetcher.Status.NextRankUpExperienceValue}"
                        )
                    );
                }
            } else if (_fetcher.Fetched && _fetcher.Request != null) {
                {
                    onUpdate?.Invoke(
                        format.Replace(
                            "{namespaceName}",
                            $"{_fetcher.Request.NamespaceName}"
                        ).Replace(
                            "{userId}",
                            $"{_fetcher.Request.UserId}"
                        ).Replace(
                            "{experienceName}",
                            $"{_fetcher.Request.ExperienceName}"
                        ).Replace(
                            "{propertyId}",
                            $"{_fetcher.Request.PropertyId}"
                        ).Replace(
                            "{rankCapValue}",
                            $"{_fetcher.Request.RankCapValue}"
                        )
                    );
                }
            } else {
                onUpdate?.Invoke(
                    format.Replace(
                        "{rankCapValue}",
                        "0"
                    )
                );
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2ExperienceAddRankCapByUserIdLabel
    {
        private Gs2ExperienceAddRankCapByUserIdFetcher _fetcher;
        private Gs2ExperienceOwnStatusFetcher _userDataFetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2ExperienceAddRankCapByUserIdFetcher>() ?? GetComponentInParent<Gs2ExperienceAddRankCapByUserIdFetcher>();
            _userDataFetcher = GetComponent<Gs2ExperienceOwnStatusFetcher>() ?? GetComponentInParent<Gs2ExperienceOwnStatusFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ExperienceAddRankCapByUserIdFetcher.");
                enabled = false;
            }

            Update();
        }

        public virtual bool HasError()
        {
            _fetcher = GetComponent<Gs2ExperienceAddRankCapByUserIdFetcher>() ?? GetComponentInParent<Gs2ExperienceAddRankCapByUserIdFetcher>(true);
            _userDataFetcher = GetComponent<Gs2ExperienceOwnStatusFetcher>() ?? GetComponentInParent<Gs2ExperienceOwnStatusFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2ExperienceAddRankCapByUserIdLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2ExperienceAddRankCapByUserIdLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ExperienceAddRankCapByUserIdLabel
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