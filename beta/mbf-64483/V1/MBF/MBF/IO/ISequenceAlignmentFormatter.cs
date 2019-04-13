﻿// *****************************************************************
//    Copyright (c) Microsoft. All rights reserved.
//    This code is licensed under the Microsoft Public License.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
// *****************************************************************

using System.Collections.Generic;
using System.IO;
using MBF.Algorithms.Alignment;

namespace MBF.IO
{
    /// <summary>
    /// Implementations of this interface write an ISequenceAlignment to a particular location, usually a
    /// file. The output is formatted according to the particular file format. A method is
    /// also provided for quickly accessing the content in string form for applications that do not
    /// need to first write to file.
    /// </summary>
    public interface ISequenceAlignmentFormatter : IFormatter
    {
        /// <summary>
        /// Writes an ISequenceAlignment to the location specified by the writer.
        /// </summary>
        /// <param name="sequenceAlignment">The sequence alignment to format.</param>
        /// <param name="writer">The TextWriter used to write the formatted sequence alignment text.</param>
        void Format(ISequenceAlignment sequenceAlignment, TextWriter writer);

        /// <summary>
        /// Writes an ISequenceAlignment to the specified file.
        /// </summary>
        /// <param name="sequenceAlignment">The sequence alignment to format.</param>
        /// <param name="filename">The name of the file to write the formatted sequence alignment text.</param>
        void Format(ISequenceAlignment sequenceAlignment, string filename);

        /// <summary>
        /// Write a collection of ISequenceAlignments to a writer.
        /// </summary>
        /// <param name="sequenceAlignments">The sequence alignments to write.</param>
        /// <param name="writer">The TextWriter used to write the formatted sequence alignments.</param>
        void Format(ICollection<ISequenceAlignment> sequenceAlignments, TextWriter writer);

        /// <summary>
        /// Write a collection of ISequenceAlignments to a file.
        /// </summary>
        /// <param name="sequenceAlignments">The sequenceAlignments to write.</param>
        /// <param name="filename">The name of the file to write the formatted sequence alignments.</param>
        void Format(ICollection<ISequenceAlignment> sequenceAlignments, string filename);

        /// <summary>
        /// Converts an ISequenceAlignment to a formatted string.
        /// </summary>
        /// <param name="sequenceAlignment">The sequence alignment to format.</param>
        /// <returns>A string of the formatted text.</returns>
        string FormatString(ISequenceAlignment sequenceAlignment);
    }
}
