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

using System.Collections.Generic;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Experience.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Experience
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Experience/Status/View/Enabler/Properties/ExperienceValue/Gs2ExperienceOwnStatusExperienceValueEnabler")]
    public partial class Gs2ExperienceOwnStatusExperienceValueEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Status != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableExperienceValues.Contains(_fetcher.Status.ExperienceValue));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableExperienceValues.Contains(_fetcher.Status.ExperienceValue));
                        break;
                    case Expression.Less:
                        target.SetActive(enableExperienceValue > _fetcher.Status.ExperienceValue);
                        break;
                    case Expression.LessEqual:
                        target.SetActive(enableExperienceValue >= _fetcher.Status.ExperienceValue);
                        break;
                    case Expression.Greater:
                        target.SetActive(enableExperienceValue < _fetcher.Status.ExperienceValue);
                        break;
                    case Expression.GreaterEqual:
                        target.SetActive(enableExperienceValue <= _fetcher.Status.ExperienceValue);
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

    public partial class Gs2ExperienceOwnStatusExperienceValueEnabler
    {
        private Gs2ExperienceOwnStatusFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2ExperienceOwnStatusFetcher>() ?? GetComponentInParent<Gs2ExperienceOwnStatusFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ExperienceOwnStatusFetcher.");
                enabled = false;
            }
            if (target == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: target is not set.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            _fetcher = GetComponent<Gs2ExperienceOwnStatusFetcher>() ?? GetComponentInParent<Gs2ExperienceOwnStatusFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            if (target == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2ExperienceOwnStatusExperienceValueEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2ExperienceOwnStatusExperienceValueEnabler
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

        public List<long> enableExperienceValues;

        public long enableExperienceValue;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ExperienceOwnStatusExperienceValueEnabler
    {
        
    }
}