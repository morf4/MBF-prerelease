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

namespace MBF.Phylogenetics
{
    /// <summary>
    /// Tree: The full input Newick Format for a single tree
    /// Tree --> Subtree ";" | Branch ";"
    /// </summary>
    public class Tree : ICloneable
    {
        #region -- Member Variables --
        #endregion  -- Member Variables --

        #region -- Constructors --
        /// <summary>
        /// default constructor
        /// </summary>
        public Tree()
        {
        }
        #endregion -- Constructors --

        #region -- Properties --
        /// <summary>
        /// Name of the tree
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Root of the tree
        /// </summary>
        public Node Root { get; set; }
        #endregion -- Properties --

        #region -- Methods --
        /// <summary>
        /// Clone object
        /// </summary>
        /// <returns>Tree as object</returns>
        public object Clone()
        {
            Tree newTree = (Tree)this.MemberwiseClone();
            return newTree;
        }
        #endregion -- Methods --
    }

}
