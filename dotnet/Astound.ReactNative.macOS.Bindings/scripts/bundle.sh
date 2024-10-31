#!/bin/bash

msg_prefix="React Bundle:"
lock_file=".lock"

if [[ -f "$lock_file" ]]; then
  echo "$msg_prefix lock file found"
  react_bundle_count=$(cat "$lock_file")
fi

if [[ $react_bundle_count =~ \\d+ ]]; then
  echo "$msg_prefix lock file is invalid"
  react_bundle_count=0
fi

react_bundle_count=$((react_bundle_count + 1))

if (( react_bundle_count > 3 )); then
  echo "$msg_prefix resetting lock"
  rm -f -- "$lock_file"
  react_bundle_count=1
fi

echo "$react_bundle_count" > ".lock"

if (( react_bundle_count > 1 )); then
  echo "$msg_prefix attempt #$react_bundle_count; skipping"
  exit 0
fi

projectPath=$(pwd)
scriptFile=$(readlink -f "$0")
scriptPath=$(dirname "$scriptFile")

source "$scriptPath/find-package.sh"

export NODE_BINARY=node
export CONFIGURATION=Release
export PLATFORM_NAME=macosx
export CONFIGURATION_BUILD_DIR=$(pwd)
export UNLOCALIZED_RESOURCES_FOLDER_PATH=${projectPath/$CONFIGURATION_BUILD_DIR\//}

./node_modules/react-native-macos/scripts/react-native-xcode.sh
