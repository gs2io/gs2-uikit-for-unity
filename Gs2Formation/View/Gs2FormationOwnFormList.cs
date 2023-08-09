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

    [AddComponentMenu("GS2 UIKit/Formation/Form/View/Gs2FormationOwnFormList")]
    public partial class Gs2FormationOwnFormList : MonoBehaviour
    {
        private List<Gs2FormationOwnFormContext> _children;

        public void Update() {
            if (_fetcher.Fetched && this._fetcher.Forms != null) {
                for (var i = 0; i < this.maximumItems; i++) {
                    if (i < this._fetcher.Forms.Count) {
                        _children[i].SetOwnForm(
                            OwnForm.New(
                                this._fetcher.Context.Mold,
                                this._fetcher.Forms[i].Index
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

    public partial class Gs2FormationOwnFormList
    {
        private Gs2FormationOwnFormListFetcher _fetcher;
        private Gs2FormationOwnMoldContext Context => _fetcher.Context;

        public void Awake()
        {
            if (prefab == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2FormationOwnFormContext Prefab.");
                enabled = false;
                return;
            }

            _fetcher = GetComponent<Gs2FormationOwnFormListFetcher>() ?? GetComponentInParent<Gs2FormationOwnFormListFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2FormationOwnFormListFetcher.");
                enabled = false;
            }

            var context = GetComponent<Gs2FormationOwnMoldContext>() ?? GetComponentInParent<Gs2FormationOwnMoldContext>(true);
            if (context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2FormationOwnFormListFetcher::Context.");
                enabled = false;
                return;
            }

            _children = new List<Gs2FormationOwnFormContext>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.Form = OwnForm.New(
                    context.Mold,
                    0
                );
                node.gameObject.SetActive(false);
                _children.Add(node);
            }
            this.prefab.gameObject.SetActive(false);
        }

        public virtual bool HasError()
        {
            _fetcher = GetComponent<Gs2FormationOwnFormListFetcher>() ?? GetComponentInParent<Gs2FormationOwnFormListFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2FormationOwnFormList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2FormationOwnFormList
    {
        public Gs2FormationOwnFormContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FormationOwnFormList
    {

    }
}