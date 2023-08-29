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

using System.Collections.Generic;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Datastore.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Datastore.Enabler
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Datastore/DataObject/View/Enabler/Properties/Scope/Gs2DatastoreOwnDataObjectScopeEnabler")]
    public partial class Gs2DatastoreOwnDataObjectScopeEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.DataObject != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableScopes.Contains(_fetcher.DataObject.Scope));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableScopes.Contains(_fetcher.DataObject.Scope));
                        break;
                    case Expression.StartsWith:
                        target.SetActive(_fetcher.DataObject.Scope.StartsWith(enableScope));
                        break;
                    case Expression.EndsWith:
                        target.SetActive(_fetcher.DataObject.Scope.EndsWith(enableScope));
                        break;
                }
            }
            else
            {
                target.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Dependent components
    /// </summary>

    public partial class Gs2DatastoreOwnDataObjectScopeEnabler
    {
        private Gs2DatastoreOwnDataObjectFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2DatastoreOwnDataObjectFetcher>() ?? GetComponentInParent<Gs2DatastoreOwnDataObjectFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2DatastoreOwnDataObjectFetcher.");
                enabled = false;
            }
            if (target == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: target is not set.");
                enabled = false;
            }
        }

        public virtual bool HasError()
        {
            _fetcher = GetComponent<Gs2DatastoreOwnDataObjectFetcher>() ?? GetComponentInParent<Gs2DatastoreOwnDataObjectFetcher>(true);
            if (_fetcher == null) {
                return true;
            }
            if (target == null) {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2DatastoreOwnDataObjectScopeEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2DatastoreOwnDataObjectScopeEnabler
    {
        public enum Expression {
            In,
            NotIn,
            StartsWith,
            EndsWith,
        }

        public Expression expression;

        public List<string> enableScopes;

        public string enableScope;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2DatastoreOwnDataObjectScopeEnabler
    {
        
    }
}