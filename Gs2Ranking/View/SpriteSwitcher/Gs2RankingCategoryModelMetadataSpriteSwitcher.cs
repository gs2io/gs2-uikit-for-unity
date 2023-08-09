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

using System;
using System.Collections.Generic;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Ranking.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Ranking.SpriteSwitcher
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Ranking/CategoryModel/View/SpriteSwitcher/Properties/Metadata/Gs2RankingCategoryModelMetadataSpriteSwitcher")]
    public partial class Gs2RankingCategoryModelMetadataSpriteSwitcher : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.CategoryModel != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        if (applyMetadatas.Contains(_fetcher.CategoryModel.Metadata)) {
                            this.onUpdate.Invoke(this.sprite);
                        }
                        break;
                    case Expression.NotIn:
                        if (!applyMetadatas.Contains(_fetcher.CategoryModel.Metadata)) {
                            this.onUpdate.Invoke(this.sprite);
                        }
                        break;
                    case Expression.StartsWith:
                        if (applyMetadata.StartsWith(_fetcher.CategoryModel.Metadata)) {
                            this.onUpdate.Invoke(this.sprite);
                        }
                        break;
                    case Expression.EndsWith:
                        if (applyMetadata.EndsWith(_fetcher.CategoryModel.Metadata)) {
                            this.onUpdate.Invoke(this.sprite);
                        }
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2RankingCategoryModelMetadataSpriteSwitcher
    {
        private Gs2RankingCategoryModelFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2RankingCategoryModelFetcher>() ?? GetComponentInParent<Gs2RankingCategoryModelFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2RankingCategoryModelFetcher.");
                enabled = false;
            }
            if (sprite == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: sprite is not set.");
                enabled = false;
            }
        }

        public bool HasError()
        {
            _fetcher = GetComponent<Gs2RankingCategoryModelFetcher>() ?? GetComponentInParent<Gs2RankingCategoryModelFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            if (sprite == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2RankingCategoryModelMetadataSpriteSwitcher
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2RankingCategoryModelMetadataSpriteSwitcher
    {
        public enum Expression {
            In,
            NotIn,
            StartsWith,
            EndsWith,
        }

        public Expression expression;

        public List<string> applyMetadatas;

        public string applyMetadata;

        public Sprite sprite;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2RankingCategoryModelMetadataSpriteSwitcher
    {
        [Serializable]
        private class UpdateEvent : UnityEvent<Sprite>
        {

        }

        [SerializeField]
        private UpdateEvent onUpdate = new UpdateEvent();

        public event UnityAction<Sprite> OnUpdate
        {
            add => onUpdate.AddListener(value);
            remove => onUpdate.RemoveListener(value);
        }
    }
}