In order to build ParametricModelingExample, Visual Studio 2013 is required and the MicroStation developer batch file must also have been set. It is recommended to first run the Visual Studio 2013 command prompt "VS2013 x64 Cross Tools command prompt" and then run the MicroStation SDK command MicroStationDeveloperShell.bat.

Make sure that the environment variables for MS and MSDE are set correctly (the MicroStationDeveloperShell.bat should do this).

The ParametricModelingExample demonstrates APIs for varibles, parameter sets and parametric cells. After loading the ParametricModelingExample.dll through "mdl load" use the following keyins to run examples:

PARAMETRICMODELINGEXAMPLE CREATE VARIABLE
PARAMETRICMODELINGEXAMPLE REMOVE VARIABLE
PARAMETRICMODELINGEXAMPLE MODIFY VARIABLE
PARAMETRICMODELINGEXAMPLE EXPORT VARIABLE
PARAMETRICMODELINGEXAMPLE IMPORT VARIABLE

