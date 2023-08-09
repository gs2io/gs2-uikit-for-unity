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
using Gs2.Unity.UiKit.Gs2SerialKey.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2SerialKey.SpriteSwitcher
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/SerialKey/SerialKey/View/SpriteSwitcher/Properties/Status/Gs2SerialKeySerialKeyStatusSpriteSwitcher")]
    public partial class Gs2SerialKeySerialKeyStatusSpriteSwitcher : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.SerialKey != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        if (applyStatuses.Contains(_fetcher.SerialKey.Status)) {
                            this.onUpdate.Invoke(this.sprite);
                        }
                        break;
                    case Expression.NotIn:
                        if (!applyStatuses.Contains(_fetcher.SerialKey.Status)) {
                            this.onUpdate.Invoke(this.sprite);
                        }
                        break;
                    case Expression.StartsWith:
                        if (applyStatus.StartsWith(_fetcher.SerialKey.Status)) {
                            this.onUpdate.Invoke(this.sprite);
                        }
                        break;
                    case Expression.EndsWith:
                        if (applyStatus.EndsWith(_fetcher.SerialKey.Status)) {
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

    public partial class Gs2SerialKeySerialKeyStatusSpriteSwitcher
    {
        private Gs2SerialKeyOwnSerialKeyFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2SerialKeyOwnSerialKeyFetcher>() ?? GetComponentInParent<Gs2SerialKeyOwnSerialKeyFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2SerialKeyOwnSerialKeyFetcher.");
                enabled = false;
            }
            if (sprite == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: sprite is not set.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            _fetcher = GetComponent<Gs2SerialKeyOwnSerialKeyFetcher>() ?? GetComponentInParent<Gs2SerialKeyOwnSerialKeyFetcher>(true);
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

    public partial class Gs2SerialKeySerialKeyStatusSpriteSwitcher
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2SerialKeySerialKeyStatusSpriteSwitcher
    {
        public enum Expression {
            In,
            NotIn,
            StartsWith,
            EndsWith,
        }

        public Expression expression;

        public List<string> applyStatuses;

        public string applyStatus;

        public Sprite sprite;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2SerialKeySerialKeyStatusSpriteSwitcher
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