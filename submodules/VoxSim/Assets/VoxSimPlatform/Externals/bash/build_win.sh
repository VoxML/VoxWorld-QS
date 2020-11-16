#!/bin/bash
# Builds VoxSim for Windows platform
# Requires MinGW to run on Windows
# Pass -b with a build configuration XML file (required)
# Pass -a with a path to Unity (optional: defaults to assumed known location in Applications on OSX or Program Files on Windows)
#  use this if you have Hub installed and need to make sure to build VoxSim with a particular version of Unity
# You must have Unity Windows build support installed
# Clean quits Unity if already open
# Quits Unity when complete "-n 1" is passed
[ $# -lt 2 ] || [ $1 != "-b" ] && { echo "Usage: $0 -b <config file>.xml [-a path/to/unity] [-n 1]"; exit 1; }
while getopts b:a:n: option
do
case "${option}"
in
b) CONFIG=${OPTARG};;
a) UNITYPATH=${OPTARG};;
n) NOQUIT=${OPTARG};;
esac
done
if [ ! -f "$CONFIG" ]; then
    echo "No file named '$CONFIG' exists"
else
    if [[ "$OSTYPE" == "darwin"* ]]; then
        osascript -e 'quit app "Unity"'
	    if [ -z "$UNITYPATH" ]; then
	        UNITYPATH="/Applications/Unity/Unity.app/Contents/MacOS/Unity"
        elif [[ "$UNITYPATH" == *"/Unity.app" ]]; then
            UNITYPATH+="/Contents/MacOS/Unity"
        fi
        if [ $NOQUIT -eq 1 ]; then
            "$UNITYPATH" -projectpath $(pwd) -executeMethod StandaloneBuild.AutoBuilder.BuildWindows VoxSim $CONFIG
        else
            "$UNITYPATH" -projectpath $(pwd) -executeMethod StandaloneBuild.AutoBuilder.BuildWindows VoxSim $CONFIG -quit
    fi
    elif [[ "$OSTYPE" == "msys" ]]; then
        taskkill //F //IM Unity.exe //T
	    if [ -z "$UNITYPATH" ]; then
	        UNITYPATH="C:/Program Files/Unity/Editor/Unity.exe"
	    fi
        if [ $NOQUIT -eq 1 ]; then
            "$UNITYPATH" -projectpath $(pwd) -executeMethod StandaloneBuild.AutoBuilder.BuildWindows VoxSim $CONFIG
        else
            "$UNITYPATH" -projectpath $(pwd) -executeMethod StandaloneBuild.AutoBuilder.BuildWindows VoxSim $CONFIG -quit
        fi
    fi
fi
