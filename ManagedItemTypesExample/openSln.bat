@echo off
@echo This batch file should be run from a VS2013 x64 Cross Tools Command Prompt
@echo Make sure that you have already run MicroStationDeveloperShell.bat in
@echo order to set the required environment variables.

IF DEFINED MSMDE GOTO checkforvs2013
ECHO MSMDE not defined. You need to run MicroStationDeveloperShell.bat first!
GOTO end

:checkforvs2013
IF DEFINED vs120comntools GOTO vardefined
ECHO Visual Studio 2013 is required for this project
GOTO end

:vardefined
rem At this point "vcvarsall.bat x86_amd64" or VS2013 x64 Cross Tools Command Prompt should already have been called
rem in order to compile successfully
@echo devenv.exe -useenv ManagedItemTypesExample.sln
start devenv.exe -useenv ManagedItemTypesExample.sln

:end