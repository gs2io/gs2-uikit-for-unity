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

using System;
using Gs2.Core.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Quest.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Quest
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Quest/QuestModel/View/Gs2QuestQuestModelLabel")]
    public partial class Gs2QuestQuestModelLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched)
            {
                onUpdate.Invoke(
                    format.Replace(
                        "{questModelId}", _fetcher.QuestModel.QuestModelId.ToString()
                    ).Replace(
                        "{name}", _fetcher.QuestModel.Name.ToString()
                    ).Replace(
                        "{metadata}", _fetcher.QuestModel.Metadata.ToString()
                    ).Replace(
                        "{contents}", _fetcher.QuestModel.Contents.ToString()
                    ).Replace(
                        "{challengePeriodEventId}", _fetcher.QuestModel.ChallengePeriodEventId.ToString()
                    ).Replace(
                        "{firstCompleteAcquireActions}", _fetcher.QuestModel.FirstCompleteAcquireActions.ToString()
                    ).Replace(
                        "{consumeActions}", _fetcher.QuestModel.ConsumeActions.ToString()
                    ).Replace(
                        "{failedAcquireActions}", _fetcher.QuestModel.FailedAcquireActions.ToString()
                    ).Replace(
                        "{premiseQuestNames}", _fetcher.QuestModel.PremiseQuestNames.ToString()
                    )
                );
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2QuestQuestModelLabel
    {
        private Gs2QuestQuestModelFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponentInParent<Gs2QuestQuestModelFetcher>();
            Update();
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2QuestQuestModelLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2QuestQuestModelLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2QuestQuestModelLabel
    {
        [Serializable]
        private class UpdateEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private UpdateEvent onUpdate = new UpdateEvent();

        public event UnityAction<string> OnUpdate
        {
            add => onUpdate.AddListener(value);
            remove => onUpdate.RemoveListener(value);
        }
    }
}