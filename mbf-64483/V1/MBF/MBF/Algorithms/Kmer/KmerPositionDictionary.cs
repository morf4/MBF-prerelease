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
using System.Threading.Tasks;
using MBF.Algorithms.Assembly.Graph;
using MBF.Properties;
using MBF.Util;

namespace MBF.Algorithms.Kmer
{
    /// <summary>
    /// Wrapper for dictionary that maps kmer strings 
    /// to list of positions of occurance. 
    /// </summary>
    public class KmerPositionDictionary
    {
        /// <summary>
        /// Maps kmer to list of positions of occurance 
        /// </summary>
        Dictionary<string, IList<int>> _kmerDictionary = new Dictionary<string, IList<int>>();

        /// <summary>
        /// Returns an enumerator that iterates through the 
        /// kmer and corresponding list of positions
        /// </summary>
        /// <returns>Enumerator over kmers</returns>
        public Dictionary<string, IList<int>>.Enumerator GetEnumerator()
        {
            return _kmerDictionary.GetEnumerator();
        }

        /// <summary>
        /// Determines whether kmer dictionary contains specified key.
        /// </summary>
        /// <param name="key">The key to locate</param>
        /// <returns>Boolean indicating if key exists</returns>
        public bool ContainsKey(string key)
        {
            return _kmerDictionary.ContainsKey(key);
        }

        /// <summary>
        /// Gets or sets the value associated with the specified key
        /// </summary>
        /// <param name="key">The key of the value to get or set</param>
        /// <returns>The value associated with the specified key</returns>
        public IList<int> this[string key]
        {
            get { return _kmerDictionary[key]; }
            set { _kmerDictionary[key] = value; }
        }
    }
}