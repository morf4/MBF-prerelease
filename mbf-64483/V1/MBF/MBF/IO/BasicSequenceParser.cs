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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using MBF.Encoding;
using MBF.Util.Logging;

namespace MBF.IO
{
    /// <summary>
    /// This is an abstract class that provides some basic operations common to sequence
    /// parsers. It is meant to be used as the base class for parser implementations
    /// if the implementer wants to make use of default behavior.
    /// </summary>
    public abstract class BasicSequenceParser : ISequenceParser
    {
        #region Fields
        /// <summary>
        /// Holds distinct symbols while parsing the sequence, used to 
        /// identify alphabet for the sequence.
        /// </summary>
        private IEnumerable<char> _distinctSymbols;
        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor chooses default encoding based on alphabet.
        /// </summary>
        protected BasicSequenceParser()
        {
        }

        /// <summary>
        /// Constructor for setting the encoding.
        /// </summary>
        /// <param name="encoding">The encoding to use for parsed ISequence objects.</param>
        protected BasicSequenceParser(IEncoding encoding)
        {
            Encoding = encoding;
        }

        #endregion

        #region ISequenceParser Members

        /// <summary>
        /// The alphabet to use for parsed ISequence objects.  If this is not set, an alphabet will
        /// be determined based on the file being parsed.
        /// </summary>
        public IAlphabet Alphabet { get; set; }

        /// <summary>
        /// The encoding to use for parsed ISequence objects.  If this is not set, the default
        /// for the given alphabet will be used.
        /// </summary>
        public IEncoding Encoding { get; set; }

        /// <summary>
        /// Parses a list of biological sequence texts from a reader.
        /// </summary>
        /// <param name="reader">A reader for a biological sequence text.</param>
        /// <returns>The list of parsed ISequence objects.</returns>
        public IList<ISequence> Parse(TextReader reader)
        {
            return Parse(reader, true);
        }

        /// <summary>
        /// Parses a list of biological sequence texts from a reader.
        /// </summary>
        /// <param name="reader">A reader for a biological sequence text.</param>
        /// <param name="isReadOnly">
        /// Flag to indicate whether the resulting sequences should be in readonly mode or not.
        /// If this flag is set to true then the resulting sequences's isReadOnly property 
        /// will be set to true, otherwise it will be set to false.
        /// </param>
        /// <returns>The list of parsed ISequence objects.</returns>
        public IList<ISequence> Parse(TextReader reader, bool isReadOnly)
        {
            using (MBFTextReader mbfReader = new MBFTextReader(reader))
            {
                return Parse(mbfReader, isReadOnly);
            }
        }

        /// <summary>
        /// Parses a list of biological sequence texts from a file.
        /// </summary>
        /// <param name="filename">The name of a biological sequence file.</param>
        /// <returns>The list of parsed ISequence objects.</returns>
        public IList<ISequence> Parse(string filename)
        {
            return Parse(filename, true);
        }

        /// <summary>
        /// Parses a list of biological sequence texts from a file.
        /// </summary>
        /// <param name="filename">The name of a biological sequence file.</param>
        /// <param name="isReadOnly">
        /// Flag to indicate whether the resulting sequences should be in readonly mode or not.
        /// If this flag is set to true then the resulting sequences's isReadOnly property 
        /// will be set to true, otherwise it will be set to false.
        /// </param>
        /// <returns>The list of parsed ISequence objects.</returns>
        public IList<ISequence> Parse(string filename, bool isReadOnly)
        {
            using (MBFTextReader mbfReader = new MBFTextReader(filename))
            {
                return Parse(mbfReader, isReadOnly);
            }
        }

        /// <summary>
        /// Parses a list of biological sequence texts from a reader.
        /// </summary>
        /// <param name="reader">A reader for a biological sequence text.</param>
        /// <param name="isReadOnly">
        /// Flag to indicate whether the resulting sequences should be in readonly mode or not.
        /// If this flag is set to true then the resulting sequences's isReadOnly property 
        /// will be set to true, otherwise it will be set to false.
        /// </param>
        /// <returns>The iterator of parsed ISequence objects.</returns>
        public IEnumerable<ISequence> ParseStream(TextReader reader, bool isReadOnly)
        {
            using (MBFTextReader mbfReader = new MBFTextReader(reader))
            {
                return ParseStream(mbfReader, isReadOnly);
            }
        }

        /// <summary>
        /// Parses a single biological sequence text from a reader.
        /// </summary>
        /// <param name="reader">A reader for a biological sequence text.</param>
        /// <returns>The parsed ISequence object.</returns>
        public ISequence ParseOne(TextReader reader)
        {
            return ParseOne(reader, true);
        }

