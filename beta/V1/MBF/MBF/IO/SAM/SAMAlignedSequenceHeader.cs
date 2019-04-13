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
using MBF.Properties;
using MBF.Util;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MBF.IO.SAM
{
    /// <summary>
    /// SAMAlignedSequenceHeader holds aligned sequence headers of the sam file format.
    /// </summary>
    [Serializable]
    public class SAMAlignedSequenceHeader
    {
        #region Fields
        #region Regular expressions
        /// <summary>
        /// Regular expression pattern for QName.
        /// </summary>
        private const string QNameRegxExprPattern = "[^ \t\n\r]+";

        /// <summary>
        /// Regular expression pattern for RName.
        /// </summary>
        private const string RNameRegxExprPattern = "[^ \t\n\r@=]+";

        /// <summary>
        /// Regular expression pattern for CIGAR.
        /// </summary>
        private const string CIGARRegxExprPattern = @"([0-9]+[MIDNSHP])+|\*";

        /// <summary>
        ///  Regular expression pattern for MRNM.
        /// </summary>
        private const string MRNMRegxExprPattern = @"[^ \t\n\r@]+";

        /// <summary>
        /// Represents the largest possible value of POS. This field is constant.
        /// </summary>
        private const int POS_MaxValue = 536870911; // ((int)Math.Pow(2, 29)) - 1

        /// <summary>
        /// Represents the smallest possible value of POS. This field is constant.
        /// </summary>
        private const int POS_MinValue = 0;

        /// <summary>
        /// Represents the largest possible value of MPOS. This field is constant.
        /// </summary>
        private const int MPOS_MaxValue = 536870911; // ((int)Math.Pow(2, 29)) - 1

        /// <summary>
        /// Represents the smallest possible value of MPOS. This field is constant.
        /// </summary>
        private const int MPOS_MinValue = 0;

        /// <summary>
        /// Represents the largest possible value of ISize. This field is constant.
        /// </summary>
        private const int ISize_MaxValue = 536870912; // ((int)Math.Pow(2, 29))

        /// <summary>
        /// Represents the smallest possible value of ISize. This field is constant.
        /// </summary>
        private const int ISize_MinValue = -536870912; // -((int)Math.Pow(2, 29))

        /// <summary>
        /// Represents the largest possible value of MapQ. This field is constant.
        /// </summary>
        private const int MapQ_MaxValue = 255; // ((int)Math.Pow(2, 8)) - 1

        /// <summary>
        /// Represents the smallest possible value of MapQ. This field is constant.
        /// </summary>
        private const int MapQ_MinValue = 0;

        /// <summary>
        /// Default value for read/query length.
        /// </summary>
        private const int DefaultReadLength = 0;

        /// <summary>
        /// Default value for CIGAR.
        /// </summary>
        private const string DefaultCIGAR = "*";

        /// <summary>
        /// Regular Expression object for QName.
        /// </summary>
        private static Regex QNameRegxExpr = new Regex(QNameRegxExprPattern);

        /// <summary>
        /// Regular Expression object for RName.
        /// </summary>
        private static Regex RNameRegxExpr = new Regex(RNameRegxExprPattern);

        /// <summary>
        /// Regular Expression object for CIGAR.
        /// </summary>
        private static Regex CIGARRegxExpr = new Regex(CIGARRegxExprPattern);

        /// <summary>
        /// Regular Expression object for MRNM.
        /// </summary>
        private static Regex MRNMRegxExpr = new Regex(MRNMRegxExprPattern);

        #endregion

        /// <summary>
        /// Holds Query pair name if paired; or Query name if unpaired.
        /// </summary>
        private string _qname;

        /// <summary>
        /// Holds Reference sequence name.
        /// </summary>
        private string _rname;

        /// <summary>
        /// Holds left co-ordinate of alignment.
        /// </summary>
        private int _pos;

        /// <summary>
        /// Holds MAPping Quality of alignment.
        /// </summary>
        private int _mapq;

        /// <summary>
        /// Holds Leftmost mate position of the clipped sequence.
        /// </summary>
        private int _mpos;

        /// <summary>
        /// Holds inferred insert size.
        /// </summary>
        private int _isize;

        /// <summary>
        /// Holds read (query sequence) length depending on CIGAR value.
        /// </summary>
        private int _readLength;

        /// <summary>
        /// Holds CIGAR value.
        /// </summary>
        private string _cigar;

        /// <summary>
        /// Holds Mate Reference sequence name (MRNM).
        /// </summary>
        private string _mrnm;

        /// <summary>
        /// Holds bin number.
        /// </summary>
        private int _bin;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates new SAMAlignedSequenceHeader instance.
        /// </summary>
        public SAMAlignedSequenceHeader()
        {
            OptionalFields = new List<SAMOptionalField>();
            DotSymbolIndices = new List<int>();
            EqualSymbolIndices = new List<int>();
            _cigar = DefaultCIGAR;
            _readLength = DefaultReadLength;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Query pair name if paired; or Query name if unpaired.
        /// </summary>  
        public string QName
        {
            get
            {
                return _qname;
            }
            set
            {
                string message = IsValidQName(value);
                if (!string.IsNullOrEmpty(message))
                {
                    throw new ArgumentException(message);
                }

                _qname = value;
            }
        }

        /// <summary>
        /// SAM flags.
        /// <see cref="SAMFlags"/>
        /// </summary>
        public SAMFlags Flag { get; set; }

        /// <summary>
        /// Reference sequence name.
        /// </summary>
        public string RName
        {
            get
            {
                return _rname;
            }
            set
            {
                string message = IsValidRName(value);
                if (!string.IsNullOrEmpty(message))
                {
                    throw new ArgumentException(message);
                }

                _rname = value;
            }
        }

        /// <summary>
        /// One-based leftmost position/coordinate of the aligned sequence.
        /// </summary>
        public int Pos
        {
            get
            {
                return _pos;
            }
            set
            {
                string message = IsValidPos(value);

                if (!string.IsNullOrEmpty(message))
                {
                    throw new ArgumentException(message);
                }

                _pos = value;
                _bin = GetBin();
            }
        }

        /// <summary>
        /// Mapping quality (phred-scaled posterior probability that the 
        /// mapping position of this read is incorrect).
        /// </summary>
        public int MapQ
        {
            get
            {
                return _mapq;
            }
            set
            {
                string message = IsValidMapQ(value);

                if (!string.IsNullOrEmpty(message))
                {
                    throw new ArgumentException(message);
                }

                _mapq = value;
            }
        }

        /// <summary>
        /// Extended CIGAR string.
        /// </summary>
        public string CIGAR
        {
            get
            {
                return _cigar;
            }
            set
            {
                string message = IsValidCIGAR(value);
                if (!string.IsNullOrEmpty(message))
                {
                    throw new ArgumentException(message);
                }

                _cigar = value;
                _readLength = GetReadLengthFromCIGAR();
                _bin = GetBin();
            }
        }

        /// <summary>
        /// Mate reference sequence name. 
        /// </summary>
        public string MRNM
        {
            get
            {
                return _mrnm;
            }
            set
            {
                string message = IsValidMRNM(value);
                if (!string.IsNullOrEmpty(message))
                {
                    throw new ArgumentException(message);
                }

                _mrnm = value;
            }
        }

        /// <summary>
        /// One-based leftmost mate position of the clipped sequence.
        /// </summary>
        public int MPos
        {
            get
            {
                return _mpos;
            }
            set
            {
                string message = IsValidMPos(value);

                if (!string.IsNullOrEmpty(message))
                {
                    throw new ArgumentException(message);
                }

                _mpos = value;
            }
        }

        /// <summary>
        /// Inferred insert size.
        /// </summary>
        public int ISize
        {
            get
            {
                return _isize;
            }
            set
            {
                string message = IsValidISize(value);

                if (!string.IsNullOrEmpty(message))
                {
                    throw new ArgumentException(message);
                }

                _isize = value;
            }
        }

        /// <summary>
        /// Gets the Bin depending on the POS and CIGAR.
        /// </summary>
        public int Bin
        {
            get
            {
                return _bin;
            }

            internal set
            {
                _bin = value;
            }
        }

        /// <summary>
        /// Gets the query length depending on CIGAR Value.
        /// </summary>
        public int QueryLength
        {
            get
            {
                return _readLength;
            }

            internal set
            {
                _readLength = value;
            }
        }

        /// <summary>
        /// Contains the list of indices of "." symbols present in the aligned sequence.
        /// As "." is not supported by DNA, RNA and Protien alphabets, while creating aligned 
        /// sequence "." symbols are replaced by "N" which has the same meaning of ".".
        /// </summary>
        public IList<int> DotSymbolIndices { get; private set; }

        /// <summary>
        /// Contains the list of "=" symbol indices present in the aligned sequence.
        /// The "=" symbol in aligned sequence indicates that the symbol at this index 
        /// is equal to the symbol present in the reference sequence. As "=" is not 
        /// supported by DNA, RNA and Protien alphabets, while creating aligned 
        /// sequence "=" symbols are replaced by the symbol present in the reference 
        /// sequence at the same index.
        /// </summary>
        public IList<int> EqualSymbolIndices { get; private set; }

        /// <summary>
        /// Optional fields.
        /// </summary>
        public IList<SAMOptionalField> OptionalFields { get; private set; }
        #endregion

        #region Public Static Methods
        /// <summary>
        /// Gets the SAMFlag for the specified integer value.
        /// </summary>
        /// <param name="value">Value for which the SAMFlag is required.</param>
        public static SAMFlags GetFlag(int value)
        {
            return (SAMFlags)value;
        }

        /// <summary>
        /// Gets the SAMFlag for the specified string value.
        /// </summary>
        /// <param name="value">Value for which the SAMFlag is required.</param>
        public static SAMFlags GetFlag(string value)
        {
            return GetFlag(int.Parse(value));
        }

        /// <summary>
        /// Gets Bin for the specified region.
        /// Note that this method returns zero for negative values.
        /// </summary>
        /// <param name="start">Zero based start co-ordinate of alignment.</param>
        /// <param name="end">Zero based end co-ordinate of the alignment.</param>
        public static int RegionToBin(int start, int end)
        {
            if (start < 0 || end <= 0)
            {
                return 0;
            }

            --end;
            if (start >> 14 == end >> 14) return ((1 << 15) - 1) / 7 + (start >> 14);
            if (start >> 17 == end >> 17) return ((1 << 12) - 1) / 7 + (start >> 17);
            if (start >> 20 == end >> 20) return ((1 << 9) - 1) / 7 + (start >> 20);
            if (start >> 23 == end >> 23) return ((1 << 6) - 1) / 7 + (start >> 23);
            if (start >> 26 == end >> 26) return ((1 << 3) - 1) / 7 + (start >> 26);
            return 0;
        }
        #endregion

        #region Private Methods
      
        /// <summary>
        /// Validates QName.
        /// </summary>
        /// <param name="qname">QName value to validate.</param>
        private static string IsValidQName(string qname)
        {
            string headerName = "QName";
            string headerValue = qname;
            // Validate for the regx.
            string message = Helper.IsValidPatternValue(headerName, headerValue, QNameRegxExpr);

            if (!string.IsNullOrEmpty(message))
            {
                return message;
            }

            // validate length.
            if (headerValue.Length > 254)
            {
                return Resource.InvalidQNameLength;
            }

            return string.Empty;
        }

        /// <summary>
        /// Validates RName.
        /// </summary>
        /// <param name="rname">RName value to validate.</param>
        private static string IsValidRName(string rname)
        {
            string headerName = "RName";
            // Validate for the regx.
            return Helper.IsValidPatternValue(headerName, rname, RNameRegxExpr);
        }

        /// <summary>
        /// Validates Pos.
        /// </summary>
        /// <param name="pos">Position value to validate.</param>
        private static string IsValidPos(int pos)
        {
            string headerName = "Pos";
            return Helper.IsValidRange(headerName, pos, POS_MinValue, POS_MaxValue);
        }

        /// <summary>
        /// Validates MapQ.
        /// </summary>
        /// <param name="mapq">MapQ value to validate.</param>
        private static string IsValidMapQ(int mapq)
        {
            string headerName = "MapQ";
            return Helper.IsValidRange(headerName, mapq, MapQ_MinValue, MapQ_MaxValue);
        }

        /// <summary>
        /// Validates CIGAR.
        /// </summary>
        /// <param name="cigar">CIGAR value to validate.</param>
        private static string IsValidCIGAR(string cigar)
        {
            string headerName = "RName";
            // Validate for the regx.
            return Helper.IsValidPatternValue(headerName, cigar, CIGARRegxExpr);
        }

        /// <summary>
        /// Validates MRNM.
        /// </summary>
        /// <param name="mrnm">MRNM value to validate.</param>
        private static string IsValidMRNM(string mrnm)
        {
            string headerName = "MRNM";
            // Validate for the regx.
            return Helper.IsValidPatternValue(headerName, mrnm, MRNMRegxExpr);
        }

        /// <summary>
        /// Validates MPos.
        /// </summary>
        /// <param name="mpos">MPOS value to validate.</param>
        private static string IsValidMPos(int mpos)
        {
            string headerName = "MPos";
            return Helper.IsValidRange(headerName, mpos, MPOS_MinValue, MPOS_MaxValue);
        }

        /// <summary>
        /// Validates ISize.
        /// </summary>
        /// <param name="isize">ISIZE value to validate.</param>
        private static string IsValidISize(int isize)
        {
            string headerName = "ISize";
            return Helper.IsValidRange(headerName, isize, ISize_MinValue, ISize_MaxValue);
        }

        /// <summary>
        /// Returns the bin number.
        /// </summary>
        private int GetBin()
        {
            // As SAM stores 1 based position and to calculte BAM Bin zero based positions are required.
            int start = Pos - 1;
            int end = start + _readLength - 1;
            return RegionToBin(start, end);
        }

        /// <summary>
        /// Gets the read sequence length depending on the CIGAR value.
        /// </summary>
        /// <returns>Length of the read.</returns>
        private int GetReadLengthFromCIGAR()
        {
            if (string.IsNullOrWhiteSpace(CIGAR) || CIGAR.Equals("*"))
            {
                return DefaultReadLength;
            }

            List<char> chars = new List<char>();
            Dictionary<int, int> dic = new Dictionary<int, int>();

            for (int i = 0; i < CIGAR.Length; i++)
            {
                char ch = CIGAR[i];
                if (Char.IsDigit(ch))
                {
                    continue;
                }

                chars.Add(ch);
                dic.Add(chars.Count - 1, i);
            }

            string CIGARforClen = "MDN";
            int len = 0;
            for (int i = 0; i < chars.Count; i++)
            {
                char ch = chars[i];
                int start = 0;
                int end = 0;
                if (CIGARforClen.Contains(ch))
                {
                    if (i == 0)
                    {
                        start = 0;
                    }
                    else
                    {
                        start = dic[i - 1] + 1;
                    }

                    end = dic[i] - start;

                    len += int.Parse(CIGAR.Substring(start, end), CultureInfo.InvariantCulture);
                }
            }

            return len;
        }
        #endregion
    }
}
