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
using Gs2.Unity.UiKit.Gs2Datastore.Fetcher;
using UnityEngine;
using UnityEngine.Events;

namespace Gs2.Unity.UiKit.Gs2Datastore.SpriteSwitcher
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Datastore/DataObjectHistory/View/SpriteSwitcher/Properties/Generation/Gs2DatastoreDataObjectHistoryGenerationSpriteSwitcher")]
    public partial class Gs2DatastoreDataObjectHistoryGenerationSpriteSwitcher : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.DataObjectHistory != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        if (applyGenerations.Contains(_fetcher.DataObjectHistory.Generation)) {
                            this.onUpdate.Invoke(this.sprite);
                        }
                        break;
                    case Expression.NotIn:
                        if (!applyGenerations.Contains(_fetcher.DataObjectHistory.Generation)) {
                            this.onUpdate.Invoke(this.sprite);
                        }
                        break;
                    case Expression.StartsWith:
                        if (applyGeneration.StartsWith(_fetcher.DataObjectHistory.Generation)) {
                            this.onUpdate.Invoke(this.sprite);
                        }
                        break;
                    case Expression.EndsWith:
                        if (applyGeneration.EndsWith(_fetcher.DataObjectHistory.Generation)) {
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

    public partial class Gs2DatastoreDataObjectHistoryGenerationSpriteSwitcher
    {
        private Gs2DatastoreOwnDataObjectHistoryFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2DatastoreOwnDataObjectHistoryFetcher>() ?? GetComponentInParent<Gs2DatastoreOwnDataObjectHistoryFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2DatastoreOwnDataObjectHistoryFetcher.");
                enabled = false;
            }
            if (sprite == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: sprite is not set.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            _fetcher = GetComponent<Gs2DatastoreOwnDataObjectHistoryFetcher>() ?? GetComponentInParent<Gs2DatastoreOwnDataObjectHistoryFetcher>(true);
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

    public partial class Gs2DatastoreDataObjectHistoryGenerationSpriteSwitcher
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2DatastoreDataObjectHistoryGenerationSpriteSwitcher
    {
        public enum Expression {
            In,
            NotIn,
            StartsWith,
            EndsWith,
        }

        public Expression expression;

        public List<string> applyGenerations;

        public string applyGeneration;

        public Sprite sprite;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2DatastoreDataObjectHistoryGenerationSpriteSwitcher
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