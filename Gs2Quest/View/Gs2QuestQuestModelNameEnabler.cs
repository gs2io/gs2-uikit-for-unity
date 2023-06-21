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
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Quest.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Quest
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Quest/QuestModel/View/Properties/Name/Gs2QuestQuestModelNameEnabler")]
    public partial class Gs2QuestQuestModelNameEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.QuestModel != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableNames.Contains(_fetcher.QuestModel.Name));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableNames.Contains(_fetcher.QuestModel.Name));
                        break;
                    case Expression.StartsWith:
                        target.SetActive(enableName.StartsWith(_fetcher.QuestModel.Name));
                        break;
                    case Expression.EndsWith:
                        target.SetActive(enableName.EndsWith(_fetcher.QuestModel.Name));
                        break;
                }
            }
            else
            {
                target.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2QuestQuestModelNameEnabler
    {
        private Gs2QuestQuestModelFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2QuestQuestModelFetcher>() ?? GetComponentInParent<Gs2QuestQuestModelFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2QuestQuestModelFetcher.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2QuestQuestModelNameEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2QuestQuestModelNameEnabler
    {
        public enum Expression {
            In,
            NotIn,
            StartsWith,
            EndsWith,
        }

        public Expression expression;

        public List<string> enableNames;

        public string enableName;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2QuestQuestModelNameEnabler
    {
        
    }
}