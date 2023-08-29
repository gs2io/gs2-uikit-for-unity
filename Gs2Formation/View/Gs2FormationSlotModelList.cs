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

    [AddComponentMenu("GS2 UIKit/Formation/SlotModel/View/Gs2FormationSlotModelList")]
    public partial class Gs2FormationSlotModelList : MonoBehaviour
    {
        private List<Gs2FormationSlotModelContext> _children;

        public void Update() {
            if (_fetcher.Fetched && this._fetcher.SlotModels != null) {
                for (var i = 0; i < this.maximumItems; i++) {
                    if (i < this._fetcher.SlotModels.Count) {
                        _children[i].SetSlotModel(
                            SlotModel.New(
                                this._fetcher.Context.FormModel,
                                this._fetcher.SlotModels[i].Name
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

    public partial class Gs2FormationSlotModelList
    {
        private Gs2FormationSlotModelListFetcher _fetcher;
        public Gs2FormationFormModelContext Context => _fetcher.Context;

        public void Awake()
        {
            if (prefab == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2FormationSlotModelContext Prefab.");
                enabled = false;
                return;
            }

            _fetcher = GetComponent<Gs2FormationSlotModelListFetcher>() ?? GetComponentInParent<Gs2FormationSlotModelListFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2FormationSlotModelListFetcher.");
                enabled = false;
                return;
            }
            var context = GetComponent<Gs2FormationFormModelContext>() ?? GetComponentInParent<Gs2FormationFormModelContext>(true);
            if (context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2FormationSlotModelListFetcher::Context.");
                enabled = false;
                return;
            }

            _children = new List<Gs2FormationSlotModelContext>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.SlotModel = SlotModel.New(
                    context.FormModel,
                    ""
                );
                node.gameObject.SetActive(false);
                _children.Add(node);
            }
            this.prefab.gameObject.SetActive(false);
        }

        public virtual bool HasError()
        {
            _fetcher = GetComponent<Gs2FormationSlotModelListFetcher>() ?? GetComponentInParent<Gs2FormationSlotModelListFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2FormationSlotModelList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2FormationSlotModelList
    {
        public Gs2FormationSlotModelContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FormationSlotModelList
    {

    }
}