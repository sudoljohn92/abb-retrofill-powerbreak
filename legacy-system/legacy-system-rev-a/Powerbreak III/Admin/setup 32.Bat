SET CURDIR=%~dp0


COPY "%CURDIR%scriptpw32.dll" "C:\Windows\System32\scriptpw.dll" 

regsvr32 scriptpw.dll
pause