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
using Gs2.Core.Util;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2SkillTree.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2SkillTree
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/SkillTree/NodeModel/View/Label/Gs2SkillTreeNodeModelLabel")]
    public partial class Gs2SkillTreeNodeModelLabel : MonoBehaviour
    {
        private void OnFetched()
        {
            this.onUpdate?.Invoke(
                this.format.Replace(
                    "{name}", $"{this._fetcher?.NodeModel?.Name}"
                ).Replace(
                    "{metadata}", $"{this._fetcher?.NodeModel?.Metadata}"
                ).Replace(
                    "{releaseVerifyActions}", $"{this._fetcher?.NodeModel?.ReleaseVerifyActions}"
                ).Replace(
                    "{releaseConsumeActions}", $"{this._fetcher?.NodeModel?.ReleaseConsumeActions}"
                ).Replace(
                    "{returnAcquireActions}", $"{this._fetcher?.NodeModel?.ReturnAcquireActions}"
                ).Replace(
                    "{restrainReturnRate}", $"{this._fetcher?.NodeModel?.RestrainReturnRate}"
                )
            );
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2SkillTreeNodeModelLabel
    {
        private Gs2SkillTreeNodeModelFetcher _fetcher;

        public void Awake()
        {
            this._fetcher = GetComponent<Gs2SkillTreeNodeModelFetcher>() ?? GetComponentInParent<Gs2SkillTreeNodeModelFetcher>();
            if (this._fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2SkillTreeNodeModelFetcher.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            this._fetcher = GetComponent<Gs2SkillTreeNodeModelFetcher>() ?? GetComponentInParent<Gs2SkillTreeNodeModelFetcher>(true);
            if (this._fetcher == null) {
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

    public partial class Gs2SkillTreeNodeModelLabel
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2SkillTreeNodeModelLabel
    {
        public string format;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2SkillTreeNodeModelLabel
    {
        [Serializable]
        private class UpdateEvent : UnityEvent<string>
        {

        }

        [SerializeField]
        private UpdateEvent onUpdate = new UpdateEvent();

        public event UnityAction<string> OnUpdate
        {
            add => this.onUpdate.AddListener(value);
            remove => this.onUpdate.RemoveListener(value);
        }
    }
}