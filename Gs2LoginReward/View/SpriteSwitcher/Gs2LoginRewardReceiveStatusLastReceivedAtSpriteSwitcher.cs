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
using Gs2.Unity.UiKit.Gs2LoginReward.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2LoginReward
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/LoginReward/ReceiveStatus/View/SpriteSwitcher/Properties/LastReceivedAt/Gs2LoginRewardReceiveStatusLastReceivedAtSpriteSwitcher")]
    public partial class Gs2LoginRewardReceiveStatusLastReceivedAtSpriteSwitcher : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.ReceiveStatus != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        if (applyLastReceivedAts.Contains(_fetcher.ReceiveStatus.LastReceivedAt)) {
                            this.onUpdate.Invoke(this.sprite);
                        }
                        break;
                    case Expression.NotIn:
                        if (!applyLastReceivedAts.Contains(_fetcher.ReceiveStatus.LastReceivedAt)) {
                            this.onUpdate.Invoke(this.sprite);
                        }
                        break;
                    case Expression.Less:
                        if (applyLastReceivedAt > _fetcher.ReceiveStatus.LastReceivedAt) {
                            this.onUpdate.Invoke(this.sprite);
                        }
                        break;
                    case Expression.LessEqual:
                        if (applyLastReceivedAt >= _fetcher.ReceiveStatus.LastReceivedAt) {
                            this.onUpdate.Invoke(this.sprite);
                        }
                        break;
                    case Expression.Greater:
                        if (applyLastReceivedAt < _fetcher.ReceiveStatus.LastReceivedAt) {
                            this.onUpdate.Invoke(this.sprite);
                        }
                        break;
                    case Expression.GreaterEqual:
                        if (applyLastReceivedAt <= _fetcher.ReceiveStatus.LastReceivedAt) {
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

    public partial class Gs2LoginRewardReceiveStatusLastReceivedAtSpriteSwitcher
    {
        private Gs2LoginRewardOwnReceiveStatusFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2LoginRewardOwnReceiveStatusFetcher>() ?? GetComponentInParent<Gs2LoginRewardOwnReceiveStatusFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2LoginRewardOwnReceiveStatusFetcher.");
                enabled = false;
            }
            if (sprite == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: sprite is not set.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            _fetcher = GetComponent<Gs2LoginRewardOwnReceiveStatusFetcher>() ?? GetComponentInParent<Gs2LoginRewardOwnReceiveStatusFetcher>(true);
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

    public partial class Gs2LoginRewardReceiveStatusLastReceivedAtSpriteSwitcher
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2LoginRewardReceiveStatusLastReceivedAtSpriteSwitcher
    {
        public enum Expression {
            In,
            NotIn,
            Less,
            LessEqual,
            Greater,
            GreaterEqual,
        }

        public Expression expression;

        public List<long> applyLastReceivedAts;

        public long applyLastReceivedAt;

        public Sprite sprite;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2LoginRewardReceiveStatusLastReceivedAtSpriteSwitcher
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