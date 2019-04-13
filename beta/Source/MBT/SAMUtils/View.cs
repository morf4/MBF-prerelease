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
using CommandLine;
using MBF;
using MBF.IO.BAM;
using MBF.IO.SAM;
using SAMUtils.Properties;
using MBF.IO;

namespace SAMUtils
{
    /// <summary>
    /// Class for View option.
    /// </summary>
    public class View
    {
        #region Public Fields

        /// <summary>
        /// Output in BAM format.
        /// </summary>
        [Argument(ArgumentType.AtMostOnce, HelpText = "Output BAM", ShortName = "b")]
        public bool BAMOutput;

        /// <summary>
        /// Print header with alignment.
        /// </summary>
        [Argument(ArgumentType.AtMostOnce, HelpText = "Print header for the SAM output", ShortName = "h")]
        public bool Header;

        /// <summary>
        /// Print only header.
        /// </summary>
        [Argument(ArgumentType.AtMostOnce, HelpText = "Print header only (no alignments)", ShortName = "H")]
        public bool HeaderOnly;

        /// <summary>
        /// Input file is in SAM format.
        /// </summary>
        [Argument(ArgumentType.AtMostOnce, HelpText = "Input is SAM format", ShortName = "S")]
        public bool SAMInput;

        /// <summary>
        /// Display uncompressed Bam file.
        /// </summary>
        [Argument(ArgumentType.AtMostOnce, HelpText = "Uncompressed BAM output", ShortName = "u")]
        public bool UnCompressedBAM;

        /// <summary>
        /// Display flag in HEX.
        /// </summary>
        [Argument(ArgumentType.AtMostOnce, HelpText = "Output FLAG in HEX", ShortName = "x")]
        public bool FlagInHex;

        /// <summary>
        /// Display flag as string.
        /// </summary>   
        [Argument(ArgumentType.AtMostOnce, HelpText = "Output FLAG in string", ShortName = "X")]
        public bool FlagAsString;

        /// <summary>
        /// Path of file containing reference name and length in tab delimited format.
        /// </summary>
        [Argument(ArgumentType.AtMostOnce, HelpText = "List of reference names and lengths in a" +
        "tab limited file rest all field will be ignored.", ShortName = "t")]
        public string ReferenceNamesAndLength;

        /// <summary>
        /// Path of file containing reference sequence.
        /// </summary>
        [Argument(ArgumentType.AtMostOnce, HelpText = "Reference sequence file", ShortName = "T")]
        public string ReferenceSequenceFile;

        /// <summary>
        /// Path of output file.
        /// </summary>
        [Argument(ArgumentType.AtMostOnce, HelpText = "Output file path", ShortName = "o")]
        public string OutputFilePath;

        /// <summary>
        /// Only output alignments with all bits in INT present in the FLAG field. 
        /// </summary>
        [Argument(ArgumentType.AtMostOnce, HelpText = "Required flag", ShortName = "f")]
        public int FlagRequired;

        /// <summary>
        /// Skip alignments with bits present in INT.
        /// </summary>
        [Argument(ArgumentType.AtMostOnce, HelpText = "Filtering flag", ShortName = "F")]
        public int FilteringFlag;

        /// <summary>
        /// Skip alignments with MAPQ smaller than INT.
        /// </summary>
        [Argument(ArgumentType.AtMostOnce, HelpText = "Minimum mapping quality", ShortName = "q")]
        public int QualityMinimumMapping;

        /// <summary>
        /// Only output reads in library STR.
        /// </summary>
        [Argument(ArgumentType.AtMostOnce, HelpText = "Only output reads in library", ShortName = "l")]
        public string Library;

        /// <summary>
        /// Only output reads in read group STR.
        /// </summary>
        [Argument(ArgumentType.AtMostOnce, HelpText = "Only output reads in read group", ShortName = "r")]
        public string ReadGroup;

