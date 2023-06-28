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

    [AddComponentMenu("GS2 UIKit/Formation/Mold/View/Gs2FormationOwnMoldList")]
    public partial class Gs2FormationOwnMoldList : MonoBehaviour
    {
        private List<Gs2FormationOwnMoldContext> _children;

        public void Update() {
            if (_fetcher.Fetched && _fetcher.Molds != null) {
                for (var i = 0; i < this.maximumItems; i++) {
                    if (i < this._fetcher.Molds.Count) {
                        _children[i].MoldModel.moldName = this._fetcher.Molds[i].Name;
                        _children[i].Mold.moldName = this._fetcher.Molds[i].Name;
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

    public partial class Gs2FormationOwnMoldList
    {
        private Gs2FormationNamespaceContext _context;
        private Gs2FormationOwnMoldListFetcher _fetcher;

        public void Awake()
        {
            _context = GetComponent<Gs2FormationNamespaceContext>() ?? GetComponentInParent<Gs2FormationNamespaceContext>();
            _fetcher = GetComponent<Gs2FormationOwnMoldListFetcher>() ?? GetComponentInParent<Gs2FormationOwnMoldListFetcher>();

            if (_context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2FormationNamespaceContext.");
                enabled = false;
            }
            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2FormationOwnMoldListFetcher.");
                enabled = false;
            }

            _children = new List<Gs2FormationOwnMoldContext>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.MoldModel = MoldModel.New(
                    _context.Namespace,
                    ""
                );
                node.Mold = OwnMold.New(
                    _context.Namespace,
                    ""
                );
                node.gameObject.SetActive(false);
                _children.Add(node);
            }
            this.prefab.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2FormationOwnMoldList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2FormationOwnMoldList
    {
        public Gs2FormationOwnMoldContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2FormationOwnMoldList
    {

    }
}