        /// <summary>
        /// Parses a single biological sequence text from a reader.
        /// </summary>
        /// <param name="reader">A reader for a biological sequence text.</param>
        /// <param name="isReadOnly">
        /// Flag to indicate whether the resulting sequence should be in readonly mode or not.
        /// If this flag is set to true then the resulting sequence's isReadOnly property 
        /// will be set to true, otherwise it will be set to false.
        /// </param>
        /// <returns>The parsed ISequence object.</returns>
        public ISequence ParseOne(TextReader reader, bool isReadOnly)
        {
            using (MBFTextReader mbfReader = new MBFTextReader(reader))
            {
                return ParseOne(mbfReader, isReadOnly);
            }
        }

        /// <summary>
        /// Parses a single biological sequence text from a file.
        /// </summary>
        /// <param name="filename">The name of a biological sequence file.</param>
        /// <returns>The parsed ISequence object.</returns>
        public ISequence ParseOne(string filename)
        {
            return ParseOne(filename, true);
        }

        /// <summary>
        /// Parses a single biological sequence text from a file.
        /// </summary>
        /// <param name="filename">The name of a biological sequence file.</param>
        /// <param name="isReadOnly">
        /// Flag to indicate whether the resulting sequence should be in readonly mode or not.
        /// If this flag is set to true then the resulting sequence's isReadOnly property 
        /// will be set to true, otherwise it will be set to false.
        /// </param>
        /// <returns>The parsed ISequence object.</returns>
        public ISequence ParseOne(string filename, bool isReadOnly)
        {
            using (MBFTextReader mbfReader = new MBFTextReader(filename))
            {
                return ParseOne(mbfReader, isReadOnly);
            }
        }

        /// <summary>
        /// Gets the name of the parser. Intended to be filled in 
        /// by classes deriving from BasicSequenceParser class
        /// with the exact name of the parser type.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Gets the description of the parser. Intended to be filled in 
        /// by classes deriving from BasicSequenceParser class
        /// with the exact details of the parser.
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// Gets the filetypes supported by the parser. Intended to be filled in 
        /// by classes deriving from BasicSequenceParser class
        /// with the exact details of the filetypes supported.
        /// </summary>
        public abstract string FileTypes { get; }

        #endregion

        #region Abstract Members

        /// <summary>
        /// Parses a single biological sequence text from a reader into a sequence.
        /// </summary>
        /// <param name="mbfReader">A reader for a biological sequence text.</param>
        /// <param name="isReadOnly">
        /// Flag to indicate whether the resulting sequence should be in readonly mode or not.
        /// If this flag is set to true then the resulting sequence's isReadOnly property 
        /// will be set to true, otherwise it will be set to false.
        /// </param>
        /// <returns>Sequence instance.</returns>
        protected abstract ISequence ParseOneWithSpecificFormat(MBFTextReader mbfReader, bool isReadOnly);

        #endregion

        #region Protected Methods

        /// <summary>
        /// Identifies Alphabet for the sepecified sequence.
        /// </summary>
        /// <param name="currentAlphabet">Currently known alphabet of the sequence, null if alphabet is unknown.</param>
        /// <param name="sequence">Sequence data.</param>
        /// <returns>Returns appropriate alphabet for the specified sequence and considering the specified current alphabet. 
        /// Returns null if any character in the sequence is unrecognized by DNA, RNA and Protien Alphabets.</returns>
        protected IAlphabet IdentifyAlphabet(IAlphabet currentAlphabet, string sequence)
        {
            if (string.IsNullOrEmpty(sequence))
            {
                return null;
            }

            // Alphabets use upper case characters so to prevent parsing errors ensure the sequence
            // is all upper case.
            sequence = sequence.ToUpperInvariant();

            // This is much faster than performing sequence.ToCharArray().Distinct()
            int characters = 0;
            if (currentAlphabet != null)
            {
                characters = Alphabets.GetHighestChar(currentAlphabet) + 1;
            }
            else
            {
                foreach (var item in Alphabets.All)
                {
                    characters = Math.Max(characters, Alphabets.GetHighestChar(item) + 1);
                }
            }

            var characterExists = new bool[characters];

            for (int i = 0; i < characterExists.Length; i++)
            {
                characterExists[i] = false;
            }
            var uniqueValues = new StringBuilder();
            for (int i = 0; i < sequence.Length; i++)
            {
                char sequenceChar = sequence[i];
                if (!characterExists[sequenceChar])
                {
                    characterExists[sequenceChar] = true;
                    uniqueValues.Append(sequenceChar);
                }
            }

            bool canClearDistinctSymbol = false;
            if (_distinctSymbols != null)
            {
                _distinctSymbols = _distinctSymbols.Union(uniqueValues.ToString()).ToList();
            }
            else
            {
                canClearDistinctSymbol = true;
                _distinctSymbols = uniqueValues.ToString().ToCharArray().ToList();
            }

            IAlphabet alphabet = null;

            if (currentAlphabet == Alphabets.Protein)
            {
                alphabet = StartCheckFromProtein();
            }
            else if (currentAlphabet == Alphabets.RNA)
            {
                alphabet = StartCheckFromRna();
            }
            else
            {
                alphabet = StartCheckFromDna();
            }

            if (canClearDistinctSymbol)
            {
                _distinctSymbols = null;
            }

            return alphabet;
        }

