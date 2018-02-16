@echo off
if exist "C:\IT\DeviceProber.exe" (
    cd "C:\IT"
    DeviceProber.exe
) else (
    powershell -Command "(New-Object Net.WebClient).DownloadFile('http://YOURSERVER.net/dl/DeviceProber/DeviceProber.exe', 'DeviceProber.exe')"

    if not exist "C:\IT" md "C:\IT" > nul  
    move DeviceProber.exe "C:\IT\DeviceProber.exe" > nul
    cd "C:\IT"
    DeviceProber.exe
)