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
 *
 * deny overwrite
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
using Gs2.Unity.Gs2Formation.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Formation.Context;
using Gs2.Unity.UiKit.Gs2Formation.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Formation
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Formation/PropertyForm/View/Gs2FormationOwnPropertyFormList")]
    public partial class Gs2FormationOwnPropertyFormList : MonoBehaviour
    {
        private List<Gs2FormationOwnPropertyFormContext> _children;

        public void Update() {
            if (_fetcher.Fetched && this._fetcher.PropertyForms != null) {
                for (var i = 0; i < this.maximumItems; i++) {
                    if (i < this._fetcher.PropertyForms.Count) {
                        _children[i].PropertyForm.formModelName = this._fetcher.PropertyForms[i].Name;
                        _children[i].PropertyForm.propertyId = this._fetcher.PropertyForms[i].PropertyId;
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

    public partial class Gs2FormationOwnPropertyFormList
    {
        private Gs2FormationOwnPropertyFormListFetcher _fetcher;
        private Gs2FormationFormModelContext Context => _fetcher.Context;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2FormationOwnPropertyFormListFetcher>() ?? GetComponentInParent<Gs2FormationOwnPropertyFormListFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2FormationOwnPropertyFormListFetcher.");
                enabled = false;
            }

            _children = new List<Gs2FormationOwnPropertyFormContext>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.PropertyForm = OwnPropertyForm.New(
                    _fetcher.Context.FormModel.Namespace,
                    _fetcher.Context.FormModel.FormModelName,
                    ""
                );
                node.gameObject.SetActive(false);
                _children.Add(node);
            }
            this.prefab.gameObject.SetActive(false);
        }

        public bool HasError()
        {
            _fetcher = GetComponent<Gs2FormationOwnPropertyFormListFetcher>() ?? GetComponentInParent<Gs2FormationOwnPropertyFormListFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2FormationOwnPropertyFormList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2FormationOwnPropertyFormList
    {
        public Gs2FormationOwnPropertyFormContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FormationOwnPropertyFormList
    {

    }
}