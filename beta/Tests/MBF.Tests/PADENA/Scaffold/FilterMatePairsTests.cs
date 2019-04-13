﻿// *****************************************************************
//    Copyright (c) Microsoft. All rights reserved.
//    This code is licensed under the Microsoft Public License.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
// *****************************************************************

using System.Collections.Generic;
using System.Linq;
using MBF.Algorithms.Assembly;
using MBF.Algorithms.Assembly.Graph;
using MBF.Algorithms.Assembly.PaDeNA;
using MBF.Algorithms.Assembly.PaDeNA.Scaffold;
using MBF.Util.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MBF.Tests
{
    /// <summary>
    /// Filter mate pairs Based on Orientation 
    /// </summary>
    [TestClass]
    public class FilterMatePairsTests : ParallelDeNovoAssembler
    {
        static FilterMatePairsTests()
        {
            Trace.Set(Trace.SeqWarnings);
            if (!ApplicationLog.Ready)
            {
                ApplicationLog.Open("mbf.tests.log");
            }
        }

        /// <summary>
        /// Contig formed in forward direction, but one mate pair doesn't support orientation.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void FilterMatePairWithTwoContigs()
        {
            const int kmerLength = 6;
            IList<ISequence> sequences = new List<ISequence>();
            Sequence seq = new Sequence(Alphabets.DNA, "GATCTGATAA");
            seq.ID = ">gi|263191773|ref|NG_015830.1| Homo sapiens insulin receptor substrate 1 (IRS1) on chromosome 2.X1:0.5K";
            sequences.Add(seq);
            seq = new Sequence(Alphabets.DNA, "ATCTGATAAG");
            seq.ID = ">gi|263191773|ref|NG_015830.1| Homo sapiens insulin receptor on chromosome 2.F:0.5K";
            sequences.Add(seq);
            seq = new Sequence(Alphabets.DNA, "TCTGATAAGG");
            seq.ID = ">gi|263191773|ref | Homo sapiens ........insulin receptor on chromosome 2.2:0.5K";
            sequences.Add(seq);
            seq = new Sequence(Alphabets.DNA, "TTTTTGATGG");
            seq.ID = ">gi|263191773|ref|NG_015830.1| Homo sapiens insulin receptor substrate 1 (IRS1) on chromosome 2.Y1:0.5K";
            sequences.Add(seq);
            seq = new Sequence(Alphabets.DNA, "TTTTGATGGC");
            seq.ID = ">gi|263191773|ref|NG_015830.1| Homo sapiens insulin receptor on chromosome 2.R:0.5K";
            sequences.Add(seq);
            seq = new Sequence(Alphabets.DNA, "TTTGATGGCA");
            seq.ID = ">gi|263191773|ref | Homo sapiens ........insulin receptor on chromosome 2.1:0.5K";
            sequences.Add(seq);

            IList<ISequence> contigs = new List<ISequence> { new Sequence(Alphabets.DNA, "GATCTGATAAGG"), 
                new Sequence(Alphabets.DNA, "TTTTTGATGGCA")};

            ReadContigMapper mapper = new ReadContigMapper();
            ReadContigMap maps = mapper.Map(contigs, sequences, kmerLength);

            MatePairMapper mapPairedReads = new MatePairMapper();
            ContigMatePairs pairs = mapPairedReads.MapContigToMatePairs(sequences, maps);

            OrientationBasedMatePairFilter filter = new OrientationBasedMatePairFilter();
            ContigMatePairs contigpairedReads = filter.FilterPairedReads(pairs);
            Assert.AreEqual(contigpairedReads.Values.Count, 1);

            Assert.IsTrue(contigpairedReads.ContainsKey(contigs[0]));
            Dictionary<ISequence, IList<ValidMatePair>> map = contigpairedReads[contigs[0]];
            Assert.IsTrue(map.ContainsKey(contigs[1]));

            List<ValidMatePair> valid = Sort(map[contigs[1]], sequences);
            Assert.AreEqual(valid[0].ForwardReadStartPosition[0], 1);
            Assert.AreEqual(valid[0].ReverseReadReverseComplementStartPosition[0], 10);
            Assert.AreEqual(valid[0].ReverseReadStartPosition[0], 10);

            Assert.AreEqual(valid[1].ForwardReadStartPosition[0], 0);
            Assert.AreEqual(valid[1].ReverseReadReverseComplementStartPosition[0], 11);
            Assert.AreEqual(valid[1].ReverseReadStartPosition[0], 9);
        }

        /// <summary>
        /// Contig formed in forward direction and reverse complementary of contig, 
        /// but one mate pair doesn't support orientation.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        [TestCategory("Priority0")]
        public void FilterMatePairWithTwoContigsReverseComplement()
        {
            const int kmerLength = 6;

            List<ISequence> sequences = new List<ISequence>();
            Sequence seq = new Sequence(Alphabets.DNA, "GATCTGATAA");
            seq.ID = ">gi|263191773|ref|NG_015830.1| Homo sapiens insulin receptor substrate 1 (IRS1) on chromosome 2.X1:0.5K";
            sequences.Add(seq);
            seq = new Sequence(Alphabets.DNA, "ATCTGATAAG");
            seq.ID = ">gi|263191773|ref|NG_015830.1| Homo sapiens insulin receptor on chromosome 2.F:0.5K";
            sequences.Add(seq);
            seq = new Sequence(Alphabets.DNA, "TCTGATAAGG");
            seq.ID = ">gi|263191773|ref | Homo sapiens ........insulin receptor on chromosome 2.2:0.5K";
            sequences.Add(seq);
            seq = new Sequence(Alphabets.DNA, "CCATCAAAAA");
            seq.ID = ">gi|263191773|ref|NG_015830.1| Homo sapiens insulin receptor substrate 1 (IRS1) on chromosome 2.Y1:0.5K";
            sequences.Add(seq);
            seq = new Sequence(Alphabets.DNA, "TTTTGATGGC");
            seq.ID = ">gi|263191773|ref|NG_015830.1| Homo sapiens insulin receptor on chromosome 2.R:0.5K";
            sequences.Add(seq);
            seq = new Sequence(Alphabets.DNA, "TTTGATGGCA");
            seq.ID = ">gi|263191773|ref | Homo sapiens ........insulin receptor on chromosome 2.1:0.5K";
            sequences.Add(seq);
            IList<ISequence> contigs = new List<ISequence> { new Sequence(Alphabets.DNA, "GATCTGATAAGG"), 
                new Sequence(Alphabets.DNA, "TGCCATCAAAAA")};

            ReadContigMapper mapper = new ReadContigMapper();
            ReadContigMap maps = mapper.Map(contigs, sequences, kmerLength);

            MatePairMapper mapPairedReads = new MatePairMapper();
            ContigMatePairs pairs = mapPairedReads.MapContigToMatePairs(sequences, maps);

            OrientationBasedMatePairFilter filter = new OrientationBasedMatePairFilter();
            ContigMatePairs contigpairedReads = filter.FilterPairedReads(pairs);
            Assert.AreEqual(contigpairedReads.Values.Count, 1);

            Assert.IsTrue(contigpairedReads.ContainsKey(contigs[0]));
            Dictionary<ISequence, IList<ValidMatePair>> map = contigpairedReads[contigs[0]];
            Assert.IsTrue(map.ContainsKey(contigs[1]));

            List<ValidMatePair> valid = Sort(map[contigs[1]], sequences);
            Assert.AreEqual(valid[0].ForwardReadStartPosition[0], 1);
            Assert.AreEqual(valid[0].ReverseReadReverseComplementStartPosition[0], 10);
            Assert.AreEqual(valid[0].ReverseReadStartPosition[0], 10);

            Assert.AreEqual(valid[1].ForwardReadStartPosition[0], 0);
            Assert.AreEqual(valid[1].ReverseReadReverseComplementStartPosition[0], 9);
            Assert.AreEqual(valid[1].ReverseReadStartPosition[0], 11);
        }

        ///<summary>
        /// Sort Valid mate pairs based on forward reads.
        /// For consistent output due to parallel implementation.
        /// </summary>
        /// <param name="list">List of Paired Reads</param>
        /// <param name="reads">Forward reads.</param>
        /// <returns>Sorted List of Paired reads</returns>
        private static List<ValidMatePair> Sort(IList<ValidMatePair> list, IList<ISequence> reads)
        {
            return (from valid in list
                    orderby valid.PairedRead.GetForwardRead(reads).ToString()
                    select valid).ToList();
        }
    }
}
