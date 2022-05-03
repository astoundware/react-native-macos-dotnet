#!/bin/bash

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
