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
using System.IO;
using System.Linq;
using System.Text;

using MBF;

namespace MBF.IO
{
    /// <summary>
    /// Writes out SequenceRange lists or groupings to a file.
    /// </summary>
    public interface ISequenceRangeFormatter
    {
        /// <summary>
        /// Writes out a list of ISequenceRange objects to a specified
        /// file location.
        /// </summary>
        void Format(IList<ISequenceRange> ranges, string fileName);

        /// <summary>
        /// Writes out a list of ISequenceRange objects to a specified
        /// text writer.
        /// </summary>
        void Format(IList<ISequenceRange> ranges, TextWriter writer);

        /// <summary>
        /// Writes out a grouping of ISequenceRange objects to a specified
        /// file location.
        /// </summary>
        void Format(SequenceRangeGrouping rangeGroup, string fileName);

        /// <summary>
        /// Writes out a grouping of ISequenceRange objects to a specified
        /// text writer.
        /// </summary>
        void Format(SequenceRangeGrouping rangeGroup, TextWriter writer);

        /// <summary>
        /// Gets the name of the sequence range formatter being
        /// implemented. This is intended to give the
        /// developer some information of the formatter type.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the description of the sequence range formatter being
        /// implemented. This is intended to give the
        /// developer some information of the formatter.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets the file extensions that the formatter implementation
        /// will support.
        /// </summary>
        string FileTypes { get; }
    }
}
