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

using System.Collections.Generic;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Experience.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Experience
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Experience/ExperienceModel/View/Properties/DefaultExperience/Gs2ExperienceExperienceModelDefaultExperienceEnabler")]
    public partial class Gs2ExperienceExperienceModelDefaultExperienceEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.ExperienceModel != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableDefaultExperiences.Contains(_fetcher.ExperienceModel.DefaultExperience));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableDefaultExperiences.Contains(_fetcher.ExperienceModel.DefaultExperience));
                        break;
                    case Expression.Less:
                        target.SetActive(enableDefaultExperience > _fetcher.ExperienceModel.DefaultExperience);
                        break;
                    case Expression.LessEqual:
                        target.SetActive(enableDefaultExperience >= _fetcher.ExperienceModel.DefaultExperience);
                        break;
                    case Expression.Greater:
                        target.SetActive(enableDefaultExperience < _fetcher.ExperienceModel.DefaultExperience);
                        break;
                    case Expression.GreaterEqual:
                        target.SetActive(enableDefaultExperience <= _fetcher.ExperienceModel.DefaultExperience);
                        break;
                }
            }
            else
            {
                target.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2ExperienceExperienceModelDefaultExperienceEnabler
    {
        private Gs2ExperienceExperienceModelFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2ExperienceExperienceModelFetcher>() ?? GetComponentInParent<Gs2ExperienceExperienceModelFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ExperienceExperienceModelFetcher.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2ExperienceExperienceModelDefaultExperienceEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2ExperienceExperienceModelDefaultExperienceEnabler
    {
        public enum Expression {
            In,
            NotIn,
            Less,
            LessEqual,
            Greater,
            GreaterEqual,
        }

        public Expression expression;

        public List<long> enableDefaultExperiences;

        public long enableDefaultExperience;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ExperienceExperienceModelDefaultExperienceEnabler
    {
        
    }
}