        /// <summary>
        /// Maps the string to a particular Molecule type and returns
        /// the instance of mapped molecule type.
        /// </summary>
        /// <param name="type">The molecule type.</param>
        /// <returns>Returns the appropriate molecule type for the specified string.</returns>
        public static MoleculeType GetMoleculeType(string type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            type = type.ToUpper(CultureInfo.InvariantCulture);
            switch (type)
            {
                case "DNA":
                    return MoleculeType.DNA;
                case "NA":
                    return MoleculeType.NA;
                case "RNA":
                    return MoleculeType.RNA;
                case "TRNA":
                    return MoleculeType.tRNA;
                case "RRNA":
                    return MoleculeType.rRNA;
                case "MRNA":
                    return MoleculeType.mRNA;
                case "URNA":
                    return MoleculeType.uRNA;
                case "SNRNA":
                    return MoleculeType.snRNA;
                case "SNORNA":
                    return MoleculeType.snoRNA;
                case "PROTEIN":
                    return MoleculeType.Protein;
                default:
                    return MoleculeType.Invalid;
            }
        }

        /// <summary>
        /// Returns Molecule type depending on the specified alphabet.
        /// </summary>
        /// <param name="alphabet">Alphabet.</param>
        /// <returns>Returns molecule type.</returns>
        public static MoleculeType GetMoleculeType(IAlphabet alphabet)
        {
            if (alphabet == Alphabets.DNA)
            {
                return MoleculeType.DNA;
            }
            else if (alphabet == Alphabets.RNA)
            {
                return MoleculeType.RNA;
            }
            else if (alphabet == Alphabets.Protein)
            {
                return MoleculeType.Protein;
            }
            else
            {
                return MoleculeType.Invalid;
            }
        }

