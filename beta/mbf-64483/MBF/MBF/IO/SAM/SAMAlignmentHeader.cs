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
using System.Globalization;
using System.Linq;
using MBF.Properties;

namespace MBF.IO.SAM
{
    /// <summary>
    /// Class to hold SAM Headers.
    /// </summary>
    [Serializable]
    public class SAMAlignmentHeader
    {
        /// <summary>
        /// Holds the mapping of record field types to its mandaroty tags.
        /// This will be used in the IsValid() method to validate the sepcified SAMAlignmentHeader.
        /// </summary>
        public static Dictionary<string, IList<string>> MandatoryTagsForFieldTypes;

        #region Constructors
        /// <summary>
        /// Static constructor.
        /// </summary>
        static SAMAlignmentHeader()
        {
            MandatoryTagsForFieldTypes = new Dictionary<string, IList<string>>();
            List<string> allowedTags = new List<string>();

            #region Mandatory tags for HD Record type.
            // File format version.
            allowedTags.Add("VN");
            MandatoryTagsForFieldTypes.Add("HD", allowedTags);
            #endregion

            #region Mandatory tags for SQ Record type.
            allowedTags = new List<string>();
            // Sequence name. Unique among all sequence records in the file. 
            // The value of this field is used in alignment records.
            allowedTags.Add("SN");

            // Sequence length.
            allowedTags.Add("LN");
            MandatoryTagsForFieldTypes.Add("SQ", allowedTags);
            #endregion

            #region Mandatory tags for RG Record type.
            allowedTags = new List<string>();
            // Unique read group identifier. The value of the ID field is used
            // in the RG tags of alignment records.
            allowedTags.Add("ID");

            // Sample (use pool name where a pool is being sequenced).
            allowedTags.Add("SM");
            MandatoryTagsForFieldTypes.Add("RG", allowedTags);
            #endregion

            #region Mandatory tags for PG Record type.
            allowedTags = new List<string>();
            // Program Name
            allowedTags.Add("ID");
            MandatoryTagsForFieldTypes.Add("PG", allowedTags);
            #endregion
        }

        /// <summary>
        /// Creates SAMAlignmentHeader instance.
        /// </summary>
        public SAMAlignmentHeader()
        {
            Comments = new List<string>();
            RecordFields = new List<SAMRecordField>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// List of record fields.
        /// It holds all available record fields except comments.
        /// </summary>
        public IList<SAMRecordField> RecordFields { get; private set; }

        /// <summary>
        /// List of comment headers.
        /// </summary>
        public IList<string> Comments { get; private set; }
        #endregion

        #region Public Methods
        /// <summary>
        /// VAlidates mandatory tags.
        /// </summary>
        /// <returns>Returns empty string if mandatory tags are present; otherwise error message.</returns>
        public string IsValid()
        {
            if (RecordFields.Count == 0)
            {
                return string.Empty;
            }

            List<SAMRecordField> fieldsToValidate = RecordFields.Where(F => MandatoryTagsForFieldTypes.Keys.Contains(F.Typecode,
                                                    StringComparer.InvariantCultureIgnoreCase)).ToList();

            foreach (SAMRecordField field in fieldsToValidate)
            {
                foreach (string tag in MandatoryTagsForFieldTypes[field.Typecode])
                {
                    if (field.Tags.FirstOrDefault(T => string.Compare(T.Tag, tag, StringComparison.InvariantCultureIgnoreCase) == 0) == null)
                    {
                        return string.Format(CultureInfo.CurrentCulture, Resource.MandatoryTagNotFound, tag, field.Typecode);
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Returns list of SequenceRanges objects which represents reference sequences present in this header. 
        /// </summary>
        public IList<SequenceRange> GetReferenceSequenceRanges()
        {
            List<SequenceRange> ranges = new List<SequenceRange>();
            List<SAMRecordField> fields = RecordFields.Where(R => String.Compare(R.Typecode, "SQ", StringComparison.OrdinalIgnoreCase) == 0).ToList();
            foreach (SAMRecordField field in fields)
            {
                SAMRecordFieldTag tag = field.Tags.FirstOrDefault(F => String.Compare(F.Tag, "SN", StringComparison.OrdinalIgnoreCase) == 0);
                if (tag != null)
                {
                    string refName = tag.Value;
                    long length;
                    tag = field.Tags.FirstOrDefault(F => String.Compare(F.Tag, "LN", StringComparison.OrdinalIgnoreCase) == 0);
                    if (tag != null && long.TryParse(tag.Value, out length))
                    {
                        SequenceRange range = new SequenceRange(refName, 0, length);
                        ranges.Add(range);
                    }
                }
            }

            return ranges.OrderBy(S => S.ID).ToList();
        }

        /// <summary>
        /// Returns list of reference sequences present in this header. 
        /// </summary>
        public IList<string> GetReferenceSequences()
        {
            List<string> refSequences = new List<string>();
            List<SAMRecordField> fields = RecordFields.Where(R => String.Compare(R.Typecode, "SQ", StringComparison.OrdinalIgnoreCase) == 0).ToList();
            foreach (SAMRecordField field in fields)
            {
                SAMRecordFieldTag tag = field.Tags.FirstOrDefault(F => String.Compare(F.Tag, "SN", StringComparison.OrdinalIgnoreCase) == 0);
                if (tag != null)
                {
                    refSequences.Add(tag.Value);
                }
            }

            refSequences.Sort();

            return refSequences;
        }
        #endregion
    }
}
