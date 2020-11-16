@setlocal enableextensions
@cd /d "%~dp0"
rmdir Assets\Plugins\VoxSimPlatform & del /Q Assets\Plugins\VoxSimPlatform & mklink /D Assets\Plugins\VoxSimPlatform ..\..\submodules\VoxSim\Assets\VoxSimPlatform