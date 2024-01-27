all: clean build pack

clean:
	dotnet clean -c Debug
	dotnet clean -c Release

build:
	dotnet build -c Debug
	dotnet build -c Release

pack:
	nuget pack Astound.ReactNative.macOS.Bindings.nuspec
