﻿/****************************************************************************
 * SparseSequenceP2TestCases.cs
 * 
 * This file contains the Sparse Sequence P2 test case validation.
 * 
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Globalization;

using Bio;
using Bio.TestAutomation.Util;
using Bio.Util.Logging;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace Bio.TestAutomation
{
    /// <summary>
    /// Test Automation code for Bio Sparse Sequence P2 level validations..
    /// </summary>
    [TestClass]
    public class SparseSequenceP2TestCases
    {

        #region Global Variables

        Utility utilityObj = new Utility(@"TestUtils\TestsConfig.xml");

        #endregion Global Variables

        #region Constructor

        /// <summary>
        /// Static constructor to open log and make other settings needed for test
        /// </summary>
        static SparseSequenceP2TestCases()
        {
            Trace.Set(Trace.SeqWarnings);
            if (!ApplicationLog.Ready)
            {
                ApplicationLog.Open("bio.automation.log");
            }
        }

        #endregion

        #region SparseSequence P2 TestCases

        /// <summary>
        /// Validate an exception by passing null value.
        /// Input Data : Valid Alphabet
        /// Output Data : Validation of Null Exception 
        /// </summary>
        [TestMethod]
        [Priority(2)]
        [TestCategory("Priority2")]
        public void ValidateSparseSequenceNullException()
        {
            string expectedErrorMessage = GetErrorMessage(
                Constants.SparseNullExceptionMessage);
            string actualError = string.Empty;
            string updatedActualError = string.Empty;
            SparseSequence sparseSeq = null;

            // Try Creating sparse sequence by passing null value.
            try
            {
                IAlphabet alphabet = null;
                sparseSeq = new SparseSequence(alphabet);
            }
            catch (ArgumentNullException e)
            {
                actualError = e.Message;
            }

            updatedActualError = Regex.Replace(actualError, "[\r\n\t]", "");
            Assert.AreEqual(expectedErrorMessage.ToUpperInvariant(),
                updatedActualError.ToUpperInvariant()); ;
            Assert.IsNull(sparseSeq);

            // Log to GUI.
            Console.WriteLine(string.Format((IFormatProvider)null,
                "SparseSequence P2: SparseSequence Exception was validated successfully {0}",
                updatedActualError));
        }

        /// <summary>
        /// Validate creation of sparse sequence by passing negative index 
        /// to Sparse Sequence.
        /// Input Data : Valid Alphabet
        /// Output Data : Validation of Exception  
        /// </summary>
        [TestMethod]
        [Priority(2)]
        [TestCategory("Priority2")]
        public void ValidateSparseSeqNegativeIndexException()
        {
            string expectedErrorMessage = GetErrorMessage(
                Constants.NegativeIndexErrorMessage);
            string actualError = string.Empty;
            string updatedActualError = string.Empty;
            SparseSequence sparseSeq = null;

            // Try Creating sparse sequence by passing null value.
            try
            {
                sparseSeq = new SparseSequence(Alphabets.DNA, -5, Alphabets.DNA.A);
            }
            catch (ArgumentException e)
            {
                actualError = e.Message;
            }

            updatedActualError = Regex.Replace(actualError, "[\r\n\t]", "");
            Assert.AreEqual(expectedErrorMessage.ToUpperInvariant(),
                updatedActualError.ToUpperInvariant());
            Assert.IsNull(sparseSeq);

            // Log to GUI.
            Console.WriteLine(string.Format((IFormatProvider)null,
                "SparseSequence P2: SparseSequence Exception was validated successfully {0}",
                updatedActualError));
        }

        /// <summary>
        ///  Validate creation of sparse sequence by passing null value as alphabet
        ///  to SparseSequence(alphabet,index,SeqItem).
        /// Input Data : Valid Alphabet
        /// Output Data : Validation of Null Exception 
        /// </summary>
        [TestMethod]
        [Priority(2)]
        [TestCategory("Priority2")]
        public void ValidateSparseSequenceAlphabetNullException()
        {
            string expectedErrorMessage = GetErrorMessage(
                Constants.SparseNullExceptionMessage);
            string actualError = string.Empty;
            string updatedActualError = string.Empty;
            SparseSequence sparseSeq = null;

            // Try Creating sparse sequence by passing null value as alphabet.
            try
            {
                sparseSeq = new SparseSequence(null, 2, Alphabets.DNA.A);
            }
            catch (ArgumentNullException e)
            {
                actualError = e.Message;
            }

            updatedActualError = Regex.Replace(actualError, "[\r\n\t]", "");

            Assert.AreEqual(expectedErrorMessage.ToUpperInvariant(),
                updatedActualError.ToUpperInvariant());
            Assert.IsNull(sparseSeq);

            // Log to GUI.
            Console.WriteLine(string.Format((IFormatProvider)null,
                "SparseSequence P2: SparseSequence Exception was validated successfully {0}",
                updatedActualError));
        }

        /// <summary>
        ///  Validate creation of sparse sequence by passing Invalid Sequence Item.
        /// Input Data : Valid Alphabet
        /// Output Data : Validation of Exception error.
        /// </summary>
        [TestMethod]
        [Priority(2)]
        [TestCategory("Priority2")]
        public void ValidateSparseSequenceInvalidSymbolException()
        {
            // Try Creating sparse sequence by passing null value as alphabet.
            try
            {
                new SparseSequence(Alphabets.DNA, 2, Alphabets.RNA.U);
            }
            catch (ArgumentException)
            {
                Console.WriteLine(
                   "SparseSequence P2: SparseSequence Exception was validated successfully");
            }
        }

        /// <summary>
        /// Validate creation of sparse sequence by passing negative index 
        /// to Sparse Sequence(alphabet;index;IList<SeqItems>).
        /// Input Data : Valid Alphabet
        /// Output Data : Validation of Exception 
        /// </summary>
        [TestMethod]
        [Priority(2)]
        [TestCategory("Priority2")]
        public void ValidateSparseIListSeqNegativeIndexException()
        {
            string expectedErrorMessage = GetErrorMessage(
               Constants.NegativeIndexErrorMessage);

            string actualError = string.Empty;
            string updatedActualError = string.Empty;
            SparseSequence sparseSeq = null;
            IEnumerable<byte> byteArray = new List<byte>() { Alphabets.DNA.A, Alphabets.DNA.C };

            // Try Creating sparse sequence by passing null value.
            try
            {
                sparseSeq = new SparseSequence(Alphabets.DNA, -5, byteArray);
            }
            catch (ArgumentException e)
            {
                actualError = e.Message;
            }

            updatedActualError = Regex.Replace(actualError, "[\r\n\t]", "");
            Assert.AreEqual(expectedErrorMessage.ToUpperInvariant(),
                updatedActualError.ToUpperInvariant());
            Assert.IsNull(sparseSeq);

            // Log to GUI.
            Console.WriteLine(string.Format((IFormatProvider)null,
                "SparseSequence P2: SparseSequence Exception was validated successfully {0}",
                updatedActualError));
        }

        /// <summary>
        ///  Validate creation of sparse sequence by passing null value as alphabet
        ///  to SparseSequence(alphabet,index,IList<SqItems>).
        ///  Input Data : Valid Alphabet.
        ///  Output Data : Validate an exception 
        /// </summary>
        [TestMethod]
        [Priority(2)]
        [TestCategory("Priority2")]
        public void ValidateSparseListSeqItemsAlphabetNullException()
        {
            string expectedErrorMessage = GetErrorMessage(
               Constants.SparseNullExceptionMessage);
            string actualError = string.Empty;
            string updatedActualError = string.Empty;
            SparseSequence sparseSeq = null;
            IEnumerable<byte> byteArray = new List<byte>() { Alphabets.DNA.A, Alphabets.DNA.C };

            // Try Creating sparse sequence by passing null value as alphabet.
            try
            {
                sparseSeq = new SparseSequence(null, 2, byteArray);
            }
            catch (ArgumentNullException e)
            {
                actualError = e.Message;
            }

            updatedActualError = Regex.Replace(actualError, "[\r\n\t]", "");
            Assert.AreEqual(expectedErrorMessage.ToUpperInvariant(),
                updatedActualError.ToUpperInvariant());
            Assert.IsNull(sparseSeq);

            // Log to GUI.
            Console.WriteLine(string.Format((IFormatProvider)null,
                "SparseSequence P2: SparseSequence Exception was validated successfully {0}",
                updatedActualError));
        }

        /// <summary>
        /// Validate creation of sparse sequence by passing null value as
        /// alphabet parameter to Sparse Sequence(alphabet;index;IList<SeqItems>).
        ///  Input Data: Valid Alphabet.
        ///  Output Data : Validate Exception.
        /// </summary>
        [TestMethod]
        [Priority(2)]
        [TestCategory("Priority2")]
        public void ValidateSparseIListSeqAlphabetException()
        {
            IEnumerable<byte> byteArray = new List<byte>() { Alphabets.RNA.U, Alphabets.RNA.U };

            // Try Creating sparse sequence by passing null value.
            try
            {
                new SparseSequence(Alphabets.DNA, 2, byteArray);
            }
            catch (ArgumentException)
            {
                Console.WriteLine(
                   "SparseSequence P2: SparseSequence Exception was validated successfully");
            }
        }

        /// <summary>
        ///  Validate Invalid start index for GetSubSequence() method
        ///  with IsReadOnly property set to False.
        ///  Input Data : Valid Alphabet.
        ///  Output Data : Validation of Exception.
        /// </summary>
        [TestMethod]
        [Priority(2)]
        [TestCategory("Priority2")]
        public void ValidateExceptionByPassingInvalidStartIndex()
        {
            //Create a sparse sequence.
            SparseSequence sparseSeq = new SparseSequence(Alphabets.DNA);

            // Try to add RNA Sequence Item to DNA sparse sequence.
            try
            {
                sparseSeq.GetSubSequence(10, 0);
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                Console.WriteLine(
                 "SparseSequence P2: SparseSequence Exception was validated successfully");
            }
        }

        /// <summary>
        ///  Validate Invalid length for GetSubSequence() method 
        ///  with IsReadOnly property set to False.
        ///  Input Data : Valid Alphabet.
        ///  Output Data : Validation of Exception.
        /// </summary>
        [TestMethod]
        [Priority(2)]
        [TestCategory("Priority2")]
        public void ValidateExceptionByPassingInvalidLength()
        {
            //Create a sparse sequence.
            SparseSequence sparseSeq = new SparseSequence(Alphabets.DNA, 5);

            try
            {
                sparseSeq.GetSubSequence(0, 10);
                Assert.Fail();
            }
            catch (ArgumentException)
            {
                // Log to GUI.
                Console.WriteLine("SparseSequence P2: SparseSequence Exception was validated successfully");
            }
        }

        /// <summary>
        /// Validate an expected output by passing invalid sequence item to
        /// contains method.
        /// Input Data : Valid Dna Alphabet
        /// Output Data : Validation of invalid Sequence item in Sparse sequence.
        /// </summary>
        [TestMethod]
        [Priority(2)]
        [TestCategory("Priority2")]
        public void ValidateSeqItemByPassingInvalidItem()
        {
            bool result = false;
            SparseSequence sparseSeq = new SparseSequence(Alphabets.DNA);

            // Set the count to insert sequence items.
            sparseSeq.Count = 2000;

            // Store all sequence items in sparse sequence object
            int insertIndex = 0;
            int[] randomNumbers = Utility.RandomNumberGenerator(2000, Alphabets.DNA.Count);
            foreach (byte item in Alphabets.DNA)
            {
                sparseSeq[randomNumbers[insertIndex]] = item;
                insertIndex++;
            }

            // Validate whether invalid sequence item contains in Sparse 
            // sequence or not.
            foreach (byte bt in sparseSeq)
            {
                Assert.AreNotEqual(bt, Alphabets.RNA.U);
            }

            Console.WriteLine(string.Format((IFormatProvider)null,
                "SparseSequence P2: SparseSequence error was validated successfully {0}",
                result));
        }

        /// <summary>
        /// Validate an exception by passing Protein sequence item to contains method 
        /// with Dna sparse sequence.contains method.
        /// Input Data : Dna Alphabet.
        /// Output Data : Validation of Protein Sequence Item.
        /// </summary>
        [TestMethod]
        [Priority(2)]
        [TestCategory("Priority2")]
        public void ValidateSeqItemsByPassingProteinSeqItemToDnaSequence()
        {
            bool result = false;
            SparseSequence sparseSeq = new SparseSequence(Alphabets.DNA);

            // Set the count to insert sequence items.
            sparseSeq.Count = 2000;

            // Store all sequence items in sparse sequence object
            int insertIndex = 0;
            int[] randomNumbers = Utility.RandomNumberGenerator(2000, Alphabets.DNA.Count);
            foreach (byte item in Alphabets.DNA)
            {
                sparseSeq[randomNumbers[insertIndex]] = item;
                insertIndex++;
            }

            // Validate whether invalid sequence item contains in Sparse 
            // sequence or not.
            foreach (byte bt in sparseSeq)
            {
                Assert.AreNotEqual(bt, Alphabets.Protein.D);
            }

            Assert.IsFalse(result);
            Console.WriteLine(string.Format((IFormatProvider)null,
                " SparseSequence P2: SparseSequence error was validated successfully.{0}",
                result));
        }

        /// <summary>
        /// Validate an exception by passing Rna sequence item to contains method 
        /// with Dna sparse sequence.contains method.
        /// Input Data : Rna Alphabet.
        /// Output Data : Validation of Protein Sequence Item.
        /// </summary>
        [TestMethod]
        [Priority(2)]
        [TestCategory("Priority2")]
        public void ValidateSeqItemsByPassingProteinSeqItemToRnaSequence()
        {
            bool result = false;
            SparseSequence sparseSeq = new SparseSequence(Alphabets.RNA);

            // Set the count to insert sequence items.
            sparseSeq.Count = 2000;

            // Store all sequence items in sparse sequence object
            int insertIndex = 0;
            int[] randomNumbers = Utility.RandomNumberGenerator(2000, Alphabets.RNA.Count);
            foreach (byte item in Alphabets.RNA)
            {
                sparseSeq[randomNumbers[insertIndex]] = item;
                insertIndex++;
            }

            // Validate whether invalid sequence item contains in Sparse 
            // sequence or not.
            foreach (byte bt in sparseSeq)
            {
                Assert.AreNotEqual(bt, Alphabets.Protein.D);
            }

            Console.WriteLine(string.Format((IFormatProvider)null,
                " SparseSequence P2: SparseSequence error was validated successfully.{0}",
                result));
        }

        /// <summary>
        /// Validate an exception by setting sparse sequence items 
        /// position greater then sparse count.
        /// Input Data : Valid Dna Alphabet
        /// Output Data : Validation of an exception by setting sparse 
        /// sequence items greater than sprase count.
        /// </summary>
        [TestMethod]
        [Priority(2)]
        [TestCategory("Priority2")]
        public void ValidateExceptionByPassingSeqItemToInvalidSequenceItemPostion()
        {
            string expectedErrorMessage = GetErrorMessage(
              Constants.InvalidSequenceCountError);
            string actualError = string.Empty;
            string updatedActualError = string.Empty;

            SparseSequence sparseSeq = new SparseSequence(Alphabets.DNA);

            // Set the count 1000 to insert sequence items.
            sparseSeq.Count = 1000;

            // Try to store seq items at 1050 position greater than sparse
            // sequence count.
            try
            {
                sparseSeq[1050] = Alphabets.DNA.A;
            }
            catch (ArgumentOutOfRangeException e)
            {
                actualError = e.Message;
            }

            updatedActualError = Regex.Replace(actualError, "[\r\n\t]", "");
            Assert.AreEqual(expectedErrorMessage.ToUpperInvariant(),
                updatedActualError.ToUpperInvariant());

            // Log to GUI.
            Console.WriteLine(string.Format((IFormatProvider)null,
                "SparseSequence P2: SparseSequence Exception was validated successfully {0}",
                updatedActualError));
        }

        /// <summary>
        /// Validate sparse sequence items index value using IndexOf(SeqItem)
        /// method by passing another sequence item other than sparse sequence item.
        /// Input Data : Valid Dna Alphabet,sequence count and IseqItem
        /// Output Data : Valiation of an expected output.
        /// </summary>
        [TestMethod]
        [Priority(2)]
        [TestCategory("Priority2")]
        public void ValidateIndexValueOfDifferentSeqItemOtherThanSparseSeqItem()
        {
            // Create a sparse sequence.
            SparseSequence sparseSeq = new SparseSequence(Alphabets.DNA);

            // Set the count to insert sequence items.
            sparseSeq.Count = 2000;

            // Store all sequence items in sparse sequence object
            int insertIndex = 0;
            int[] randomNumbers = Utility.RandomNumberGenerator(2000, Alphabets.DNA.Count);
            foreach (byte item in Alphabets.DNA)
            {
                sparseSeq[randomNumbers[insertIndex]] = item;
                insertIndex++;
            }

            foreach (byte bt in sparseSeq)
            {
                Assert.AreNotEqual(bt, Alphabets.RNA.U);
            }

            // Log to GUI.
            Console.WriteLine("SparseSequence P2: SparseSequence Exception was validated successfully");
        }

        /// <summary>
        /// Validate an exception by passing invalid size value.
        /// Input Data : Valid Alphabet
        /// Output Data : Validation of Out of Range Exception 
        /// </summary>
        [TestMethod]
        [Priority(2)]
        [TestCategory("Priority2")]
        public void ValidateSparseSequenceInvalidSize()
        {
            SparseSequence sparseSeq = null;

            // Try Creating sparse sequence by passing invalid size.
            try
            {
                sparseSeq = new SparseSequence(Alphabets.DNA, -1);
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                LogMessage(ex.Message);
            }

            Assert.IsNull(sparseSeq);
        }

        /// <summary>
        /// Validate an exception by passing invalid size value.
        /// Input Data : Valid Alphabet
        /// Output Data : Validation of Out of Range Exception 
        /// </summary>
        [TestMethod]
        [Priority(2)]
        [TestCategory("Priority2")]
        public void ValidateSparseSequenceInvalidIndex()
        {
            SparseSequence sparseSeq = null;

            // Try Creating sparse sequence by passing invalid index.
            try
            {
                sparseSeq = new SparseSequence(Alphabets.DNA, -1, null);
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                LogMessage(ex.Message);
            }

            Assert.IsNull(sparseSeq);
        }

        /// <summary>
        /// Validate an exception by passing invalid sequence item.
        /// Input Data : Valid Alphabet
        /// Output Data : Validation of null Exception 
        /// </summary>
        [TestMethod]
        [Priority(2)]
        [TestCategory("Priority2")]
        public void ValidateSparseSequenceInvalidSeqItem()
        {
            SparseSequence sparseSeq = null;

            // Try Creating sparse sequence by passing invalid sequence item.
            try
            {
                sparseSeq = new SparseSequence(Alphabets.DNA, 0, null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                LogMessage(ex.Message);
            }

            Assert.IsNull(sparseSeq);
        }

        /// <summary>
        /// Validate an exception by passing invalid size value.
        /// Input Data : Valid Alphabet
        /// Output Data : Validation of null Exception 
        /// </summary>
        [TestMethod]
        [Priority(2)]
        [TestCategory("Priority2")]
        public void ValidateSparseSequenceInvalidAlphabet()
        {
            SparseSequence sparseSeq = null;
            Sequence seq = new Sequence(Alphabets.RNA, "U");
            byte seqItem = seq[0];

            // Try Creating sparse sequence by passing invalid alphabet.
            try
            {
                sparseSeq = new SparseSequence(Alphabets.DNA, 0, seqItem);
                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                LogMessage(ex.Message);
            }

            Assert.IsNull(sparseSeq);
        }

        /// <summary>
        /// Validate an exception by passing invalid size value.
        /// Input Data : Valid Alphabet
        /// Output Data : Validation of Out of Range Exception 
        /// </summary>
        [TestMethod]
        [Priority(2)]
        [TestCategory("Priority2")]
        public void ValidateSparseSequenceListInvalidIndex()
        {
            SparseSequence sparseSeq = null;
            Sequence seq = new Sequence(Alphabets.DNA, "G");
            List<byte> seqItemList = new List<byte>();
            seqItemList.Add(seq[0]);

            // Try Creating sparse sequence by passing invalid index.
            try
            {
                sparseSeq = new SparseSequence(Alphabets.DNA, -1, seqItemList);
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                LogMessage(ex.Message);
            }

            Assert.IsNull(sparseSeq);
        }

        /// <summary>
        /// Validate an exception by passing invalid size value.
        /// Input Data : Valid Alphabet
        /// Output Data : Validation of null Exception 
        /// </summary>
        [TestMethod]
        [Priority(2)]
        [TestCategory("Priority2")]
        public void ValidateSparseSequenceListInvalidAlphabet()
        {
            SparseSequence sparseSeq = null;
            Sequence seq = new Sequence(Alphabets.RNA, "U");
            List<byte> seqItemList = new List<byte>();
            seqItemList.Add(seq[0]);

            // Try Creating sparse sequence by passing Invalid alphabet
            try
            {
                sparseSeq = new SparseSequence(Alphabets.DNA, 0, seqItemList);
                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                LogMessage(ex.Message);
            }

            Assert.IsNull(sparseSeq);
        }

        /// <summary>
        /// Validate an exception by passing invalid indexer value.
        /// Input Data : Valid Alphabet
        /// Output Data : Validation of Out of Range Exception 
        /// </summary>
        [TestMethod]
        [Priority(2)]
        [TestCategory("Priority2")]
        public void ValidateSparseSequenceInvalidIndexer()
        {
            SparseSequence sparseSeq = CreateSparseSequence(Alphabets.DNA, 0);

            try
            {
                byte seqItem = sparseSeq[-1];
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                LogMessage(ex.Message);
            }
        }

        /// <summary>
        /// Validate an exception by passing invalid indexer value.
        /// Input Data : Valid Alphabet
        /// Output Data : Validation of Out of Range Exception 
        /// </summary>
        [TestMethod]
        [Priority(2)]
        [TestCategory("Priority2")]
        public void ValidateSparseSequenceSetInvalidIndexer()
        {
            SparseSequence sparseSeq = CreateSparseSequence(Alphabets.DNA, 0);
            byte seqItem = 67;
            try
            {
                sparseSeq[-1] = seqItem;
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                LogMessage(ex.Message);
            }
        }

        /// <summary>
        /// Validate an exception by passing invalid count value.
        /// Input Data : Valid Alphabet
        /// Output Data : Validation of out of range Exception 
        /// </summary>
        [TestMethod]
        [Priority(2)]
        [TestCategory("Priority2")]
        public void ValidateSparseSequenceInvalidCount()
        {
            SparseSequence sparseSeq = CreateSparseSequence(Alphabets.DNA, 0);

            try
            {
                sparseSeq.Count = -1;
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                LogMessage(ex.Message);
            }
        }

        /// <summary>
        /// Invalidates CopyTo
        /// </summary>
        [TestMethod]
        [Priority(2)]
        [TestCategory("Priority2")]
        public void InvalidateCopyTo()
        {
            List<byte> byteList = new List<byte>();
            byteList.Add(Alphabets.DNA.Gap);
            byteList.Add(Alphabets.DNA.G);
            byteList.Add(Alphabets.DNA.A);
            byteList.Add(Alphabets.DNA.Gap);
            byteList.Add(Alphabets.DNA.T);
            byteList.Add(Alphabets.DNA.C);
            byteList.Add(Alphabets.DNA.Gap);
            byteList.Add(Alphabets.DNA.Gap);

            ISequence iSeq = new SparseSequence(Alphabets.DNA, 0, byteList);
            SparseSequence seqObj = new SparseSequence(iSeq);

            //check with null array
            byte[] array = null;
            try
            {
                seqObj.CopyTo(array, 10, 20);
                Assert.Fail();
            }
            catch (ArgumentNullException anex)
            {
                ApplicationLog.WriteLine("Successfully caught ArgumentNullException : " + anex.Message);
            }

            //check with more than available length
            array = new byte[byteList.Count];
            try
            {
                seqObj.CopyTo(array, 0, byteList.Count + 100);
                Assert.Fail();
            }
            catch (ArgumentException aex)
            {
                ApplicationLog.WriteLine("Successfully caught ArgumentException : " + aex.Message);
            }

            //check with negative start
            array = new byte[byteList.Count];
            try
            {
                seqObj.CopyTo(array, -5, byteList.Count);
                Assert.Fail();
            }
            catch (ArgumentException aex)
            {
                ApplicationLog.WriteLine("Successfully caught ArgumentException : " + aex.Message);
            }

            //check with negative count
            array = new byte[byteList.Count];
            try
            {
                seqObj.CopyTo(array, 0, -5);
                Assert.Fail();
            }
            catch (ArgumentException aex)
            {
                ApplicationLog.WriteLine("Successfully caught ArgumentException : " + aex.Message);
            }
        }
        #endregion SparseSequence P2 TestCases

        #region Supporting methods

        /// <summary>
        /// Get Error messages from xml node.
        /// </summary>
        /// <param name="nodeName">Name of the error message node </param>
        string GetErrorMessage(string nodeName)
        {
            string expectedErrorMessage = utilityObj.xmlUtil.GetTextValue(
                Constants.DnaVirtualSeqNode, nodeName);

            return expectedErrorMessage;
        }

        /// <summary>
        ///  Create sparse sequence and insert all sequence items of alphabet.
        /// </summary>
        /// <param name="alphabet"></param>
        /// <returns></returns>
        private static SparseSequence CreateSparseSequence(IAlphabet alphabet, int insertPosition)
        {
            // Create sequence item list
            List<byte> sequenceList = new List<byte>();
            foreach (byte item in alphabet)
            {
                sequenceList.Add(item);
            }

            // Store sequence item in sparse sequence object using list of sequence items
            SparseSequence sparseSeq = new SparseSequence(alphabet, insertPosition, sequenceList);

            return sparseSeq;
        }

        /// <summary>
        /// Logs the message to Console and Log
        /// </summary>
        /// <param name="ex">Exception to log</param>
        private static void LogMessage(string message)
        {
            ApplicationLog.WriteLine(string.Format((IFormatProvider)null,
                "SparseSequence P2: SparseSequence Exception was validated successfully {0}",
                message));
            // Log to GUI.
            Console.WriteLine(string.Format((IFormatProvider)null,
                "SparseSequence P2: SparseSequence Exception was validated successfully {0}",
                message));
        }

        #endregion Supproting methods.
    }
}
