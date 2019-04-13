﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Bio.Algorithms.Alignment;
using Bio.Util;

namespace Bio.IO.SAM
{
    /// <summary>
    /// Class to hold Reads or aligned sequence or query sequence in SAM Alignment.
    /// </summary>
    [Serializable]
    public class SAMAlignedSequence : IAlignedSequence
    {
        #region Fields
        /// <summary>
        /// SAM aligned sequence header.
        /// </summary>
        private SAMAlignedSequenceHeader seqHeader;

        /// <summary>
        /// Holds metadata of this aligned sequence.
        /// </summary>
        private Dictionary<string, object> metadata;

        /// <summary>
        /// Holds aligned sequence.
        /// </summary>
        private List<ISequence> sequences;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets read/query/aligned sequence.
        /// </summary>
        public ISequence QuerySequence
        {
            get
            {
                if (sequences == null || sequences.Count == 0)
                {
                    return null;
                }

                return sequences[0];
            }
            set
            {
                if (sequences == null)
                {
                    sequences = new List<ISequence>();
                }

                if (sequences.Count == 0)
                {
                    sequences.Add(value);
                }
                else
                {
                    sequences[0] = value;
                }
            }
        }

        /// <summary>
        /// Query pair name if paired; or Query name if unpaired.
        /// </summary>  
        public string QName
        {
            get
            {
                return seqHeader.QName;
            }
            set
            {
                seqHeader.QName = value;
            }
        }

        /// <summary>
        /// SAM flags.
        /// <see cref="SAMFlags"/>
        /// </summary>
        public SAMFlags Flag
        {
            get
            {
                return seqHeader.Flag;
            }
            set
            {
                seqHeader.Flag = value;
            }
        }

        /// <summary>
        /// Reference sequence name.
        /// </summary>
        public string RName
        {
            get
            {
                return seqHeader.RName;
            }
            set
            {
                seqHeader.RName = value;
            }
        }

        /// <summary>
        /// One-based leftmost position/coordinate of the clipped sequence.
        /// </summary>
        public int Pos
        {
            get
            {
                return seqHeader.Pos;
            }
            set
            {
                seqHeader.Pos = value;
            }
        }

        /// <summary>
        /// Gets bin depending on POS and CIGAR values.
        /// </summary>
        public int Bin
        {
            get
            {
                return seqHeader.Bin;
            }

            internal set
            {
                seqHeader.Bin = value;
            }
        }

        /// <summary>
        /// Gets Query length depending on CIGAR value.
        /// </summary>
        public int QueryLength
        {
            get
            {
                return seqHeader.QueryLength;
            }
        }

        /// <summary>
        /// Mapping quality (phred-scaled posterior probability that the 
        /// mapping position of this read is incorrect).
        /// </summary>
        public int MapQ
        {
            get
            {

                return seqHeader.MapQ;
            }
            set
            {
                seqHeader.MapQ = value;
            }
        }

        /// <summary>
        /// Extended CIGAR string.
        /// </summary>
        public string CIGAR
        {
            get
            {
                return seqHeader.CIGAR;
            }

            set
            {
                seqHeader.CIGAR = value;
            }
        }

        /// <summary>
        /// Mate reference sequence name. 
        /// </summary>
        public string MRNM
        {
            get
            {
                return seqHeader.MRNM;
            }

            set
            {
                seqHeader.MRNM = value;
            }
        }

        /// <summary>
        /// One-based leftmost mate position of the clipped sequence.
        /// </summary>
        public int MPos
        {
            get
            {
                return seqHeader.MPos;
            }

            set
            {
                seqHeader.MPos = value;
            }
        }

        /// <summary>
        /// Inferred insert size.
        /// </summary>
        public int ISize
        {
            get
            {
                return seqHeader.ISize;
            }

            set
            {
                seqHeader.ISize = value;
            }
        }

        /// <summary>
        /// Contains the list of indices of "." symbols present in the aligned sequence.
        /// As "." is not supported by DNA, RNA and Protien alphabets, while creating aligned 
        /// sequence "." symbols are replaced by "N" which has the same meaning of ".".
        /// </summary>
        public IList<int> DotSymbolIndexes
        {
            get
            {
                return seqHeader.DotSymbolIndices;
            }
        }

        /// <summary>
        /// Contains the list of "=" symbol indices present in the aligned sequence.
        /// The "=" symbol in aligned sequence indicates that the symbol at this index 
        /// is equal to the symbol present in the reference sequence. As "=" is not 
        /// supported by DNA, RNA and Protien alphabets, while creating aligned 
        /// sequence "=" symbols are replaced by the symbol present in the reference 
        /// sequence at the same index.
        /// </summary>
        public IList<int> EqualSymbolIndexes
        {
            get
            {
                return seqHeader.EqualSymbolIndices;
            }
        }

        /// <summary>
        /// Optional fields.
        /// </summary>
        public IList<SAMOptionalField> OptionalFields
        {
            get
            {
                return seqHeader.OptionalFields;
            }
        }

        /// <summary>
        /// Metadata of this aligned sequence.
        /// SAMAlignedSequenceHeader is stored with the key "SAMAlignedSequenceHeader".
        /// </summary>
        public Dictionary<string, object> Metadata
        {
            get { return metadata; }
        }

        /// <summary>
        /// Always returns QuerySequence in a list.
        /// </summary>
        public IList<ISequence> Sequences
        {
            get 
            {
                if (sequences == null)
                {
                    sequences = new List<ISequence>();
                }

                return sequences.AsReadOnly(); 
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates new instance of SAMAlignedSequence.
        /// </summary>
        public SAMAlignedSequence() : this(new SAMAlignedSequenceHeader()) { }

        /// <summary>
        /// Creates new instance of SAMAlignedSequence with specified SAMAlignedSequenceHeader.
        /// </summary>
        /// <param name="seqHeader"></param>
        public SAMAlignedSequence(SAMAlignedSequenceHeader seqHeader)
        {
            this.seqHeader = seqHeader;
            metadata = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            metadata.Add(Helper.SAMAlignedSequenceHeaderKey, seqHeader);
        }

        /// <summary>
        /// Constructor for deserialization.
        /// </summary>
        /// <param name="info">Serialization Info.</param>
        /// <param name="context">Streaming context.</param>
        protected SAMAlignedSequence(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            seqHeader = (SAMAlignedSequenceHeader)info.GetValue("header", typeof(SAMAlignedSequenceHeader));
            metadata = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            metadata.Add(Helper.SAMAlignedSequenceHeaderKey, seqHeader);
            QuerySequence = (ISequence)info.GetValue("sequence", typeof(ISequence));
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets quality values.
        /// </summary>
        public byte[] GetQualityValues()
        {
            QualitativeSequence seq = QuerySequence as QualitativeSequence;
            if (seq != null)
            {
                return seq.QualityScores.ToArray();
            }
            else
            {
                return new byte[0];
            }
        }

        /// <summary>
        /// Method for serializing the SAMAlignedSequence.
        /// </summary>
        /// <param name="info">Serialization Info.</param>
        /// <param name="context">Streaming context.</param>
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            info.AddValue("header", seqHeader);
            info.AddValue("sequence", QuerySequence);
        }
        #endregion
    }
}
