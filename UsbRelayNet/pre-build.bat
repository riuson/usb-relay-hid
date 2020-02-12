@echo on

set destination=%2\Properties\AssemblyInfoVersion.cs

REM file version
for /f %%i in ('git --git-dir %1/.git log --date^=iso --pretty^=tformat:"%%at" -1') do set COMMIT_AT=%%i
set /a "TIMESPAN=%COMMIT_AT% - 946684800"
set /a "DAYS=%TIMESPAN% / 86400"
set /a "SECONDS2=(%TIMESPAN% - (%DAYS% * 86400)) / 2"
set FILE_VERSION=1.0.%DAYS%.%SECONDS2%
echo File version: %FILE_VERSION%
 
echo revision info:
git --git-dir %1/.git log --date=iso --pretty=tformat:"hash: %%H%%nauthor date: %%ad" -1 

echo using System.Reflection; > %destination%
echo.  >> %destination%
git --git-dir %1/.git log --date=iso --pretty=tformat:"[assembly: AssemblyInformationalVersionAttribute(%%x22#%%h from %%ad%%x22)] %%n[assembly: AssemblyVersion(%%x22%FILE_VERSION%%%x22)] %%n[assembly: AssemblyFileVersion(%%x22%FILE_VERSION%%%x22)]" -1 >> %destination%


echo Generated version file:
echo --------------------
type %destination%
echo --------------------

if exist %destination% goto END

echo %destination% not generated.
echo generating empty stub...

( echo using System.Reflection; & echo. & echo. & echo. & echo [assembly: AssemblyInformationalVersionAttribute^("???"^)]) > %destination%
echo Generated version file:
echo --------------------
type %destination%
echo --------------------

:END
echo Done.