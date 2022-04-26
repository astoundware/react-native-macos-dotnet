#!/bin/bash

export NODE_BINARY=node
export CONFIGURATION=Release
export PLATFORM_NAME=macosx
export UNLOCALIZED_RESOURCES_FOLDER_PATH=Resources
export CONFIGURATION_BUILD_DIR=$(pwd)

found=false

while [ $found != 'true' ] && [ $(pwd) != '/' ]
do
    if [ -d 'node_modules' ]; then
        found=true
    else
        cd ..
    fi
done

./node_modules/react-native-macos/scripts/react-native-xcode.sh
