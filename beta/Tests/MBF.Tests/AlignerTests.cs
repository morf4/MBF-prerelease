﻿// *****************************************************************
//    Copyright (c) Microsoft. All rights reserved.
//    This code is licensed under the Microsoft Public License.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
// *****************************************************************

namespace MBF.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using MBF;
    using MBF.Algorithms.Alignment;
    using MBF.SimilarityMatrices;
    using MBF.Util.Logging;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    /// <summary>
    /// Test the alignment algorithms.
    /// </summary>
    [TestClass]
    public class AlignerTests
    {
        /// <summary>
        /// Initializes static members of the AlignerTests class
        /// Static constructor to open log and make other settings needed for test
        /// </summary>
        static AlignerTests()
        {
            Trace.Set(Trace.SeqWarnings);
            if (!ApplicationLog.Ready)
            {
                ApplicationLog.Open("mbf.tests.log");
            }
        }

        #region Needleman-Wunsch Aligner

        /// <summary>
        /// Test NeedlemanWunschAligner using Protein sequence and Simple Gap Penalty Function.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void NeedlemanWunschProteinSeqSimpleGap()
        {
            string sequenceString1 = "HEAGAWGHEE";
            string sequenceString2 = "PAWHEAE";

            Sequence sequence1 = new Sequence(Alphabets.Protein, sequenceString1);
            Sequence sequence2 = new Sequence(Alphabets.Protein, sequenceString2);

            SimilarityMatrix sm = new SimilarityMatrix(SimilarityMatrix.StandardSimilarityMatrix.Blosum50);
            int gapPenalty = -8;

            NeedlemanWunschAligner nw = new NeedlemanWunschAligner();
            nw.SimilarityMatrix = sm;
            nw.GapOpenCost = gapPenalty;
            IList<IPairwiseSequenceAlignment> result = nw.AlignSimple(sequence1, sequence2);

            ApplicationLog.WriteLine(string.Format((IFormatProvider)null, "{0}, Simple; Matrix {1}; GapOpenCost {2}", nw.Name, nw.SimilarityMatrix.Name, nw.GapOpenCost));
            ApplicationLog.WriteLine(string.Format((IFormatProvider)null, "score {0}", result[0].PairwiseAlignedSequences[0].Score));
            ApplicationLog.WriteLine(string.Format((IFormatProvider)null, "input 0     {0}", result[0].FirstSequence.ToString()));
            ApplicationLog.WriteLine(string.Format((IFormatProvider)null, "input 1     {0}", result[0].SecondSequence.ToString()));
            ApplicationLog.WriteLine(string.Format((IFormatProvider)null, "result 0    {0}", result[0].PairwiseAlignedSequences[0].FirstSequence.ToString()));
            ApplicationLog.WriteLine(string.Format((IFormatProvider)null, "result 1    {0}", result[0].PairwiseAlignedSequences[0].SecondSequence.ToString()));
            ApplicationLog.WriteLine(string.Format((IFormatProvider)null, "consesus    {0}", result[0].PairwiseAlignedSequences[0].Consensus));

            IList<IPairwiseSequenceAlignment> expectedOutput = new List<IPairwiseSequenceAlignment>();
            IPairwiseSequenceAlignment align = new PairwiseSequenceAlignment();
            PairwiseAlignedSequence alignedSeq = new PairwiseAlignedSequence();
            alignedSeq.FirstSequence = new Sequence(Alphabets.Protein, "HEAGAWGHE-E");
            alignedSeq.SecondSequence = new Sequence(Alphabets.Protein, "--P-AW-HEAE");
            alignedSeq.Consensus = new Sequence(Alphabets.Protein, "HEXGAWGHEAE");
            alignedSeq.Score = 1;
            alignedSeq.FirstOffset = 0;
            alignedSeq.SecondOffset = 2;
            align.PairwiseAlignedSequences.Add(alignedSeq);
            expectedOutput.Add(align);
            Assert.IsTrue(CompareAlignment(result, expectedOutput));
        }

        /// <summary>
        /// Test NeedlemanWunschAligner using Protein sequence and Affine Gap Penalty Function.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void NeedlemanWunschProteinSeqAffineGap()
        {
            string sequenceString1 = "HEAGAWGHEE";
            string sequenceString2 = "PAWHEAE";

            Sequence sequence1 = new Sequence(Alphabets.Protein, sequenceString1);
            Sequence sequence2 = new Sequence(Alphabets.Protein, sequenceString2);

            SimilarityMatrix sm = new SimilarityMatrix(SimilarityMatrix.StandardSimilarityMatrix.Blosum50);
            int gapPenalty = -8;

            NeedlemanWunschAligner nw = new NeedlemanWunschAligner();
            nw.SimilarityMatrix = sm;
            nw.GapOpenCost = gapPenalty;
            nw.GapExtensionCost = -1;
            IList<IPairwiseSequenceAlignment> result = nw.Align(sequence1, sequence2);

            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "{0}, Affine; Matrix {1}; GapOpenCost {2}; GapExtenstionCost {3}",
                nw.Name, nw.SimilarityMatrix.Name, nw.GapOpenCost, nw.GapExtensionCost));
            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "score {0}", result[0].PairwiseAlignedSequences[0].Score));
            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "input 0     {0}", result[0].FirstSequence.ToString()));
            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "input 1     {0}", result[0].SecondSequence.ToString()));
            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "result 0    {0}", result[0].PairwiseAlignedSequences[0].FirstSequence.ToString()));
            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "result 1    {0}", result[0].PairwiseAlignedSequences[0].SecondSequence.ToString()));
            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "consesus    {0}", result[0].PairwiseAlignedSequences[0].Consensus));

            IList<IPairwiseSequenceAlignment> expectedOutput = new List<IPairwiseSequenceAlignment>();
            IPairwiseSequenceAlignment align = new PairwiseSequenceAlignment();
            PairwiseAlignedSequence alignedSeq = new PairwiseAlignedSequence();
            alignedSeq.FirstSequence = new Sequence(Alphabets.Protein, "HEAGAWGHE-E");
            alignedSeq.SecondSequence = new Sequence(Alphabets.Protein, "---PAW-HEAE");
            alignedSeq.Consensus = new Sequence(Alphabets.Protein, "HEAXAWGHEAE");
            alignedSeq.Score = 14;
            alignedSeq.FirstOffset = 0;
            alignedSeq.SecondOffset = 3;
            align.PairwiseAlignedSequences.Add(alignedSeq);
            expectedOutput.Add(align);
            Assert.IsTrue(CompareAlignment(result, expectedOutput));
        }

        /// <summary>
        /// Test NeedlemanWunschAligner using DNA sequence and Simple Gap Penalty Function.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void NeedlemanWunschDnaSeqSimpleGap()
        {
            Sequence sequence1 = new Sequence(Alphabets.DNA, "GAATTCAGTTA");
            Sequence sequence2 = new Sequence(Alphabets.DNA, "GGATCGA");
            SimilarityMatrix sm = new DiagonalSimilarityMatrix(2, -1, MoleculeType.DNA);
            int gapPenalty = -2;
            NeedlemanWunschAligner nw = new NeedlemanWunschAligner();
            nw.SimilarityMatrix = sm;
            nw.GapOpenCost = gapPenalty;

            IList<IPairwiseSequenceAlignment> result = nw.AlignSimple(sequence1, sequence2);

            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "{0}, Simple; Matrix {1}; GapOpenCost {2}", nw.Name, nw.SimilarityMatrix.Name, nw.GapOpenCost));
            ApplicationLog.WriteLine(string.Format(
                (IFormatProvider)null, "score {0}", result[0].PairwiseAlignedSequences[0].Score));
            ApplicationLog.WriteLine(string.Format(
                (IFormatProvider)null, "input 0     {0}", result[0].FirstSequence.ToString()));
            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "input 1     {0}", result[0].SecondSequence.ToString()));
            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "result 0    {0}", result[0].PairwiseAlignedSequences[0].FirstSequence.ToString()));
            ApplicationLog.WriteLine(string.Format((IFormatProvider)null, "result 1    {0}", result[0].PairwiseAlignedSequences[0].SecondSequence.ToString()));
            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "consesus    {0}", result[0].PairwiseAlignedSequences[0].Consensus));

            IList<IPairwiseSequenceAlignment> expectedOutput = new List<IPairwiseSequenceAlignment>();
            IPairwiseSequenceAlignment align = new PairwiseSequenceAlignment();
            PairwiseAlignedSequence alignedSeq = new PairwiseAlignedSequence();
            alignedSeq.FirstSequence = new Sequence(Alphabets.DNA, "GAATTCAGTTA");
            alignedSeq.SecondSequence = new Sequence(Alphabets.DNA, "GGA-TC-G--A");
            alignedSeq.Consensus = new Sequence(Alphabets.DNA, "GRATTCAGTTA");
            alignedSeq.Score = 3;
            alignedSeq.FirstOffset = 0;
            alignedSeq.SecondOffset = 0;
            align.PairwiseAlignedSequences.Add(alignedSeq);
            expectedOutput.Add(align);
            Assert.IsTrue(CompareAlignment(result, expectedOutput));
        }

        /// <summary>
        /// Test NeedlemanWunschAligner using DNA sequence and Simple Gap Penalty Function and Custom Matrix.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void NeedlemanWunschDnaSeqSimpleGapCustomMatrix()
        {
            int[][] customMatrix = new int[][]
            {
                // A   G   C   T
                new int[] { 10, -1, -3, -4 }, // A
                new int[] { -1,  7, -5, -3 }, // G
                new int[] { -3, -5,  9,  0 }, // C
                new int[] { -4, -3,  0,  8 }  // T
            };
            Sequence sequence1 = new Sequence(Alphabets.DNA, "AGACTAGTTAC");
            Sequence sequence2 = new Sequence(Alphabets.DNA, "CGAGACGT");
            SimilarityMatrix sm = new SimilarityMatrix(customMatrix, "AGCT", "Simple DNA matrix", MoleculeType.DNA);
            int gapPenalty = -5;

            NeedlemanWunschAligner nw = new NeedlemanWunschAligner();
            nw.SimilarityMatrix = sm;
            nw.GapOpenCost = gapPenalty;

            IList<IPairwiseSequenceAlignment> result = nw.AlignSimple(sequence1, sequence2);

            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "{0}, Simple; Matrix {1}; GapOpenCost {2}", nw.Name, nw.SimilarityMatrix.Name, nw.GapOpenCost));
            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "score {0}", result[0].PairwiseAlignedSequences[0].Score));
            ApplicationLog.WriteLine(string.Format(
                (IFormatProvider)null, "input 0     {0}", result[0].FirstSequence.ToString()));
            ApplicationLog.WriteLine(string.Format(
                (IFormatProvider)null, "input 1     {0}", result[0].SecondSequence.ToString()));
            ApplicationLog.WriteLine(string.Format(
                (IFormatProvider)null, "result 0    {0}", result[0].PairwiseAlignedSequences[0].FirstSequence.ToString()));
            ApplicationLog.WriteLine(string.Format(
                (IFormatProvider)null, "result 1    {0}", result[0].PairwiseAlignedSequences[0].SecondSequence.ToString()));
            ApplicationLog.WriteLine(string.Format(
                (IFormatProvider)null, "consesus    {0}", result[0].PairwiseAlignedSequences[0].Consensus.ToString()));

            IList<IPairwiseSequenceAlignment> expectedOutput = new List<IPairwiseSequenceAlignment>();

            IPairwiseSequenceAlignment align = new PairwiseSequenceAlignment();
            PairwiseAlignedSequence alignedSeq = new PairwiseAlignedSequence();
            alignedSeq.FirstSequence = new Sequence(Alphabets.DNA, "--AGACTAGTTAC");
            alignedSeq.SecondSequence = new Sequence(Alphabets.DNA, "CGAGAC--G-T--");
            alignedSeq.Consensus = new Sequence(Alphabets.DNA, "CGAGACTAGTTAC");
            alignedSeq.Score = 16;
            alignedSeq.FirstOffset = 2;
            alignedSeq.SecondOffset = 0;
            align.PairwiseAlignedSequences.Add(alignedSeq);
            expectedOutput.Add(align);
            Assert.IsTrue(CompareAlignment(result, expectedOutput));
        }

        #endregion Needleman-Wunsch Aligner

        #region Smith-Waterman Aligner

        /// <summary>
        /// Test SmithWatermanAligner using Protein Sequence and Simple Gap Penalty Function.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void SmithWatermanProteinSeqSimpleGap()
        {
            string sequenceString1 = "HEAGAWGHEE";
            string sequenceString2 = "PAWHEAE";

            Sequence sequence1 = new Sequence(Alphabets.Protein, sequenceString1);
            Sequence sequence2 = new Sequence(Alphabets.Protein, sequenceString2);

            SimilarityMatrix sm = new SimilarityMatrix(SimilarityMatrix.StandardSimilarityMatrix.Blosum50);
            int gapPenalty = -8;

            SmithWatermanAligner sw = new SmithWatermanAligner();
            sw.SimilarityMatrix = sm;
            sw.GapOpenCost = gapPenalty;
            IList<IPairwiseSequenceAlignment> result = sw.AlignSimple(sequence1, sequence2);

            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
            "{0}, Simple; Matrix {1}; GapOpenCost {2}", sw.Name, sw.SimilarityMatrix.Name, sw.GapOpenCost));
            foreach (IPairwiseSequenceAlignment sequenceResult in result)
            {
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "score {0}", sequenceResult.PairwiseAlignedSequences[0].Score));
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "input 0     {0}", sequenceResult.FirstSequence.ToString()));
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "input 1     {0}", sequenceResult.SecondSequence.ToString()));
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "result 0    {0}", sequenceResult.PairwiseAlignedSequences[0].FirstSequence.ToString()));
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "result 1    {0}", sequenceResult.PairwiseAlignedSequences[0].SecondSequence.ToString()));
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "consesus    {0}", sequenceResult.PairwiseAlignedSequences[0].Consensus.ToString()));
            }

            IList<IPairwiseSequenceAlignment> expectedOutput = new List<IPairwiseSequenceAlignment>();
            IPairwiseSequenceAlignment align = new PairwiseSequenceAlignment();
            PairwiseAlignedSequence alignedSeq = new PairwiseAlignedSequence();
            alignedSeq.FirstSequence = new Sequence(Alphabets.Protein, "AWGHE");
            alignedSeq.SecondSequence = new Sequence(Alphabets.Protein, "AW-HE");
            alignedSeq.Consensus = new Sequence(Alphabets.Protein, "AWGHE");
            alignedSeq.Score = 28;
            alignedSeq.FirstOffset = 0;
            alignedSeq.SecondOffset = 3;
            align.PairwiseAlignedSequences.Add(alignedSeq);
            expectedOutput.Add(align);
            Assert.IsTrue(CompareAlignment(result, expectedOutput));
        }

        /// <summary>
        /// Test SmithWatermanAligner using Protein sequence and Affine Gap Penalty Function.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void SmithWatermanProteinSeqAffineGap()
        {
            string sequenceString1 = "HEAGAWGHEE";
            string sequenceString2 = "PAWHEAE";

            Sequence sequence1 = new Sequence(Alphabets.Protein, sequenceString1);
            Sequence sequence2 = new Sequence(Alphabets.Protein, sequenceString2);

            SimilarityMatrix sm = new SimilarityMatrix(SimilarityMatrix.StandardSimilarityMatrix.Blosum50);
            int gapPenalty = -8;

            SmithWatermanAligner sw = new SmithWatermanAligner();
            sw.SimilarityMatrix = sm;
            sw.GapOpenCost = gapPenalty;
            sw.GapExtensionCost = -1;
            IList<IPairwiseSequenceAlignment> result = sw.Align(sequence1, sequence2);

            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
            "{0}, Affine; Matrix {1}; GapOpenCost {2}; GapExtenstionCost {3}", sw.Name, sw.SimilarityMatrix.Name, sw.GapOpenCost, sw.GapExtensionCost));
            foreach (IPairwiseSequenceAlignment sequenceResult in result)
            {
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "score {0}", sequenceResult.PairwiseAlignedSequences[0].Score));
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "input 0     {0}", sequenceResult.FirstSequence.ToString()));
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "input 1     {0}", sequenceResult.SecondSequence.ToString()));
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "result 0    {0}", sequenceResult.PairwiseAlignedSequences[0].FirstSequence.ToString()));
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "result 1    {0}", sequenceResult.PairwiseAlignedSequences[0].SecondSequence.ToString()));
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "consesus    {0}", sequenceResult.PairwiseAlignedSequences[0].Consensus.ToString()));
            }

            IList<IPairwiseSequenceAlignment> expectedOutput = new List<IPairwiseSequenceAlignment>();
            IPairwiseSequenceAlignment align = new PairwiseSequenceAlignment();
            PairwiseAlignedSequence alignedSeq = new PairwiseAlignedSequence();
            alignedSeq.FirstSequence = new Sequence(Alphabets.Protein, "AWGHE");
            alignedSeq.SecondSequence = new Sequence(Alphabets.Protein, "AW-HE");
            alignedSeq.Consensus = new Sequence(Alphabets.Protein, "AWGHE");
            alignedSeq.Score = 28;
            alignedSeq.FirstOffset = 0;
            alignedSeq.SecondOffset = 3;
            align.PairwiseAlignedSequences.Add(alignedSeq);
            expectedOutput.Add(align);
            Assert.IsTrue(CompareAlignment(result, expectedOutput));
        }

        /// <summary>
        /// Test SmithWatermanAligner for cases where it returns multiple alignments
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void SmithWatermanAlignerMultipleAlignments1()
        {
            Sequence sequence1 = new Sequence(Alphabets.DNA, "AAATTCCCAG");
            Sequence sequence2 = new Sequence(Alphabets.DNA, "AAAGCCC");
            SimilarityMatrix sm = new DiagonalSimilarityMatrix(5, -20, MoleculeType.DNA);
            int gapPenalty = -10;
            SmithWatermanAligner sw = new SmithWatermanAligner();
            sw.SimilarityMatrix = sm;
            sw.GapOpenCost = gapPenalty;
            IList<IPairwiseSequenceAlignment> result = sw.AlignSimple(sequence1, sequence2);

            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "{0}, Simple; Matrix {1}; GapOpenCost {2}", sw.Name, sw.SimilarityMatrix.Name, sw.GapOpenCost));
            foreach (IPairwiseSequenceAlignment sequenceResult in result)
            {
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "score {0}", sequenceResult.PairwiseAlignedSequences[0].Score));
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "input 0     {0}", sequenceResult.FirstSequence.ToString()));
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "input 1     {0}", sequenceResult.SecondSequence.ToString()));
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "result 0    {0}", sequenceResult.PairwiseAlignedSequences[0].FirstSequence.ToString()));
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "result 1    {0}", sequenceResult.PairwiseAlignedSequences[0].SecondSequence.ToString()));
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "consesus    {0}", sequenceResult.PairwiseAlignedSequences[0].Consensus.ToString()));
            }

            IList<IPairwiseSequenceAlignment> expectedOutput = new List<IPairwiseSequenceAlignment>();
            IPairwiseSequenceAlignment align = new PairwiseSequenceAlignment(sequence1, sequence2);

            // First alignment
            PairwiseAlignedSequence alignedSeq = new PairwiseAlignedSequence();
            alignedSeq.FirstSequence = new Sequence(Alphabets.DNA, "AAA");
            alignedSeq.SecondSequence = new Sequence(Alphabets.DNA, "AAA");
            alignedSeq.Consensus = new Sequence(Alphabets.DNA, "AAA");
            alignedSeq.Score = 15;
            alignedSeq.FirstOffset = 0;
            alignedSeq.SecondOffset = 0;
            align.PairwiseAlignedSequences.Add(alignedSeq);

            // Second alignment
            alignedSeq = new PairwiseAlignedSequence();
            alignedSeq.FirstSequence = new Sequence(Alphabets.DNA, "CCC");
            alignedSeq.SecondSequence = new Sequence(Alphabets.DNA, "CCC");
            alignedSeq.Consensus = new Sequence(Alphabets.DNA, "CCC");
            alignedSeq.Score = 15;
            alignedSeq.FirstOffset = 0;
            alignedSeq.SecondOffset = 1;
            align.PairwiseAlignedSequences.Add(alignedSeq);

            expectedOutput.Add(align);
            Assert.IsTrue(CompareAlignment(result, expectedOutput));
        }

        /// <summary>
        /// Test SmithWatermanAligner for cases where it returns multiple alignments
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void SmithWatermanAlignerMultipleAlignments2()
        {
            Sequence sequence1 = new Sequence(Alphabets.DNA, "AAAAGGGGGGCCCC");
            Sequence sequence2 = new Sequence(Alphabets.DNA, "AAAATTTTTTTCCCC");
            SimilarityMatrix sm = new DiagonalSimilarityMatrix(5, -4, MoleculeType.DNA);
            int gapPenalty = -10;
            SmithWatermanAligner sw = new SmithWatermanAligner();
            sw.SimilarityMatrix = sm;
            sw.GapOpenCost = gapPenalty;
            IList<IPairwiseSequenceAlignment> result = sw.AlignSimple(sequence1, sequence2);

            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "{0}, Simple; Matrix {1}; GapOpenCost {2}", sw.Name, sw.SimilarityMatrix.Name, sw.GapOpenCost));
            foreach (IPairwiseSequenceAlignment sequenceResult in result)
            {
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "score {0}", sequenceResult.PairwiseAlignedSequences[0].Score));
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "input 0     {0}", sequenceResult.FirstSequence.ToString()));
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "input 1     {0}", sequenceResult.SecondSequence.ToString()));
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "result 0    {0}", sequenceResult.PairwiseAlignedSequences[0].FirstSequence.ToString()));
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "result 1    {0}", sequenceResult.PairwiseAlignedSequences[0].SecondSequence.ToString()));
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "consesus    {0}", sequenceResult.PairwiseAlignedSequences[0].Consensus.ToString()));
            }

            IList<IPairwiseSequenceAlignment> expectedOutput = new List<IPairwiseSequenceAlignment>();
            IPairwiseSequenceAlignment align = new PairwiseSequenceAlignment(sequence1, sequence2);

            // First alignment
            PairwiseAlignedSequence alignedSeq = new PairwiseAlignedSequence();
            alignedSeq.FirstSequence = new Sequence(Alphabets.DNA, "AAAA");
            alignedSeq.SecondSequence = new Sequence(Alphabets.DNA, "AAAA");
            alignedSeq.Consensus = new Sequence(Alphabets.DNA, "AAAA");
            alignedSeq.Score = 20;
            alignedSeq.FirstOffset = 0;
            alignedSeq.SecondOffset = 0;
            align.PairwiseAlignedSequences.Add(alignedSeq);

            // Second alignment
            alignedSeq = new PairwiseAlignedSequence();
            alignedSeq.FirstSequence = new Sequence(Alphabets.DNA, "CCCC");
            alignedSeq.SecondSequence = new Sequence(Alphabets.DNA, "CCCC");
            alignedSeq.Consensus = new Sequence(Alphabets.DNA, "CCCC");
            alignedSeq.Score = 20;
            alignedSeq.FirstOffset = 1;
            alignedSeq.SecondOffset = 0;
            align.PairwiseAlignedSequences.Add(alignedSeq);

            expectedOutput.Add(align);
            Assert.IsTrue(CompareAlignment(result, expectedOutput));
        }
        #endregion Smith-Waterman Aligner

        #region PairwiseOverlap Aligner

        /// <summary>
        /// Test PairwiseOverlapAligner using Protein sequence and Simple Gap Penalty Function.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void PairwiseOverlapProteinSeqSimpleGap()
        {
            string sequenceString1 = "HEAGAWGHEE";
            string sequenceString2 = "PAWHEAE";

            Sequence sequence1 = new Sequence(Alphabets.Protein, sequenceString1);
            Sequence sequence2 = new Sequence(Alphabets.Protein, sequenceString2);

            SimilarityMatrix sm = new SimilarityMatrix(SimilarityMatrix.StandardSimilarityMatrix.Blosum50);
            int gapPenalty = -8;

            PairwiseOverlapAligner overlap = new PairwiseOverlapAligner();
            overlap.SimilarityMatrix = sm;
            overlap.GapOpenCost = gapPenalty;
            IList<IPairwiseSequenceAlignment> result = overlap.AlignSimple(sequence1, sequence2);

            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "{0}, Simple; Matrix {1}; GapOpenCost {2}", overlap.Name, overlap.SimilarityMatrix.Name, overlap.GapOpenCost));
            foreach (IPairwiseSequenceAlignment sequenceResult in result)
            {
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "score {0}", sequenceResult.PairwiseAlignedSequences[0].Score));
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "input 0     {0}", sequenceResult.FirstSequence.ToString()));
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "input 1     {0}", sequenceResult.SecondSequence.ToString()));
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "result 0    {0}", sequenceResult.PairwiseAlignedSequences[0].FirstSequence.ToString()));
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "result 1    {0}", sequenceResult.PairwiseAlignedSequences[0].SecondSequence.ToString()));
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "consesus    {0}", sequenceResult.PairwiseAlignedSequences[0].Consensus.ToString()));
            }

            IList<IPairwiseSequenceAlignment> expectedOutput = new List<IPairwiseSequenceAlignment>();
            IPairwiseSequenceAlignment align = new PairwiseSequenceAlignment();
            PairwiseAlignedSequence alignedSeq = new PairwiseAlignedSequence();
            alignedSeq.FirstSequence = new Sequence(Alphabets.Protein, "GAWGHEE");
            alignedSeq.SecondSequence = new Sequence(Alphabets.Protein, "PAW-HEA");
            alignedSeq.Consensus = new Sequence(Alphabets.Protein, "XAWGHEX");
            alignedSeq.Score = 25;
            alignedSeq.FirstOffset = 0;
            alignedSeq.SecondOffset = 3;
            align.PairwiseAlignedSequences.Add(alignedSeq);
            expectedOutput.Add(align);
            Assert.IsTrue(CompareAlignment(result, expectedOutput));
        }

        /// <summary>
        /// Test PairwiseOverlapAligner using Protein sequence and Affine Gap Penalty Function.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void PairwiseOverlapProteinSeqAffineGap()
        {
            string sequenceString1 = "HEAGAWGHEE";
            string sequenceString2 = "PAWHEAE";

            Sequence sequence1 = new Sequence(Alphabets.Protein, sequenceString1);
            Sequence sequence2 = new Sequence(Alphabets.Protein, sequenceString2);

            SimilarityMatrix sm = new SimilarityMatrix(SimilarityMatrix.StandardSimilarityMatrix.Blosum50);
            int gapPenalty = -8;

            PairwiseOverlapAligner overlap = new PairwiseOverlapAligner();
            overlap.SimilarityMatrix = sm;
            overlap.GapOpenCost = gapPenalty;
            overlap.GapExtensionCost = -1;
            IList<IPairwiseSequenceAlignment> result = overlap.Align(sequence1, sequence2);

            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
            "{0}, Affine; Matrix {1}; GapOpenCost {2}; GapExtenstionCost {3}",
            overlap.Name, overlap.SimilarityMatrix.Name, overlap.GapOpenCost, overlap.GapExtensionCost));
            foreach (IPairwiseSequenceAlignment sequenceResult in result)
            {
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "score {0}", sequenceResult.PairwiseAlignedSequences[0].Score));
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "input 0     {0}", sequenceResult.FirstSequence.ToString()));
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "input 1     {0}", sequenceResult.SecondSequence.ToString()));
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "result 0    {0}", sequenceResult.PairwiseAlignedSequences[0].FirstSequence.ToString()));
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "result 1    {0}", sequenceResult.PairwiseAlignedSequences[0].SecondSequence.ToString()));
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "consesus    {0}", sequenceResult.PairwiseAlignedSequences[0].Consensus.ToString()));
            }

            IList<IPairwiseSequenceAlignment> expectedOutput = new List<IPairwiseSequenceAlignment>();
            IPairwiseSequenceAlignment align = new PairwiseSequenceAlignment();
            PairwiseAlignedSequence alignedSeq = new PairwiseAlignedSequence();
            alignedSeq.FirstSequence = new Sequence(Alphabets.Protein, "GAWGHEE");
            alignedSeq.SecondSequence = new Sequence(Alphabets.Protein, "PAW-HEA");
            alignedSeq.Consensus = new Sequence(Alphabets.Protein, "XAWGHEX");
            alignedSeq.Score = 25;
            alignedSeq.FirstOffset = 0;
            alignedSeq.SecondOffset = 3;
            align.PairwiseAlignedSequences.Add(alignedSeq);
            expectedOutput.Add(align);
            Assert.IsTrue(CompareAlignment(result, expectedOutput));
        }

        /// <summary>
        /// Test PairwiseOverlapAligner using Protein sequence with zero overlap
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void PairwiseOverlapProteinSeqWithZeroOverlap()
        {
            Sequence sequence1 = new Sequence(Alphabets.Protein, "ABCDE");
            Sequence sequence2 = new Sequence(Alphabets.Protein, "VWXYZ");
            SimilarityMatrix sm = new DiagonalSimilarityMatrix(5, -5, MoleculeType.Protein);
            int gapPenalty = -10;
            PairwiseOverlapAligner overlap = new PairwiseOverlapAligner();
            overlap.SimilarityMatrix = sm;
            overlap.GapOpenCost = gapPenalty;
            overlap.GapExtensionCost = -1;
            IList<IPairwiseSequenceAlignment> result = overlap.Align(sequence1, sequence2);

            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "{0}, Simple; Matrix {1}; GapOpenCost {2}", overlap.Name, overlap.SimilarityMatrix.Name, overlap.GapOpenCost));
            foreach (IPairwiseSequenceAlignment sequenceResult in result)
            {
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "score {0}", sequenceResult.PairwiseAlignedSequences[0].Score));
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "input 0     {0}", sequenceResult.FirstSequence.ToString()));
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "input 1     {0}", sequenceResult.SecondSequence.ToString()));
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "result 0    {0}", sequenceResult.PairwiseAlignedSequences[0].FirstSequence.ToString()));
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "result 1    {0}", sequenceResult.PairwiseAlignedSequences[0].SecondSequence.ToString()));
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "consesus    {0}", sequenceResult.PairwiseAlignedSequences[0].Consensus.ToString()));
            }

            IList<IPairwiseSequenceAlignment> expectedOutput = new List<IPairwiseSequenceAlignment>();
            Assert.IsTrue(CompareAlignment(result, expectedOutput));
        }

        /// <summary>
        /// Test PairwiseOverlapAligner for cases where it returns multiple alignments
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void PairwiseOverlapMultipleAlignments()
        {
            Sequence sequence1 = new Sequence(Alphabets.DNA, "CCCAACCC");
            Sequence sequence2 = new Sequence(Alphabets.DNA, "CCC");
            SimilarityMatrix sm = new DiagonalSimilarityMatrix(5, -20, MoleculeType.DNA);
            int gapPenalty = -10;
            PairwiseOverlapAligner overlap = new PairwiseOverlapAligner();
            overlap.SimilarityMatrix = sm;
            overlap.GapOpenCost = gapPenalty;
            IList<IPairwiseSequenceAlignment> result = overlap.AlignSimple(sequence1, sequence2);

            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "{0}, Simple; Matrix {1}; GapOpenCost {2}", overlap.Name, overlap.SimilarityMatrix.Name, overlap.GapOpenCost));
            foreach (IPairwiseSequenceAlignment sequenceResult in result)
            {
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "score {0}", sequenceResult.PairwiseAlignedSequences[0].Score));
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "input 0     {0}", sequenceResult.FirstSequence.ToString()));
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "input 1     {0}", sequenceResult.SecondSequence.ToString()));
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "result 0    {0}", sequenceResult.PairwiseAlignedSequences[0].FirstSequence.ToString()));
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "result 1    {0}", sequenceResult.PairwiseAlignedSequences[0].SecondSequence.ToString()));
                ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "consesus    {0}", sequenceResult.PairwiseAlignedSequences[0].Consensus.ToString()));
            }

            IList<IPairwiseSequenceAlignment> expectedOutput = new List<IPairwiseSequenceAlignment>();
            IPairwiseSequenceAlignment align = new PairwiseSequenceAlignment();

            // First alignment
            PairwiseAlignedSequence alignedSeq = new PairwiseAlignedSequence();
            alignedSeq.FirstSequence = new Sequence(Alphabets.DNA, "CCC");
            alignedSeq.SecondSequence = new Sequence(Alphabets.DNA, "CCC");
            alignedSeq.Consensus = new Sequence(Alphabets.DNA, "CCC");
            alignedSeq.Score = 15;
            alignedSeq.FirstOffset = 0;
            alignedSeq.SecondOffset = 0;
            align.PairwiseAlignedSequences.Add(alignedSeq);

            // Second alignment
            alignedSeq = new PairwiseAlignedSequence();
            alignedSeq.FirstSequence = new Sequence(Alphabets.DNA, "CCC");
            alignedSeq.SecondSequence = new Sequence(Alphabets.DNA, "CCC");
            alignedSeq.Consensus = new Sequence(Alphabets.DNA, "CCC");
            alignedSeq.Score = 15;
            alignedSeq.FirstOffset = 0;
            alignedSeq.SecondOffset = 5;
            align.PairwiseAlignedSequences.Add(alignedSeq);

            expectedOutput.Add(align);
            Assert.IsTrue(CompareAlignment(result, expectedOutput));
        }
        #endregion PairwiseOverlap Aligner

        /// <summary>
        /// Tests the name and description property of the aligners.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void TestAlignerProperties()
        {
            ISequenceAligner aligner = new NeedlemanWunschAligner();

            Assert.AreEqual(aligner.Name, Properties.Resource.NEEDLEMAN_NAME);
            //            Assert.AreEqual(aligner.Description, Properties.Resource.NEEDLEMAN_DESCRIPTION);

            aligner = new SmithWatermanAligner();

            // Assert.AreEqual(aligner.Name, Properties.Resource.SMITH_NAME);
            // Assert.AreEqual(aligner.Description, Properties.Resource.SMITH_DESCRIPTION);

            aligner = new PairwiseOverlapAligner();

            //            Assert.AreEqual(aligner.Name, Properties.Resource.PAIRWISE_NAME);
            //           Assert.AreEqual(aligner.Description, Properties.Resource.PAIRWISE_DESCRIPTION);
        }

        #region MUMmer Test Cases

        /// <summary>
        /// Test MUMmer 3 Align with extension penalty.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void TestMUMmer3AlignSingleMum()
        {
            string reference = "TTAATTTTAG";
            string search = "AGTTTAGAG";

            Sequence referenceSeq = null;
            Sequence searchSeq = null;
            List<ISequence> searchSeqs = null;

            referenceSeq = new Sequence(Alphabets.DNA, reference);
            searchSeq = new Sequence(Alphabets.DNA, search);

            searchSeqs = new List<ISequence>();
            searchSeqs.Add(searchSeq);

            MUMmer mummer = new MUMmer3();
            mummer.LengthOfMUM = 3;
            mummer.PairWiseAlgorithm = new NeedlemanWunschAligner();

            mummer.GapExtensionCost = -2;
            IList<IPairwiseSequenceAlignment> result = mummer.Align(referenceSeq, searchSeqs);

            // Check if output is not null
            Assert.AreNotEqual(null, result);

            IList<IPairwiseSequenceAlignment> expectedOutput = new List<IPairwiseSequenceAlignment>();
            IPairwiseSequenceAlignment align = new PairwiseSequenceAlignment();
            PairwiseAlignedSequence alignedSeq = new PairwiseAlignedSequence();
            alignedSeq.FirstSequence = new Sequence(Alphabets.DNA, "TTAATTTTAG--");
            alignedSeq.SecondSequence = new Sequence(Alphabets.DNA, "---AGTTTAGAG");
            alignedSeq.Consensus = new Sequence(Alphabets.DNA, "TTAAKTTTAGAG");
            alignedSeq.Score = -6;
            alignedSeq.FirstOffset = 0;
            alignedSeq.SecondOffset = 3;
            align.PairwiseAlignedSequences.Add(alignedSeq);
            expectedOutput.Add(align);
            Assert.IsTrue(CompareAlignment(result, expectedOutput));
        }

        /// <summary>
        /// MUMmer 3 test where we get single MUMs.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void TestMUMmer3SingleMum()
        {
            string reference = "TTAATTTTAG";
            string search = "AGTTTAGAG";

            Sequence referenceSeq = null;
            Sequence searchSeq = null;
            List<ISequence> searchSeqs = null;

            referenceSeq = new Sequence(Alphabets.DNA, reference);
            searchSeq = new Sequence(Alphabets.DNA, search);

            searchSeqs = new List<ISequence>();
            searchSeqs.Add(searchSeq);

            MUMmer mummer = new MUMmer3();
            mummer.LengthOfMUM = 3;
            mummer.PairWiseAlgorithm = new NeedlemanWunschAligner();

            IList<IPairwiseSequenceAlignment> result = mummer.AlignSimple(referenceSeq, searchSeqs);

            // Check if output is not null
            Assert.AreNotEqual(0, result.Count);

            IList<IPairwiseSequenceAlignment> expectedOutput = new List<IPairwiseSequenceAlignment>();
            IPairwiseSequenceAlignment align = new PairwiseSequenceAlignment();
            PairwiseAlignedSequence alignedSeq = new PairwiseAlignedSequence();
            alignedSeq.FirstSequence = new Sequence(Alphabets.DNA, "TTAATTTTAG--");
            alignedSeq.SecondSequence = new Sequence(Alphabets.DNA, "---AGTTTAGAG");
            alignedSeq.Consensus = new Sequence(Alphabets.DNA, "TTAAKTTTAGAG");
            alignedSeq.Score = -39;
            alignedSeq.FirstOffset = 0;
            alignedSeq.SecondOffset = 3;
            align.PairwiseAlignedSequences.Add(alignedSeq);
            expectedOutput.Add(align);
            Assert.IsTrue(CompareAlignment(result, expectedOutput));
        }

        /// <summary>
        /// MUMmer 3 test where we get multiple MUMs.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void TestMUMmer3MultipleMum()
        {
            string reference = "ATGCGCATCCCCTT";
            string search = "GCGCCCCCTA";

            Sequence referenceSeq = null;
            Sequence searchSeq = null;

            referenceSeq = new Sequence(Alphabets.DNA, reference);
            searchSeq = new Sequence(Alphabets.DNA, search);

            List<ISequence> searchSeqs = new List<ISequence>();
            searchSeqs.Add(searchSeq);

            MUMmer mummer = new MUMmer3();
            mummer.LengthOfMUM = 4;
            mummer.PairWiseAlgorithm = new NeedlemanWunschAligner();

            IList<IPairwiseSequenceAlignment> result = mummer.AlignSimple(referenceSeq, searchSeqs);

            // Check if output is not null
            Assert.AreNotEqual(null, result);
            IList<IPairwiseSequenceAlignment> expectedOutput = new List<IPairwiseSequenceAlignment>();
            IPairwiseSequenceAlignment align = new PairwiseSequenceAlignment();
            PairwiseAlignedSequence alignedSeq = new PairwiseAlignedSequence();
            alignedSeq.FirstSequence = new Sequence(Alphabets.DNA, "ATGCGCATCCCCTT");
            alignedSeq.SecondSequence = new Sequence(Alphabets.DNA, "--GCGC--CCCCTA");
            alignedSeq.Consensus = new Sequence(Alphabets.DNA, "ATGCGCATCCCCTW");
            alignedSeq.Score = -11;
            alignedSeq.FirstOffset = 0;
            alignedSeq.SecondOffset = 2;
            align.PairwiseAlignedSequences.Add(alignedSeq);
            expectedOutput.Add(align);
            Assert.IsTrue(CompareAlignment(result, expectedOutput));
        }

        /// <summary>
        /// MUMmer 3 test where we get multiple MUMs.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void TestMUMmer3MultipleMumWithCustomMatrix()
        {
            string reference = "ATGCGCATCCCCTT";
            string search = "GCGCCCCCTA";

            Sequence referenceSeq = null;
            Sequence searchSeq = null;

            referenceSeq = new Sequence(Alphabets.DNA, reference);
            searchSeq = new Sequence(Alphabets.DNA, search);

            List<ISequence> searchSeqs = new List<ISequence>();
            searchSeqs.Add(searchSeq);

            int[][] customMatrix = new int[][]
            {
                // A   G   C   T
                new int[] { 3,  -2, -2, -2 }, // A
                new int[] { -2,  3, -2, -2 }, // G
                new int[] { -2, -2,  3, -2 }, // C
                new int[] { -2, -2, -2,  3 }  // T
            };

            int gapOpenCost = -6;
            SimilarityMatrix similarityMatrix = new SimilarityMatrix(
                    customMatrix,
                    "AGCT",
                    "Simple DNA matrix",
                    MoleculeType.DNA);

            MUMmer mummer = new MUMmer3();
            mummer.LengthOfMUM = 4;
            mummer.PairWiseAlgorithm = new NeedlemanWunschAligner();
            mummer.SimilarityMatrix = similarityMatrix;
            mummer.GapOpenCost = gapOpenCost;
            mummer.GapExtensionCost = -2;

            IList<IPairwiseSequenceAlignment> result = mummer.AlignSimple(referenceSeq, searchSeqs);

            // Check if output is not null
            Assert.AreNotEqual(null, result);

            IList<IPairwiseSequenceAlignment> expectedOutput = new List<IPairwiseSequenceAlignment>();
            IPairwiseSequenceAlignment align = new PairwiseSequenceAlignment();
            PairwiseAlignedSequence alignedSeq = new PairwiseAlignedSequence();
            alignedSeq.FirstSequence = new Sequence(Alphabets.DNA, "ATGCGCATCCCCTT");
            alignedSeq.SecondSequence = new Sequence(Alphabets.DNA, "--GCGC--CCCCTA");
            alignedSeq.Consensus = new Sequence(Alphabets.DNA, "ATGCGCATCCCCTW");
            alignedSeq.Score = 1;
            alignedSeq.FirstOffset = 0;
            alignedSeq.SecondOffset = 2;
            align.PairwiseAlignedSequences.Add(alignedSeq);
            expectedOutput.Add(align);
            Assert.IsTrue(CompareAlignment(result, expectedOutput));
        }

        /// <summary>
        /// Test MUMmer 3 Align with RNA.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void TestMUMmer3WithRNAAlignSingleMum()
        {
            string reference = "AUGCSWRYKMBVHDN";
            string search = "UAUASWRYBB";

            Sequence referenceSeq = null;
            Sequence searchSeq = null;
            List<ISequence> searchSeqs = null;

            referenceSeq = new Sequence(Alphabets.RNA, reference);
            searchSeq = new Sequence(Alphabets.RNA, search);

            searchSeqs = new List<ISequence>();
            searchSeqs.Add(searchSeq);

            MUMmer3 mummer = new MUMmer3();
            mummer.LengthOfMUM = 3;
            mummer.PairWiseAlgorithm = new NeedlemanWunschAligner();

            mummer.SimilarityMatrix = new SimilarityMatrix(SimilarityMatrix.StandardSimilarityMatrix.AmbiguousRna);
            mummer.GapOpenCost = -8;
            mummer.GapExtensionCost = -2;
            IList<IPairwiseSequenceAlignment> result = mummer.Align(referenceSeq, searchSeqs);

            // Check if output is not null
            Assert.AreNotEqual(0, result.Count);

            IList<IPairwiseSequenceAlignment> expectedOutput = new List<IPairwiseSequenceAlignment>();
            IPairwiseSequenceAlignment align = new PairwiseSequenceAlignment();
            PairwiseAlignedSequence alignedSeq = new PairwiseAlignedSequence();
            alignedSeq.FirstSequence = new Sequence(Alphabets.RNA, "-AUGCSWRYKMBVHDN");
            alignedSeq.SecondSequence = new Sequence(Alphabets.RNA, "UAU-ASWRY--B---B");
            alignedSeq.Consensus = new Sequence(Alphabets.RNA, "UAUGMSWRYKMBVHDN");
            alignedSeq.Score = -7;
            alignedSeq.FirstOffset = 1;
            alignedSeq.SecondOffset = 0;
            align.PairwiseAlignedSequences.Add(alignedSeq);
            expectedOutput.Add(align);
            Assert.IsTrue(CompareAlignment(result, expectedOutput));
        }

        #endregion MUMer Test Cases

        #region NUCmer Test Cases

        /// <summary>
        /// Test NUCmer3 with single valid extendable cluster among multiple
        /// clusters and single reference sequence.
        /// Also covers extend backward start 
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void TestNUCmer3SingleCluster()
        {
            string reference = "AGAAAAGTTTTCA";
            string search = "TTTTGAGATAAAATC";

            Sequence referenceSeq = null;
            Sequence searchSeq = null;
            List<ISequence> referenceSeqs = null;
            List<ISequence> searchSeqs = null;

            referenceSeq = new Sequence(Alphabets.DNA, reference);
            referenceSeq.ID = "R1";

            searchSeq = new Sequence(Alphabets.DNA, search);
            searchSeq.ID = "Q1";

            referenceSeqs = new List<ISequence>();
            referenceSeqs.Add(referenceSeq);

            searchSeqs = new List<ISequence>();
            searchSeqs.Add(searchSeq);

            NUCmer nucmer = new NUCmer3();
            nucmer.FixedSeparation = 0;
            nucmer.MinimumScore = 2;
            nucmer.SeparationFactor = -1;
            nucmer.LengthOfMUM = 3;
            IList<IPairwiseSequenceAlignment> result = nucmer.Align(referenceSeqs, searchSeqs);

            // Check if output is not null
            Assert.AreNotEqual(null, result);

            IList<IPairwiseSequenceAlignment> expectedOutput = new List<IPairwiseSequenceAlignment>();
            IPairwiseSequenceAlignment align = new PairwiseSequenceAlignment();
            PairwiseAlignedSequence alignedSeq = new PairwiseAlignedSequence();
            alignedSeq.FirstSequence = new Sequence(Alphabets.DNA, "AG--AAAA");
            alignedSeq.SecondSequence = new Sequence(Alphabets.DNA, "AGATAAAA");
            alignedSeq.Consensus = new Sequence(Alphabets.DNA, "AGATAAAA");
            alignedSeq.Score = -11;
            alignedSeq.FirstOffset = 5;
            alignedSeq.SecondOffset = 0;
            align.PairwiseAlignedSequences.Add(alignedSeq);
            alignedSeq = new PairwiseAlignedSequence();
            alignedSeq.FirstSequence = new Sequence(Alphabets.DNA, "TTTT");
            alignedSeq.SecondSequence = new Sequence(Alphabets.DNA, "TTTT");
            alignedSeq.Consensus = new Sequence(Alphabets.DNA, "TTTT");
            alignedSeq.Score = 12;
            alignedSeq.FirstOffset = 0;
            alignedSeq.SecondOffset = 7;
            align.PairwiseAlignedSequences.Add(alignedSeq);
            expectedOutput.Add(align);
            Assert.IsTrue(CompareAlignment(result, expectedOutput));
        }

        /// <summary>
        /// Test NUCmer3 with multiple valid extendable clusters and single
        /// reference sequences
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void TestNUCmer3MultipleClusters()
        {
            string reference = "ATGCGCATCCCCTAGCT";
            string search = "CCGCGCCCCCTCAGCT";

            Sequence referenceSeq = null;
            Sequence searchSeq = null;
            List<ISequence> referenceSeqs = null;
            List<ISequence> searchSeqs = null;

            referenceSeq = new Sequence(Alphabets.DNA, reference);
            referenceSeq.ID = "R1";

            searchSeq = new Sequence(Alphabets.DNA, search);
            searchSeq.ID = "Q1";

            referenceSeqs = new List<ISequence>();
            referenceSeqs.Add(referenceSeq);

            searchSeqs = new List<ISequence>();
            searchSeqs.Add(searchSeq);

            NUCmer nucmer = new NUCmer3();
            nucmer.FixedSeparation = 0;
            nucmer.MinimumScore = 2;
            nucmer.SeparationFactor = -1;
            nucmer.LengthOfMUM = 3;
            IList<IPairwiseSequenceAlignment> result = nucmer.Align(referenceSeqs, searchSeqs);

            // Check if output is not null
            Assert.AreNotEqual(0, result);

            IList<IPairwiseSequenceAlignment> expectedOutput = new List<IPairwiseSequenceAlignment>();
            IPairwiseSequenceAlignment align = new PairwiseSequenceAlignment();
            PairwiseAlignedSequence alignedSeq = new PairwiseAlignedSequence();
            alignedSeq.FirstSequence = new Sequence(Alphabets.DNA, "GCGCATCCCCT-AGCT");
            alignedSeq.SecondSequence = new Sequence(Alphabets.DNA, "GCGC--CCCCTCAGCT");
            alignedSeq.Consensus = new Sequence(Alphabets.DNA, "GCGCATCCCCTCAGCT");
            alignedSeq.Score = -11;
            alignedSeq.FirstOffset = 0;
            alignedSeq.SecondOffset = 0;
            align.PairwiseAlignedSequences.Add(alignedSeq);
            expectedOutput.Add(align);
            Assert.IsTrue(CompareAlignment(result, expectedOutput));
        }

        /// <summary>
        /// Test NUCmer3 with multiple reference sequences
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void TestNUCmer3MultipleReferences()
        {
            Sequence referenceSeq = null;
            Sequence searchSeq = null;
            List<ISequence> referenceSeqs = null;
            List<ISequence> searchSeqs = null;

            referenceSeqs = new List<ISequence>();

            string reference = "ATGCGCATCCCC";
            referenceSeq = new Sequence(Alphabets.DNA, reference);
            referenceSeq.ID = "R1";
            referenceSeqs.Add(referenceSeq);

            reference = "TAGCT";
            referenceSeq = new Sequence(Alphabets.DNA, reference);
            referenceSeq.ID = "R11";
            referenceSeqs.Add(referenceSeq);

            searchSeqs = new List<ISequence>();

            string search = "CCGCGCCCCCTCAGCT";
            searchSeq = new Sequence(Alphabets.DNA, search);
            searchSeq.ID = "Q1";
            searchSeqs.Add(searchSeq);

            NUCmer nucmer = new NUCmer3();
            nucmer.FixedSeparation = 0;
            nucmer.MinimumScore = 2;
            nucmer.SeparationFactor = -1;
            nucmer.LengthOfMUM = 3;
            IList<IPairwiseSequenceAlignment> result = nucmer.Align(referenceSeqs, searchSeqs);

            // Check if output is not null
            Assert.AreNotEqual(null, result);
            IList<IPairwiseSequenceAlignment> expectedOutput = new List<IPairwiseSequenceAlignment>();
            IPairwiseSequenceAlignment align = new PairwiseSequenceAlignment();
            PairwiseAlignedSequence alignedSeq = new PairwiseAlignedSequence();
            alignedSeq.FirstSequence = new Sequence(Alphabets.DNA, "GCGCATCCCC");
            alignedSeq.SecondSequence = new Sequence(Alphabets.DNA, "GCG---CCCC");
            alignedSeq.Consensus = new Sequence(Alphabets.DNA, "GCGCATCCCC");
            alignedSeq.Score = -16;
            alignedSeq.FirstOffset = 0;
            alignedSeq.SecondOffset = 0;
            align.PairwiseAlignedSequences.Add(alignedSeq);
            alignedSeq = new PairwiseAlignedSequence();
            alignedSeq.FirstSequence = new Sequence(Alphabets.DNA, "CCCC");
            alignedSeq.SecondSequence = new Sequence(Alphabets.DNA, "CCCC");
            alignedSeq.Consensus = new Sequence(Alphabets.DNA, "CCCC");
            alignedSeq.Score = 12;
            alignedSeq.FirstOffset = 0;
            alignedSeq.SecondOffset = 2;
            align.PairwiseAlignedSequences.Add(alignedSeq);
            alignedSeq = new PairwiseAlignedSequence();
            alignedSeq.FirstSequence = new Sequence(Alphabets.DNA, "AGCT");
            alignedSeq.SecondSequence = new Sequence(Alphabets.DNA, "AGCT");
            alignedSeq.Consensus = new Sequence(Alphabets.DNA, "AGCT");
            alignedSeq.Score = 12;
            alignedSeq.FirstOffset = 11;
            alignedSeq.SecondOffset = 0;
            align.PairwiseAlignedSequences.Add(alignedSeq);
            expectedOutput.Add(align);
            Assert.IsTrue(CompareAlignment(result, expectedOutput));
        }

        /// <summary>
        /// Test NUCmer3 with multiple reference sequences
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void TestNUCmer3MultipleReferencesAndQueries()
        {
            Sequence referenceSeq = null;
            Sequence searchSeq = null;
            List<ISequence> referenceSeqs = null;
            List<ISequence> searchSeqs = null;

            referenceSeqs = new List<ISequence>();

            string reference = "ATGCGCATCCCC";
            referenceSeq = new Sequence(Alphabets.DNA, reference);
            referenceSeq.ID = "R1";
            referenceSeqs.Add(referenceSeq);

            reference = "TAGCT";
            referenceSeq = new Sequence(Alphabets.DNA, reference);
            referenceSeq.ID = "R11";
            referenceSeqs.Add(referenceSeq);

            searchSeqs = new List<ISequence>();

            string search = "CCGCGCCCCCTC";
            searchSeq = new Sequence(Alphabets.DNA, search);
            searchSeq.ID = "Q1";
            searchSeqs.Add(searchSeq);

            search = "AGCT";
            searchSeq = new Sequence(Alphabets.DNA, search);
            searchSeq.ID = "Q11";
            searchSeqs.Add(searchSeq);

            NUCmer nucmer = new NUCmer3();
            nucmer.FixedSeparation = 0;
            nucmer.MinimumScore = 2;
            nucmer.SeparationFactor = -1;
            nucmer.LengthOfMUM = 3;
            IList<IPairwiseSequenceAlignment> result = nucmer.Align(referenceSeqs, searchSeqs);

            // Check if output is not null
            Assert.AreNotEqual(null, result);

            IList<IPairwiseSequenceAlignment> expectedOutput = new List<IPairwiseSequenceAlignment>();
            IPairwiseSequenceAlignment align = new PairwiseSequenceAlignment();
            PairwiseAlignedSequence alignedSeq = new PairwiseAlignedSequence();

            alignedSeq.FirstSequence = new Sequence(Alphabets.DNA, "GCGCATCCCC");
            alignedSeq.SecondSequence = new Sequence(Alphabets.DNA, "GCG---CCCC");
            alignedSeq.Consensus = new Sequence(Alphabets.DNA, "GCGCATCCCC");
            alignedSeq.Score = -16;
            alignedSeq.FirstOffset = 0;
            alignedSeq.SecondOffset = 0;
            align.PairwiseAlignedSequences.Add(alignedSeq);
            alignedSeq = new PairwiseAlignedSequence();
            alignedSeq.FirstSequence = new Sequence(Alphabets.DNA, "CCCC");
            alignedSeq.SecondSequence = new Sequence(Alphabets.DNA, "CCCC");
            alignedSeq.Consensus = new Sequence(Alphabets.DNA, "CCCC");
            alignedSeq.Score = 12;
            alignedSeq.FirstOffset = 0;
            alignedSeq.SecondOffset = 2;
            align.PairwiseAlignedSequences.Add(alignedSeq);
            expectedOutput.Add(align);
            align = new PairwiseSequenceAlignment();
            alignedSeq = new PairwiseAlignedSequence();
            alignedSeq.FirstSequence = new Sequence(Alphabets.DNA, "AGCT");
            alignedSeq.SecondSequence = new Sequence(Alphabets.DNA, "AGCT");
            alignedSeq.Consensus = new Sequence(Alphabets.DNA, "AGCT");
            alignedSeq.Score = 12;
            alignedSeq.FirstOffset = 0;
            alignedSeq.SecondOffset = 1;
            align.PairwiseAlignedSequences.Add(alignedSeq);
            expectedOutput.Add(align);
            Assert.IsTrue(CompareAlignment(result, expectedOutput));
        }

        /// <summary>
        /// Test NUCmer3 with multiple reference sequences
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void TestNUCmer3CustomBreakLength()
        {
            Sequence referenceSeq = null;
            Sequence searchSeq = null;
            List<ISequence> referenceSeqs = null;
            List<ISequence> searchSeqs = null;

            referenceSeqs = new List<ISequence>();

            string reference = "CAAAAGGGATTGCAAATGTTGGAGTGAATGCCATTACCTACCGGCTAGGAGGAGT";
            referenceSeq = new Sequence(Alphabets.DNA, reference);
            referenceSeq.ID = "R1";
            referenceSeqs.Add(referenceSeq);

            reference = "CCCCCCCCC";
            referenceSeq = new Sequence(Alphabets.DNA, reference);
            referenceSeq.ID = "R2";
            referenceSeqs.Add(referenceSeq);

            reference = "TTTTT";
            referenceSeq = new Sequence(Alphabets.DNA, reference);
            referenceSeq.ID = "R3";
            referenceSeqs.Add(referenceSeq);

            searchSeqs = new List<ISequence>();

            string search = "CATTAATGATAAAGGGAAAGAAGTCCTCGTGCTATGGGGCATTCACCATCCATCTACTAGTGCTGACCAA";
            searchSeq = new Sequence(Alphabets.DNA, search);
            searchSeq.ID = "Q1";
            searchSeqs.Add(searchSeq);

            search = "CAAAGTCTCTATCAGAATGCAGATGCAGATGTTTTTGTGGGGTCATCAAGATATAGCAAGAAGTTCAAGC";
            searchSeq = new Sequence(Alphabets.DNA, search);
            searchSeq.ID = "Q2";
            searchSeqs.Add(searchSeq);

            search = "AAGCAAAATTAAACAGAGAAGAAATAGATGGGGTAAAGCTGGAATCAACAAGGATTTACCAGATTTTGGC";
            searchSeq = new Sequence(Alphabets.DNA, search);
            searchSeq.ID = "Q3";
            searchSeqs.Add(searchSeq);

            NUCmer nucmer = new NUCmer3();
            nucmer.MaximumSeparation = 0;
            nucmer.MinimumScore = 2;
            nucmer.SeparationFactor = 0.12F;
            nucmer.LengthOfMUM = 5;
            nucmer.BreakLength = 2;
            IList<IPairwiseSequenceAlignment> result = nucmer.Align(referenceSeqs, searchSeqs);

            // Check if output is not null
            Assert.AreNotEqual(null, result);

            List<IPairwiseSequenceAlignment> expectedOutput = new List<IPairwiseSequenceAlignment>();
            IPairwiseSequenceAlignment align = new PairwiseSequenceAlignment();
            PairwiseAlignedSequence alignedSeq = new PairwiseAlignedSequence();

            alignedSeq.FirstSequence = new Sequence(Alphabets.DNA, "AAAGGGA");
            alignedSeq.SecondSequence = new Sequence(Alphabets.DNA, "AAAGGGA");
            alignedSeq.Consensus = new Sequence(Alphabets.DNA, "AAAGGGA");
            alignedSeq.Score = 21;
            alignedSeq.FirstOffset = 8;
            alignedSeq.SecondOffset = 0;
            align.PairwiseAlignedSequences.Add(alignedSeq);
            alignedSeq = new PairwiseAlignedSequence();
            alignedSeq.FirstSequence = new Sequence(Alphabets.DNA, "CATTA");
            alignedSeq.SecondSequence = new Sequence(Alphabets.DNA, "CATTA");
            alignedSeq.Consensus = new Sequence(Alphabets.DNA, "CATTA");
            alignedSeq.Score = 15;
            alignedSeq.FirstOffset = 0;
            alignedSeq.SecondOffset = 31;
            align.PairwiseAlignedSequences.Add(alignedSeq);
            expectedOutput.Add(align);

            align = new PairwiseSequenceAlignment();
            alignedSeq = new PairwiseAlignedSequence();
            alignedSeq.FirstSequence = new Sequence(Alphabets.DNA, "ATGTT");
            alignedSeq.SecondSequence = new Sequence(Alphabets.DNA, "ATGTT");
            alignedSeq.Consensus = new Sequence(Alphabets.DNA, "ATGTT");
            alignedSeq.Score = 15;
            alignedSeq.FirstOffset = 13;
            alignedSeq.SecondOffset = 0;
            align.PairwiseAlignedSequences.Add(alignedSeq);
            alignedSeq = new PairwiseAlignedSequence();
            alignedSeq.FirstSequence = new Sequence(Alphabets.DNA, "GAATGC");
            alignedSeq.SecondSequence = new Sequence(Alphabets.DNA, "GAATGC");
            alignedSeq.Consensus = new Sequence(Alphabets.DNA, "GAATGC");
            alignedSeq.Score = 18;
            alignedSeq.FirstOffset = 0;
            alignedSeq.SecondOffset = 11;
            align.PairwiseAlignedSequences.Add(alignedSeq);
            alignedSeq = new PairwiseAlignedSequence();
            alignedSeq.FirstSequence = new Sequence(Alphabets.DNA, "TTTTT");
            alignedSeq.SecondSequence = new Sequence(Alphabets.DNA, "TTTTT");
            alignedSeq.Consensus = new Sequence(Alphabets.DNA, "TTTTT");
            alignedSeq.Score = 15;
            alignedSeq.FirstOffset = 31;
            alignedSeq.SecondOffset = 0;
            align.PairwiseAlignedSequences.Add(alignedSeq);
            expectedOutput.Add(align);

            align = new PairwiseSequenceAlignment();
            alignedSeq = new PairwiseAlignedSequence();
            alignedSeq.FirstSequence = new Sequence(Alphabets.DNA, "CAAAA");
            alignedSeq.SecondSequence = new Sequence(Alphabets.DNA, "CAAAA");
            alignedSeq.Consensus = new Sequence(Alphabets.DNA, "CAAAA");
            alignedSeq.Score = 15;
            alignedSeq.FirstOffset = 3;
            alignedSeq.SecondOffset = 0;
            align.PairwiseAlignedSequences.Add(alignedSeq);
            alignedSeq = new PairwiseAlignedSequence();
            alignedSeq.FirstSequence = new Sequence(Alphabets.DNA, "GGATT");
            alignedSeq.SecondSequence = new Sequence(Alphabets.DNA, "GGATT");
            alignedSeq.Consensus = new Sequence(Alphabets.DNA, "GGATT");
            alignedSeq.Score = 15;
            alignedSeq.FirstOffset = 45;
            alignedSeq.SecondOffset = 0;
            align.PairwiseAlignedSequences.Add(alignedSeq);
            alignedSeq = new PairwiseAlignedSequence();
            alignedSeq.FirstSequence = new Sequence(Alphabets.DNA, "GCAAA");
            alignedSeq.SecondSequence = new Sequence(Alphabets.DNA, "GCAAA");
            alignedSeq.Consensus = new Sequence(Alphabets.DNA, "GCAAA");
            alignedSeq.Score = 15;
            alignedSeq.FirstOffset = 0;
            alignedSeq.SecondOffset = 9;
            align.PairwiseAlignedSequences.Add(alignedSeq);
            alignedSeq = new PairwiseAlignedSequence();
            alignedSeq.FirstSequence = new Sequence(Alphabets.DNA, "TTACC");
            alignedSeq.SecondSequence = new Sequence(Alphabets.DNA, "TTACC");
            alignedSeq.Consensus = new Sequence(Alphabets.DNA, "TTACC");
            alignedSeq.Score = 15;
            alignedSeq.FirstOffset = 22;
            alignedSeq.SecondOffset = 0;
            align.PairwiseAlignedSequences.Add(alignedSeq);
            expectedOutput.Add(align);
            Assert.IsTrue(CompareAlignment(result, expectedOutput));
        }

        #endregion NUCmer Test Cases

        /// <summary>
        /// Compare the alignment of mummer and defined alignment
        /// </summary>
        /// <param name="result">output of Aligners</param>
        /// <param name="expectedAlignment">expected output</param>
        /// <returns>Compare result of alignments</returns>
        private static bool CompareAlignment(
                IList<IPairwiseSequenceAlignment> result,
                IList<IPairwiseSequenceAlignment> expectedAlignment)
        {
            bool output = true;
            if (result.Count == expectedAlignment.Count)
            {
                for (int count = 0; count < result.Count; count++)
                {
                    if (result[count].PairwiseAlignedSequences.Count == expectedAlignment[count].PairwiseAlignedSequences.Count)
                    {
                        for (int count1 = 0; count1 < result[count].PairwiseAlignedSequences.Count; count1++)
                        {
                            if (result[count].PairwiseAlignedSequences[count1].FirstSequence.ToString().Equals(
                                    expectedAlignment[count].PairwiseAlignedSequences[count1].FirstSequence.ToString())
                                && result[count].PairwiseAlignedSequences[count1].SecondSequence.ToString().Equals(
                                    expectedAlignment[count].PairwiseAlignedSequences[count1].SecondSequence.ToString())
                                && result[count].PairwiseAlignedSequences[count1].Consensus.ToString().Equals(
                                    expectedAlignment[count].PairwiseAlignedSequences[count1].Consensus.ToString())
                                && result[count].PairwiseAlignedSequences[count1].FirstOffset ==
                                    expectedAlignment[count].PairwiseAlignedSequences[count1].FirstOffset
                                && result[count].PairwiseAlignedSequences[count1].SecondOffset ==
                                    expectedAlignment[count].PairwiseAlignedSequences[count1].SecondOffset
                                && result[count].PairwiseAlignedSequences[count1].Score ==
                                    expectedAlignment[count].PairwiseAlignedSequences[count1].Score)
                            {
                                output = true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }

            return output;
        }
    }
}