        ///<summary>
        ///If no region is specified, all the alignments will be printed; 
        ///otherwise only alignments overlapping the specified regions will be output. 
        ///</summary>
        [Argument(ArgumentType.AtMostOnce,
            HelpText = "A region can be presented, for example, in the following format:\n" +
                        "          ‘chr2’ (the whole chr2),\n" +
                        "          ‘chr2:1000000’ (region starting from 1,000,000bp)\n" +
                        "          or ‘chr2:1,000,000-2,000,000’\n" +
                        "          (region between 1,000,000 and 2,000,000bp including the end points).\n" +
                        "          The coordinate is 1-based.\n", ShortName = "R")]
        public string Region;

        /// <summary>
        /// Path of input file.
        /// </summary>
        [DefaultArgument(ArgumentType.AtMostOnce, HelpText = "Input SAM/BAM file path")]
        public string InputFilePath;

        /// <summary>
        /// Usage.
        /// </summary>
        [Argument(ArgumentType.AtMostOnce)]
        public bool Usage;

        #endregion

        #region Private Fields

        /// <summary>
        /// Stores Region Variable.
        /// </summary>
        private Region _region;

        /// <summary>
        /// Stores information about RG present in header used for storing library information of the reads.
        /// </summary>
        private List<SAMRecordField> _rgRecFields;

        /// <summary>
        /// Writes to Console or File based on user option.
        /// </summary>
        private TextWriter _writer;

        /// <summary>
        /// Holds Uncompressed out put of BAM.
        /// </summary>
        private Stream _bamUncompressedOutStream;

        /// <summary>
        /// Holds compressed out put of BAM.
        /// </summary>
        private Stream _bamCompressedOutStream;

        /// <summary>
        /// holds bam formatter.
        /// </summary>
        private BAMFormatter _bamformatter;

        /// <summary>
        /// holds bam parser;
        /// </summary>
        private BAMParser _bamparser;

        /// <summary>
        /// holds max limit upto which memory stream can be used.
        /// 1GB = 1 * 1024 * 1024 * 1024.
        /// This limits the max memory utilization to ~1.25GB.
        /// </summary>
        private const long MemStreamLimit = 1 * 1024 * 1024 * 1024;

        /// <summary>
        /// Temp file path for uncompressed bam file.
        /// </summary>
        private string _uncompressedTempfile = string.Empty;

        /// <summary>
        /// Temp file path for compressed bam file.
        /// </summary>
        private string _compressedTempfile = string.Empty;
        #endregion

        #region Public Methods

