#!/bin/bash

projectPath=$(pwd)
scriptFile=$(readlink -f "$0")
scriptPath=$(dirname "$scriptFile")

source "$scriptPath/find-package.sh"

export NODE_BINARY=node
export CONFIGURATION=Release
export PLATFORM_NAME=macosx
export CONFIGURATION_BUILD_DIR=$(pwd)
export UNLOCALIZED_RESOURCES_FOLDER_PATH=${projectPath/$CONFIGURATION_BUILD_DIR\//}/Resources

./node_modules/react-native-macos/scripts/react-native-xcode.sh
