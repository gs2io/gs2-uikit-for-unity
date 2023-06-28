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
using Gs2.Gs2Quest.Request;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Quest.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Quest.Label
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Quest/Progress/View/Label/Transaction/Gs2QuestDeleteProgressByUserIdLabel")]
    public partial class Gs2QuestDeleteProgressByUserIdLabel : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Request != null &&
                    _userDataFetcher != null && _userDataFetcher.Fetched && _userDataFetcher.Progress != null) {
                {
                    onUpdate?.Invoke(
                        format.Replace(
                            "{namespaceName}",
                            $"{_fetcher.Request.NamespaceName}"
                        ).Replace(
                            "{userId}",
                            $"{_fetcher.Request.UserId}"
                        ).Replace(
                            "{userData:progressId}",
                            $"{_userDataFetcher.Progress.ProgressId}"
                        ).Replace(
                            "{userData:transactionId}",
                            $"{_userDataFetcher.Progress.TransactionId}"
                        ).Replace(
                            "{userData:questModelId}",
                            $"{_userDataFetcher.Progress.QuestModelId}"
                        ).Replace(
                            "{userData:randomSeed}",
                            $"{_userDataFetcher.Progress.RandomSeed}"
                        ).Replace(
                            "{userData:rewards}",
                            $"{_userDataFetcher.Progress.Rewards}"
                        )
                    );
                }
            } else if (_fetcher.Fetched && _fetcher.Request != null) {
                {
                    onUpdate?.Invoke(
                        format.Replace(
                            "{namespaceName}",
                            $"{_fetcher.Request.NamespaceName}"
                        ).Replace(
                            "{userId}",
                            $"{_fetcher.Request.UserId}"
                        )
                    );
                }
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2QuestDeleteProgressByUserIdLabel
    {
        private Gs2QuestDeleteProgressByUserIdFetcher _fetcher;
        private Gs2QuestOwnProgressFetcher _userDataFetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2QuestDeleteProgressByUserIdFetcher>() ?? GetComponentInParent<Gs2QuestDeleteProgressByUserIdFetcher>();
            _userDataFetcher = GetComponent<Gs2QuestOwnProgressFetcher>() ?? GetComponentInParent<Gs2QuestOwnProgressFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2QuestDeleteProgressByUserIdFetcher.");
                enabled = false;
            }

            Update();
        }

        public bool HasError()
        {
            _fetcher = GetComponent<Gs2QuestDeleteProgressByUserIdFetcher>() ?? GetComponentInParent<Gs2QuestDeleteProgressByUserIdFetcher>(true);
            _userDataFetcher = GetComponent<Gs2QuestOwnProgressFetcher>() ?? GetComponentInParent<Gs2QuestOwnProgressFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2QuestDeleteProgressByUserIdLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2QuestDeleteProgressByUserIdLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2QuestDeleteProgressByUserIdLabel
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