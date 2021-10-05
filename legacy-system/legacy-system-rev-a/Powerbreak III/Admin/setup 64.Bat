SET CURDIR=%~dp0


COPY "%CURDIR%scriptpw64.dll" "C:\Windows\System32\scriptpw.dll" 

regsvr32 scriptpw.dll
pause