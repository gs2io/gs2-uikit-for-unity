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
using Gs2.Unity.Gs2Experience.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Experience.Context;
using Gs2.Unity.UiKit.Gs2Experience.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Experience
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Experience/Status/View/Gs2ExperienceOwnStatusList")]
    public partial class Gs2ExperienceOwnStatusList : MonoBehaviour
    {
        private List<Gs2ExperienceOwnStatusContext> _children;

        public void Update() {
            if (_fetcher.Fetched && this._fetcher.Statuses != null) {
                for (var i = 0; i < this.maximumItems; i++) {
                    if (i < this._fetcher.Statuses.Count) {
                        _children[i].Status.experienceName = this._fetcher.Statuses[i].ExperienceName;
                        _children[i].Status.propertyId = this._fetcher.Statuses[i].PropertyId;
                        _children[i].gameObject.SetActive(true);
                    }
                    else {
                        _children[i].gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2ExperienceOwnStatusList
    {
        private Gs2ExperienceOwnStatusListFetcher _fetcher;
        private Gs2ExperienceNamespaceContext Context => _fetcher.Context;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2ExperienceOwnStatusListFetcher>() ?? GetComponentInParent<Gs2ExperienceOwnStatusListFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2ExperienceOwnStatusListFetcher.");
                enabled = false;
            }

            _children = new List<Gs2ExperienceOwnStatusContext>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.Status = OwnStatus.New(
                    _fetcher.Context.Namespace,
                    "",
                    ""
                );
                node.gameObject.SetActive(false);
                _children.Add(node);
            }
            this.prefab.gameObject.SetActive(false);
        }

        public bool HasError()
        {
            _fetcher = GetComponent<Gs2ExperienceOwnStatusListFetcher>() ?? GetComponentInParent<Gs2ExperienceOwnStatusListFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2ExperienceOwnStatusList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2ExperienceOwnStatusList
    {
        public Gs2ExperienceOwnStatusContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2ExperienceOwnStatusList
    {

    }
}