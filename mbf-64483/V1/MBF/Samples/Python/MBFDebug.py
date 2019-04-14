#// *****************************************************************
#//    Copyright (c) Microsoft. All rights reserved.
#//    This code is licensed under the Microsoft Public License.
#//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
#//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
#//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
#//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
#// *****************************************************************

# This is meant to be run by ipy.exe to build the MBF.IronPython dll, copy the other necessary
# files to bin\Debug, and then start the demo in the debugger.

import clr
import os
from os import path
from System.IO import File
from System.IO import Directory
import sys

build_dir = "bin\\Debug"

def deploy_file(filename):
    "Copies a file to the bin\Debug folder, replacing any file of the same name already there."
    new_filename = build_dir + "\\" + filename[filename.rfind("\\") + 1 :]
    try:
        if File.Exists(new_filename):
            File.Delete(new_filename)
    except:
        # don't worry about replacing read-only files that we can't delete
        pass
    else:
        File.Copy(filename, new_filename)

try:
    # make build dir if needed
    if not path.exists(build_dir):
        os.mkdir(build_dir)
    
    # get list of files to put in dll
    filenames = os.listdir("MBFIronPython")
    for i in range(0, len(filenames)):
	    filenames[i] = "MBFIronPython\\" + filenames[i]
	    
	# build dll
    clr.CompileModules(build_dir + "\\MBF.IronPython.dll", *filenames)
    
    # copy demo file
    deploy_file("MBFMenu.py")

    deploy_file("..\\..\\..\\Build\\Binaries\\Debug\\MBF.dll")
    deploy_file("..\\..\\..\\Build\\Binaries\\Debug\\MBF.WebServiceHandlers.dll")
        
    # copy test file
    deploy_file("Data\\Small_Size.gbk")
    
    # run the demo
    import MBFMenu
        
except:
    print "An error occurred: " + `sys.exc_info()` + "\n"
    raw_input("Press enter to exit: ")
