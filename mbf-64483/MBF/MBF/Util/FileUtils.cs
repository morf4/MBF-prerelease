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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace MBF.Util
{

    /// <summary>
    /// A static class of methods related to files.
    /// </summary>
    public static class FileUtils
    {
        /// <summary>
        /// The string that tells the parser how comments are marked.
        /// </summary>
        public const string CommentHeader = "Comment token:";

        /// <summary>
        /// Open a FileInfo as a StreamReader that skips over comments.
        /// </summary>
        /// <param name="file">The FileInfo to read</param>
        /// <returns>a StreamReader that skips over comments</returns>
        public static StreamReader OpenTextStripComments(this FileInfo file)
        {
            return new CommentedStreamReader(file);
        }

        /// <summary>
        /// Open the named file as a StreamReader that skips over comments.
        /// </summary>
        /// <param name="filename">The file to read</param>
        /// <returns>a StreamReader that skips over comments</returns>
        public static StreamReader OpenTextStripComments(string filename)
        {
            return new CommentedStreamReader(filename);
        }

        /// <summary>
        /// Filter the comments out of a stream.
        /// </summary>
        /// <param name="stream">The steam to filter</param>
        /// <returns>a StreamReader that skips over comments</returns>
        public static StreamReader StripComments(Stream stream)
        {
            return new CommentedStreamReader(stream);
        }


        /// <summary>
        /// Read the first line of a file after any comments.
        /// </summary>
        /// <param name="file">The FileInfo from which to read.</param>
        /// <returns>The first line of a file after skipping any comments.</returns>
        public static string ReadLine(FileInfo file)
        {
            using (StreamReader streamReader = file.OpenTextStripComments())
            {
                return streamReader.ReadLine();
            }
        }

        /// <summary>
        /// Read the first line of a file after any comments.
        /// </summary>
        /// <param name="filename">A name of the file from which to read</param>
        /// <returns>The first line of a file after skipping any comments.</returns>
        public static string ReadLine(string filename)
        {
            FileInfo file = new FileInfo(filename);
            if (!file.Exists)
                throw new FileNotFoundException(filename + " does not exist.");
            return ReadLine(file);
            //using (StreamReader streamReader = FileUtils.OpenTextStripComments(filename))
            //{
            //    return streamReader.ReadLine();
            //}
        }

        /// <summary>
        /// </summary>
        /// <param name="fileName">The name of the file from which to read.</param>
        /// <returns>a sequence of lines from a file</returns>
        public static IEnumerable<string> ReadEachLine(string fileName)
        {
            FileInfo file = new FileInfo(fileName);
            return file.ReadEachLine();
        }

        /// <summary>
        /// Returns a sequence of lines from a TextReader.
        /// </summary>
        /// <param name="textReader">A textReader from which to read lines.</param>
        /// <returns>a sequence of lines from a TextReader</returns>
        public static IEnumerable<string> ReadEachLine(TextReader textReader)
        {
            string line;
            while (null != (line = textReader.ReadLine()))
            {
                yield return line;
            }
        }

        /// <summary>
        /// Returns a sequence of lines from a file.
        /// </summary>
        /// <param name="file">A FileInfo from which to read lines.</param>
        /// <returns>a sequence of lines from a file</returns>
        public static IEnumerable<string> ReadEachLine(this FileInfo file)
        {
            using (TextReader textReader = file.OpenTextStripComments())
            {
                string line;
                while (null != (line = textReader.ReadLine()))
                {
                    yield return line;
                }
            }
        }

        /// <summary>
        /// Returns the lines of a file as a pair with both lines and their index number
        /// </summary>
        /// <param name="fileName">The name of the file to read.</param>
        /// <returns>A sequence of KeyValuePair's. The key is the line and the value is the index number.</returns>
        public static IEnumerable<KeyValuePair<string, int>> ReadEachIndexedLine(string fileName)
        {
            FileInfo file = new FileInfo(fileName);
            return file.ReadEachIndexedLine();
        }

        /// <summary>
        /// Returns the lines of a TextReader as a pair with both lines and their index number
        /// </summary>
        /// <param name="textReader">The TextReader that is the source of lines.</param>
        /// <returns>A sequence of KeyValuePair's. The key is the line and the value is the index number.</returns>
        public static IEnumerable<KeyValuePair<string, int>> ReadEachIndexedLine(TextReader textReader)
        {
            string line;
            int i = 0;
            while (null != (line = textReader.ReadLine()))
            {
                yield return new KeyValuePair<string, int>(line, i);
                ++i;
            }
        }

        /// <summary>
        /// Returns the lines of a file as a pair with both lines and their index number
        /// </summary>
        /// <param name="file">A FileInfo to read from</param>
        /// <returns>A sequence of KeyValuePair's. The key is the line and the value is the index number.</returns>
        public static IEnumerable<KeyValuePair<string, int>> ReadEachIndexedLine(this FileInfo file)
        {
            using (TextReader textReader = file.OpenTextStripComments())
            {
                string line;
                int i = 0;
                while (null != (line = textReader.ReadLine()))
                {
                    yield return new KeyValuePair<string, int>(line, i);
                    ++i;
                }
            }
        }

        //!!!should "new FileStream" be in a Using so Dispose gets done?

        /// <summary>
        /// Read a file stripping out comment, but with ReadWrite sharing.
        /// </summary>
        /// <param name="filename">The file to read</param>
        /// <returns>A StreamReader</returns>
        public static StreamReader GetTextReaderWithExternalReadWriteAccess(string filename)
        {
            //return new StreamReader(new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite));
            return FileUtils.StripComments(new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite));
        }

        /// <summary>
        /// Get all the files that fit a pattern. The patterns can contain '*' as a wildcard. Patterns can
        /// include directories. Patterns can be combined into larger patterns with '+'
        /// </summary>
        /// <param name="inputPattern">A file pattern.</param>
        /// <param name="zeroIsOK">True if its OK that no actual files match the pattern between '+''s.</param>
        /// <returns>The names of actual files that match the pattern.</returns>
        public static IEnumerable<string> GetFiles(string inputPattern, bool zeroIsOK)
        {
            foreach (string inputSubPattern in inputPattern.Split('+'))
            {
                bool isZero = true;
                string directoryName = Path.GetDirectoryName(inputSubPattern);
                if (directoryName == "")
                {
                    directoryName = ".";
                }

                foreach (string fileName in Directory.GetFiles(directoryName, Path.GetFileName(inputSubPattern)))
                {
                    yield return fileName;
                    isZero = false;
                }
                Helper.CheckCondition(!isZero || zeroIsOK, Properties.Resource.ErrorNoFilesMatchSpecifiedName, inputSubPattern);
            }
        }

        /// <summary>
        /// Create a directory for a file if the file's directory does not already exist.
        /// </summary>
        /// <param name="fileName">The file to create a directory for.</param>
        public static void CreateDirectoryForFileIfNeeded(string fileName)
        {
            string outputDirectoryName = Path.GetDirectoryName(fileName);
            if (!string.IsNullOrEmpty( outputDirectoryName))
            {
                Directory.CreateDirectory(outputDirectoryName);
            }
        }

        /// <summary>
        /// Creates a directory for a file if the file's directory does not already exist.
        /// </summary>
        /// <param name="fileInfo">The file for while the directory will be created.</param>
        public static void CreateDirectoryForFileIfNeeded(this FileInfo fileInfo)
        {
            if (!fileInfo.Directory.Exists)
                fileInfo.Directory.Create();
        }

        /// <summary>
        /// Returns the assembly in which program execution began. If no such assembly exists (for example, if this is running in Silverlight), then returns the calling assembly.
        /// </summary>
        /// <returns></returns>
        public static Assembly GetEntryOrCallingAssembly()
        {
            Assembly entryAssembly = Assembly.GetEntryAssembly();
            if (null != entryAssembly)
            {
                return entryAssembly;
            }
            return Assembly.GetCallingAssembly();
        }

    }


    /// <summary>
    /// A stream reader that can skip over comments in the input.
    /// </summary>
    public class CommentedStreamReader : StreamReader
    {

        bool _haveReadFirstLine = false;
        bool _isCommented = false;

        /// <summary>
        /// The string used to mark a line as a comment line.
        /// </summary>
        public string CommentToken
        {
            get;
            private set;
        }

        /// <summary>
        /// Create a CommentedStreamReader from a FileInfo
        /// </summary>
        /// <param name="fileInfo">The fileinfo to read.</param>
        public CommentedStreamReader(FileInfo fileInfo) : base(fileInfo.OpenRead()) { }

        /// <summary>
        /// Create a CommentedStreamReader from a file
        /// </summary>
        /// <param name="filename">The file to read</param>
        public CommentedStreamReader(string filename) : base(filename) { }

        /// <summary>
        /// Create a CommentedStreamReader from a stream
        /// </summary>
        /// <param name="stream">The stream to create a CommentedStreamReader from</param>
        public CommentedStreamReader(Stream stream) : base(stream) { }

        /// <summary>
        /// Returns the next noncomment line
        /// </summary>
        /// <returns>The next noncomment line</returns>
        public override string ReadLine()
        {
            return ReadCommentOrNonCommentLine(false);
        }

        /// <summary>
        /// Returns the next comment line
        /// </summary>
        /// <returns>A comment line</returns>
        public string ReadCommentLine()
        {
            return ReadCommentOrNonCommentLine(true);
        }


        /// <summary>
        /// Read the next line
        /// </summary>
        /// <param name="returnComment">if true, returns the next comment line; otherwise, returns the next noncomment line.</param>
        /// <returns>the next line</returns>
        protected string ReadCommentOrNonCommentLine(bool returnComment)
        {
            string line = base.ReadLine();

            if (line == null)
            {
                return null;
            }
            else if (!_haveReadFirstLine)
            {
                _haveReadFirstLine = true;
                if (line.StartsWith(FileUtils.CommentHeader))
                {
                    CommentToken = line.Substring(FileUtils.CommentHeader.Length);
                    _isCommented = true;
                    Helper.CheckCondition(CommentToken.Length > 0, Properties.Resource.ExpectedNonZeroLengthCommentToken);
                    if (returnComment)
                        return line;
                    else
                        return ReadCommentOrNonCommentLine(returnComment);
                }
                else
                {
                    if (returnComment)
                        return ReadCommentOrNonCommentLine(returnComment);
                    else
                        return line;
                }

            }
            else if (_isCommented && line.StartsWith(CommentToken))
            {
                if (returnComment)
                    return line;
                else
                    return ReadCommentOrNonCommentLine(returnComment);
            }
            else
            {
                if (returnComment)
                    return ReadCommentOrNonCommentLine(returnComment);
                else
                    return line;
            }
        }

#pragma warning disable 1591
        public override int Read()
#pragma warning restore 1591
        {
            throw new NotImplementedException("Not bothering to implement this.");
        }

#pragma warning disable 1591
        public override int Read(char[] buffer, int index, int count)
#pragma warning restore 1591
        {
            throw new NotImplementedException("Not bothering to implement this.");
        }

#pragma warning disable 1591
        public override int Peek()
#pragma warning restore 1591
        {
            throw new NotImplementedException("Not bothering to implement this.");
        }

#pragma warning disable 1591
        public override int ReadBlock(char[] buffer, int index, int count)
#pragma warning restore 1591
        {
            throw new NotImplementedException("Not bothering to implement this.");
        }

#pragma warning disable 1591
        public override string ReadToEnd()
#pragma warning restore 1591
        {
            StringBuilder sb = new StringBuilder();
            string line;
            while (null != (line = ReadLine()))
            {
                sb.AppendLine(line);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Read all the comments from the stream
        /// </summary>
        /// <returns>A sequence of comment lines.</returns>
        public IEnumerable<string> ReadAllComments()
        {
            string line;
            while (null != (line = ReadCommentLine()))
            {
                yield return line;
            }
        }


    }

}