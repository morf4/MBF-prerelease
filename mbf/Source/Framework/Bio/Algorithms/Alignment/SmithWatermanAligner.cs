﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using Bio.Util.Logging;

namespace Bio.Algorithms.Alignment
{
    /// <summary>
    /// Implements the SmithWaterman algorithm for partial alignment.
    /// See Chapter 2 in Biological Sequence Analysis; Durbin, Eddy, Krogh and Mitchison; 
    /// Cambridge Press; 1998.
    /// </summary>
    public class SmithWatermanAligner : DynamicProgrammingPairwiseAligner
    {
         /// <summary>
        /// Gets the name of the current Alignment algorithm used.
        /// This is a overridden property from the abstract parent.
        /// This property returns the Name of our algorithm i.e 
        /// Smith-Waterman algorithm.
        /// </summary>
        public override string Name
        {
            get
            {
                return Properties.Resource.SMITH_NAME;
            }
        }

        /// <summary>
        /// Gets the Description of the current Alignment algorithm used.
        /// This is a overriden property from the abstract parent.
        /// This property returns a simple description of what 
        /// SmithWatermanAligner class implements.
        /// </summary>
        public override string Description
        {
            get
            {
                return Properties.Resource.SMITH_DESCRIPTION;
            }
        }

        /// <summary>
        /// Creates the Simple aligner job
        /// </summary>
        /// <param name="sequenceA">First aligned sequence</param>
        /// <param name="sequenceB">Second aligned sequence</param>
        /// <returns></returns>
        protected override DynamicProgrammingPairwiseAlignerJob CreateSimpleAlignmentJob(ISequence sequenceA, ISequence sequenceB)
        {
            return new SmithWatermanSimpleAlignmentJob(this.SimilarityMatrix, this.GapOpenCost, sequenceA, sequenceB);
        }

        /// <summary>
        /// Creates the Affine aligner job
        /// </summary>
        /// <param name="sequenceA">First aligned sequence</param>
        /// <param name="sequenceB">Second aligned sequence</param>
        /// <returns></returns>
        protected override DynamicProgrammingPairwiseAlignerJob CreateAffineAlignmentJob(ISequence sequenceA, ISequence sequenceB)
        {
            return new SmithWatermanAffineAlignmentJob(this.SimilarityMatrix, this.GapOpenCost, this.GapExtensionCost, sequenceA, sequenceB);
        }
    }
}
