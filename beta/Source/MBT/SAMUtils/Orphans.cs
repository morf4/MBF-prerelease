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
using CommandLine;
using MBF.IO.BAM;
using MBF.IO.SAM;
using MBF;

namespace SAMUtils
{
    /// <summary>
    /// Orphan regions
    /// </summary>
    public class Orphans
    {
        #region Public Fields

        /// <summary>
        /// Usage.
        /// </summary>
        [Argument(ArgumentType.AtMostOnce)]
        public bool Usage;

        /// <summary>
        /// Input file is in SAM format.
        /// </summary>
        [Argument(ArgumentType.AtMostOnce, HelpText = "Input is SAM format", ShortName = "S")]
        public bool SAMInput;

        #endregion Public Fields

        #region Public Methods
        /// <summary>
        /// Display Chromosomes with orphan regions.
        /// </summary>
        /// <param name="inputFile">Path of the input file</param>
        /// <param name="mean">Mean value</param>
        /// <param name="standardDeviation">Standard deviation value</param>
        public void DisplayOrpanChromosomes(string inputFile)
        {
            if (string.IsNullOrEmpty(inputFile))
            {
                throw new InvalidOperationException("Input File Not specified");
            }

            DisplayOrphans(inputFile);
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Get chromoses with orphan regions
        /// </summary>
        /// <param name="filename">Path of the BAM file</param>
        /// <param name="mean">Mean value</param>
        /// <param name="deviation">Standard deviation</param>
        /// <returns></returns>
        private void DisplayOrphans(string filename)
        {

            SequenceAlignmentMap alignmentMapobj = null;

            if (!SAMInput)
            {
                BAMParser bamParser = new BAMParser();
                alignmentMapobj = bamParser.Parse(filename);
            }
            else
            {
                SAMParser samParser = new SAMParser();
                alignmentMapobj = samParser.Parse(filename);
            }

            // get reads from sequence alignment map object.
            IList<PairedRead> pairedReads = null;

            // Get Aligned sequences
            IList<SAMAlignedSequence> alignedSeqs = alignmentMapobj.QuerySequences;
            pairedReads = alignmentMapobj.GetPairedReads(0, 0);


            // Get the orphan regions.
            var orphans = pairedReads.Where(PR => PR.PairedType == PairedReadType.Orphan);

            if (orphans.Count() == 0)
            {
                Console.WriteLine("No Orphans to display");
            }

            List<ISequenceRange> orphanRegions = new List<ISequenceRange>(orphans.Count());
            foreach (PairedRead orphanRead in orphans)
            {
                orphanRegions.Add(GetRegion(orphanRead.Read1));
            }

            // Get sequence range grouping object.
            SequenceRangeGrouping rangeGroup = new SequenceRangeGrouping(orphanRegions);

            if (rangeGroup.GroupIDs.Count() == 0)
            {
                Console.Write("\r\nNo Orphan reads to display");
            }
            else
            {
                Console.Write("Region of Orphan reads:");
                DisplaySequenceRange(rangeGroup);
            }

            SequenceRangeGrouping mergedRegions = rangeGroup.MergeOverlaps();

            if (mergedRegions.GroupIDs.Count() == 0)
            {
                Console.Write("\r\nNo hot spots to display");
            }
            else
            {
                Console.Write("\r\nChromosomal hot spot:");
                DisplaySequenceRange(mergedRegions);
            }
        }

        /// <summary>
        /// Display Sequence range grops
        /// </summary>
        /// <param name="seqRangeGrops">Sequence Ranges grops</param>
        private static void DisplaySequenceRange(SequenceRangeGrouping seqRangeGrop)
        {
            IEnumerable<string> rangeGroupIds = seqRangeGrop.GroupIDs;
            string rangeID = string.Empty;

            // Display Sequence Ranges
            Console.Write("\r\nChromosome\t\tStart\tEnd");

            foreach (string groupID in rangeGroupIds)
            {
                rangeID = groupID;

                // Get SequenceRangeIds.
                List<ISequenceRange> rangeList = seqRangeGrop.GetGroup(rangeID);

                foreach (ISequenceRange seqRange in rangeList)
                {
                    Console.Write("\n{0}\t\t\t{1}\t{2}", seqRange.ID.ToString(),
                        seqRange.Start.ToString(), seqRange.End.ToString());

                }
            }

        }

        /// <summary>
        /// Gets an instance of SequenceRange class which represets alignment reigon of 
        /// specified aligned sequence (read) with reference sequence.
        /// </summary>
        /// <param name="alignedSequence">Aligned sequence.</param>
        private static ISequenceRange GetRegion(SAMAlignedSequence alignedSequence)
        {
            string refSeqName = alignedSequence.RName;
            long startPos = alignedSequence.Pos;
            long endPos = alignedSequence.Pos + alignedSequence.QueryLength;
            return new SequenceRange(refSeqName, startPos, endPos);
        }

        #endregion Private Methods
    }
}
