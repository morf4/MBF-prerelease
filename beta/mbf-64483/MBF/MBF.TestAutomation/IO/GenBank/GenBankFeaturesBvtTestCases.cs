﻿// *****************************************************************
//    Copyright (c) Microsoft. All rights reserved.
//    This code is licensed under the Microsoft Public License.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
// *****************************************************************

/****************************************************************************
 * GenBankFeaturesBvtTestCases.cs
 * 
 *   This file contains the GenBank Features Bvt test cases.
 * 
***************************************************************************/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using MBF.IO;
using MBF.IO.GenBank;
using MBF.TestAutomation.Util;
using MBF.Util.Logging;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MBF.TestAutomation.IO.GenBank
{
    /// <summary>
    /// GenBank Features Bvt test case implementation.
    /// </summary>
    [TestClass]
    public class GenBankFeaturesBvtTestCases
    {
        #region Enums

        /// <summary>
        /// GenBank location operator used for different testcases.
        /// </summary>
        enum LocationOperatorParameter
        {
            Join,
            Complement,
            Order,
            Default
        };

        #endregion Enums

        #region Global Variables

        Utility _utilityObj = new Utility(@"TestUtils\GenBankFeaturesTestConfig.xml");

        #endregion Global Variables

        #region Constructor

        /// <summary>
        /// Static constructor to open log and make other settings needed for test
        /// </summary>
        static GenBankFeaturesBvtTestCases()
        {
            Trace.Set(Trace.SeqWarnings);
            if (!ApplicationLog.Ready)
            {
                ApplicationLog.Open("mbf.automation.log");
            }
        }

        #endregion Constructor

        #region Genbank Features Bvt test cases

        /// <summary>
        /// Format a valid DNA sequence to GenBank file
        /// and validate GenBank features.
        /// Input : DNA Sequence
        /// Output : Validate GenBank features.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ValidateGenBankFeaturesForDNASequence()
        {
            ValidateGenBankFeatures(Constants.SimpleGenBankDnaNodeName,
                "DNA");
        }

        /// <summary>
        /// Format a valid Protein sequence to GenBank file
        /// and validate GenBank features.
        /// Input : Protein Sequence
        /// Output : Validate GenBank features.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ValidateGenBankFeaturesForPROTEINSequence()
        {
            ValidateGenBankFeatures(Constants.SimpleGenBankProNodeName,
                "Protein");
        }

        /// <summary>
        /// Format a valid RNA sequence to GenBank file
        /// and validate GenBank features.
        /// Input : RNA Sequence
        /// Output : Validate GenBank features.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ValidateGenBankFeaturesForRNASequence()
        {
            ValidateGenBankFeatures(Constants.SimpleGenBankRnaNodeName,
                "RNA");
        }

        /// <summary>
        /// Format a valid DNA sequence to GenBank file,
        /// add new features and validate GenBank features.
        /// Input : DNA Sequence
        /// Output : Validate addition of new GenBank features.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ValidateAdditionOfGenBankFeaturesForDNASequence()
        {
            ValidateAdditionGenBankFeatures(Constants.SimpleGenBankDnaNodeName);
        }

        /// <summary>
        /// Format a valid Protein sequence to GenBank file,
        /// add new features and validate GenBank features.
        /// Input : Protein Sequence
        /// Output : Validate addition of new GenBank features.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ValidateAdditionOfGenBankFeaturesForPROTEINSequence()
        {
            ValidateAdditionGenBankFeatures(Constants.SimpleGenBankProNodeName);
        }

        /// <summary>
        /// Format a valid RNA sequence to GenBank file
        /// add new features and validate GenBank features.
        /// Input : RNA Sequence
        /// Output : Validate addition of new GenBank features.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ValidateAdditionOfGenBankFeaturesForRNASequence()
        {
            ValidateAdditionGenBankFeatures(Constants.RNAGenBankFeaturesNode);
        }

        /// <summary>
        /// Format a valid DNA sequence to GenBank file
        /// and validate GenBank DNA sequence standard features.
        /// Input : Valid DNA sequence.
        /// Output : Validation 
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ValidateDNASeqStandardFeaturesKey()
        {
            ValidateStandardFeaturesKey(Constants.DNAStandardFeaturesKeyNode,
               "DNA");
        }

        /// <summary>
        /// Format a valid Protein sequence to GenBank file
        /// and validate GenBank Protein seq standard features.
        /// Input : Valid Protein sequence.
        /// Output : Validation 
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ValidatePROTEINSeqStandardFeaturesKey()
        {
            ValidateStandardFeaturesKey(Constants.SimpleGenBankProNodeName,
                "Protein");
        }

        /// <summary>
        /// Format a valid sequence to 
        /// GenBank file using GenBankFormatter(File-Path) constructor and 
        /// validate GenBank Features.
        /// Input : MultiSequence GenBank DNA file.
        /// Validation : Validate GenBank Features.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ValidateGenBankFeaturesForMultipleDNASequence()
        {
            ValidateGenBankFeatures(Constants.MultiSequenceGenBankDNANode,
                "DNA");
        }

        /// <summary>
        /// Format a valid sequence to 
        /// GenBank file using GenBankFormatter(File-Path) constructor and 
        /// validate GenBank Features.
        /// Input : MultiSequence GenBank Protein file.
        /// Validation : Validate GenBank Features.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ValidateGenBankFeaturesForMultiplePROTEINSequence()
        {
            ValidateGenBankFeatures(Constants.MultiSeqGenBankProteinNode,
                "Protein");
        }

        /// <summary>
        /// Validate GenBank features with Binary Formatter.
        /// Input : GenBank DNA Sequence..
        /// Output : Validation of GenBank features with Binary formatterObj.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ValidateGenBankFeaturesWithBinaryFormatter()
        {
            // Get Values from XML node.
            string filePath = _utilityObj._xmlUtil.GetTextValue(
                Constants.DNAStandardFeaturesKeyNode, Constants.FilePathNode);
            string expectedCodningSeqCount = _utilityObj._xmlUtil.GetTextValue(
                 Constants.DNAStandardFeaturesKeyNode, Constants.CDSCount);
            string mRNAFeatureCount = _utilityObj._xmlUtil.GetTextValue(
                Constants.DNAStandardFeaturesKeyNode, Constants.mRNACount);
            string exonFeatureCount = _utilityObj._xmlUtil.GetTextValue(
                 Constants.DNAStandardFeaturesKeyNode, Constants.ExonCount);
            string intronFeatureCount = _utilityObj._xmlUtil.GetTextValue(
                Constants.DNAStandardFeaturesKeyNode, Constants.IntronCount);
            string expectedtRNA = _utilityObj._xmlUtil.GetTextValue(
                Constants.DNAStandardFeaturesKeyNode, Constants.tRNACount);
            string expectedGeneCount = _utilityObj._xmlUtil.GetTextValue(
                Constants.DNAStandardFeaturesKeyNode, Constants.GeneCount);
            string expectedPromoterCount = _utilityObj._xmlUtil.GetTextValue(
                Constants.DNAStandardFeaturesKeyNode, Constants.PromoterCount);
            string miscFeatureCount = _utilityObj._xmlUtil.GetTextValue(
                Constants.DNAStandardFeaturesKeyNode, Constants.MiscFeatureCount);
            string allFeaturesCount = _utilityObj._xmlUtil.GetTextValue(
                Constants.DNAStandardFeaturesKeyNode, Constants.GenBankFeaturesCount);

            // Opne file in edit mode.
            using (Stream fileStream = File.Open("GenBankData", FileMode.Create))
            {
                // Create Binary Formatter object. 
                BinaryFormatter formatterObj = new BinaryFormatter();

                // Parse a GenBank file.
                ISequenceParser parserObj = new GenBankParser();
                ISequence seq = parserObj.ParseOne(filePath);

                // Validate Metadata before serialization.
                GenBankMetadata metadata = seq.Metadata[Constants.GenBank] as GenBankMetadata;

                Assert.AreEqual(metadata.Features.Introns.Count.ToString((IFormatProvider)null),
                    intronFeatureCount);
                Assert.AreEqual(metadata.Features.Genes.Count.ToString((IFormatProvider)null),
                    expectedGeneCount);
                Assert.AreEqual(metadata.Features.Exons.Count.ToString((IFormatProvider)null),
                    exonFeatureCount);
                Assert.AreEqual(metadata.Features.MiscFeatures.Count.ToString((IFormatProvider)null),
                    miscFeatureCount);
                Assert.AreEqual(metadata.Features.Promoters.Count.ToString((IFormatProvider)null),
                    expectedPromoterCount);
                Assert.AreEqual(metadata.Features.TransferRNAs.Count.ToString((IFormatProvider)null),
                    expectedtRNA);
                Assert.AreEqual(metadata.Features.All.Count.ToString((IFormatProvider)null),
                    allFeaturesCount);
                Assert.AreEqual(metadata.Features.CodingSequences.Count.ToString((IFormatProvider)null),
                    expectedCodningSeqCount);
                Assert.AreEqual(metadata.Features.MessengerRNAs.Count.ToString((IFormatProvider)null),
                    mRNAFeatureCount);

                formatterObj.Serialize(fileStream, metadata);
                fileStream.Seek(0, SeekOrigin.Begin);

                // Deserialize data.
                GenBankMetadata deserializedMetadata =
                    (GenBankMetadata)formatterObj.Deserialize(fileStream);

                // Validate GenBank features after deserialization.
                Assert.AreNotSame(metadata, deserializedMetadata);
                Assert.AreEqual(deserializedMetadata.Features.All.Count.ToString((IFormatProvider)null),
                    allFeaturesCount);
                Assert.AreEqual(deserializedMetadata.Features.CodingSequences.Count.ToString((IFormatProvider)null),
                    expectedCodningSeqCount);
                Assert.AreEqual(deserializedMetadata.Features.Exons.Count.ToString((IFormatProvider)null),
                    exonFeatureCount);
                Assert.AreEqual(deserializedMetadata.Features.Introns.Count.ToString((IFormatProvider)null),
                    intronFeatureCount);
                Assert.AreEqual(deserializedMetadata.Features.Genes.Count.ToString((IFormatProvider)null),
                    expectedGeneCount);
                Assert.AreEqual(deserializedMetadata.Features.MiscFeatures.Count.ToString((IFormatProvider)null),
                    miscFeatureCount);
                Assert.AreEqual(deserializedMetadata.Features.Promoters.Count.ToString((IFormatProvider)null),
                    expectedPromoterCount);
                Assert.AreEqual(deserializedMetadata.Features.TransferRNAs.Count.ToString((IFormatProvider)null),
                    expectedtRNA);
                Assert.AreEqual(deserializedMetadata.Features.MessengerRNAs.Count.ToString((IFormatProvider)null),
                    mRNAFeatureCount);

                // Log NUnit GUI.
                Console.WriteLine(string.Format((IFormatProvider)null,
                    "GenBank Features BVT: Successfully validated the mRNA GenBank features '{0}'",
                   deserializedMetadata.Features.MessengerRNAs.Count.ToString((IFormatProvider)null)));
                Console.WriteLine(string.Format((IFormatProvider)null,
                    "GenBank Features BVT: Successfully validated the tRNA GenBank features '{0}'",
                   deserializedMetadata.Features.TransferRNAs.Count.ToString((IFormatProvider)null)));
                Console.WriteLine(string.Format((IFormatProvider)null,
                    "GenBank Features BVT: Successfully validated the misc GenBank features '{0}'",
                    deserializedMetadata.Features.MiscFeatures.Count.ToString((IFormatProvider)null)));
                Console.WriteLine(string.Format((IFormatProvider)null,
                    "GenBank Features BVT: Successfully validated the Promoters GenBank features '{0}'",
                   deserializedMetadata.Features.Promoters.Count.ToString((IFormatProvider)null)));
            }

        }

        /// <summary>
        /// Parse a Valid DNA Sequence and Validate Features 
        /// within specified range.
        /// Input : Valid DNA Sequence and specified range.
        /// Ouput : Validation of features count within specified range.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ValidateFeaturesWithinRangeForDNASequence()
        {
            ValidateGetFeatures(Constants.DNAStandardFeaturesKeyNode, null);
        }

        /// <summary>
        /// Parse a Valid RNA Sequence and Validate Features 
        /// within specified range.
        /// Input : Valid RNA Sequence and specified range.
        /// Ouput : Validation of features count within specified range.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ValidateFeaturesWithinRangeForRNASequence()
        {
            ValidateGetFeatures(Constants.RNAGenBankFeaturesNode, null);
        }

        /// <summary>
        /// Parse a Valid Protein Seq and Validate features 
        /// within specified range.
        /// Input : Valid Protein Sequence and specified range.
        /// Ouput : Validation of features count within specified range.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ValidateFeaturesWithinRangeForPROTEINSequence()
        {
            ValidateGetFeatures(Constants.SimpleGenBankProNodeName, null);
        }

        /// <summary>
        /// Parse a Valid DNA Sequence and Validate CDS Qualifiers.
        /// Input : Valid DNA Sequence.
        /// Ouput : Validation of all CDS Qualifiers..
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ValidateDNASequenceCDSQualifiers()
        {
            ValidateCDSQualifiers(Constants.DNAStandardFeaturesKeyNode, "DNA");
        }

        /// <summary>
        /// Parse a Valid Protein Sequence and Validate CDS Qualifiers.
        /// Input : Valid Protein Sequence.
        /// Ouput : Validation of all CDS Qualifiers..
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ValidatePROTEINSequenceCDSQualifiers()
        {
            ValidateCDSQualifiers(Constants.SimpleGenBankProNodeName, "Protein");
        }

        /// <summary>
        /// Parse a Valid RNA Sequence and Validate CDS Qualifiers.
        /// Input : Valid RNA Sequence.
        /// Ouput : Validation of all CDS Qualifiers..
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ValidateRNASequenceCDSQualifiers()
        {
            ValidateCDSQualifiers(Constants.RNAGenBankFeaturesNode, "RNA");
        }

        /// <summary>
        /// Parse a Valid DNA Sequence and Validate CDS Qualifiers.
        /// Input : Valid DNA Sequence and accession number.
        /// Ouput : Validation of all CDS Qualifiers..
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ValidateFeaturesUsingAccessionForDNASequence()
        {
            ValidateGetFeatures(Constants.DNAStandardFeaturesKeyNode, "Accession");
        }

        /// <summary>
        /// Parse a Valid RNA Sequence and Validate CDS Qualifiers.
        /// Input : Valid RNA Sequence and accession number.
        /// Ouput : Validation of all CDS Qualifiers..
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ValidateFeaturesUsingAccessionForRNASequence()
        {
            ValidateGetFeatures(Constants.RNAGenBankFeaturesNode, "Accession");
        }

        /// <summary>
        /// Parse a Valid Protein Sequence and Validate CDS Qualifiers.
        /// Input : Valid Protein Sequence and accession number.
        /// Ouput : Validation of all CDS Qualifiers..
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ValidateFeaturesUsingAccessionForPROTEINSequence()
        {
            ValidateGetFeatures(Constants.SimpleGenBankProNodeName, "Accession");
        }

        /// <summary>
        /// Parse a Valid DNA Sequence and validate citation referenced
        /// present in GenBank metadata.
        /// Input : Valid DNA Sequence and specified range.
        /// Ouput : Validation of citation referneced.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ValidateCitationReferencedForDNASequence()
        {
            ValidateCitationReferenced(Constants.DNAStandardFeaturesKeyNode);
        }

        /// <summary>
        /// Parse a Valid RNA Sequence and validate citation referenced
        /// present in GenBank metadata.
        /// Input : Valid RNA Sequence and specified range.
        /// Ouput : Validation of citation referneced.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ValidateCitationReferencedForRNASequence()
        {
            ValidateCitationReferenced(Constants.RNAGenBankFeaturesNode);
        }

        /// <summary>
        /// Parse a Valid Protein Sequence and validate citation referenced
        /// present in GenBank metadata.
        /// Input : Valid Protein Sequence and specified range.
        /// Ouput : Validation of citation referneced.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ValidateCitationReferencedForPROTEINSequence()
        {
            ValidateCitationReferenced(Constants.SimpleGenBankProNodeName);
        }

        /// <summary>
        /// Parse a Valid DNA Sequence and validate citation referenced
        /// present in GenBank metadata by passing featureItem.
        /// Input : Valid DNA Sequence and specified range.
        /// Ouput : Validation of citation referneced.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ValidateCitationReferencedForDNASequenceUsingFeatureItem()
        {
            ValidateCitationReferencedUsingFeatureItem(
                Constants.DNAStandardFeaturesKeyNode);
        }

        /// <summary>
        /// Parse a Valid RNA Sequence and validate citation referenced
        /// present in GenBank metadata by passing featureItem.
        /// Input : Valid RNA Sequence and specified range.
        /// Ouput : Validation of citation referneced.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ValidateCitationReferencedForRNASequenceUsingFeatureItem()
        {
            ValidateCitationReferencedUsingFeatureItem(Constants.RNAGenBankFeaturesNode);
        }

        /// <summary>
        /// Parse a Valid Protein Sequence and validate citation referenced
        /// present in GenBank metadata by passing featureItem.
        /// Input : Valid Protein Sequence and specified range.
        /// Ouput : Validation of citation referneced.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ValidateCitationReferencedForPROTEINSequenceUsingFeatureItem()
        {
            ValidateCitationReferencedUsingFeatureItem(Constants.SimpleGenBankProNodeName);
        }

        /// <summary>
        /// Vaslidate Genbank Properties.
        /// Input : Genbank sequence.
        /// Output : validation of GenBank features.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ValidateGenBankFeatureProperties()
        {
            // Get Values from XML node.
            string filePath = _utilityObj._xmlUtil.GetTextValue(
                Constants.DNAStandardFeaturesKeyNode, Constants.FilePathNode);
            string mRNAFeatureCount = _utilityObj._xmlUtil.GetTextValue(
                Constants.DNAStandardFeaturesKeyNode, Constants.mRNACount);
            string exonFeatureCount = _utilityObj._xmlUtil.GetTextValue(
                Constants.DNAStandardFeaturesKeyNode, Constants.ExonCount);
            string intronFeatureCount = _utilityObj._xmlUtil.GetTextValue(
                Constants.DNAStandardFeaturesKeyNode, Constants.IntronCount);
            string cdsFeatureCount = _utilityObj._xmlUtil.GetTextValue(
                Constants.DNAStandardFeaturesKeyNode, Constants.CDSCount);
            string allFeaturesCount = _utilityObj._xmlUtil.GetTextValue(
                Constants.DNAStandardFeaturesKeyNode, Constants.GenBankFeaturesCount);
            string GenesCount = _utilityObj._xmlUtil.GetTextValue(
                Constants.DNAStandardFeaturesKeyNode, Constants.GeneCount);
            string miscFeaturesCount = _utilityObj._xmlUtil.GetTextValue(
                Constants.DNAStandardFeaturesKeyNode, Constants.MiscFeatureCount);
            string rRNACount = _utilityObj._xmlUtil.GetTextValue(
                Constants.DNAStandardFeaturesKeyNode, Constants.rRNACount);
            string tRNACount = _utilityObj._xmlUtil.GetTextValue(
                Constants.DNAStandardFeaturesKeyNode, Constants.tRNACount);
            string zeroValue = _utilityObj._xmlUtil.GetTextValue(
                Constants.DNAStandardFeaturesKeyNode, Constants.emptyCount);

            ISequenceParser parserObj = new GenBankParser();
            ISequence seq = parserObj.ParseOne(filePath);

            // Get all metada features. Hitting all the properties in the metadata feature.
            GenBankMetadata metadata = (GenBankMetadata)seq.Metadata[Constants.GenBank];
            List<FeatureItem> allFeatures = metadata.Features.All;
            List<Minus10Signal> minus10Signal = metadata.Features.Minus10Signals;
            List<Minus35Signal> minus35Signal = metadata.Features.Minus35Signals;
            List<ThreePrimeUtr> threePrimeUTR = metadata.Features.ThreePrimeUTRs;
            List<FivePrimeUtr> fivePrimeUTR = metadata.Features.FivePrimeUTRs;
            List<Attenuator> attenuator = metadata.Features.Attenuators;
            List<CaatSignal> caatSignal = metadata.Features.CAATSignals;
            List<CodingSequence> CDS = metadata.Features.CodingSequences;
            List<DisplacementLoop> displacementLoop = metadata.Features.DisplacementLoops;
            List<Enhancer> enhancer = metadata.Features.Enhancers;
            List<Exon> exonList = metadata.Features.Exons;
            List<GCSingal> gcsSignal = metadata.Features.GCSignals;
            List<Gene> genesList = metadata.Features.Genes;
            List<InterveningDNA> interveningDNA = metadata.Features.InterveningDNAs;
            List<Intron> intronList = metadata.Features.Introns;
            List<LongTerminalRepeat> LTR = metadata.Features.LongTerminalRepeats;
            List<MaturePeptide> matPeptide = metadata.Features.MaturePeptides;
            List<MiscBinding> miscBinding = metadata.Features.MiscBindings;
            List<MiscDifference> miscDifference = metadata.Features.MiscDifferences;
            List<MiscFeature> miscFeatures = metadata.Features.MiscFeatures;
            List<MiscRecombination> miscRecobination =
                metadata.Features.MiscRecombinations;
            List<MiscRNA> miscRNA = metadata.Features.MiscRNAs;
            List<MiscSignal> miscSignal = metadata.Features.MiscSignals;
            List<MiscStructure> miscStructure = metadata.Features.MiscStructures;
            List<ModifiedBase> modifierBase = metadata.Features.ModifiedBases;
            List<MessengerRNA> mRNA = metadata.Features.MessengerRNAs;
            List<NonCodingRNA> nonCodingRNA = metadata.Features.NonCodingRNAs;
            List<OperonRegion> operonRegion = metadata.Features.OperonRegions;
            List<PolyASignal> polySignal = metadata.Features.PolyASignals;
            List<PolyASite> polySites = metadata.Features.PolyASites;
            List<PrecursorRNA> precursorRNA = metadata.Features.PrecursorRNAs;
            List<ProteinBindingSite> proteinBindingSites =
                metadata.Features.ProteinBindingSites;
            List<RibosomeBindingSite> rBindingSites =
                metadata.Features.RibosomeBindingSites;
            List<ReplicationOrigin> repliconOrigin =
                metadata.Features.ReplicationOrigins;
            List<RepeatRegion> repeatRegion = metadata.Features.RepeatRegions;
            List<RibosomalRNA> rRNA = metadata.Features.RibosomalRNAs;
            List<SignalPeptide> signalPeptide = metadata.Features.SignalPeptides;
            List<StemLoop> stemLoop = metadata.Features.StemLoops;
            List<TataSignal> tataSignals = metadata.Features.TATASignals;
            List<Terminator> terminator = metadata.Features.Terminators;
            List<TransferMessengerRNA> tmRNA =
                metadata.Features.TransferMessengerRNAs;
            List<TransitPeptide> transitPeptide = metadata.Features.TransitPeptides;
            List<TransferRNA> tRNA = metadata.Features.TransferRNAs;
            List<UnsureSequenceRegion> unSecureRegion =
                metadata.Features.UnsureSequenceRegions;
            List<Variation> variations = metadata.Features.Variations;

            // Validate GenBank Features.
            Assert.AreEqual(minus10Signal.Count, Convert.ToInt32(zeroValue, (IFormatProvider)null));
            Assert.AreEqual(minus35Signal.Count, Convert.ToInt32(zeroValue, (IFormatProvider)null));
            Assert.AreEqual(threePrimeUTR.Count, Convert.ToInt32(zeroValue, (IFormatProvider)null));
            Assert.AreEqual(fivePrimeUTR.Count, Convert.ToInt32(zeroValue, (IFormatProvider)null));
            Assert.AreEqual(caatSignal.Count, Convert.ToInt32(zeroValue, (IFormatProvider)null));
            Assert.AreEqual(attenuator.Count, Convert.ToInt32(zeroValue, (IFormatProvider)null));
            Assert.AreEqual(displacementLoop.Count, Convert.ToInt32(zeroValue, (IFormatProvider)null));

            Assert.AreEqual(enhancer.Count, Convert.ToInt32(zeroValue, (IFormatProvider)null));
            Assert.AreEqual(gcsSignal.Count, Convert.ToInt32(zeroValue, (IFormatProvider)null));
            Assert.AreEqual(genesList.Count.ToString((IFormatProvider)null), GenesCount);
            Assert.AreEqual(interveningDNA.Count, Convert.ToInt32(zeroValue, (IFormatProvider)null));
            Assert.AreEqual(LTR.Count, Convert.ToInt32(zeroValue, (IFormatProvider)null));
            Assert.AreEqual(matPeptide.Count, Convert.ToInt32(zeroValue, (IFormatProvider)null));
            Assert.AreEqual(miscBinding.Count, Convert.ToInt32(zeroValue, (IFormatProvider)null));


            Assert.AreEqual(miscDifference.Count, Convert.ToInt32(zeroValue, (IFormatProvider)null));
            Assert.AreEqual(miscFeatures.Count.ToString((IFormatProvider)null), miscFeaturesCount);
            Assert.AreEqual(miscRecobination.Count, Convert.ToInt32(zeroValue, (IFormatProvider)null));
            Assert.AreEqual(miscSignal.Count, Convert.ToInt32(zeroValue, (IFormatProvider)null));
            Assert.AreEqual(modifierBase.Count, Convert.ToInt32(zeroValue, (IFormatProvider)null));
            Assert.AreEqual(miscRNA.Count, Convert.ToInt32(zeroValue, (IFormatProvider)null));
            Assert.AreEqual(miscStructure.Count, Convert.ToInt32(zeroValue, (IFormatProvider)null));
            Assert.AreEqual(mRNA.Count.ToString((IFormatProvider)null), mRNAFeatureCount);
            Assert.AreEqual(nonCodingRNA.Count, Convert.ToInt32(zeroValue, (IFormatProvider)null));
            Assert.AreEqual(operonRegion.Count, Convert.ToInt32(zeroValue, (IFormatProvider)null));

            Assert.AreEqual(polySignal.Count, Convert.ToInt32(zeroValue, (IFormatProvider)null));
            Assert.AreEqual(polySites.Count, Convert.ToInt32(zeroValue, (IFormatProvider)null));
            Assert.AreEqual(precursorRNA.Count, Convert.ToInt32(zeroValue, (IFormatProvider)null));
            Assert.AreEqual(proteinBindingSites.Count, Convert.ToInt32(zeroValue, (IFormatProvider)null));
            Assert.AreEqual(rBindingSites.Count, Convert.ToInt32(zeroValue, (IFormatProvider)null));
            Assert.AreEqual(repliconOrigin.Count, Convert.ToInt32(zeroValue, (IFormatProvider)null));

            Assert.AreEqual(rRNA.Count.ToString((IFormatProvider)null), rRNACount);
            Assert.AreEqual(signalPeptide.Count, Convert.ToInt32(zeroValue, (IFormatProvider)null));
            Assert.AreEqual(stemLoop.Count, Convert.ToInt32(zeroValue, (IFormatProvider)null));
            Assert.AreEqual(tataSignals.Count, Convert.ToInt32(zeroValue, (IFormatProvider)null));
            Assert.AreEqual(repeatRegion.Count, Convert.ToInt32(zeroValue, (IFormatProvider)null));
            Assert.AreEqual(terminator.Count, Convert.ToInt32(zeroValue, (IFormatProvider)null));
            Assert.AreEqual(tmRNA.Count, Convert.ToInt32(zeroValue, (IFormatProvider)null));
            Assert.AreEqual(variations.Count, Convert.ToInt32(zeroValue, (IFormatProvider)null));

            Assert.AreEqual(tRNA.Count.ToString((IFormatProvider)null), tRNACount);
            Assert.AreEqual(transitPeptide.Count, Convert.ToInt32(zeroValue, (IFormatProvider)null));
            Assert.AreEqual(unSecureRegion.Count, Convert.ToInt32(zeroValue, (IFormatProvider)null));
            Assert.AreEqual(stemLoop.Count, Convert.ToInt32(zeroValue, (IFormatProvider)null));

            Assert.AreEqual(allFeatures.Count, Convert.ToInt32(allFeaturesCount, (IFormatProvider)null));
            Assert.AreEqual(CDS.Count, Convert.ToInt32(cdsFeatureCount, (IFormatProvider)null));
            Assert.AreEqual(exonList.Count, Convert.ToInt32(exonFeatureCount, (IFormatProvider)null));
            Assert.AreEqual(intronList.Count, Convert.ToInt32(intronFeatureCount, (IFormatProvider)null));
        }

        /// <summary>
        /// Validate location builder with normal string.
        /// Input Data : Location string "345678910";
        /// Output Data : Validation of Location start,end position,seperator
        /// and operators.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ValidateNormalStringLocationBuilder()
        {
            ValidateLocationBuilder(Constants.NormalLocationBuilderNode,
                LocationOperatorParameter.Default, false);
        }

        /// <summary>
        /// Validate location builder with Single dot seperator string.
        /// Input Data : Location string "1098945.2098765";
        /// Output Data : Validation of Location start,end position,seperator
        /// and operators.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ValidateSingleDotSeperatorLocationBuilder()
        {
            ValidateLocationBuilder(Constants.SingleDotLocationBuilderNode,
                LocationOperatorParameter.Default, false);
        }

        /// <summary>
        /// Validate location builder with Join Opperator.
        /// Input Data : Location string "join(26300..26395)";
        /// Output Data : Validation of Location start,end position,seperator
        /// and operators.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ValidateJoinOperatorLocationBuilder()
        {
            ValidateLocationBuilder(Constants.JoinOperatorLocationBuilderNode,
                LocationOperatorParameter.Join, true);
        }

        /// <summary>
        /// Validate location builder with Join Opperator.
        /// Input Data : Location string "complement(45745..50256)";
        /// Output Data : Validation of Location start,end position,seperator
        /// and operators.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ValidateComplementOperatorLocationBuilder()
        {
            ValidateLocationBuilder(Constants.ComplementOperatorLocationBuilderNode,
                LocationOperatorParameter.Complement, true);
        }

        /// <summary>
        /// Validate location builder with Order Opperator.
        /// Input Data : Location string "order(9214567.50980256)";
        /// Output Data : Validation of Location start,end position,seperator
        /// and operators.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ValidateOrderOperatorLocationBuilder()
        {
            ValidateLocationBuilder(Constants.OrderOperatorLocationBuilderNode,
                LocationOperatorParameter.Order, true);
        }

        /// <summary>
        /// Validate CDS feature location builder by passing GenBank file.
        /// Input Data : Location string "join(136..202,AF032048.1:67..345,
        /// AF032048.1:1162..1175)";
        /// Output Data : Validation of Location start,end position,seperator
        /// and operators.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ValidateSubSequenceGenBankFile()
        {
            ValidateLocationBuilder(Constants.GenBankFileLocationBuilderNode,
                LocationOperatorParameter.Join, true);
        }

        /// <summary>
        /// Validate SubSequence start, end and range of sequence.
        /// Input Data : GenBank file;
        /// Output Data : Validation of Location start,end position,seperator
        /// and operators.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ValidateSequenceFeatureForRna()
        {
            ValidateSequenceFeature(Constants.GenBankFileSubSequenceNode);
        }

        /// <summary>
        /// Validate SubSequence start, end and range of sequence.
        /// Input Data : Dna GenBank file;
        /// Output Data : Validation of Location start,end position,seperator
        /// and operators.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ValidateSequenceFeatureForDna()
        {
            ValidateSequenceFeature(Constants.GenBankFileSubSequenceDnaNode);
        }

        /// <summary>
        /// Validate SubSequence start, end and range of sequence.
        /// Input Data :Protein GenBank file;
        /// Output Data : Validation of Location start,end position,seperator
        /// and operators.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ValidateSequenceFeatureForProteinA()
        {
            ValidateSequenceFeature(Constants.GenBankFileSubSequenceProteinNode);
        }

        /// <summary>
        /// Validate SubSequence start, end and range of sequence.
        /// Input Data : GenBank file;
        /// Output Data : Validation of Location start,end position,seperator
        /// and operators.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ValidateSequenceFeatureUsingReferencedSequence()
        {
            // Get Values from XML node.
            string filePath = _utilityObj._xmlUtil.GetTextValue(
                Constants.GenBankFileSubSequenceNode, Constants.FilePathNode);
            string subSequence = _utilityObj._xmlUtil.GetTextValue(
                Constants.GenBankFileSubSequenceNode, Constants.ExpectedSubSequence);
            string subSequenceStart = _utilityObj._xmlUtil.GetTextValue(
                Constants.GenBankFileSubSequenceNode, Constants.SequenceStart);
            string subSequenceEnd = _utilityObj._xmlUtil.GetTextValue(
                Constants.GenBankFileSubSequenceNode, Constants.SequenceEnd);
            string referenceSeq = _utilityObj._xmlUtil.GetTextValue(
                Constants.GenBankFileSubSequenceNode, Constants.referenceSeq);

            ISequence sequence;
            ISequence firstFeatureSeq = null;

            // Parse a genBank file.
            Sequence refSequence = new Sequence(Alphabets.RNA, referenceSeq);
            GenBankParser parserObj = new GenBankParser();
            sequence = parserObj.ParseOne(filePath);
            GenBankMetadata metadata =
                sequence.Metadata[Constants.GenBank] as GenBankMetadata;

            // Get Subsequence feature,start and end postions.
            Dictionary<string, ISequence> referenceSequences =
                new Dictionary<string, ISequence>();
            referenceSequences.Add(Constants.Reference, refSequence);
            firstFeatureSeq = metadata.Features.All[0].GetSubSequence(sequence,
                referenceSequences);

            // Validate SubSequence.
            Assert.AreEqual(firstFeatureSeq.ToString(), subSequence);
            Assert.AreEqual(metadata.Features.All[0].Location.Start.ToString((IFormatProvider)null),
                subSequenceStart);
            Assert.AreEqual(metadata.Features.All[0].Location.End.ToString((IFormatProvider)null),
                subSequenceEnd);
            Assert.IsNull(metadata.Features.All[0].Location.Accession);
            Assert.AreEqual(metadata.Features.All[0].Location.StartData.ToString(),
                subSequenceStart);
            Assert.AreEqual(metadata.Features.All[0].Location.EndData.ToString(),
                subSequenceEnd);

            // Log to NUnit GUI
            Console.WriteLine(string.Format((IFormatProvider)null,
                "GenBank Features BVT: Successfully validated the Subsequence feature '{0}'",
                firstFeatureSeq.ToString()));
            Console.WriteLine(string.Format((IFormatProvider)null,
                "GenBank Features BVT: Successfully validated the start of subsequence'{0}'",
                metadata.Features.All[0].Location.Start.ToString((IFormatProvider)null)));
        }

        /// <summary>
        /// Validate Sequence features for Exon,CDS,Intron..
        /// Input Data : Sequence feature item and location.
        /// Output Data : Validation of created sequence features.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ValidateGenBankSubFeatures()
        {
            ValidateGenBankSubFeatures(Constants.GenBankSequenceFeaturesNode);
        }

        /// <summary>
        /// Validate Sequence features for features with Join operator.
        /// Input Data : Sequence feature item and location.
        /// Output Data : Validation of created sequence features.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ValidateGenBankSubFeatureswithOperator()
        {
            ValidateGenBankSubFeatures(Constants.GenBankSequenceFeaturesForMRNA);
        }

        /// <summary>
        /// Validate Sequence features for features with Empty sub-operator.
        /// Input Data : Sequence feature item and location.
        /// Output Data : Validation of created sequence features.
        /// </summary>
        [TestMethod]
        [Priority(0)]
        public void ValidateGenBankSubFeatureswithEmptyOperator()
        {
            ValidateAdditionGenBankFeatures(
                Constants.OperatorGenBankFileNode);
        }

        #endregion Genbank Features Bvt test cases

        #region Supporting Methods

        /// <summary>
        /// Validate GenBank features.
        /// </summary>
        /// <param name="nodeName">xml node name.</param>
        /// <param name="methodName">Name of the method</param>
        void ValidateGenBankFeatures(string nodeName,
            string methodName)
        {
            // Get Values from XML node.
            string filePath = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.FilePathNode);
            string alphabetName = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.AlphabetNameNode);
            string expectedSequence = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.ExpectedSequenceNode);
            string mRNAFeatureCount = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.mRNACount);
            string exonFeatureCount = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.ExonCount);
            string intronFeatureCount = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.IntronCount);
            string cdsFeatureCount = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.CDSCount);
            string allFeaturesCount = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.GenBankFeaturesCount);
            string expectedCDSKey = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.CDSKey);
            string expectedIntronKey = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.IntronKey);
            string expectedExonKey = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.ExonKey);
            string mRNAKey = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.mRNAKey);
            string sourceKeyName = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.SourceKey);
            string proteinKey = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.ProteinKeyName);

            ISequenceParser parserObj = new GenBankParser();
            IList<ISequence> sequenceList = parserObj.Parse(filePath);

            if (sequenceList.Count == 1)
            {
                string expectedUpdatedSequence =
                    expectedSequence.Replace("\r", "").Replace("\n", "").Replace(" ", "");
                Sequence orgSeq =
                    new Sequence(Utility.GetAlphabet(alphabetName),
                        expectedUpdatedSequence);
                orgSeq.ID = sequenceList[0].ID;
                orgSeq.MoleculeType = sequenceList[0].MoleculeType;

                orgSeq.Metadata.Add(Constants.GenBank,
                    (GenBankMetadata)sequenceList[0].Metadata[Constants.GenBank]);

                ISequenceFormatter formatterObj = new GenBankFormatter();
                formatterObj.Format(orgSeq, Constants.GenBankTempFileName);
            }
            else
            {
                string expectedUpdatedSequence =
                    expectedSequence.Replace("\r", "").Replace("\n", "").Replace(" ", "");
                Sequence orgSeq =
                    new Sequence(Utility.GetAlphabet(alphabetName), expectedUpdatedSequence);
                orgSeq.ID = sequenceList[1].ID;
                orgSeq.MoleculeType = sequenceList[1].MoleculeType;

                orgSeq.Metadata.Add(Constants.GenBank,
                    (GenBankMetadata)sequenceList[1].Metadata[Constants.GenBank]);

                ISequenceFormatter formatterObj = new GenBankFormatter();
                formatterObj.Format(orgSeq, Constants.GenBankTempFileName);
            }

            // parse a temporary file.
            GenBankParser tempParserObj = new GenBankParser();
            IList<ISequence> tempFileSeqList = tempParserObj.Parse(
                Constants.GenBankTempFileName);

            ISequence sequence = tempFileSeqList[0];

            GenBankMetadata metadata = (GenBankMetadata)sequence.Metadata[Constants.GenBank];

            // Validate formatted temporary file GenBank Features.
            Assert.AreEqual(metadata.Features.All.Count,
                Convert.ToInt32(allFeaturesCount, (IFormatProvider)null));
            Assert.AreEqual(metadata.Features.CodingSequences.Count,
                Convert.ToInt32(cdsFeatureCount, (IFormatProvider)null));
            Assert.AreEqual(metadata.Features.Exons.Count,
                Convert.ToInt32(exonFeatureCount, (IFormatProvider)null));
            Assert.AreEqual(metadata.Features.Introns.Count,
                Convert.ToInt32(intronFeatureCount, (IFormatProvider)null));
            Assert.AreEqual(metadata.Features.MessengerRNAs.Count,
                Convert.ToInt32(mRNAFeatureCount, (IFormatProvider)null));
            Assert.AreEqual(metadata.Features.Attenuators.Count, 0);
            Assert.AreEqual(metadata.Features.CAATSignals.Count, 0);
            Assert.AreEqual(metadata.Features.DisplacementLoops.Count, 0);
            Assert.AreEqual(metadata.Features.Enhancers.Count, 0);
            Assert.AreEqual(metadata.Features.Genes.Count, 0);

            if ((0 == string.Compare(methodName, "DNA", true,
                CultureInfo.CurrentCulture))
                || (0 == string.Compare(methodName, "RNA", true,
                CultureInfo.CurrentCulture)))
            {
                IList<FeatureItem> featureList = metadata.Features.All;
                Assert.AreEqual(featureList[0].Key.ToString((IFormatProvider)null), sourceKeyName);
                Assert.AreEqual(featureList[1].Key.ToString((IFormatProvider)null), mRNAKey);
                Assert.AreEqual(featureList[3].Key.ToString((IFormatProvider)null), expectedCDSKey);
                Assert.AreEqual(featureList[5].Key.ToString((IFormatProvider)null), expectedExonKey);
                Assert.AreEqual(featureList[6].Key.ToString((IFormatProvider)null), expectedIntronKey);
                ApplicationLog.WriteLine(
                    "GenBank Features BVT: Successfully validated the GenBank Features");
                Console.WriteLine(string.Format((IFormatProvider)null,
                    "GenBank Features BVT: Successfully validated the CDS feature '{0}'",
                    featureList[3].Key.ToString((IFormatProvider)null)));
                Console.WriteLine(string.Format((IFormatProvider)null,
                    "GenBank Features BVT: Successfully validated the Exon feature '{0}'",
                    featureList[5].Key.ToString((IFormatProvider)null)));
            }
            else
            {
                IList<FeatureItem> proFeatureList = metadata.Features.All;
                Assert.AreEqual(proFeatureList[0].Key.ToString((IFormatProvider)null), sourceKeyName);
                Assert.AreEqual(proFeatureList[1].Key.ToString((IFormatProvider)null), proteinKey);
                Assert.AreEqual(proFeatureList[2].Key.ToString((IFormatProvider)null), expectedCDSKey);
                ApplicationLog.WriteLine(
                "GenBank Features BVT: Successfully validated the GenBank Features");
                Console.WriteLine(string.Format((IFormatProvider)null,
                    "GenBank Features BVT: Successfully validated the CDS feature '{0}'",
                    proFeatureList[2].Key.ToString((IFormatProvider)null)));
                Console.WriteLine(string.Format((IFormatProvider)null,
                    "GenBank Features BVT: Successfully validated the Source feature '{0}'",
                    proFeatureList[0].Key.ToString((IFormatProvider)null)));
            }

            File.Delete(Constants.GenBankTempFileName);
        }

        /// <summary>
        /// Validate addition of GenBank features.
        /// </summary>
        /// <param name="nodeName">xml node name.</param>
        void ValidateAdditionGenBankFeatures(string nodeName)
        {
            // Get Values from XML node.
            string filePath = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.FilePathNode);
            string alphabetName = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.AlphabetNameNode);
            string expectedSequence = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.ExpectedSequenceNode);
            string addFirstKey = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.FirstKey);
            string addSecondKey = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.SecondKey);
            string addFirstLocation = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.FirstLocation);
            string addSecondLocation = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.SecondLocation);
            string addFirstQualifier = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.FirstQualifier);
            string addSecondQualifier = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.SecondQualifier);

            ISequenceParser parser1 = new GenBankParser();
            IList<ISequence> seqList1 = parser1.Parse(filePath);
            LocationBuilder localBuilderObj = new LocationBuilder();

            string expectedUpdatedSequence =
                expectedSequence.Replace("\r", "").Replace("\n", "").Replace(" ", "");
            Sequence orgSeq = new Sequence(Utility.GetAlphabet(alphabetName),
                expectedUpdatedSequence);
            orgSeq.ID = seqList1[0].ID;
            orgSeq.MoleculeType = seqList1[0].MoleculeType;

            orgSeq.Metadata.Add(Constants.GenBank,
                (GenBankMetadata)seqList1[0].Metadata[Constants.GenBank]);

            ISequenceFormatter formatterObj = new GenBankFormatter();
            formatterObj.Format(orgSeq, Constants.GenBankTempFileName);

            // parse GenBank file.
            GenBankParser parserObj = new GenBankParser();
            IList<ISequence> seqList = parserObj.Parse(Constants.GenBankTempFileName);

            ISequence seq = seqList[0];
            GenBankMetadata metadata = (GenBankMetadata)seq.Metadata[Constants.GenBank];

            // Add a new features to Genbank features list.
            metadata.Features = new SequenceFeatures();
            FeatureItem feature = new FeatureItem(addFirstKey, addFirstLocation);
            List<string> qualifierValues = new List<string>();
            qualifierValues.Add(addFirstQualifier);
            qualifierValues.Add(addFirstQualifier);
            feature.Qualifiers.Add(addFirstQualifier, qualifierValues);
            metadata.Features.All.Add(feature);

            feature = new FeatureItem(addSecondKey, addSecondLocation);
            qualifierValues = new List<string>();
            qualifierValues.Add(addSecondQualifier);
            qualifierValues.Add(addSecondQualifier);
            feature.Qualifiers.Add(addSecondQualifier, qualifierValues);
            metadata.Features.All.Add(feature);

            // Validate added GenBank features.
            Assert.AreEqual(metadata.Features.All[0].Key.ToString((IFormatProvider)null), addFirstKey);
            Assert.AreEqual(
                localBuilderObj.GetLocationString(metadata.Features.All[0].Location),
                addFirstLocation);
            Assert.AreEqual(metadata.Features.All[1].Key.ToString((IFormatProvider)null), addSecondKey);
            Assert.AreEqual(localBuilderObj.GetLocationString(metadata.Features.All[1].Location),
                addSecondLocation);

            //Log to Nunit GUI.
            Console.WriteLine(string.Format((IFormatProvider)null,
                "GenBank Features BVT: Successfully validated the new feature '{0}'",
                metadata.Features.All[0].Key.ToString((IFormatProvider)null)));
            Console.WriteLine(string.Format((IFormatProvider)null,
                "GenBank Features BVT: Successfully validated the new location '{0}'",
                metadata.Features.All[0].Location.ToString()));
            Console.WriteLine(string.Format((IFormatProvider)null,
                "GenBank Features BVT: Successfully validated the new feature '{0}'",
                metadata.Features.All[1].Key.ToString((IFormatProvider)null)));
            Console.WriteLine(string.Format((IFormatProvider)null,
                "GenBank Features BVT: Successfully validated the new location '{0}'",
                metadata.Features.All[1].Location.ToString()));

            File.Delete(Constants.GenBankTempFileName);
        }

        /// <summary>
        /// Validate GenBank standard features key.
        /// </summary>
        /// <param name="nodeName">xml node name.</param>
        /// <param name="methodName">Name of the method</param>
        void ValidateStandardFeaturesKey(string nodeName, string methodName)
        {
            // Get Values from XML node.
            string filePath = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.FilePathNode);
            string expectedCondingSeqCount = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.CDSCount);
            string exonFeatureCount = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.ExonCount);
            string expectedtRNA = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.tRNACount);
            string expectedGeneCount = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.GeneCount);
            string miscFeatureCount = _utilityObj._xmlUtil.GetTextValue(
                 nodeName, Constants.MiscFeatureCount);
            string expectedCDSKey = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.CDSKey);
            string expectedIntronKey = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.IntronKey);
            string mRNAKey = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.mRNAKey);
            string allFeaturesCount = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.StandardFeaturesCount);

            // Parse a file.
            ISequenceParser parserObj = new GenBankParser();
            ISequence seq = parserObj.ParseOne(filePath);

            GenBankMetadata metadata =
                seq.Metadata[Constants.GenBank] as GenBankMetadata;

            if ((0 == string.Compare(methodName, "DNA",
                true, CultureInfo.CurrentCulture))
              || (0 == string.Compare(methodName, "RNA",
              true, CultureInfo.CurrentCulture)))
            {
                // Validate standard features keys.
                Assert.AreEqual(metadata.Features.CodingSequences.Count.ToString((IFormatProvider)null),
                    expectedCondingSeqCount);
                Assert.AreEqual(metadata.Features.Exons.Count.ToString((IFormatProvider)null),
                    exonFeatureCount);
                Assert.AreEqual(metadata.Features.TransferRNAs.Count.ToString((IFormatProvider)null),
                    expectedtRNA);
                Assert.AreEqual(metadata.Features.Genes.Count.ToString((IFormatProvider)null),
                    expectedGeneCount);
                Assert.AreEqual(metadata.Features.MiscFeatures.Count.ToString((IFormatProvider)null),
                    miscFeatureCount);
                Assert.AreEqual(StandardFeatureKeys.CodingSequence.ToString((IFormatProvider)null),
                    expectedCDSKey);
                Assert.AreEqual(StandardFeatureKeys.Intron.ToString((IFormatProvider)null),
                    expectedIntronKey);
                Assert.AreEqual(StandardFeatureKeys.MessengerRNA.ToString((IFormatProvider)null),
                    mRNAKey);
                Assert.AreEqual(StandardFeatureKeys.All.Count.ToString((IFormatProvider)null),
                    allFeaturesCount);

                //Log to Nunit GUI.
                Console.WriteLine(string.Format((IFormatProvider)null,
                    "GenBank Features BVT: Successfully validated the standard feature key '{0}'",
                    StandardFeatureKeys.Intron.ToString((IFormatProvider)null)));
                Console.WriteLine(string.Format((IFormatProvider)null,
                    "GenBank Features BVT: Successfully validated the standard feature key '{0}'",
                    StandardFeatureKeys.MessengerRNA.ToString((IFormatProvider)null)));
            }
            else
            {
                Assert.AreEqual(metadata.Features.CodingSequences.Count.ToString((IFormatProvider)null),
                    expectedCondingSeqCount);
                Assert.AreEqual(StandardFeatureKeys.CodingSequence.ToString((IFormatProvider)null),
                    expectedCDSKey);

                //Log to Nunit GUI.
                Console.WriteLine(string.Format((IFormatProvider)null,
                    "GenBank Features BVT: Successfully validated the standard feature key '{0}'",
                    StandardFeatureKeys.CodingSequence.ToString((IFormatProvider)null)));
            }
        }

        /// <summary>
        /// Validate GenBank Get features with specified range.
        /// </summary>
        /// <param name="nodeName">xml node name.</param>
        /// <param name="methodName">name of method</param>
        void ValidateGetFeatures(string nodeName, string methodName)
        {
            // Get Values from XML node.
            string filePath = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.FilePathNode);
            string expectedFirstRangeStartPoint = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.FirstRangeStartPoint);
            string expectedSecondRangeStartPoint = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.SecondRangeStartPoint);
            string expectedFirstRangeEndPoint = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.FirstRangeEndPoint);
            string expectedSecondRangeEndPoint = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.SecondRangeEndPoint);
            string expectedCountWithinSecondRange = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.FeaturesWithinSecondRange);
            string expectedCountWithinFirstRange = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.FeaturesWithinFirstRange);

            // Parse a GenBank file.
            ISequenceParser parserObj = new GenBankParser();
            ISequence seq = parserObj.ParseOne(filePath);

            GenBankMetadata metadata =
                seq.Metadata[Constants.GenBank] as GenBankMetadata;
            List<CodingSequence> cdsList = metadata.Features.CodingSequences;
            string accessionNumber = cdsList[0].Location.Accession;

            if ((0 == string.Compare(methodName, "Accession",
                true, CultureInfo.CurrentCulture)))
            {
                // Validate GetFeature within specified range.
                Assert.AreEqual(metadata.GetFeatures(accessionNumber,
                    Convert.ToInt32(expectedFirstRangeStartPoint, (IFormatProvider)null),
                    Convert.ToInt32(expectedFirstRangeEndPoint, (IFormatProvider)null)).Count.ToString((IFormatProvider)null),
                    expectedCountWithinFirstRange);
                Assert.AreEqual(metadata.GetFeatures(accessionNumber,
                    Convert.ToInt32(expectedSecondRangeStartPoint, (IFormatProvider)null),
                    Convert.ToInt32(expectedSecondRangeEndPoint, (IFormatProvider)null)).Count.ToString((IFormatProvider)null),
                    expectedCountWithinSecondRange);

                // Log NUnit GUI.
                Console.WriteLine(string.Format((IFormatProvider)null,
                    "GenBank Features BVT: Successfully validated the Get GenBank features '{0}'",
                    metadata.GetFeatures(accessionNumber,
                    Convert.ToInt32(expectedSecondRangeStartPoint, (IFormatProvider)null),
                    Convert.ToInt32(expectedSecondRangeEndPoint, (IFormatProvider)null)).Count.ToString((IFormatProvider)null)));
                Console.WriteLine(string.Format((IFormatProvider)null,
                    "GenBank Features BVT: Successfully validated the Get GenBank features '{0}'",
                    metadata.GetFeatures(accessionNumber,
                    Convert.ToInt32(expectedFirstRangeStartPoint, (IFormatProvider)null),
                    Convert.ToInt32(expectedFirstRangeEndPoint, (IFormatProvider)null)).Count.ToString((IFormatProvider)null)));
            }
            else
            {
                // Validate GetFeature within specified range.
                Assert.AreEqual(metadata.GetFeatures(
                    Convert.ToInt32(expectedFirstRangeStartPoint, (IFormatProvider)null),
                    Convert.ToInt32(expectedFirstRangeEndPoint, (IFormatProvider)null)).Count.ToString((IFormatProvider)null),
                    expectedCountWithinFirstRange);
                Assert.AreEqual(metadata.GetFeatures(
                    Convert.ToInt32(expectedSecondRangeStartPoint, (IFormatProvider)null),
                    Convert.ToInt32(expectedSecondRangeEndPoint, (IFormatProvider)null)).Count.ToString((IFormatProvider)null),
                    expectedCountWithinSecondRange);

                // Log NUnit GUI.
                Console.WriteLine(string.Format((IFormatProvider)null,
                    "GenBank Features BVT: Successfully validated the Get GenBank features '{0}'",
                    metadata.GetFeatures(Convert.ToInt32(expectedSecondRangeStartPoint, (IFormatProvider)null),
                    Convert.ToInt32(expectedSecondRangeEndPoint, (IFormatProvider)null)).Count.ToString((IFormatProvider)null)));
                Console.WriteLine(string.Format((IFormatProvider)null,
                    "GenBank Features BVT: Successfully validated the Get GenBank features '{0}'",
                    metadata.GetFeatures(Convert.ToInt32(expectedFirstRangeStartPoint, (IFormatProvider)null),
                    Convert.ToInt32(expectedFirstRangeEndPoint, (IFormatProvider)null)).Count.ToString((IFormatProvider)null)));
            }
        }

        /// <summary>
        /// Validate GenBank Citation referenced present in GenBank Metadata.
        /// </summary>
        /// <param name="nodeName">xml node name.</param>
        void ValidateCitationReferenced(string nodeName)
        {
            // Get Values from XML node.
            string filePath = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.FilePathNode);
            string expectedCitationReferenced = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.citationReferencedCount);

            // Parse a GenBank file.
            ISequenceParser parserObj = new GenBankParser();
            ISequence seq = parserObj.ParseOne(filePath);

            GenBankMetadata metadata =
                seq.Metadata[Constants.GenBank] as GenBankMetadata;

            // Get a list citationReferenced present in GenBank file.
            List<CitationReference> citationReferenceList =
                metadata.GetCitationsReferredInFeatures();

            // Validate citation referenced present in GenBank features.
            Assert.AreEqual(citationReferenceList.Count.ToString((IFormatProvider)null),
                expectedCitationReferenced);

            // Log NUnit GUI.
            Console.WriteLine(string.Format((IFormatProvider)null,
                "GenBank Features BVT: Successfully validated citation referenced '{0}'",
                citationReferenceList.Count.ToString((IFormatProvider)null)));
        }

        /// <summary>
        /// Validate GenBank Citation referenced by passing featureItem present in GenBank Metadata.
        /// </summary>
        /// <param name="nodeName">xml node name.</param>
        void ValidateCitationReferencedUsingFeatureItem(string nodeName)
        {
            // Get Values from XML node.
            string filePath = _utilityObj._xmlUtil.GetTextValue(
                  nodeName, Constants.FilePathNode);
            string expectedCitationReferenced = _utilityObj._xmlUtil.GetTextValue(
                  nodeName, Constants.citationReferencedCount);

            // Parse a GenBank file.
            ISequenceParser parserObj = new GenBankParser();
            ISequence seq = parserObj.ParseOne(filePath);

            GenBankMetadata metadata =
                seq.Metadata[Constants.GenBank] as GenBankMetadata;
            IList<FeatureItem> featureList = metadata.Features.All;

            // Get a list citationReferenced present in GenBank file.
            List<CitationReference> citationReferenceList =
                metadata.GetCitationsReferredInFeature(featureList[0]);

            Assert.AreEqual(citationReferenceList.Count.ToString((IFormatProvider)null),
                expectedCitationReferenced);

            // Log NUnit GUI.
            Console.WriteLine(string.Format((IFormatProvider)null,
                "GenBank Features BVT: Successfully validated citation reference '{0}'",
                citationReferenceList.Count.ToString((IFormatProvider)null)));
        }

        /// <summary>
        /// Validate All qualifiers in CDS feature.
        /// </summary>
        /// <param name="nodeName">xml node name.</param>
        void ValidateCDSQualifiers(string nodeName, string methodName)
        {
            // Get Values from XML node.
            string filePath = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.FilePathNode);
            string expectedCDSProduct = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.CDSProductQualifier);
            string expectedCDSException = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.CDSException);
            string expectedCDSCodonStart = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.CDSCodonStart);
            string expectedCDSLabel = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.CDSLabel);
            string expectedCDSDBReference = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.CDSDBReference);
            string expectedGeneSymbol = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.GeneSymbol);

            // Parse a GenBank file.
            ISequenceParser parserObj = new GenBankParser();
            ISequence seq = parserObj.ParseOne(filePath);

            GenBankMetadata metadata =
                seq.Metadata[Constants.GenBank] as GenBankMetadata;

            // Get CDS qaulifier.value.
            List<CodingSequence> cdsQualifiers = metadata.Features.CodingSequences;
            List<string> codonStartValue = cdsQualifiers[0].CodonStart;
            List<string> productValue = cdsQualifiers[0].Product;
            List<string> DBReferenceValue = cdsQualifiers[0].DatabaseCrossReference;


            // validate CDS qualifiers.
            if ((0 == string.Compare(methodName, "DNA",
                true, CultureInfo.CurrentCulture))
               || (0 == string.Compare(methodName, "RNA",
               true, CultureInfo.CurrentCulture)))
            {
                Assert.AreEqual(cdsQualifiers[0].Label,
                    expectedCDSLabel);
                Assert.AreEqual(cdsQualifiers[0].Exception.ToString((IFormatProvider)null),
                    expectedCDSException);
                Assert.AreEqual(productValue[0].ToString(),
                    expectedCDSProduct);
                Assert.AreEqual(codonStartValue[0].ToString(),
                    expectedCDSCodonStart);
                Assert.IsTrue(string.IsNullOrEmpty(cdsQualifiers[0].Allele));
                Assert.IsFalse(string.IsNullOrEmpty(cdsQualifiers[0].Citation.ToString()));
                Assert.AreEqual(DBReferenceValue[0].ToString(),
                    expectedCDSDBReference);
                Assert.AreEqual(cdsQualifiers[0].GeneSymbol,
                    expectedGeneSymbol);
                Console.WriteLine(string.Format((IFormatProvider)null,
                    "GenBank Features BVT: Successfully validated the CDS Qualifiers '{0}'",
                     cdsQualifiers[0].Label));
                Console.WriteLine(string.Format((IFormatProvider)null,
                    "GenBank Features BVT: Successfully validated the CDS Qualifiers '{0}'",
                     productValue[0].ToString()));
                Console.WriteLine(string.Format((IFormatProvider)null,
                    "GenBank Features BVT: Successfully validated the CDS Qualifiers '{0}'",
                     DBReferenceValue[0].ToString()));
            }
            else
            {
                Assert.AreEqual(cdsQualifiers[0].Label,
                    expectedCDSLabel);
                Assert.AreEqual(cdsQualifiers[0].Exception.ToString((IFormatProvider)null),
                    expectedCDSException);
                Assert.IsTrue(string.IsNullOrEmpty(cdsQualifiers[0].Allele));
                Assert.IsFalse(string.IsNullOrEmpty(cdsQualifiers[0].Citation.ToString()));
                Assert.AreEqual(DBReferenceValue[0].ToString(),
                    expectedCDSDBReference);
                Assert.AreEqual(cdsQualifiers[0].GeneSymbol,
                    expectedGeneSymbol);
                Console.WriteLine(string.Format((IFormatProvider)null,
                    "GenBank Features BVT: Successfully validated the CDS Qualifiers '{0}'",
                    cdsQualifiers[0].Label));
                Console.WriteLine(string.Format((IFormatProvider)null,
                    "GenBank Features BVT: Successfully validated the CDS Qualifiers '{0}'",
                    DBReferenceValue[0].ToString()));
            }
        }

        /// <summary>
        /// Validate general Location builder.
        /// </summary>
        /// <param name="operatorPam">Different operator parameter name</param>
        /// <param name="nodeName">Different location string node name</param>
        /// <param name="isOperator">True if operator else false.</param>
        void ValidateLocationBuilder(string nodeName,
            LocationOperatorParameter operatorPam, bool isOperator)
        {
            // Get Values from XML node.
            string locationString = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.LocationStringValue);
            string locationStartPosition = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.LoocationStartNode);
            string locationEndPosition = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.LoocationEndNode);
            string locationSeperatorNode = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.LocationSeperatorNode);
            string expectedLocationString = string.Empty;
            string sublocationStartPosition = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.SubLocationStart);
            string sublocationEndPosition = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.SubLocationEnd);
            string sublocationSeperatorNode = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.SubLocationSeperator);
            string subLocationsCount = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.SubLocationCount);

            // Build a new location 
            ILocationBuilder locationBuilderObj = new LocationBuilder();
            ILocation location = locationBuilderObj.GetLocation(locationString);
            expectedLocationString = locationBuilderObj.GetLocationString(location);

            // Validate constructed location starts,end and location string.
            Assert.AreEqual(locationStartPosition, location.Start.ToString((IFormatProvider)null));
            Assert.AreEqual(locationString, expectedLocationString);
            Assert.AreEqual(locationEndPosition, location.End.ToString((IFormatProvider)null));

            switch (operatorPam)
            {
                case LocationOperatorParameter.Join:
                    Assert.AreEqual(LocationOperator.Join, location.Operator);
                    break;
                case LocationOperatorParameter.Complement:
                    Assert.AreEqual(LocationOperator.Complement, location.Operator);
                    break;
                case LocationOperatorParameter.Order:
                    Assert.AreEqual(LocationOperator.Order, location.Operator);
                    break;
                default:
                    Assert.AreEqual(LocationOperator.None, location.Operator);
                    Assert.AreEqual(locationSeperatorNode,
                        location.Separator.ToString((IFormatProvider)null));
                    Assert.IsTrue(string.IsNullOrEmpty(location.Accession));
                    Assert.IsNotNull(location.SubLocations);
                    break;
            }

            if (isOperator)
            {
                Assert.IsTrue(string.IsNullOrEmpty(location.Separator));
                Assert.AreEqual(sublocationEndPosition,
                    location.SubLocations[0].End.ToString((IFormatProvider)null));
                Assert.AreEqual(sublocationSeperatorNode,
                    location.SubLocations[0].Separator.ToString((IFormatProvider)null));
                Assert.AreEqual(Convert.ToInt32(subLocationsCount, (IFormatProvider)null),
                    location.SubLocations.Count);
                Assert.AreEqual(sublocationStartPosition,
                    location.SubLocations[0].Start.ToString((IFormatProvider)null));
                Assert.AreEqual(LocationOperator.None,
                    location.SubLocations[0].Operator);
                Assert.AreEqual(0,
                    location.SubLocations[0].SubLocations.Count);
            }

            // Log to Nunit GUI.
            Console.WriteLine(string.Format((IFormatProvider)null,
                "GenBank Features BVT: Successfully validated Location builder'{0}'",
                 expectedLocationString));
            Console.WriteLine(string.Format((IFormatProvider)null,
                "GenBank Features BVT: Successfully validated the Location start'{0}'",
                 location.Start));
            Console.WriteLine(string.Format((IFormatProvider)null,
                "GenBank Features BVT: Successfully validated the Location end'{0}'",
                 location.End));
        }

        /// <summary>
        /// Validate addition of GenBank features.
        /// </summary>
        /// <param name="nodeName">xml node name.</param>
        void ValidateGenBankSubFeatures(string nodeName)
        {
            // Get Values from XML node.
            string firstKey = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.FirstKey);
            string secondKey = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.SecondKey);
            string thirdKey = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.ThirdFeatureKey);
            string fourthKey = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.FourthKey);
            string fifthKey = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.FifthKey);
            string firstLocation = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.FirstLocation);
            string secondLocation = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.SecondLocation);
            string thirdLocation = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.ThirdLocation);
            string fourthLocation = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.FourthLocation);
            string fifthLocation = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.FifthLocation);
            string featuresCount = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.MainFeaturesCount);
            string secondCount = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.SecondCount);
            string thirdCount = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.ThirdCount);

            // Create a feature items
            SequenceFeatures seqFeatures = new SequenceFeatures();
            FeatureItem firstItem = new FeatureItem(firstKey, firstLocation);
            FeatureItem secondItem = new FeatureItem(secondKey, secondLocation);
            FeatureItem thirdItem = new FeatureItem(thirdKey, thirdLocation);
            FeatureItem fourthItem = new FeatureItem(fourthKey, fourthLocation);
            FeatureItem fifthItem = new FeatureItem(fifthKey, fifthLocation);

            seqFeatures.All.Add(firstItem);
            seqFeatures.All.Add(secondItem);
            seqFeatures.All.Add(thirdItem);
            seqFeatures.All.Add(fourthItem);
            seqFeatures.All.Add(fifthItem);

            // Validate sub features .
            List<FeatureItem> subFeatures = firstItem.GetSubFeatures(seqFeatures);
            Assert.AreEqual(Convert.ToInt32(featuresCount, (IFormatProvider)null), subFeatures.Count);
            subFeatures = secondItem.GetSubFeatures(seqFeatures);
            Assert.AreEqual(Convert.ToInt32(secondCount, (IFormatProvider)null), subFeatures.Count);
            subFeatures = thirdItem.GetSubFeatures(seqFeatures);
            Assert.AreEqual(Convert.ToInt32(thirdCount, (IFormatProvider)null), subFeatures.Count);

            //Log to Nunit GUI.
            Console.WriteLine(string.Format((IFormatProvider)null,
                "GenBank Features BVT: Successfully validated the sub feature '{0}'",
                subFeatures.Count));
        }

        /// <summary>
        /// Validate Seqeunce feature of GenBank file.
        /// </summary>
        /// <param name="nodeName">xml node name. for different alphabet</param>
        void ValidateSequenceFeature(string nodeName)
        {
            // Get Values from XML node.
            string filePath = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.FilePathNode);
            string subSequence = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.ExpectedSubSequence);
            string subSequenceStart = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.SequenceStart);
            string subSequenceEnd = _utilityObj._xmlUtil.GetTextValue(
                nodeName, Constants.SequenceEnd);
            ISequence sequence;
            ISequence firstFeatureSeq = null;

            // Parse a genBank file.
            GenBankParser parserObj = new GenBankParser();
            sequence = parserObj.ParseOne(filePath);
            GenBankMetadata metadata =
                sequence.Metadata[Constants.GenBank] as GenBankMetadata;

            // Get Subsequence feature,start and end postions.
            firstFeatureSeq =
                metadata.Features.All[0].GetSubSequence(sequence);

            // Validate SubSequence.
            Assert.AreEqual(firstFeatureSeq.ToString(), subSequence);
            Assert.AreEqual(metadata.Features.All[0].Location.Start.ToString((IFormatProvider)null),
                subSequenceStart);
            Assert.AreEqual(metadata.Features.All[0].Location.End.ToString((IFormatProvider)null),
                subSequenceEnd);
            Assert.IsNull(metadata.Features.All[0].Location.Accession);
            Assert.AreEqual(metadata.Features.All[0].Location.StartData.ToString(),
                subSequenceStart);
            Assert.AreEqual(metadata.Features.All[0].Location.EndData.ToString(),
                subSequenceEnd);

            // Log to NUnit GUI
            Console.WriteLine(string.Format((IFormatProvider)null,
                "GenBank Features BVT: Successfully validated the Subsequence feature '{0}'",
                firstFeatureSeq.ToString()));
            Console.WriteLine(string.Format((IFormatProvider)null,
                "GenBank Features BVT: Successfully validated the start of subsequence'{0}'",
                metadata.Features.All[0].Location.Start.ToString((IFormatProvider)null)));
        }

        # endregion Supporting Methods
    }
}