﻿// *****************************************************************
//    Copyright (c) Microsoft. All rights reserved.
//    This code is licensed under the Microsoft Public License.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
// *****************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBF.Algorithms.Alignment
{
    /// <summary>
    /// An ordered list of clusters between two sequences A and B
    /// </summary>
    public class Synteny
    {
        /// <summary>
        /// Reference sequence
        /// </summary>
        private ISequence _referenceSequence;

        /// <summary>
        /// Query sequence
        /// </summary>
        private ISequence _querySequence;

        /// <summary>
        /// List of clusters
        /// </summary>
        private IList<Cluster> _clusters;

        /// <summary>
        /// Initializes a new instance of the Synteny class
        /// </summary>
        /// <param name="referenceSequence">Reference sequence</param>
        /// <param name="querySequence">Query sequence</param>
        public Synteny(
                ISequence referenceSequence,
                ISequence querySequence)
        {
            _referenceSequence = referenceSequence;
            _querySequence = querySequence;
            _clusters = new List<Cluster>();
        }

        /// <summary>
        /// Gets reference sequence
        /// </summary>
        public ISequence ReferenceSequence
        {
            get { return _referenceSequence; }
        }

        /// <summary>
        /// Gets query sequence
        /// </summary>
        public ISequence QuerySequence
        {
            get { return _querySequence; }
        }

        /// <summary>
        /// Gets list of clusters
        /// </summary>
        public IList<Cluster> Clusters
        {
            get { return _clusters; }
        }
    }
}