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
using Gs2.Unity.Gs2Quest.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Quest.Context;
using Gs2.Unity.UiKit.Gs2Quest.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Quest
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Quest/QuestGroupModel/View/Gs2QuestQuestGroupModelList")]
    public partial class Gs2QuestQuestGroupModelList : MonoBehaviour
    {
        private List<Gs2QuestQuestGroupModelContext> _children;

        public void Update() {
            if (_fetcher.Fetched && this._fetcher.QuestGroupModels != null) {
                for (var i = 0; i < this.maximumItems; i++) {
                    if (i < this._fetcher.QuestGroupModels.Count) {
                        _children[i].QuestGroupModel.questGroupName = this._fetcher.QuestGroupModels[i].Name;
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

    public partial class Gs2QuestQuestGroupModelList
    {
        private Gs2QuestQuestGroupModelListFetcher _fetcher;
        public Gs2QuestNamespaceContext Context => _fetcher.Context;

        public void Awake()
        {
            if (prefab == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2QuestQuestGroupModelContext Prefab.");
                enabled = false;
                return;
            }

            _fetcher = GetComponent<Gs2QuestQuestGroupModelListFetcher>() ?? GetComponentInParent<Gs2QuestQuestGroupModelListFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2QuestQuestGroupModelListFetcher.");
                enabled = false;
                return;
            }
            var context = GetComponent<Gs2QuestNamespaceContext>() ?? GetComponentInParent<Gs2QuestNamespaceContext>(true);
            if (context == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2QuestQuestGroupModelListFetcher::Context.");
                enabled = false;
                return;
            }

            _children = new List<Gs2QuestQuestGroupModelContext>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.QuestGroupModel = QuestGroupModel.New(
                    context.Namespace,
                    ""
                );
                node.gameObject.SetActive(false);
                _children.Add(node);
            }
            this.prefab.gameObject.SetActive(false);
        }

        public bool HasError()
        {
            _fetcher = GetComponent<Gs2QuestQuestGroupModelListFetcher>() ?? GetComponentInParent<Gs2QuestQuestGroupModelListFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2QuestQuestGroupModelList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2QuestQuestGroupModelList
    {
        public Gs2QuestQuestGroupModelContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2QuestQuestGroupModelList
    {

    }
}