﻿// *****************************************************************
//    Copyright (c) Microsoft. All rights reserved.
//    This code is licensed under the Microsoft Public License.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
// *****************************************************************

using System;

namespace MBF.IO.SAM
{
    /// <summary>
    /// This class holds tag persent in the header lines.
    /// For example, consider the following header line.
    /// @HD	VN:1.0
    /// In this example VN:1.0 is the SAMRecordFieldTag.
    /// Where VN is stored in Tag property and 1.0 is stored 
    /// in the value property of this class.
    /// </summary>
    [Serializable]
    public class SAMRecordFieldTag
    {
        /// <summary>
        /// Creates new SAMRecordFieldTag instance.
        /// </summary>
        /// <param name="tag">Record field tag.</param>
        /// <param name="value">Record field value.</param>
        public SAMRecordFieldTag(string tag, string value)
        {
            Tag = tag;
            Value = value;
        }

        /// <summary>
        /// Record field tag.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Record field tag.
        /// </summary>
        public string Value { get; set; }
    }
}
