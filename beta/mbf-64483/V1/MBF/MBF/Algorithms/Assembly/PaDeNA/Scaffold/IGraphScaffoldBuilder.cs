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

namespace MBF.Algorithms.Assembly.PaDeNA.Scaffold
{
    /// <summary>
    /// Generates Scaffolds using Graph.
    /// </summary>
    public interface IGraphScaffoldBuilder : IDisposable
    {
        /// <summary>
        /// Builds scaffolds from list of reads and contigs
        /// </summary>
        /// <param name="reads">List of reads</param>
        /// <param name="contigs">List of contigs</param>
        /// <param name="kmerLength">Kmer Length</param>
        /// <param name="depth">Depth for graph traversal</param>
        /// <param name="redundancy">Number of mate pairs required to create a link between two contigs.
        ///  Hierarchical Scaffolding With Bambus
        ///  by: Mihai Pop, Daniel S. Kosack, Steven L. Salzberg
        ///  Genome Research, Vol. 14, No. 1. (January 2004), pp. 149-159.</param>
        /// <returns>List of scaffold sequences</returns>
        IList<ISequence> BuildScaffold(
           IList<ISequence> reads,
           IList<ISequence> contigs,
           int kmerLength,
           int depth = 10,
           int redundancy = 2);
    }
}
