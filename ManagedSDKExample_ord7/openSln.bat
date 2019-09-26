@echo off
@echo This batch file should be run from a VS2017 x64 Cross Tools Command Prompt
@echo Make sure that you have already run OpenRoadsDesignerSDKDeveloperShell.bat in
@echo order to set the required environment variables.

IF DEFINED ORDE GOTO checkforvs2017
ECHO ORDE not defined. You need to run OpenRoadsDesignerSDKDeveloperShell.bat first!
GOTO end

:checkforvs2017
IF DEFINED vs140comntools GOTO vardefined
ECHO Visual Studio 2017 is required for this project
GOTO end

:vardefined
rem At this point "vcvarsall.bat x86_amd64" or VS2017 x64 Cross Tools Command Prompt should already have been called
rem in order to compile successfully
@echo devenv.exe -useenv "%~dp0ManagedSDKExample.sln"
start devenv.exe -useenv "%~dp0ManagedSDKExample.sln"
 
:end
