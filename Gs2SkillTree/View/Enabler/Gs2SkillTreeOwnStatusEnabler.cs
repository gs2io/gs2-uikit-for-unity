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

using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2SkillTree.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2SkillTree
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/SkillTree/Status/View/Enabler/Gs2SkillTreeOwnStatusEnabler")]
    public partial class Gs2SkillTreeOwnStatusEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (!_fetcher.Fetched)
            {
                target.SetActive(loading);
            }
            else
            {
                if (_fetcher.Status == null)
                {
                    target.SetActive(notFound);
                }
                else
                {
                    target.SetActive(loaded);
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2SkillTreeOwnStatusEnabler
    {
        private Gs2SkillTreeOwnStatusFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2SkillTreeOwnStatusFetcher>() ?? GetComponentInParent<Gs2SkillTreeOwnStatusFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2SkillTreeOwnStatusFetcher.");
                enabled = false;
            }
            if (target == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: target is not set.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            _fetcher = GetComponent<Gs2SkillTreeOwnStatusFetcher>() ?? GetComponentInParent<Gs2SkillTreeOwnStatusFetcher>(true);
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

    public partial class Gs2SkillTreeOwnStatusEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2SkillTreeOwnStatusEnabler
    {
        public bool loading;
        public bool notFound;
        public bool loaded;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2SkillTreeOwnStatusEnabler
    {

    }
}