all: clean build pack

clean:
	dotnet clean -c Release

build:
	dotnet build -c Release

pack:
	nuget pack Astound.ReactNative.macOS.Bindings.nuspec
