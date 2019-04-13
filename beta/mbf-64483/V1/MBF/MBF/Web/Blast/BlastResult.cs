﻿// *****************************************************************
//    Copyright (c) Microsoft. All rights reserved.
//    This code is licensed under the Microsoft Public License.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
// *****************************************************************

using System.Collections.Generic;

namespace MBF.Web.Blast
{
    /// <summary>
    /// A single BLAST search result. This is represented by a single XML
    /// document in BLAST XML format. It consist of some introductory information
    /// such as BLAST version, a structure listing the search parameters, and
    /// a list of Iterations (represented in the BlastSearchRecord class).
    /// </summary>
    public class BlastResult
    {
        #region Fields

        /// <summary>
        /// List of BlastSearchRecords in the document.
        /// </summary>
        private IList<BlastSearchRecord> _records = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public BlastResult()
        {
            _records = new List<BlastSearchRecord>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// The summary data for the search
        /// </summary>
        public BlastXmlMetadata Metadata { get; set; }

        /// <summary>
        /// The list of BlastSearchRecords in the document.
        /// </summary>
        public IList<BlastSearchRecord> Records 
        {
            get { return _records; }
        }

        #endregion

        #region Methods
        #endregion
    }
}
