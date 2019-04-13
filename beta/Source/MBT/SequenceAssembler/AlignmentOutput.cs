﻿// -------------------------------------------------------------------------------------
// <copyright file="AlignmentOutput.cs" company="Microsoft">
//  Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// <summary>
// This is a buisness object class which
// holds the output of a particular ailgnment process.
// </summary>
// -------------------------------------------------------------------------------------
namespace SequenceAssembler
{
    #region -- Using Directive --

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using MBF.Algorithms.Assembly;
    using MBF.Algorithms.Alignment;

    #endregion -- Using Directive --

    /// <summary>
    /// This is a buisness object class which
    /// holds the output of a particular alignment process.
    /// </summary>
    public class AlignmentOutput
    {
        /// <summary>
        /// Result of the alignment process
        /// </summary>
        public IList<ISequenceAlignment> AlignerResult { get; set; }

        /// <summary>
        /// Name of the aligner used
        /// </summary>
        public string AlignerName { get; set; }

        /// <summary>
        /// Number of input sequences
        /// </summary>
        public int InputSequenceCount { get; set; }

        /// <summary>
        /// Gets or sets end time of the assembly.
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Gets or sets start time of the assembly.
        /// </summary>
        public DateTime StartTime { get; set; }
    }
}