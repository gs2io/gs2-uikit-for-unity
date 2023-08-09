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
using Gs2.Unity.UiKit.Gs2Idle.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Idle
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Idle/Status/View/SpriteSwitcher/Properties/IdleMinutes/Gs2IdleStatusIdleMinutesSpriteSwitcher")]
    public partial class Gs2IdleStatusIdleMinutesSpriteSwitcher : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.Status != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        if (applyIdleMinuteses.Contains(_fetcher.Status.IdleMinutes)) {
                            this.onUpdate.Invoke(this.sprite);
                        }
                        break;
                    case Expression.NotIn:
                        if (!applyIdleMinuteses.Contains(_fetcher.Status.IdleMinutes)) {
                            this.onUpdate.Invoke(this.sprite);
                        }
                        break;
                    case Expression.Less:
                        if (applyIdleMinutes > _fetcher.Status.IdleMinutes) {
                            this.onUpdate.Invoke(this.sprite);
                        }
                        break;
                    case Expression.LessEqual:
                        if (applyIdleMinutes >= _fetcher.Status.IdleMinutes) {
                            this.onUpdate.Invoke(this.sprite);
                        }
                        break;
                    case Expression.Greater:
                        if (applyIdleMinutes < _fetcher.Status.IdleMinutes) {
                            this.onUpdate.Invoke(this.sprite);
                        }
                        break;
                    case Expression.GreaterEqual:
                        if (applyIdleMinutes <= _fetcher.Status.IdleMinutes) {
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

    public partial class Gs2IdleStatusIdleMinutesSpriteSwitcher
    {
        private Gs2IdleOwnStatusFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2IdleOwnStatusFetcher>() ?? GetComponentInParent<Gs2IdleOwnStatusFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2IdleOwnStatusFetcher.");
                enabled = false;
            }
            if (sprite == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: sprite is not set.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            _fetcher = GetComponent<Gs2IdleOwnStatusFetcher>() ?? GetComponentInParent<Gs2IdleOwnStatusFetcher>(true);
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

    public partial class Gs2IdleStatusIdleMinutesSpriteSwitcher
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2IdleStatusIdleMinutesSpriteSwitcher
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

        public List<int> applyIdleMinuteses;

        public int applyIdleMinutes;

        public Sprite sprite;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2IdleStatusIdleMinutesSpriteSwitcher
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