        /// <summary>
        /// Returns the alphabet depending on the specified molecule type.
        /// </summary>
        /// <param name="moleculeType">Molecule type.</param>
        /// <returns>IAlphabet instance.</returns>
        public static IAlphabet GetAlphabet(MoleculeType moleculeType)
        {
            switch (moleculeType)
            {
                case MoleculeType.DNA:
                case MoleculeType.NA:
                    return Alphabets.DNA;
                case MoleculeType.RNA:
                case MoleculeType.tRNA:
                case MoleculeType.rRNA:
                case MoleculeType.mRNA:
                case MoleculeType.uRNA:
                case MoleculeType.snRNA:
                case MoleculeType.snoRNA:
                    return Alphabets.RNA;
                case MoleculeType.Protein:
                    return Alphabets.Protein;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Parses a list of sequences using a MBFTextReader.
        /// </summary>
        /// <remarks>
        /// This method should be overridden by any parsers that need to process file-scope
        /// metadata that applies to all of the sequences in the file.
        /// </remarks>
        /// <param name="mbfReader">A reader for a biological sequence text.</param>
        /// <param name="isReadOnly">
        /// Flag to indicate whether the resulting sequences should be in readonly mode or not.
        /// If this flag is set to true then the resulting sequences's isReadOnly property 
        /// will be set to true, otherwise it will be set to false.
        /// </param>
        /// <returns>The list of parsed ISequence objects.</returns>
        protected virtual IList<ISequence> Parse(MBFTextReader mbfReader, bool isReadOnly)
        {
            if (mbfReader == null)
            {
                throw new ArgumentNullException("mbfReader");
            }

            List<ISequence> sequences = new List<ISequence>();
            sequences.AddRange(ParseStream(mbfReader, isReadOnly));
            return sequences;
        }

        /// <summary>
        /// Parses a list of sequences using a MBFTextReader.
        /// </summary>
        /// <remarks>
        /// This method should be overridden by any parsers that need to process file-scope
        /// metadata that applies to all of the sequences in the file.
        /// </remarks>
        /// <param name="mbfReader">A reader for a biological sequence text.</param>
        /// <param name="isReadOnly">
        /// Flag to indicate whether the resulting sequences should be in readonly mode or not.
        /// If this flag is set to true then the resulting sequences's isReadOnly property 
        /// will be set to true, otherwise it will be set to false.
        /// </param>
        /// <returns>The iterator of parsed ISequence objects.</returns>
        protected virtual IEnumerable<ISequence> ParseStream(MBFTextReader mbfReader, bool isReadOnly)
        {
            if (mbfReader == null)
            {
                throw new ArgumentNullException("mbfReader");
            }

            // no empty files allowed
            if (!mbfReader.HasLines)
            {
                Trace.Report(Properties.Resource.IONoTextToParse);
                throw new InvalidDataException(Properties.Resource.IONoTextToParse);
            }

            while (mbfReader.HasLines)
            {
                yield return ParseOne(mbfReader, isReadOnly);
            }
        }

        #endregion

        #region Private Methods

        // Parses a single sequences using a MBFTextReader.
        private ISequence ParseOne(MBFTextReader mbfReader, bool isReadOnly)
        {
            if (mbfReader == null)
            {
                throw new ArgumentNullException("mbfReader");
            }

            // no empty files allowed
            if (!mbfReader.HasLines)
            {
                string message = Properties.Resource.IONoTextToParse;
                Trace.Report(message);
                throw new InvalidDataException(message);
            }

            _distinctSymbols = new List<char>();

            // do the actual parsing
            ISequence sequence = ParseOneWithSpecificFormat(mbfReader, isReadOnly);

            _distinctSymbols = null;

            return sequence;
        }

        /// <summary>
        /// Returns Dna alphabet if all the symbols in distinctSymbols are 
        /// known by Dna alphabet else it continue to verify with Rna alpabet by 
        /// calling StartCheckFromRna method.
        /// </summary>
        /// <returns>If success then returns an instance of IAlphabet else returns null.</returns>
        private IAlphabet StartCheckFromDna()
        {
            if (!IsDnaAlphabet(_distinctSymbols))
            {
                return StartCheckFromRna();
            }

            return Alphabets.DNA;
        }

        /// <summary>
        /// Returns Rna alphabet if all the symbols in distinctSymbols are 
        /// known by Rna alphabet else it continue to verify with Protein alpabet by 
        /// calling StartCheckFromProtein method.
        /// </summary>
        /// <returns>If success then returns an instance of IAlphabet else returns null.</returns>
        private IAlphabet StartCheckFromRna()
        {
            if (!IsRnaAlphabet(_distinctSymbols))
            {
                return StartCheckFromProtein();
            }

            return Alphabets.RNA;
        }

        /// <summary>
        /// Returns Protein alphabet if all the symbols in distinctSymbols are 
        /// known by protein alphabet else returns null.
        /// </summary>
        /// <returns>If all symbols in distinctSymbols are known by protein alphabet 
        /// then returns protein Alphabet else returns null.</returns>
        private IAlphabet StartCheckFromProtein()
        {
            if (!IsProteinAlphabet(_distinctSymbols))
            {
                return null;
            }

            return Alphabets.Protein;
        }

        /// <summary>
        /// Returns true if all symbols in the specified list are known by Dna.
        /// </summary>
        /// <param name="characters">List of symbols.</param>
        /// <returns>True if all symbols are known else returns false.</returns>
        private static bool IsDnaAlphabet(IEnumerable<char> characters)
        {
            foreach (char ch in characters)
            {
                if (Alphabets.DNA.LookupBySymbol(ch) == null)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Returns true if all symbols in the specified list are known by Rna.
        /// </summary>
        /// <param name="characters">List of symbols.</param>
        /// <returns>True if all symbols are known else returns false.</returns>
        private static bool IsRnaAlphabet(IEnumerable<char> characters)
        {
            foreach (char ch in characters)
            {
                if (Alphabets.RNA.LookupBySymbol(ch) == null)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Returns true if all symbols in the specified list are known by Protein.
        /// </summary>
        /// <param name="characters">List of symbols.</param>
        /// <returns>True if all symbols are known else returns false.</returns>
        private static bool IsProteinAlphabet(IEnumerable<char> characters)
        {
            foreach (char ch in characters)
            {
                if (Alphabets.Protein.LookupBySymbol(ch) == null)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}
