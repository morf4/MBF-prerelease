﻿// *****************************************************************
//    Copyright (c) Microsoft. All rights reserved.
//    This code is licensed under the Microsoft Public License.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
// *****************************************************************

using System.Collections.Generic;

namespace MBF.Algorithms.SuffixTree
{
    /// <summary>
    /// Implementation of ISuffixTree
    ///     Represents a Suffix Tree object
    /// </summary>
    public class SequenceSuffixTree
    {
        /// <summary>
        /// List of Edges in Suffix Tree
        /// </summary>
        private Dictionary<int, Edge> _edges = null;

        /// <summary>
        /// Sequence of Suffix Tree
        /// </summary>
        private ISequence _sequence = null;

        /// <summary>
        /// Initializes a new instance of the SuffixTree class
        /// </summary>
        /// <param name="sequence">Input sequence</param>
        public SequenceSuffixTree(ISequence sequence)
        {
            _sequence = sequence;
            _edges = new Dictionary<int, Edge>();
        }

        /// <summary>
        /// Gets list of Edges in Suffix Tree
        /// </summary>
        public Dictionary<int, Edge> Edges
        {
            get { return _edges; }
        }

        /// <summary>
        /// Gets sequence of Suffix Tree
        /// </summary>
        public ISequence Sequence
        {
            get { return _sequence; }
        }
    }
}