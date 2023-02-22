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
 *
 * deny overwrite
 */
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable CheckNamespace

using System;
using System.Collections.Generic;
using System.Linq;
using Gs2.Unity.Gs2Inbox.ScriptableObject;
using Gs2.Unity.UiKit.Core.Acquire;
using Gs2.Unity.UiKit.Gs2Inbox.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Inbox.Context
{
    [AddComponentMenu("GS2 UIKit/Inbox/Message/Convert/Gs2InboxConvertMessageReadAcquireActionsToAcquireActions")]
    public class Gs2InboxConvertMessageReadAcquireActionsToAcquireActions : MonoBehaviour
    {
        private Gs2InboxOwnMessageFetcher _fetcher;

        public void Awake() {
            _fetcher = GetComponentInParent<Gs2InboxOwnMessageFetcher>();
        }

        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Message != null)
            {
                this.onConverted.Invoke(
                    _fetcher.Message.ReadAcquireActions.Select(v => AcquireAction.New(
                        v.Action,
                        v.Request
                    )).ToList()
                );
                enabled = false;
            }
        }

        [Serializable]
        private class ConvertEvent : UnityEvent<List<AcquireAction>>
        {

        }

        [SerializeField]
        private ConvertEvent onConverted = new ConvertEvent();

        public event UnityAction<List<AcquireAction>> OnConvert
        {
            add => onConverted.AddListener(value);
            remove => onConverted.RemoveListener(value);
        }
    }
}