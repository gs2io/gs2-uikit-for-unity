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

    [AddComponentMenu("GS2 UIKit/Formation/FormModel/View/Gs2FormationFormModelList")]
    public partial class Gs2FormationFormModelList : MonoBehaviour
    {
        private List<Gs2FormationFormModelContext> _children;

        public void Update() {
            if (_fetcher.Fetched && this._fetcher.FormModels != null) {
                for (var i = 0; i < this.maximumItems; i++) {
                    if (i < this._fetcher.FormModels.Count) {
                        _children[i].SetFormModel(
                            FormModel.NewByFormModelName(
                                this._fetcher.Context.Namespace,
                                this._fetcher.FormModels[i].Name
                            )
                        );
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

    public partial class Gs2FormationFormModelList
    {
        private Gs2FormationFormModelListFetcher _fetcher;
        public Gs2FormationNamespaceContext Context => _fetcher.Context;

        public void Awake()
        {
            if (prefab == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2FormationFormModelContext Prefab.");
                enabled = false;
                return;
            }

            _fetcher = GetComponent<Gs2FormationFormModelListFetcher>() ?? GetComponentInParent<Gs2FormationFormModelListFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2FormationFormModelListFetcher.");
                enabled = false;
                return;
            }
            var context = GetComponent<Gs2FormationNamespaceContext>() ?? GetComponentInParent<Gs2FormationNamespaceContext>(true);
            if (context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2FormationFormModelListFetcher::Context.");
                enabled = false;
                return;
            }

            _children = new List<Gs2FormationFormModelContext>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.FormModel = FormModel.NewByFormModelName(
                    context.Namespace,
                    ""
                );
                node.gameObject.SetActive(false);
                _children.Add(node);
            }
            this.prefab.gameObject.SetActive(false);
        }

        public virtual bool HasError()
        {
            _fetcher = GetComponent<Gs2FormationFormModelListFetcher>() ?? GetComponentInParent<Gs2FormationFormModelListFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2FormationFormModelList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2FormationFormModelList
    {
        public Gs2FormationFormModelContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FormationFormModelList
    {

    }
}