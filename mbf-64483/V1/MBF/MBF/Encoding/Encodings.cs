﻿// *****************************************************************
//    Copyright (c) Microsoft. All rights reserved.
//    This code is licensed under the Microsoft Public License.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
// *****************************************************************

using System.Collections.Generic;

namespace MBF.Encoding
{
    /// <summary>
    /// The currently supported and built-in encodings for sequence items.
    /// </summary>
    public static class Encodings
    {
        //IupacAA,
        //Iupac3AA,
        //IupacNA,
        //NcbiStdAA,
        //NcbiEaa,

        /// <summary>
        /// The NCBI2na encoding standard - For DNA and RNA nucleotides
        /// </summary>
        public static readonly IupacNAEncoding IupacNA = IupacNAEncoding.Instance;

        /// <summary>
        /// The NCBI2na encoding standard - For DNA and RNA nucleotides
        /// </summary>
        public static readonly Ncbi2NAEncoding Ncbi2NA = Ncbi2NAEncoding.Instance;

        /// <summary>
        /// The NCBI4na encoding standard - For DNA and RNA nucleotides
        /// </summary>
        public static readonly Ncbi4NAEncoding Ncbi4NA = Ncbi4NAEncoding.Instance;

        /// <summary>
        /// The NCBIeaa encoding standard - For Amino Acids
        /// </summary>
        public static readonly NcbiEAAEncoding NcbiEAA = NcbiEAAEncoding.Instance;

        /// <summary>
        /// The NCBIstdaa encoding standrad - For Amino Acids
        /// </summary>
        public static readonly NcbiStdAAEncoding NcbiStdAA = NcbiStdAAEncoding.Instance;

        /// <summary>
        /// List of all supported encodings.
        /// </summary>
        private static List<IEncoding> all = new List<IEncoding>(){
            Encodings.IupacNA,
            Encodings.Ncbi2NA,
            Encodings.Ncbi4NA,
            Encodings.NcbiEAA,
            Encodings.NcbiStdAA};

        /// <summary>
        ///  Gets the list of all encodings which is supported by the framework.
        /// </summary>
        public static IList<IEncoding> All
        {
            get
            {
                return all.AsReadOnly();
            }
        }
    }
}
