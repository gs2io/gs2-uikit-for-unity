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
using System.Collections.Generic;
using System.Linq;
using Gs2.Unity.Core;
using Gs2.Unity.Gs2Quest.ScriptableObject;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Core.Consume;
using Gs2.Unity.UiKit.Gs2Quest.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Quest.Context
{
    [AddComponentMenu("GS2 UIKit/Quest/QuestModel/Convert/Gs2QuestConvertQuestModelConsumeActionsToConsumeActions")]
    public class Gs2QuestConvertQuestModelConsumeActionsToConsumeActions : MonoBehaviour
    {
        private Gs2QuestQuestModelFetcher _fetcher;

        private int _callbackCount;
        public int count = 1;

        public void Awake() {
            _fetcher = GetComponentInParent<Gs2QuestQuestModelFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2QuestQuestModelFetcher.");
                enabled = false;
            }
        }

        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.QuestModel != null && _callbackCount != count)
            {
                this.onConverted.Invoke(
                    _fetcher.QuestModel.ConsumeActions.Select(v => ConsumeAction.New(
                        v.Action,
                        v.Request
                    ) * count).ToList()
                );
                _callbackCount = count;
            }
        }

        public void SetCount(int count)
        {
            this.count = count;
        }

        [Serializable]
        private class ConvertEvent : UnityEvent<List<ConsumeAction>>
        {

        }

        [SerializeField]
        private ConvertEvent onConverted = new ConvertEvent();

        public event UnityAction<List<ConsumeAction>> OnConvert
        {
            add => onConverted.AddListener(value);
            remove => onConverted.RemoveListener(value);
        }
    }
}