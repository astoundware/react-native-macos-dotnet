#!/bin/bash

projectPath=$(pwd)

export NODE_BINARY=node
export CONFIGURATION=Release
export PLATFORM_NAME=macosx

found=false

while [ $found != 'true' ] && [ $(pwd) != '/' ]
do
    if [ -d 'node_modules' ]; then
        found=true
    else
        cd ..
    fi
done

if [ ! -d ./node_modules/react-native-macos ]; then
    echo "error: Failed to find react-native-macos. Please ensure the npm package is installed." >&2
    exit 1
fi

export CONFIGURATION_BUILD_DIR=$(pwd)
export UNLOCALIZED_RESOURCES_FOLDER_PATH=${projectPath/$CONFIGURATION_BUILD_DIR\//}/Resources

./node_modules/react-native-macos/scripts/react-native-xcode.sh
