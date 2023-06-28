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
using Gs2.Unity.Gs2Ranking.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Ranking.Context;
using Gs2.Unity.UiKit.Gs2Ranking.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Ranking
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Ranking/CategoryModel/View/Gs2RankingCategoryModelList")]
    public partial class Gs2RankingCategoryModelList : MonoBehaviour
    {
        private List<Gs2RankingCategoryModelContext> _children;

        public void Update() {
            if (_fetcher.Fetched && this._fetcher.CategoryModels != null) {
                for (var i = 0; i < this.maximumItems; i++) {
                    if (i < this._fetcher.CategoryModels.Count) {
                        _children[i].CategoryModel.categoryName = this._fetcher.CategoryModels[i].Name;
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

    public partial class Gs2RankingCategoryModelList
    {
        private Gs2RankingNamespaceContext _context;
        private Gs2RankingCategoryModelListFetcher _fetcher;

        public void Awake()
        {
            _context = GetComponent<Gs2RankingNamespaceContext>() ?? GetComponentInParent<Gs2RankingNamespaceContext>();
            _fetcher = GetComponent<Gs2RankingCategoryModelListFetcher>() ?? GetComponentInParent<Gs2RankingCategoryModelListFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2RankingCategoryModelListFetcher.");
                enabled = false;
            }

            _children = new List<Gs2RankingCategoryModelContext>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.CategoryModel = CategoryModel.New(
                    _context.Namespace,
                    ""
                );
                node.gameObject.SetActive(false);
                _children.Add(node);
            }
            this.prefab.gameObject.SetActive(false);
        }

        public bool HasError()
        {
            _context = GetComponent<Gs2RankingNamespaceContext>() ?? GetComponentInParent<Gs2RankingNamespaceContext>(true);
            _fetcher = GetComponent<Gs2RankingCategoryModelListFetcher>() ?? GetComponentInParent<Gs2RankingCategoryModelListFetcher>(true);
            if (_context == null) {
                return true;
            }
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2RankingCategoryModelList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2RankingCategoryModelList
    {
        public Gs2RankingCategoryModelContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2RankingCategoryModelList
    {

    }
}