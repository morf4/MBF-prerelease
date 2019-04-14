# Copyright Microsoft Corporation. All rights reserved.
from System.Collections.Generic import List
import Util
Util.add_mbfdotnet_reference("MBF")
from MBF import *
from MBF.Algorithms.Alignment import *
from MBF.Algorithms.Assembly import *
from MBF.SimilarityMatrices import *

def assemble(fragment_list, overlap_algorithm):
    "Performs sequence assembly using the given pairwise alignment algorithm.\n\
    Returns an IDeNovoAssembly object."
    assembler = OverlapDeNovoAssembler()
    assembler.MergeThreshold = 4
    assembler.OverlapAlgorithm = overlap_algorithm
    assembler.OverlapAlgorithm.SimilarityMatrix = DiagonalSimilarityMatrix(1, -8, MoleculeType.DNA)
    assembler.OverlapAlgorithm.GapOpenCost = -8
    assembler.ConsensusResolver = SimpleConsensusResolver(66)
    assembler.AssumeStandardOrientation = 1
    c_sharp_list = List[ISequence](fragment_list)
    return assembler.Assemble(c_sharp_list)
    
def assemble_nw(seq_list):
    "Performs sequence assembly using the Needleman-Wunsch algorithm."
    return assemble(seq_list, NeedlemanWunschAligner())
    
def assemble_sw(seq_list):
    "Performs sequence assembly using the Smith-Waterman algorithm."
    return assemble(seq_list, SmithWatermanAligner())
    
def assemble_pairwise(seq_list):
    "Performs sequence assembly using the pairwise alignment algorithm."
    return assemble(seq_list, PairwiseOverlapAligner())
    
