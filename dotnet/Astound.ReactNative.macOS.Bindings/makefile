colon := :
$(colon) := :

all: clean build pack

clean:
	msbuild /t$(:)Clean /p$(:)Configuration=Debug
	msbuild /t$(:)Clean /p$(:)Configuration=Release

build:
	msbuild /t$(:)Build /p$(:)Configuration=Debug
	msbuild /t$(:)Build /p$(:)Configuration=Release

pack:
	nuget pack Astound.ReactNative.macOS.Bindings.nuspec