        /// <summary>
        /// Extract/print all or sub alignments in SAM or BAM format.
        /// By default, this command assumes the file on the command line is in
        /// BAM format and it prints the alignments in SAM.
        /// SAMUtil.exe view in.bam
        /// </summary>
        public void ViewResult()
        {
            try
            {
                if (string.IsNullOrEmpty(InputFilePath))
                {
                    throw new InvalidOperationException("Input File Not specified");
                }

                if (!string.IsNullOrEmpty(Region))
                {
                    StringToRegionConverter();
                }

                Initialize();
                SAMAlignmentHeader header = null;

                if (!SAMInput)
                {
                    Stream stream = new FileStream(InputFilePath, FileMode.Open, FileAccess.Read);
                    try
                    {
                        header = _bamparser.GetHeader(stream);
                    }
                    catch
                    {
                        throw new InvalidOperationException(Resources.InvalidBAMFile);
                    }


                    WriteHeader(header);

                    if (!HeaderOnly)
                    {
                        if (!string.IsNullOrEmpty(Library))
                        {
                            _rgRecFields = header.RecordFields.Where(R => R.Typecode.ToUpper().Equals("RG")).ToList();
                        }

                        foreach (SAMAlignedSequence alignedSequence in GetAlignedSequence(stream))
                        {
                            WriteAlignedSequence(header, alignedSequence);
                        }
                    }
                }
                else
                {
                    MBFTextReader textReader = new MBFTextReader(InputFilePath);
                    try
                    {
                        header = SAMParser.ParseSAMHeader(textReader);
                    }
                    catch
                    {
                        throw new InvalidOperationException(Resources.InvalidSAMFile);
                    }

                    if (header == null)
                    {
                        throw new InvalidOperationException("SAM file doesn't contian header");
                    }

                    WriteHeader(header);

                    if (!HeaderOnly)
                    {
                        if (!string.IsNullOrEmpty(Library))
                        {
                            _rgRecFields = header.RecordFields.Where(R => R.Typecode.ToUpper().Equals("RG")).ToList();
                        }

                        foreach (SAMAlignedSequence alignedSeq in GetAlignedSequence(textReader))
                        {
                            WriteAlignedSequence(header, alignedSeq);
                        }
                    }

                    if (UnCompressedBAM)
                    {
                        _bamUncompressedOutStream.Flush();
                        if (_writer != null)
                        {
                            DisplayBAMContent(_bamUncompressedOutStream);
                        }
                    }

                    if (BAMOutput && !UnCompressedBAM)
                    {
                        _bamUncompressedOutStream.Flush();
                        _bamUncompressedOutStream.Seek(0, SeekOrigin.Begin);
                        _bamformatter.CompressBAMFile(_bamUncompressedOutStream, _bamCompressedOutStream);
                        _bamCompressedOutStream.Flush();
                        if (_writer != null)
                        {
                            DisplayBAMContent(_bamCompressedOutStream);
                        }
                    }
                }
            }
            finally
            {
                Close();
            }
        }

        #endregion

        #region Private Methods
        /// <summary>
        ///  Initializes required parsers, formatters, input and output files based on user option.
        /// </summary>
        private void Initialize()
        {
            _bamparser = new BAMParser();
            _bamformatter = new BAMFormatter();

            _bamUncompressedOutStream = null;
            _bamCompressedOutStream = null;

            if (string.IsNullOrEmpty(OutputFilePath))
            {
                _writer = Console.Out;
            }
            else
            {
                if (UnCompressedBAM || BAMOutput)
                {
                    _writer = null;

                    if (UnCompressedBAM)
                    {
                        _bamUncompressedOutStream = new FileStream(OutputFilePath, FileMode.Create, FileAccess.ReadWrite);
                    }
                    else
                    {
                        _bamCompressedOutStream = new FileStream(OutputFilePath, FileMode.Create, FileAccess.ReadWrite);
                    }
                }
                else
                {
                    _writer = new StreamWriter(OutputFilePath);
                }
            }

            #region Intialize temp files
            long inputfileSize = (new FileInfo(InputFilePath)).Length;
            long unCompressedSize = inputfileSize;

            if (!SAMInput)
            {
                unCompressedSize = inputfileSize * 4; // as uncompressed bam file will be Aprox 4 times that of the compressed file.
            }

            long compressedSize = unCompressedSize / 4;

            // uncompressed file is required for both uncompressed and compressed outputs.
            if ((UnCompressedBAM || BAMOutput) && _bamUncompressedOutStream == null)
            {
                if (HeaderOnly || (MemStreamLimit >= unCompressedSize))
                {
                    _bamUncompressedOutStream = new MemoryStream();
                }
                else
                {
                    _uncompressedTempfile = Path.GetTempFileName();
                    _bamUncompressedOutStream = new FileStream(_uncompressedTempfile, FileMode.Open, FileAccess.ReadWrite);
                }
            }

            if (BAMOutput && !UnCompressedBAM && _bamCompressedOutStream == null)
            {
                if (HeaderOnly || (MemStreamLimit >= compressedSize))
                {
                    _bamCompressedOutStream = new MemoryStream((int)(inputfileSize));
                }
                else
                {
                    _compressedTempfile = Path.GetTempFileName();
                    _bamCompressedOutStream = new FileStream(_compressedTempfile, FileMode.Open, FileAccess.ReadWrite);
                }
            }
            #endregion Intialize temp files
        }

