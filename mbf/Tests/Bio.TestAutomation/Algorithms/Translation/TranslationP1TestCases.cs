﻿/****************************************************************************
 * TranslationP1TestCases.cs
 * 
 * This file contains the Translation P1 Test Cases which includes Codons, 
 * Complementation, ProteinTranslation and Transcription.
 * 
******************************************************************************/

using System;
using Bio;
using Bio.Algorithms.Translation;
using Bio.TestAutomation.Util;
using Bio.Util.Logging;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bio.TestAutomation.Algorithms.Translation
{

    /// <summary>
    /// Test Automation code for bio Translation and P1 level validations.
    /// </summary>
    [TestClass]
    public class TranslationP1TestCases
    {

        #region Global Variables

        Utility utilityObj = new Utility(@"TestUtils\TestsConfig.xml");

        #endregion Global Variables

        #region Constructor

        /// <summary>
        /// Static constructor to open log and make other settings needed for test
        /// </summary>
        static TranslationP1TestCases()
        {
            Trace.Set(Trace.SeqWarnings);
            if (!ApplicationLog.Ready)
            {
                ApplicationLog.Open("bio.automation.log");
            }
        }

        #endregion Constructor

        #region  Codons P1 TestCases

        /// <summary>
        /// Validate a aminoacod for a given RNA Sequence with 12 characters.
        /// Input Data :Sequence - 'AUGCCUGUUUGA'.
        /// Output Data : Corresponding amino acid 'Aspartic Acid'.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateLookupWithRnaSequence()
        {
            string alphabetName = utilityObj.xmlUtil.GetTextValue(
                Constants.SimpleRnaAlphabetNode, Constants.AlphabetNameNode);
            string expectedSeq = utilityObj.xmlUtil.GetTextValue(
                Constants.CodonsNode, Constants.ExpectedNormalString);
            string expectedAminoAcid = utilityObj.xmlUtil.GetTextValue(
                Constants.CodonsNode, Constants.OffsetZeroTwelveCharsAminoAcidV2);
            string expectedOffset = utilityObj.xmlUtil.GetTextValue(
                Constants.CodonsNode, Constants.OffsetVaule);
            string aminoAcid = null;

            Sequence seq = new Sequence(Utility.GetAlphabet(alphabetName), expectedSeq);
            // Validate Codons lookup method.
            aminoAcid = Codons.Lookup(seq, Convert.ToInt32(expectedOffset, null)).ToString();

            // Validate amino acids for a given sequence.
            Assert.AreEqual(expectedAminoAcid, aminoAcid);
            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "Translation P1: Amino Acid {0} is expected.", aminoAcid));
            ApplicationLog.WriteLine(
                "Translation P1: Amino Acid validation for a given sequence was completed successfully.");
        }

        /// <summary>
        /// Validate a aminoacod for a given RNA Sequence with offset value "6".
        /// Input Data : Valid Sequence - 'AUGCCUGUUUGA'.
        /// Output Data : Corresponding amino acid 'Arginine'.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateLookupWithOffsetValueSix()
        {
            string alphabetName = utilityObj.xmlUtil.GetTextValue(
                Constants.SimpleRnaAlphabetNode, Constants.AlphabetNameNode);
            string expectedSeq = utilityObj.xmlUtil.GetTextValue(
                Constants.CodonsNode, Constants.ExpectedNormalString);
            string expectedAminoAcid = utilityObj.xmlUtil.GetTextValue(
                Constants.CodonsNode, Constants.OffsetZeroSixCharsAminoAcidV2);
            string expectedOffset = utilityObj.xmlUtil.GetTextValue(
                Constants.CodonsNode, Constants.OffsetVaule2);

            Sequence seq = new Sequence(Utility.GetAlphabet(alphabetName), expectedSeq);
            // Validate Codons lookup method.
            string aminoAcid = Codons.Lookup(seq, Convert.ToInt32(expectedOffset, null)).ToString();

            // Validate amino acids for a given sequence.
            Assert.AreEqual(expectedAminoAcid, aminoAcid);
            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "Translation P1: Amino Acid {0} is expected.", aminoAcid));
            ApplicationLog.WriteLine(
                "Translation P1: Amino Acid validation for a given sequence was completed successfully.");
        }

        /// <summary>
        /// Validate a amino acid for a given RNA Sequence with Six Characters and offset value "6".
        /// Input Data : Valid Sequence - 'GUUUGA'.
        /// Output Data : Corresponding amino acid 'Arginine'.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateLookupWithSixChars()
        {
            string alphabetName = utilityObj.xmlUtil.GetTextValue(
                Constants.SimpleRnaAlphabetNode, Constants.AlphabetNameNode);
            string expectedSeq = utilityObj.xmlUtil.GetTextValue(
                Constants.CodonsNode, Constants.SequenceWithSixChars);
            string expectedAminoAcid = utilityObj.xmlUtil.GetTextValue(
                Constants.CodonsNode, Constants.OffsetZeroSixCharsAminoAcidV2);
            string expectedOffset = utilityObj.xmlUtil.GetTextValue(
                Constants.CodonsNode, Constants.OffsetVaule2);

            Sequence seq = new Sequence(Utility.GetAlphabet(alphabetName), expectedSeq);
            // Validate Codons lookup method.
            string aminoAcid = Codons.Lookup(seq, Convert.ToInt32(expectedOffset, null)).ToString();

            // Validate amino acids for a given sequencet.
            Assert.AreEqual(expectedAminoAcid, aminoAcid);
            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "Translation P1: Amino Acid {0} is expected.", aminoAcid));
            ApplicationLog.WriteLine(
                "Translation P1: Amino Acid validation for a given sequence was completed successfully.");
        }

        /// <summary>
        /// Validate a aminoacod for a given DNA Sequence with offset value "0".
        /// Input Data : Valid Sequence - 'ATGGCG'.
        /// Output Data : Corresponding amino acid 'Tyrosine'.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateLookupWithDnaSequence()
        {
            string alphabetName = utilityObj.xmlUtil.GetTextValue(
                Constants.SimpleDnaAlphabetNode, Constants.AlphabetNameNode);
            string expectedSeq = utilityObj.xmlUtil.GetTextValue(
                Constants.TranscribeNode, Constants.DnaSequence);
            string expectedAminoAcid = utilityObj.xmlUtil.GetTextValue(
                Constants.CodonsNode, Constants.DnaSeqAminoAcidV2);
            string expectedOffset = utilityObj.xmlUtil.GetTextValue(
                Constants.CodonsNode, Constants.OffsetVaule);

            Sequence seq = new Sequence(Utility.GetAlphabet(alphabetName), expectedSeq);
            // Transcribe DNA to RNA.
            ISequence transcribe = Transcription.Transcribe(seq);

            // Validate Codons lookup method.
            string aminoAcid = Codons.Lookup(transcribe, Convert.ToInt32(expectedOffset, null)).ToString();

            // Validate amino acids for a given sequence.
            Assert.AreEqual(expectedAminoAcid, aminoAcid);
            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "Translation P1: Amino Acid {0} is expected.", aminoAcid));
            ApplicationLog.WriteLine(
                "Translation P1: Amino Acid validation for a given sequence was completed successfully.");
        }

        /// <summary>
        /// Validates a aminoacid for a given 3 characters RNA Sequence with offset value "0".
        /// Input Data : Valid Sequence - 'AUG'.
        /// Output Data : Corresponding amino acid ''Methionine'.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateRnaCodonsTraslationWithOffset()
        {
            ValidateCodonsTranslationWithOffset(Constants.RnaSequenceWithThreeChars,
                Constants.OffsetVaule, Constants.AminoAcidForThreeCharsV2);
        }

        /// <summary>
        /// Validates a aminoacid for a given 12 characters RNA Sequence with offset value "3".
        /// Input Data : Valid Sequence - 'AUGCGCCCGAUG'.
        /// Output Data : Corresponding amino acid 'Arginine'.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateRnaCodonsTraslationWithThreeOffset()
        {
            ValidateCodonsTranslationWithOffset(Constants.RnaSequenceWithTwelveChars,
                Constants.OffsetVaule1, Constants.AminoAcidForOffsetTwelveV2);
        }

        #endregion Codons P1 TestCases

        #region Complement P1 TestCases

        /// <summary>
        /// Validate Complement of Single DNA Synbol.
        /// Input Data : Valid DNA Symbol - 'A'
        /// Output Data : Complement of DNA Symbol - 'T'
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateSingleDnaSymbolComplementation()
        {
            // Get Node values from XML.
            string alphabetName = utilityObj.xmlUtil.GetTextValue(
                Constants.SimpleDnaAlphabetNode, Constants.AlphabetNameNode);
            string expectedSeq = utilityObj.xmlUtil.GetTextValue(
                Constants.ComplementNode, Constants.DnaSymbol);
            string expectedComplement = utilityObj.xmlUtil.GetTextValue(
                Constants.ComplementNode, Constants.DnaSymbolComplement);
            ISequence complement = null;

            Sequence seq = new Sequence(Utility.GetAlphabet(alphabetName), expectedSeq);
            // Complement DNA Sequence.
            complement = seq.GetComplementedSequence();

            // Validate Single DNA Symbol Complement.
            Assert.AreEqual(new string(complement.Select(a => (char)a).ToArray()), expectedComplement);

            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                    "Translation P1: Complement {0} is expected.", complement));
            ApplicationLog.WriteLine(
                "Translation P1: Complement of Single DNA Symbol was validate successfully.");
        }

        /// <summary>
        /// Validate Complement of DNA Sequence with more than twelve characters.
        /// Input Data : Valid DNA Symbol - 'ATATGTAGGTACCCGATA'
        /// Output Data : Complement of DNA Symbol - 'TATACATCCATGGGCTAT'
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateMoreThanTwelveCharsDnaComplementation()
        {
            // Get Node values from XML.
            string alphabetName = utilityObj.xmlUtil.GetTextValue(
                Constants.SimpleDnaAlphabetNode, Constants.AlphabetNameNode);
            string expectedSeq = utilityObj.xmlUtil.GetTextValue(
                Constants.ComplementNode, Constants.DnaSymbol);
            string expectedComplement = utilityObj.xmlUtil.GetTextValue(
                Constants.ComplementNode, Constants.DnaSymbolComplement);
            ISequence complement = null;

            Sequence seq = new Sequence(Utility.GetAlphabet(alphabetName), expectedSeq);
            // Complement DNA Sequence.
            complement = seq.GetComplementedSequence();

            // Validate Complement for a given sequence.
            Assert.AreEqual(new string(complement.Select(a => (char)a).ToArray()), expectedComplement);
            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "Translation P1: Complement {0} is expected.", complement));
            ApplicationLog.WriteLine(
                "Translation P1: Complement of More than twelve DNA characters sequence was validate successfully.");
        }

        /// <summary>
        /// Validate Reverse Complement of Single symbol DNA Synbol.
        /// Input Data : Valid DNA Symbol - 'A'
        /// Output Data : Reverse Complement of DNA Symbol - 'T'
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateSingleSymbolDnaRevComplementation()
        {
            // Get Node values from XML.
            string alphabetName = utilityObj.xmlUtil.GetTextValue(
                Constants.SimpleDnaAlphabetNode, Constants.AlphabetNameNode);
            string expectedSeq = utilityObj.xmlUtil.GetTextValue(
                Constants.ComplementNode, Constants.DnaSymbol);
            string expectedRevComplement = utilityObj.xmlUtil.GetTextValue(
                Constants.ComplementNode, Constants.DnaSymbolRevComplement);
            ISequence revComplement = null;

            Sequence seq = new Sequence(Utility.GetAlphabet(alphabetName), expectedSeq);
            // Reverse Complement DNA Sequence.
            revComplement = seq.GetReverseComplementedSequence();

            // Validate Single DNA Symbol Reverse Complement.
            Assert.AreEqual(new string(revComplement.Select(a => (char)a).ToArray()), expectedRevComplement);

            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "Translation P1: Complement {0} is expected.", revComplement));
            ApplicationLog.WriteLine(
                "Translation P1: Reverse Complement of Single DNA Symbol was validate successfully.");
        }

        /// <summary>
        /// Validate Reverse Complement of DNA Sequence with more than twelve characters.
        /// Input Data : Valid DNA Sequence - 'ATATGTAGGTACCCGATA'
        /// Output Data : Reverse Complement of DNA Sequence - 'TATACATCCATGGGCTAT'
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateMoreThanTwelveCharsDnaRevComplementation()
        {
            // Get Node values from XML.
            string alphabetName = utilityObj.xmlUtil.GetTextValue(
                Constants.SimpleDnaAlphabetNode, Constants.AlphabetNameNode);
            string expectedSeq = utilityObj.xmlUtil.GetTextValue(
                Constants.ComplementNode, Constants.DnaSequenceWithMoreThanTwelveChars);
            string expectedComplement = utilityObj.xmlUtil.GetTextValue(
                Constants.ComplementNode, Constants.DnaRevComplementForMoreThanTwelveChars);
            ISequence revComplement = null;

            Sequence seq = new Sequence(Utility.GetAlphabet(alphabetName), expectedSeq);
            // Reverse Complement DNA Sequence.
            revComplement = seq.GetReverseComplementedSequence();

            // Validate Reverse Complement. for a given sequence.
            Assert.AreEqual(new string(revComplement.Select(a => (char)a).ToArray()), expectedComplement);
            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "Translation P1: Rev Complement {0} is expected.", revComplement));
            ApplicationLog.WriteLine(
                "Translation P1: Reverse Complement of More than twelve DNA characters sequence was validate successfully.");
        }

        /// <summary>
        /// Validates Complement of DNA Sequence with twelve characters.
        /// Input Data : Valid DNA Sequence - 'AGGTACCCGATA'
        /// Output Data : Complement of DNA Sequence - 'TCCATGGGCTAT'
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateDnaComplementationWithTweleveChars()
        {
            // Get Node values from XML.
            string alphabetName = utilityObj.xmlUtil.GetTextValue(
                Constants.SimpleDnaAlphabetNode, Constants.AlphabetNameNode);
            string expectedSeq = utilityObj.xmlUtil.GetTextValue(
                Constants.ComplementNode, Constants.DnaSequence);
            string expectedComplement = utilityObj.xmlUtil.GetTextValue(
                Constants.ComplementNode, Constants.DnaComplement);
            ISequence complement = null;

            Sequence seq = new Sequence(Utility.GetAlphabet(alphabetName), expectedSeq);
            //Complement DNA Sequence.
            complement = seq.GetComplementedSequence();

            // Validate Complement for a given sequence.
            Assert.AreEqual(new string(complement.Select(a => (char)a).ToArray()), expectedComplement);
            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "Translation P1: Complement {0} is expected.", complement));
            ApplicationLog.WriteLine(
                "Translation P1: Complement of twelve DNA characters sequence validated successfully.");
        }

        /// <summary>
        /// Validates Reverse Complement of DNA Sequence with twelve characters.
        /// Input Data : Valid DNA Sequence - 'AGGTACCCGATA'
        /// Output Data : Reverse Complement of DNA Sequence - 'TATCGGGTACCT'
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateDnaReverseComplementationWithTweleveChars()
        {
            // Get Node values from XML.
            string alphabetName = utilityObj.xmlUtil.GetTextValue(
                Constants.SimpleDnaAlphabetNode, Constants.AlphabetNameNode);
            string expectedSeq = utilityObj.xmlUtil.GetTextValue(
                Constants.ComplementNode, Constants.DnaSequence);
            string expectedRevComplement = utilityObj.xmlUtil.GetTextValue(
                Constants.ComplementNode, Constants.DnaRevComplement);
            ISequence revComplement = null;

            Sequence seq = new Sequence(Utility.GetAlphabet(alphabetName), expectedSeq);
            // Reverse Complement DNA Sequence.
            revComplement = seq.GetReverseComplementedSequence();

            // Validate Reverse Complement for a given sequence.
            Assert.AreEqual(new string(revComplement.Select(a => (char)a).ToArray()), expectedRevComplement);
            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "Translation P1: Reverse Complement {0} is expected.", revComplement));
            ApplicationLog.WriteLine(
                "Translation P1: Reverse Complement of twelve DNA characters sequence validated successfully.");
        }

        #endregion Complement P1 TestCases

        #region Transcribe P1 TestCases

        /// <summary>
        /// Validate Single symbol transcribe.
        /// Input Data : Valid DNA Symbol - 'G'
        /// Output Data : Transcribe - 'G'
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateSingleSymbolTranscribe()
        {
            // Get Node values from XML.
            string alphabetName = utilityObj.xmlUtil.GetTextValue(
                Constants.SimpleDnaAlphabetNode, Constants.AlphabetNameNode);
            string expectedSeq = utilityObj.xmlUtil.GetTextValue(
                Constants.TranscribeNode, Constants.DnaSymbol);
            string expectedTranscribe = utilityObj.xmlUtil.GetTextValue(
                Constants.TranscribeNode, Constants.DnaSymbolTranscribeV2);
            ISequence transcribe = null;

            Sequence seq = new Sequence(Utility.GetAlphabet(alphabetName), expectedSeq);
            //Transcribe DNA Symbol.
            transcribe = Transcription.Transcribe(seq);

            // Validate Single DNA Symbol transcribe.
            Assert.AreEqual(new string(transcribe.Select(a => (char)a).ToArray()), expectedTranscribe);

            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "Translation P1: Transcribe {0} is expected.", transcribe));
            ApplicationLog.WriteLine(
                "Translation P1: Transcribe of Single DNA Symbol was validate successfully.");
        }

        /// <summary>
        /// Validate DNA Sequence Transcribe with more than twelve characters.
        /// Input Data : Valid DNA Sequence - 'ATATGTAGGTACCCGATA'
        /// Output Data : Transcribe Seqeunce - 'AUAUGUAGGUACCCGAUA'
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateMoreThanTwelveCharsDnaTranscribe()
        {
            // Get Node values from XML.
            string alphabetName = utilityObj.xmlUtil.GetTextValue(
                Constants.SimpleDnaAlphabetNode, Constants.AlphabetNameNode);
            string expectedSeq = utilityObj.xmlUtil.GetTextValue(
                Constants.ComplementNode, Constants.DnaSequenceWithMoreThanTwelveChars);
            string expectedTranscribe = utilityObj.xmlUtil.GetTextValue(
                Constants.TranscribeNode, Constants.DnaTranscribeForMoreThanTwelveCharsV2);
            ISequence transcribe = null;

            Sequence seq = new Sequence(Utility.GetAlphabet(alphabetName), expectedSeq);
            // Transcribe DNA Symbol.
            transcribe = Transcription.Transcribe(seq);

            // Validate transcribe.for a given sequence.
            Assert.AreEqual(new string(transcribe.Select(a => (char)a).ToArray()), expectedTranscribe);
            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "Translation P1: Transcribe {0} is expected.", transcribe));
            ApplicationLog.WriteLine(
                "Translation P1: Transcribe of Single DNA Symbol was validate successfully.");
        }

        /// <summary>
        /// Validate Reverse Transcribe for a single DNA Synbol.
        /// Input Data : Valid DNA Symbol - 'G'
        /// Output Data : Transcribe Symbol - 'G'
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateSingleSymbolDnaReverseTranscribe()
        {
            // Get Node values from XML.
            string alphabetName = utilityObj.xmlUtil.GetTextValue(
                Constants.SimpleDnaAlphabetNode, Constants.AlphabetNameNode);
            string expectedSeq = utilityObj.xmlUtil.GetTextValue(
                Constants.TranscribeNode, Constants.DnaSymbol);
            string expectedTranscribe = utilityObj.xmlUtil.GetTextValue(
                Constants.TranscribeNode, Constants.DnaSymbolRevTranscribe);
            ISequence transcribe = null;

            Sequence seq = new Sequence(Utility.GetAlphabet(alphabetName), expectedSeq);
            // Reverse Transcribe DNA Symbol.
            transcribe = Transcription.Transcribe(seq);

            ISequence revTranscribe = Transcription.ReverseTranscribe(transcribe);

            // Validate Single DNA Symbol Reverse transcribe.
            Assert.AreEqual(new string(revTranscribe.Select(a => (char)a).ToArray()), expectedTranscribe);
            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "Translation P1: Reverse Transcribe {0} is expected.", revTranscribe));
            ApplicationLog.WriteLine(
                "Translation P1: Reverse Transcribe of Single DNA Symbol was validate successfully.");
        }

        /// <summary>
        /// Validate DNA Sequence Reverse Transcribe with more than twelve characters.
        /// Input Data : Valid DNA Sequence - 'ATATGTAGGTACCCGATA'
        /// Output Data : Reverse Transcribe Seqeunce - 'AUAUGUAGGUACCCGAUA'
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateMoreThanTwelveCharsDnaReverseTranscribe()
        {
            // Get Node values from XML.
            string alphabetName = utilityObj.xmlUtil.GetTextValue(
                Constants.SimpleDnaAlphabetNode, Constants.AlphabetNameNode);
            string expectedSeq = utilityObj.xmlUtil.GetTextValue(
                Constants.ComplementNode, Constants.DnaSequenceWithMoreThanTwelveChars);
            string expectedTranscribe = utilityObj.xmlUtil.GetTextValue(
                Constants.TranscribeNode, Constants.DnaRevTranscribeForMoreThanTwelveChars);
            ISequence transcribe = null;

            Sequence seq = new Sequence(Utility.GetAlphabet(alphabetName), expectedSeq);
            // Reverse Transcribe DNA Sequence.
            transcribe = Transcription.Transcribe(seq);
            ISequence revTranscribe = Transcription.ReverseTranscribe(transcribe);

            // Validate reverse transcribe.for a given sequence.
            Assert.AreEqual(new string(revTranscribe.Select(a => (char)a).ToArray()), expectedTranscribe);
            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "Translation P1: Reverse Transcribe {0} is expected.", revTranscribe));
            ApplicationLog.WriteLine(
                "Translation P1: Reverse Transcribe of Single DNA Symbol was validate successfully.");
        }

        /// <summary>
        /// Validates DNA Sequence Transcribe with three characters.
        /// Input Data : Valid DNA Sequence - 'ACG'
        /// Output Data : Transcribe Seqeunce - 'ACG'
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateDnaToRnaTranscribeWithThreeChars()
        {
            ValidateDnaToRnaTranscribe(Constants.DnaSequenceWithThreeChars,
                Constants.TranscribeForThreeCharsV2);
        }

        /// <summary>
        /// Validates DNA Sequence Transcribe with twelve characters.
        /// Input Data : Valid DNA Sequence - 'ATATGTAGGTAC'
        /// Output Data : Transcribe Seqeunce - 'AUAUGUAGGUAC'
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateDnaToRnaTranscribeWithTwelveChars()
        {
            ValidateDnaToRnaTranscribe(Constants.DnaSequenceWithTweleveChars,
                Constants.TranscribeForTweleveCharsV2);
        }

        /// <summary>
        /// Validates RNA Sequence reverse Transcribe with three characters.
        /// Input Data : Valid RNA Sequence - 'UGC'
        /// Output Data : Reverse Transcribe Seqeunce - 'TGC'
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateRnaToDnaReverseTranscribeWithThreeChars()
        {
            ValidateRnaToDnaReverseTranscribe(Constants.TranscribeForThreeChars,
                Constants.DnaSequenceWithThreeCharsV2);
        }

        /// <summary>
        /// Validates DNA Sequence Transcribe with twelve characters.
        /// Input Data : Valid RNA Sequence - 'UAUACAUCCAUG'
        /// Output Data : Reverse Transcribe Seqeunce - 'TATACATCCATG'
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateRnaToDnaReverseTranscribeWithTwelveChars()
        {
            ValidateRnaToDnaReverseTranscribe(Constants.TranscribeForTweleveChars,
                Constants.DnaSequenceWithTweleveCharsV2);
        }

        #endregion Transcribe P1 TestCases

        #region Translation P1 TestCases

        /// <summary>
        /// Validate a Protein translation for a twelve characters sequence.
        /// Input Data : Seqeunce - 'CGCAUGCCGAUG'
        /// Output Data : "RMPM".
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateTwelveCharsProteinTranslation()
        {
            // Get Node values from XML.
            string expectedSeq = utilityObj.xmlUtil.GetTextValue(
                Constants.TranslationNode, Constants.RnaSequenceWithTwelveChars);
            string expectedProtein = utilityObj.xmlUtil.GetTextValue(
                Constants.TranslationNode, Constants.AminoAcidForTwelveChars);
            ISequence protein = null;

            // Translate twelve characters RNA to protein.
            Sequence proteinTranslation = new Sequence(Alphabets.RNA, expectedSeq);
            protein = ProteinTranslation.Translate(proteinTranslation);

            // Validate Protein Translation.
            Assert.AreEqual(new string(protein.Select(a => (char)a).ToArray()), expectedProtein);
            ApplicationLog.WriteLine(
                "Translation P1: Protein translation validation is completed successfully.");
        }

        /// <summary>
        /// Validate a Protein translation for a given symbol.
        /// Input Data : Valid Symbol - 'A'
        /// Output Data : "Null"
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateSingleSymbolTranslation()
        {
            // Get Node values from XML.
            string expectedSeq = utilityObj.xmlUtil.GetTextValue(
                Constants.TranscribeNode, Constants.DnaSymbol);
            ISequence protein = null;

            // Translate Single char RNA to protein.
            Sequence proteinTranslation = new Sequence(Alphabets.RNA, expectedSeq);
            protein = ProteinTranslation.Translate(proteinTranslation);

            // Validate Protein Translation.
            Assert.AreEqual(new string(protein.Select(a => (char)a).ToArray()), string.Empty);

            ApplicationLog.WriteLine(
                "Translation P1: Amino Acid validation for a given sequence was completed successfully.");
        }

        /// <summary>
        /// Validate a Protein translation for more than twelve characters sequence.
        /// Input Data : Sequence - 'AUGCGCCCGAUGCGC'
        /// Output Data : "Methionine,Arginine,Proline,Methionine,Arginine".
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateMoreThanTwelveCharsProteinTranslation()
        {
            // Get Node values from XML.
            string expectedSeq = utilityObj.xmlUtil.GetTextValue(
                Constants.TranslationNode, Constants.RnaSequenceWithMoreThanTwelveChars);
            string expectedProtein = utilityObj.xmlUtil.GetTextValue(
                Constants.TranslationNode, Constants.AminoAcidForMoreThanTwelveChars);
            ISequence protein = null;

            // Translate more than twelve characters RNA to protein.
            Sequence proteinTranslation = new Sequence(Alphabets.RNA, expectedSeq);
            protein = ProteinTranslation.Translate(proteinTranslation);

            // Validate Protein Translation.
            Assert.AreEqual(new string(protein.Select(a => (char)a).ToArray()), expectedProtein);
            ApplicationLog.WriteLine(
                "Translation P1: Protein translation validation is completed successfully.");
        }

        /// <summary>
        /// Validate a Protein translation for more Six Chars with Offset value "0".
        /// Input Data : Sequence - 'UACCGC'
        /// Output Data : "Tyrosine,Arginine".
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateSixCharsProteinTranslation()
        {
            // Get Node values from XML.
            string expectedSeq = utilityObj.xmlUtil.GetTextValue(
                Constants.TranslationNode, Constants.RnaSequence);
            string expectedProtein = utilityObj.xmlUtil.GetTextValue(
                Constants.TranslationNode, Constants.AminoAcidForSixChars);
            string expectedOffset = utilityObj.xmlUtil.GetTextValue(
                Constants.CodonsNode, Constants.OffsetVaule);
            ISequence protein = null;

            // Translate Six characters RNA to protein.
            Sequence proteinTranslation = new Sequence(Alphabets.RNA, expectedSeq);
            protein = ProteinTranslation.Translate(proteinTranslation,
                Convert.ToInt32(expectedOffset, null));

            // Validate Protein Translation.
            Assert.AreEqual(new string(protein.Select(a => (char)a).ToArray()), expectedProtein);
            ApplicationLog.WriteLine(
                "Translation P1: Protein translation validation is completed successfully.");
        }

        /// <summary>
        /// Validate a Protein translation for a twelve characters sequence with offset value "3".
        /// Input Data : Sequence - 'CGCAUGCCGAUG'
        /// Output Data : "Arginine,Methionine,Proline,Methionine".
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateTwelveCharsProteinTranslationWithOffset()
        {
            // Get Node values from XML.
            string expectedSeq = utilityObj.xmlUtil.GetTextValue(
                Constants.TranslationNode, Constants.RnaSequenceWithTwelveChars);
            string expectedProtein = utilityObj.xmlUtil.GetTextValue(
                Constants.TranslationNode, Constants.AminoAcidForMoreOffsetThree);
            string expectedOffset = utilityObj.xmlUtil.GetTextValue(
                Constants.CodonsNode, Constants.OffsetVaule1);
            ISequence protein = null;

            // Translate twelve characters RNA to protein.
            Sequence proteinTranslation = new Sequence(Alphabets.RNA, expectedSeq);
            protein = ProteinTranslation.Translate(proteinTranslation,
                Convert.ToInt32(expectedOffset, null));

            // Validate Protein Translation.
            Assert.AreEqual(new string(protein.Select(a => (char)a).ToArray()), expectedProtein);
            ApplicationLog.WriteLine(
                "Translation P1: Protein translation validation is completed successfully.");
        }

        /// <summary>
        /// Validate Protein Translation for a given DNA.
        /// Input Data : Sequence - 'ATGGCG'
        /// Output Data : "Tyrosine,Arginine".
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateDnaProteinTranslation()
        {
            // Get Node values from XML.
            string alphabetName = utilityObj.xmlUtil.GetTextValue(
                Constants.SimpleDnaAlphabetNode, Constants.AlphabetNameNode);
            string expectedSeq = utilityObj.xmlUtil.GetTextValue(
                Constants.TranscribeNode, Constants.DnaSequence);
            string expectedProtein = utilityObj.xmlUtil.GetTextValue(
                Constants.TranslationNode, Constants.AminoAcidForSixCharsV2);
            ISequence transcribe = null;

            Sequence seq = new Sequence(Utility.GetAlphabet(alphabetName), expectedSeq);
            // Transcription of DNA Sequence.
            transcribe = Transcription.Transcribe(seq);

            // Protein Translation.
            ISequence proteinTranslation = new Sequence(Alphabets.RNA,
                new string(transcribe.Select(a => (char)a).ToArray()));
            ISequence protein = ProteinTranslation.Translate(proteinTranslation);

            // Validate Protein Translation.
            Assert.AreEqual(expectedProtein, new string(protein.Select(a => (char)a).ToArray()));
            ApplicationLog.WriteLine(
                "Translation P1: Protein translation validation is completed successfully.");
        }

        /// <summary>
        /// Validate a Protein translation for a twelve characters sequence with offset value "6".
        /// Input Data : Sequence - 'CGCAUGCCGAUG'
        /// Output Data : "PM".
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateTwelveCharsProteinTranslationWithOffsetSix()
        {
            // Get Node values from XML.
            string expectedSeq = utilityObj.xmlUtil.GetTextValue(
                Constants.TranslationNode, Constants.RnaSequenceWithTwelveChars);
            string expectedProtein = utilityObj.xmlUtil.GetTextValue(
                Constants.TranslationNode, Constants.AminoAcidForOffsetSix);
            string expectedOffset = utilityObj.xmlUtil.GetTextValue(
                Constants.CodonsNode, Constants.OffsetVaule2);
            ISequence protein = null;

            // Translate twelve characters RNA to protein with  offset value "6"
            Sequence proteinTranslation = new Sequence(Alphabets.RNA, expectedSeq);
            protein = ProteinTranslation.Translate(proteinTranslation,
                Convert.ToInt32(expectedOffset, null));

            // Validate Protein Translation.
            Assert.AreEqual(new string(protein.Select(a => (char)a).ToArray()), expectedProtein);
            ApplicationLog.WriteLine(
                "Translation P1: Protein translation validation is completed successfully.");
        }

        /// <summary>
        /// Validate a Protein translation for a twelve characters sequence with offset value "12".
        /// Input Data : Sequence - 'CGCAUGCCGAUG'
        /// Output Data : Empty string.
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateTwelveCharsProteinTranslationWithOffsetTwelve()
        {
            // Get Node values from XML.
            string expectedSeq = utilityObj.xmlUtil.GetTextValue(
                Constants.TranslationNode, Constants.RnaSequenceWithTwelveChars);
            string expectedOffset = utilityObj.xmlUtil.GetTextValue(
                Constants.CodonsNode, Constants.OffsetVaule3);
            ISequence protein = null;

            // Translate twelve characters RNA to protein with  offset value "12".
            Sequence proteinTranslation = new Sequence(Alphabets.RNA, expectedSeq);
            protein = ProteinTranslation.Translate(proteinTranslation,
               Convert.ToInt32(expectedOffset, null));

            // Validate Protein Translation.
            Assert.AreEqual(new string(protein.Select(a => (char)a).ToArray()), string.Empty);
            ApplicationLog.WriteLine(
                "Translation P1: Protein translation validation is completed successfully.");
        }

        /// <summary>
        /// Validate a Protein translation for a twelve characters sequence with offset value "12".
        /// Input Data : Sequence - 'AUGCGCCCGAUGCGC'
        /// Output Data : "Arginine".
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateMoreThanTwelveCharsProteinTranslationWithOffsetTwelve()
        {
            // Get Node values from XML.
            string expectedSeq = utilityObj.xmlUtil.GetTextValue(
                Constants.TranslationNode, Constants.RnaSequenceWithMoreThanTwelveChars);
            string expectedProtein = utilityObj.xmlUtil.GetTextValue(
                Constants.TranslationNode, Constants.AminoAcidForOffsetTwelve);
            string expectedOffset = utilityObj.xmlUtil.GetTextValue(
                Constants.CodonsNode, Constants.OffsetVaule3);
            ISequence protein = null;

            // Translate twelve characters RNA to protein with  offset value "12".
            Sequence proteinTranslation = new Sequence(Alphabets.RNA, expectedSeq);
            protein = ProteinTranslation.Translate(proteinTranslation,
               Convert.ToInt32(expectedOffset, null));

            // Validate Protein Translation.
            Assert.AreEqual(new string(protein.Select(a => (char)a).ToArray()), expectedProtein);
            ApplicationLog.WriteLine(
                "Translation P1: Protein translation validation is completed successfully.");
        }

        /// <summary>
        /// Validates a Protein Translation for a given DNA sequence with offset value "3".
        /// Input Data : Sequence - 'ATGGCG'
        /// Output Data : "Arginine".
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateDnaProteinTranslationWithOffset()
        {
            // Get Node values from XML.
            string alphabetName = utilityObj.xmlUtil.GetTextValue(Constants.SimpleDnaAlphabetNode,
                Constants.AlphabetNameNode);
            string expectedSeq = utilityObj.xmlUtil.GetTextValue(Constants.TranscribeNode,
                Constants.DnaSequence);
            string expectedProtein = utilityObj.xmlUtil.GetTextValue(Constants.TranslationNode,
                Constants.AminoAcidForOffsetTwelveDna);
            string expectedOffset = utilityObj.xmlUtil.GetTextValue(Constants.CodonsNode,
                Constants.OffsetVaule1);
            ISequence transcribe = null;

            Sequence seq = new Sequence(Utility.GetAlphabet(alphabetName), expectedSeq);
            // Transcription of DNA Sequence.
            transcribe = Transcription.Transcribe(seq);

            // Protein Translation by passing offset value.
            ISequence proteinTranslation = new Sequence(Alphabets.RNA, new string(transcribe.Select(a => (char)a).ToArray()));
            ISequence protein = ProteinTranslation.Translate(proteinTranslation,
                Convert.ToInt32(expectedOffset, null));

            // Validate Protein Translation.
            Assert.AreEqual(expectedProtein, new string(protein.Select(a => (char)a).ToArray()));
            ApplicationLog.WriteLine(
                "Translation P1: Protein translation validation is completed successfully.");
        }

        /// <summary>
        /// Validates a Protein Translation for a given RNA sequence with offset value "3".
        /// Input Data : Sequence - 'AUGCGCCCGAUG'
        /// Output Data : "RPM".
        /// </summary>
        [TestMethod]
        [Priority(1)]
        [TestCategory("Priority1")]
        public void ValidateRnaProteinTranslationWithThreeOffset()
        {
            // Get Node values from XML.
            string expectedSeq = utilityObj.xmlUtil.GetTextValue(Constants.TranslationNode,
                Constants.RnaSequenceWithTwelveChars);
            string expectedProtein = utilityObj.xmlUtil.GetTextValue(Constants.TranslationNode,
                Constants.AminoAcidForMoreOffsetThree);
            string expectedOffset = utilityObj.xmlUtil.GetTextValue(Constants.CodonsNode,
                Constants.OffsetVaule1);
            ISequence protein = null;

            // Translate twelve characters RNA to protein.
            Sequence proteinTranslation = new Sequence(Alphabets.RNA, expectedSeq);
            protein = ProteinTranslation.Translate(proteinTranslation,
               Convert.ToInt32(expectedOffset, null));

            // Validate Protein Translation.
            Assert.AreEqual(new string(protein.Select(a => (char)a).ToArray()), expectedProtein);
            ApplicationLog.WriteLine(
                "Translation P1: Protein translation validation is completed successfully.");
        }

        #endregion Translation P1 TestCases

        #region Helper Methods

        /// <summary>
        /// Passes a valid RNA sequence with offset value and validates if
        /// Lookup(sequence) method is returning corresponding amino acid.
        /// </summary>
        /// <param name="sequenceNode">Sequence xml node.</param>
        /// <param name="offsetNode">Offset node with value.</param>
        /// <param name="aminoAcidNode">Expected amino acid value.</param>
        void ValidateCodonsTranslationWithOffset(string sequenceNode,
            string offsetNode, string aminoAcidNode)
        {
            // Get Node values from XML.
            string expectedSeq = utilityObj.xmlUtil.GetTextValue(Constants.TranslationNode,
                sequenceNode);
            string expectedAminoAcid = utilityObj.xmlUtil.GetTextValue(Constants.TranslationNode,
                aminoAcidNode);
            string expectedOffset = utilityObj.xmlUtil.GetTextValue(Constants.CodonsNode, offsetNode);
            string aminoAcid = null;

            // Translate Rna to corresponding amino acid.
            Sequence codonsTranslation = new Sequence(Alphabets.RNA, expectedSeq);
            aminoAcid = Codons.Lookup(codonsTranslation, int.Parse(expectedOffset, null)).ToString();

            // Validate Codon Translation.
            Assert.AreEqual(expectedAminoAcid.ToString((IFormatProvider)null), aminoAcid);
            ApplicationLog.WriteLine(
                "Translation P1: Codon translation with offset validation is completed successfully.");
        }

        /// <summary>
        /// Passes a valid DNA sequence and validates corresponding transcribe sequence i.e. RNA Sequence
        /// </summary>
        /// <param name="sequenceNode">Sequence xml node.</param>
        /// <param name="revTranscribeNode">Expected transcribe sequence.</param>
        void ValidateDnaToRnaTranscribe(string sequenceNode, string transcribeNode)
        {
            // Get Node values from XML.
            string alphabetName = utilityObj.xmlUtil.GetTextValue(Constants.SimpleDnaAlphabetNode,
                Constants.AlphabetNameNode);
            string expectedSeq = utilityObj.xmlUtil.GetTextValue(Constants.TranscribeNode,
                sequenceNode);
            string expectedTranscribe = utilityObj.xmlUtil.GetTextValue(Constants.TranscribeNode,
                transcribeNode);
            ISequence transcribe = null;

            Sequence seq = new Sequence(Utility.GetAlphabet(alphabetName), expectedSeq);
            // Transcribe DNA Sequence.
            transcribe = Transcription.Transcribe(seq);

            // Validate Dna to Rna transcribe.
            Assert.AreEqual(expectedTranscribe, new string(transcribe.Select(a => (char)a).ToArray()));
            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "Translation P1: Transcribe {0} is expected.", transcribe));
            ApplicationLog.WriteLine(
                "Translation P1: Transcribe to Dna to Rna sequence completed successfully.");
        }

        /// <summary>
        /// Passes a valid RNA sequence and validates corresponding Reverse transcribe sequence i.e. DNA Sequence
        /// </summary>
        /// <param name="sequenceNode">Sequence xml node.</param>
        /// <param name="revTranscribeNode">Expected reverse transcribe sequence.</param>
        void ValidateRnaToDnaReverseTranscribe(string sequenceNode, string revTranscribeNode)
        {
            // Get Node values from XML.
            string alphabetName = utilityObj.xmlUtil.GetTextValue(Constants.SimpleRnaAlphabetNode,
                Constants.AlphabetNameNode);
            string expectedRnaSeq = utilityObj.xmlUtil.GetTextValue(Constants.TranscribeNode,
                sequenceNode);
            string expectedReverseTranscribe = utilityObj.xmlUtil.GetTextValue(Constants.TranscribeNode,
                revTranscribeNode);
            ISequence revTranscribe = null;

            Sequence seq = new Sequence(Utility.GetAlphabet(alphabetName), expectedRnaSeq);
            // Reverse Transcribe Rna Sequence.
            revTranscribe = Transcription.ReverseTranscribe(seq);

            // Validate Rna to Dna Reverse Transcribe.
            Assert.AreEqual(expectedReverseTranscribe, new string(revTranscribe.Select(a => (char)a).ToArray()));

            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "Translation P1: Reverse Transcribe {0} is expected.", revTranscribe));
            ApplicationLog.WriteLine(
                "Translation P1: Reverse Transcribe from Rna to Dna sequence completed successfully.");
        }

        #endregion
    }
}
