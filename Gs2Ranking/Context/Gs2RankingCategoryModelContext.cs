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

using Gs2.Unity.Gs2Ranking.ScriptableObject;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Ranking.Context
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Ranking/CategoryModel/Context/Gs2RankingCategoryModelContext")]
    public partial class Gs2RankingCategoryModelContext : MonoBehaviour
    {
        public void Start() {
            if (CategoryModel == null) {
                Debug.LogError("CategoryModel is not set in Gs2RankingCategoryModelContext.");
            }
        }

        public bool HasError() {
            if (CategoryModel == null) {
                if (transform.parent != null && transform.parent.GetComponent<Gs2RankingCategoryModelList>() != null) {
                    return false;
                }
                else {
                    return true;
                }
            }
            return false;
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2RankingCategoryModelContext
    {

    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2RankingCategoryModelContext
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2RankingCategoryModelContext
    {
        public CategoryModel CategoryModel;

        public void SetCategoryModel(CategoryModel CategoryModel) {
            this.CategoryModel = CategoryModel;
        }
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2RankingCategoryModelContext
    {

    }
}