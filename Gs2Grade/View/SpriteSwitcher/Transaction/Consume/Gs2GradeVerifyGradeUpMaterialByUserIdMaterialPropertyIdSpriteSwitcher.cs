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
using System.Linq;
using System.Collections.Generic;
using Gs2.Gs2Grade.Request;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Core.Fetcher;
using Gs2.Unity.UiKit.Gs2Grade.Fetcher;
using Gs2.Util.LitJson;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Grade.SpriteSwitcher
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Grade/Status/View/SpriteSwitcher/Transaction/Properties/MaterialPropertyId/Gs2GradeVerifyGradeUpMaterialByUserIdMaterialPropertyIdSpriteSwitcher")]
    public partial class Gs2GradeVerifyGradeUpMaterialByUserIdMaterialPropertyIdSpriteSwitcher : MonoBehaviour
    {
        private void OnFetched()
        {
            switch(this.expression)
            {
                case Expression.In:
                    if (this.applyMaterialPropertyIds.Contains(this._fetcher.Request?.MaterialPropertyId ?? "")) {
                        this.onUpdate.Invoke(this.sprite);
                    }
                    break;
                case Expression.NotIn:
                    if (!this.applyMaterialPropertyIds.Contains(this._fetcher.Request?.MaterialPropertyId ?? "")) {
                        this.onUpdate.Invoke(this.sprite);
                    }
                    break;
                case Expression.StartsWith:
                    if ((this._fetcher.Request?.MaterialPropertyId ?? "").StartsWith(this.applyMaterialPropertyId)) {
                        this.onUpdate.Invoke(this.sprite);
                    }
                    break;
                case Expression.EndsWith:
                    if ((this._fetcher.Request?.MaterialPropertyId ?? "").EndsWith(this.applyMaterialPropertyId)) {
                        this.onUpdate.Invoke(this.sprite);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2GradeVerifyGradeUpMaterialByUserIdMaterialPropertyIdSpriteSwitcher
    {
        private Gs2GradeVerifyGradeUpMaterialByUserIdFetcher _fetcher;

        public void Awake()
        {
            this._fetcher = GetComponent<Gs2GradeVerifyGradeUpMaterialByUserIdFetcher>() ?? GetComponentInParent<Gs2GradeVerifyGradeUpMaterialByUserIdFetcher>();
            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2GradeVerifyGradeUpMaterialByUserIdFetcher.");
                enabled = false;
            }
            if (this.sprite == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: sprite is not set.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._fetcher = GetComponent<Gs2GradeVerifyGradeUpMaterialByUserIdFetcher>() ?? GetComponentInParent<Gs2GradeVerifyGradeUpMaterialByUserIdFetcher>(true);
            if (this._fetcher == null) {
                return true;
            }
            if (this.sprite == null) {
                return true;
            }
            return false;
        }

        private UnityAction _onFetched;

        public void OnEnable()
        {
            this._onFetched = () =>
            {
                OnFetched();
            };
            this._fetcher.OnFetched.AddListener(this._onFetched);
            if (this._fetcher.Fetched) {
                OnFetched();
            }
        }

        public void OnDisable()
        {
            if (this._onFetched != null) {
                this._fetcher.OnFetched.RemoveListener(this._onFetched);
                this._onFetched = null;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2GradeVerifyGradeUpMaterialByUserIdMaterialPropertyIdSpriteSwitcher
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2GradeVerifyGradeUpMaterialByUserIdMaterialPropertyIdSpriteSwitcher
    {
        public enum Expression {
            In,
            NotIn,
            StartsWith,
            EndsWith,
        }

        public Expression expression;

        public List<string> applyMaterialPropertyIds;

        public string applyMaterialPropertyId;

        public Sprite sprite;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2GradeVerifyGradeUpMaterialByUserIdMaterialPropertyIdSpriteSwitcher
    {
        [Serializable]
        private class UpdateEvent : UnityEvent<Sprite>
        {

        }

        [SerializeField]
        private UpdateEvent onUpdate = new UpdateEvent();

        public event UnityAction<Sprite> OnUpdate
        {
            add => this.onUpdate.AddListener(value);
            remove => this.onUpdate.RemoveListener(value);
        }
    }
}