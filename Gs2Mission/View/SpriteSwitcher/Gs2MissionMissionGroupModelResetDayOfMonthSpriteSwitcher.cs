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
using Gs2.Unity.UiKit.Gs2Mission.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Mission
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Mission/MissionGroupModel/View/SpriteSwitcher/Properties/ResetDayOfMonth/Gs2MissionMissionGroupModelResetDayOfMonthSpriteSwitcher")]
    public partial class Gs2MissionMissionGroupModelResetDayOfMonthSpriteSwitcher : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.MissionGroupModel != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        if (applyResetDayOfMonths.Contains(_fetcher.MissionGroupModel.ResetDayOfMonth)) {
                            this.onUpdate.Invoke(this.sprite);
                        }
                        break;
                    case Expression.NotIn:
                        if (!applyResetDayOfMonths.Contains(_fetcher.MissionGroupModel.ResetDayOfMonth)) {
                            this.onUpdate.Invoke(this.sprite);
                        }
                        break;
                    case Expression.Less:
                        if (applyResetDayOfMonth > _fetcher.MissionGroupModel.ResetDayOfMonth) {
                            this.onUpdate.Invoke(this.sprite);
                        }
                        break;
                    case Expression.LessEqual:
                        if (applyResetDayOfMonth >= _fetcher.MissionGroupModel.ResetDayOfMonth) {
                            this.onUpdate.Invoke(this.sprite);
                        }
                        break;
                    case Expression.Greater:
                        if (applyResetDayOfMonth < _fetcher.MissionGroupModel.ResetDayOfMonth) {
                            this.onUpdate.Invoke(this.sprite);
                        }
                        break;
                    case Expression.GreaterEqual:
                        if (applyResetDayOfMonth <= _fetcher.MissionGroupModel.ResetDayOfMonth) {
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

    public partial class Gs2MissionMissionGroupModelResetDayOfMonthSpriteSwitcher
    {
        private Gs2MissionMissionGroupModelFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2MissionMissionGroupModelFetcher>() ?? GetComponentInParent<Gs2MissionMissionGroupModelFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2MissionMissionGroupModelFetcher.");
                enabled = false;
            }
            if (sprite == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: sprite is not set.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            _fetcher = GetComponent<Gs2MissionMissionGroupModelFetcher>() ?? GetComponentInParent<Gs2MissionMissionGroupModelFetcher>(true);
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

    public partial class Gs2MissionMissionGroupModelResetDayOfMonthSpriteSwitcher
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2MissionMissionGroupModelResetDayOfMonthSpriteSwitcher
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

        public List<int> applyResetDayOfMonths;

        public int applyResetDayOfMonth;

        public Sprite sprite;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2MissionMissionGroupModelResetDayOfMonthSpriteSwitcher
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