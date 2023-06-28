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

using System.Collections.Generic;
using Gs2.Unity.UiKit.Core;
using Gs2.Unity.UiKit.Gs2Datastore.Fetcher;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Datastore
{
    /// <summary>
    /// Main
    /// </summary>

	[AddComponentMenu("GS2 UIKit/Datastore/DataObjectHistory/View/Enabler/Properties/ContentLength/Gs2DatastoreDataObjectHistoryContentLengthEnabler")]
    public partial class Gs2DatastoreDataObjectHistoryContentLengthEnabler : MonoBehaviour
    {
        public void Update()
        {
            if (_fetcher.Fetched && _fetcher.DataObjectHistory != null)
            {
                switch(expression)
                {
                    case Expression.In:
                        target.SetActive(enableContentLengths.Contains(_fetcher.DataObjectHistory.ContentLength));
                        break;
                    case Expression.NotIn:
                        target.SetActive(!enableContentLengths.Contains(_fetcher.DataObjectHistory.ContentLength));
                        break;
                    case Expression.Less:
                        target.SetActive(enableContentLength > _fetcher.DataObjectHistory.ContentLength);
                        break;
                    case Expression.LessEqual:
                        target.SetActive(enableContentLength >= _fetcher.DataObjectHistory.ContentLength);
                        break;
                    case Expression.Greater:
                        target.SetActive(enableContentLength < _fetcher.DataObjectHistory.ContentLength);
                        break;
                    case Expression.GreaterEqual:
                        target.SetActive(enableContentLength <= _fetcher.DataObjectHistory.ContentLength);
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

    public partial class Gs2DatastoreDataObjectHistoryContentLengthEnabler
    {
        private Gs2DatastoreOwnDataObjectHistoryFetcher _fetcher;

        public void Awake()
        {
            _fetcher = GetComponent<Gs2DatastoreOwnDataObjectHistoryFetcher>() ?? GetComponentInParent<Gs2DatastoreOwnDataObjectHistoryFetcher>();

            if (_fetcher == null) {
                Debug.LogError($"{gameObject.GetFullPath()}: Couldn't find the Gs2DatastoreOwnDataObjectHistoryFetcher.");
                enabled = false;
            }
        }
    }

    /// <summary>
    /// Public properties
    /// </summary>

    public partial class Gs2DatastoreDataObjectHistoryContentLengthEnabler
    {

    }

    /// <summary>
    /// Parameters for Inspector
    /// </summary>

    public partial class Gs2DatastoreDataObjectHistoryContentLengthEnabler
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

        public List<long> enableContentLengths;

        public long enableContentLength;

        public GameObject target;
    }

    /// <summary>
    /// Event handlers
    /// </summary>
    public partial class Gs2DatastoreDataObjectHistoryContentLengthEnabler
    {
        
    }
}