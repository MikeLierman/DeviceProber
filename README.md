# DeviceProber

Small command line application that pulls info Device Manager, outputs # of devices w/ missing drivers, # of devices in error state, # of devices OK, along with proper exit code, for use on RMM dashboards. Supports Windows based workstations and servers. Read below for more info. Screenshot of what this script returns when using Solarwinds:

####For update information and discussion, join the Invise Labs Discord: https://discord.gg/gK7NQ7h

####Follow me on Twitter: https://twitter.com/MikeLierman

### How it Works — READ FIRST BEFORE DOWNLOADING
Most RMM solutions only allow you to upload scripts, not .exe files. This script allows you to quickly discern if a device and it's associated driver is in warning or error staes, or if the device drivers are missing completely. Basically giving you the same information from Device Manager, at a glance, straight from your RMM dashboard. This info is even viewable using the RMM mobile app.

GETTING STARTED
1. Download the latest release (ready folder). https://github.com/MikeLierman/DeviceProber/releases. Inside you will see 2 files. DeviceProber.exe, and a batch file. 
2. Upload DeviceProber.exe to your web server. Services like Dropbox or Mega will not work because you do not have direct DL access.
3. Edit the batch file and point the URL to yours.
4. Test it. Move the batch file to an empty folder. Open Admin CMD, cd to batch file, and execute. Script will check if the SmartProber.exe binary already exists, it it does, it's executed, if not, it's downloaded. Default save/run directory is C:\IT. This can be changed.
5. DeviceProber.exe quickly loop through all system drivers, and report back how many are OK, in ERROR state, in DISABLED stated, or MISSING altogether. After which the script will return an exit code used by your RMM dashboard to determine PASS or FAIL on the "check." If you've done everything correctly, in command prompt you will see a count of number of OK devices, and other states if there are any. If you do not see this, you messed up, go back to step 1.
6. After verifying that you understand how the script functions, go ahead and upload JUST THE BATCH FILE to your RMM dashboard script manager. Ensure that you CHANGE the default TIMEOUT to 120 seconds. It could take up to this long for the remote file to download and the script to execute.
7. Deploy it to several machines as a test before deploying to every connected agent.

### Download
https://github.com/MikeLierman/DeviceProber/releases

### Important Notes
* Script isn't perfect. Let me know if you run into issues.

### Known Bugs
* ?

### Planned Features
* ?

### About Us
Check our site http://invi.se/labs for annoucements and other projects. We code scripts and programs to make our lives as IT Professionals easier. 