        /// <summary>
        /// Displays pending data and closes all streams.
        /// 
        /// </summary>
        private void Close()
        {
            if (_writer != null)
            {
                _writer.Close();
            }

            if (_bamCompressedOutStream != null)
            {
                _bamCompressedOutStream.Close();
                _bamCompressedOutStream = null;
            }

            if (_bamUncompressedOutStream != null)
            {
                _bamUncompressedOutStream.Close();
                _bamUncompressedOutStream = null;
            }

            if (string.IsNullOrEmpty(_uncompressedTempfile) && File.Exists(_uncompressedTempfile))
            {
                File.Delete(_uncompressedTempfile);
            }

            if (string.IsNullOrEmpty(_compressedTempfile) && File.Exists(_compressedTempfile))
            {
                File.Delete(_compressedTempfile);
            }

            _bamformatter = null;
            if (_bamparser != null)
            {
                _bamparser.Dispose();
                _bamparser = null;
            }
        }

        /// <summary>
        /// Gets a value to indicate whether filter is required or not.
        /// </summary>
        private bool IsFilterApplied()
        {
            if (FlagRequired != 0 || FilteringFlag != 0
                || QualityMinimumMapping != 0 || !string.IsNullOrEmpty(Library)
                || !string.IsNullOrEmpty(ReadGroup) || !string.IsNullOrEmpty(Region))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Writes the header to output stream
        /// </summary>
        /// <param name="header"></param>
        private void WriteHeader(SAMAlignmentHeader header)
        {
            if (!Header && !HeaderOnly)
            {
                return;
            }

            if (UnCompressedBAM || BAMOutput)
            {
                // Incase of compressed bamoutput uncompressed file will be compressed before sending it to output stream.
                _bamformatter.WriteHeader(header, _bamUncompressedOutStream);
            }
            else
            {
                SAMFormatter.WriteHeader(header, _writer);
            }
        }

        /// <summary>
        /// Writes aligned sequence to output stream.
        /// </summary>
        /// <param name="header">Alignment header.</param>
        /// <param name="alignedSequence">Aligned sequence to write.</param>
        private void WriteAlignedSequence(SAMAlignmentHeader header, SAMAlignedSequence alignedSequence)
        {
            if (UnCompressedBAM || BAMOutput)
            {
                // Incase of compressed bamoutput uncompressed file will be compressed before sending it to output stream.
                _bamformatter.WriteAlignedSequence(header, alignedSequence, _bamUncompressedOutStream);
            }
            else
            {
                SAMFormatter.WriteSAMAlignedSequence(alignedSequence, _writer);
            }
        }

        /// <summary>
        /// Gets Aligned seqeunces in the Specified BAM file.
        /// </summary>
        /// <param name="textReader">BAM file stream.</param>
        private IEnumerable<SAMAlignedSequence> GetAlignedSequence(Stream bamStream)
        {
            bool isFilterRequired = IsFilterApplied();
            bool display = true;

            while (!_bamparser.IsEOF())
            {
                SAMAlignedSequence alignedSequence = _bamparser.GetAlignedSequence(false);
                if (isFilterRequired)
                {
                    display = Filter(alignedSequence);
                }

                if (display)
                {
                    yield return alignedSequence;
                }
            }
        }

        /// <summary>
        /// Gets Aligned seqeunces in the Specified SAM file.
        /// </summary>
        /// <param name="textReader">SAM file stream.</param>
        private IEnumerable<SAMAlignedSequence> GetAlignedSequence(MBFTextReader textReader)
        {
            bool isFilterRequired = IsFilterApplied();
            bool display = true;
            //Displays SAM as output.

            while (textReader.HasLines)
            {
                SAMAlignedSequence alignedSequence = SAMParser.ParseSequence(textReader, false);
                if (isFilterRequired)
                {
                    display = Filter(alignedSequence);
                }

                if (display)
                {
                    yield return alignedSequence;
                }

                textReader.GoToNextLine();
            }
        }

        /// <summary>
        /// Displays the bam content to Console.
        /// </summary>
        /// <param name="stream">BAM stream</param>
        private void DisplayBAMContent(Stream stream)
        {
            int blockSizeToRead = 4096;
            stream.Seek(0, SeekOrigin.Begin);

            byte[] bytes = new byte[blockSizeToRead];
            int bytesRead = 0;
            for (int i = 0; i < stream.Length / blockSizeToRead; i++)
            {
                bytesRead = stream.Read(bytes, 0, (int)stream.Length);

                if (bytesRead > 0)
                {
                    string str = System.Text.ASCIIEncoding.ASCII.GetString(bytes, 0, bytesRead);
                    _writer.Write(str);
                }
            }

            if (stream.Position < (stream.Length - 1))
            {
                bytesRead = stream.Read(bytes, 0, (int)stream.Length);
                if (bytesRead > 0)
                {
                    string str = System.Text.ASCIIEncoding.ASCII.GetString(bytes, 0, bytesRead);
                    _writer.Write(str);
                }
            }

            _writer.Flush();
        }

        /// <summary>
        /// Converts Flag to string.
        /// In a string FLAG, each character represents one bit with
        ///p=0x1 (paired), P=0x2 (properly paired), u=0x4 (unmapped),
        ///U=0x8 (mate unmapped), r=0x10 (reverse), R=0x20 (mate reverse)
        ///1=0x40 (first), 2=0x80 (second), s=0x100 (not primary),
        ///f=0x200 (failure) and d=0x400 (duplicate). 
        /// </summary>
        /// <param name="flag">Sequence Flag.</param>
        /// <returns>String of flag.</returns>
        private string GetFlagDesc(SAMFlags flag)
        {
            string str = string.Empty;
            if ((flag & SAMFlags.PairedRead) == SAMFlags.PairedRead)
            {
                str = str + "p";
            }


            if ((flag & SAMFlags.MappedInProperPair) == SAMFlags.MappedInProperPair)
            {
                str = str + "P";
            }

            if ((flag & SAMFlags.UnmappedQuery) == SAMFlags.UnmappedQuery)
            {
                str = str + "u";
            }

            if ((flag & SAMFlags.UnmappedMate) == SAMFlags.UnmappedMate)
            {
                str = str + "U";
            }

            if ((flag & SAMFlags.QueryOnReverseStrand) == SAMFlags.QueryOnReverseStrand)
            {
                str = str + "r";
            }

            if ((flag & SAMFlags.MateOnReverseStrand) == SAMFlags.MateOnReverseStrand)
            {
                str = str + "R";
            }

            if ((flag & SAMFlags.FirstReadInPair) == SAMFlags.FirstReadInPair)
            {
                str = str + "1";
            }

            if ((flag & SAMFlags.SecondReadInPair) == SAMFlags.SecondReadInPair)
            {
                str = str + "2";
            }

            if ((flag & SAMFlags.NonPrimeAlignment) == SAMFlags.NonPrimeAlignment)
            {
                str = str + "s";
            }

            if ((flag & SAMFlags.QualityCheckFailure)
                == SAMFlags.QualityCheckFailure)
            {
                str = str + "f";
            }

            if ((flag & SAMFlags.Duplicate) == SAMFlags.Duplicate)
            {
                str = str + "d";
            }

            return str;
        }

        /// <summary>
        /// Filters Sequence based on user inputs.
        /// </summary>
        /// <param name="alignedSequence">Aligned Sequence.</param>
        /// <returns>Whether aligned sequence matches user defined options.</returns>
        private bool Filter(SAMAlignedSequence alignedSequence)
        {
            bool filter = true;
            if (filter && FlagRequired != 0)
            {
                filter = (((int)alignedSequence.Flag) & FlagRequired) == FlagRequired;
            }

            if (filter && FilteringFlag != 0)
            {
                filter = ((((int)alignedSequence.Flag) & FilteringFlag) == 0);
            }

            if (filter && QualityMinimumMapping != 0)
            {
                filter = alignedSequence.MapQ == QualityMinimumMapping;
            }

            if (filter && !string.IsNullOrEmpty(Library))
            {
                filter = _rgRecFields.First(
                        a => a.Tags.First(
                        b => b.Tag.Equals("ID")).Value.Equals(alignedSequence.OptionalFields.First(
                        c => c.Tag.Equals("RG")).Value)).Tags.First(
                        d => d.Tag.Equals("LB")).Value.Equals(Library);
            }

            if (filter && !string.IsNullOrEmpty(ReadGroup))
            {
                filter = alignedSequence.OptionalFields.AsParallel().Where(
                   O => O.Tag.ToUpper().Equals("RG")).ToList().Any(a => a.Value.Equals(ReadGroup));
            }

            if (filter && !string.IsNullOrEmpty(Region))
            {
                if (alignedSequence.RName.Equals(_region.Chromosome))
                {
                    if (_region.Start > -1)
                    {
                        if (alignedSequence.Pos >= _region.Start)
                        {
                            if (_region.End > -1)
                            {
                                if (alignedSequence.Pos <= _region.End)
                                {
                                    filter = true;
                                }
                                else
                                {
                                    filter = false;
                                }
                            }
                            else
                            {
                                filter = true;
                            }
                        }
                        else
                        {
                            filter = false;
                        }
                    }
                    else
                    {
                        filter = true;
                    }
                }
                else
                {
                    filter = false;
                }
            }

            return filter;
        }

        /// <summary>
        /// Converts region passed as command line argument to Region strucure.
        /// </summary>
        /// <param name="region">String passed as command line argument.</param>
        /// <returns>Region structure.</returns>
        private void StringToRegionConverter()
        {
            string[] splitRegion = Region.Split(new char[] { ':', '-' });
            if (splitRegion.Length == 1)
            {
                _region = new Region()
                {
                    Chromosome = splitRegion[0],
                    Start = -1,
                    End = -1
                };
            }
            else if (splitRegion.Length == 2)
            {
                _region = new Region()
                {
                    Chromosome = splitRegion[0],
                    Start = uint.Parse(splitRegion[1], CultureInfo.InvariantCulture),
                    End = -1
                };
            }
            else if (splitRegion.Length == 3)
            {
                _region = new Region()
                {
                    Chromosome = splitRegion[0],
                    Start = uint.Parse(splitRegion[1], CultureInfo.InvariantCulture),
                    End = uint.Parse(splitRegion[2], CultureInfo.InvariantCulture)
                };
            }
            else
            {
                throw new InvalidOperationException("Region cannot be parsed");
            }
        }

        #endregion
    }

    #region Public Structures

    /// <summary>
    /// An alignment may be given multiple times if it is overlapping several regions. 
    /// A region can be presented, for example, in the following format: 
    /// ‘chr2’ (the whole chr2), 
    /// ‘chr2:1000000’ (region starting from 1,000,000bp) 
    /// or ‘chr2:1,000,000-2,000,000’ (region between 1,000,000 and 2,000,000bp including the end points). 
    /// The coordinate is 1-based. 
    /// </summary>
    public struct Region
    {
        /// <summary>
        /// Chromosome Number.
        /// </summary>
        public string Chromosome { get; set; }

        /// <summary>
        /// Start position of alignment.
        /// </summary>
        public long Start { get; set; }

        /// <summary>
        /// End position of alignment.
        /// </summary>
        public long End { get; set; }
    }

    #endregion
}