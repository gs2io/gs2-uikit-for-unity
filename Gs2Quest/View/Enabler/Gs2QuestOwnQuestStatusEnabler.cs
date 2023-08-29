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

using System.Linq;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Quest.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Quest
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Quest/QuestModel/View/Enabler/Gs2QuestQuestStatusEnabler")]
    public partial class Gs2QuestOwnQuestStatusEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (!this._completedQuestListFetcher.Fetched 
                || !this._questModelFetcher.Fetched
                || this._questModelFetcher.QuestModel == null)
            {
                target.SetActive(loading);
            }
            else {
                var completedQuestNames = this._completedQuestListFetcher.CompletedQuestList.CompleteQuestNames;
                var premiseQuestNames = this._questModelFetcher.QuestModel.PremiseQuestNames;
                if (premiseQuestNames.Count(v => completedQuestNames.Contains(v)) != premiseQuestNames.Count) {
                    target.SetActive(this.cantChallenge);
                }
                else {
                    if (completedQuestNames.Contains(this._questModelFetcher.QuestModel.Name)) {
                        target.SetActive(this.completed);
                    }
                    else {
                        target.SetActive(this.notCompleted);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2QuestOwnQuestStatusEnabler
    {
        private Gs2QuestOwnCompletedQuestListFetcher _completedQuestListFetcher;
        private Gs2QuestQuestModelFetcher _questModelFetcher;

        public void Awake()
        {
            this._completedQuestListFetcher = GetComponent<Gs2QuestOwnCompletedQuestListFetcher>() ?? GetComponentInParent<Gs2QuestOwnCompletedQuestListFetcher>();
            this._questModelFetcher = GetComponent<Gs2QuestQuestModelFetcher>() ?? GetComponentInParent<Gs2QuestQuestModelFetcher>();

            if (this._completedQuestListFetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2QuestOwnCompletedQuestListFetcher.");
                enabled = false;
            }
            if (this._questModelFetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2QuestQuestModelFetcher.");
                enabled = false;
            }
            if (target == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: target is not set.");
                enabled = false;
            }
        }

        public bool HasError()
        {
            this._completedQuestListFetcher = GetComponent<Gs2QuestOwnCompletedQuestListFetcher>() ?? GetComponentInParent<Gs2QuestOwnCompletedQuestListFetcher>();
            this._questModelFetcher = GetComponent<Gs2QuestQuestModelFetcher>() ?? GetComponentInParent<Gs2QuestQuestModelFetcher>(true);
            if (this._completedQuestListFetcher == null) {
                return true;
            }
            if (this._questModelFetcher == null) {
                return true;
            }
            if (target == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2QuestOwnQuestStatusEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2QuestOwnQuestStatusEnabler
    {
        public bool loading;
        public bool cantChallenge;
        public bool notCompleted;
        public bool completed;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2QuestOwnQuestStatusEnabler
    {

    }
}