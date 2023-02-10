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

using System.Collections.Generic;
using Gs2.Unity.Gs2Quest.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Quest.Context;
using Gs2.Unity.UiKit.Gs2Quest.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Quest
{
    /// <summary>
    /// Main
    /// </summary>

    [AddComponentMenu("GS2 UIKit/Quest/CompletedQuestList/View/Gs2QuestCompletedQuestListList")]
    public partial class Gs2QuestCompletedQuestListList : MonoBehaviour
    {
        private List<Gs2QuestCompletedQuestListContext> _children;

        public void Update() {
            if (_fetcher.Fetched) {
                for (var i = 0; i < this.maximumItems; i++) {
                    if (i < this._fetcher.CompletedQuestList.Count) {
                        _children[i].CompletedQuestList.questGroupName = this._fetcher.CompletedQuestList[i].QuestGroupName;
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

    public partial class Gs2QuestCompletedQuestListList
    {
        private Gs2QuestUserContext _context;
        private Gs2QuestCompletedQuestListListFetcher _fetcher;

        public void Awake()
        {
            _context = GetComponentInParent<Gs2QuestUserContext>();
            _fetcher = GetComponentInParent<Gs2QuestCompletedQuestListListFetcher>();

            _children = new List<Gs2QuestCompletedQuestListContext>();
            for (var i = 0; i < this.maximumItems; i++) {
                var node = Instantiate(this.prefab, transform);
                node.CompletedQuestList = CompletedQuestList.New(
                    _context.User,
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

    public partial class Gs2QuestCompletedQuestListList
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2QuestCompletedQuestListList
    {
        public Gs2QuestCompletedQuestListContext prefab;
        public int maximumItems;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2QuestCompletedQuestListList
    {

    }
}