﻿// *****************************************************************
//    Copyright (c) Microsoft. All rights reserved.
//    This code is licensed under the Microsoft Public License.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
// *****************************************************************

namespace MBF.Algorithms.Assembly
{
    /// <summary>
    /// Stores Information of Library
    /// </summary>
    public struct CloneLibraryInformation
    {
        /// <summary>
        /// Gets or sets name of library
        /// </summary>
        public string LibraryName { get; set; }

        /// <summary>
        /// Gets or sets mean length of Insert
        /// </summary>
        public float MeanLengthOfInsert { get; set; }

        /// <summary>
        /// Gets or sets standard deviation of length of inserts
        /// </summary>
        public float StandardDeviationOfInsert { get; set; }

        /// <summary>
        /// Overrides == Operator
        /// </summary>
        /// <param name="obj1">First Input Object</param>
        /// <param name="obj2">Second Input Object</param>
        /// <returns>Result of reference comparison</returns>
        public static bool operator ==(CloneLibraryInformation obj1, CloneLibraryInformation obj2)
        {
            if (System.Object.ReferenceEquals(obj1, obj2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Overrides != Operator
        /// </summary>
        /// <param name="obj1">First Input Object</param>
        /// <param name="obj2">Second Input Object</param>
        /// <returns>Result of reference comparison</returns>
        public static bool operator !=(CloneLibraryInformation obj1, CloneLibraryInformation obj2)
        {
            return !(obj1 == obj2);
        }
        /// <summary>
        /// Override Equals method
        /// </summary>
        /// <param name="obj">Input Object</param>
        /// <returns>Result of comparison</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            CloneLibraryInformation info = (CloneLibraryInformation)obj;
            return ((LibraryName == info.LibraryName) && (MeanLengthOfInsert == info.MeanLengthOfInsert)
                && (StandardDeviationOfInsert == info.StandardDeviationOfInsert));
        }

        /// <summary>
        /// Returns the Hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}