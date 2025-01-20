#Set-PSDebug -Trace 1

cd $PSScriptRoot

# setup the driver that talks to the RAM's microcontroller
#  fyi i just pulled this definition from Fury_CTRL.exe creates
#  this was very enlightening to me, i had no idea it was this easy to install .sys style drivers
# there's so many other "driver manager" tools and chatter out there that never make this point clear
if (!(get-service NTIOLib_KSFX -ErrorAction SilentlyContinue)) {
  # apparently powershell's "new-service" cmdlet doesn't support type=kernel which is what differentiats a driver from a service so we'll use the sc.exe, oh well whatever...
  sc.exe create NTIOLib_KSFX type=kernel start=demand error=normal binpath="\??\$PSScriptRoot\NTIOLib_X64.sys"
}

Start-Service NTIOLib_KSFX

# amazing for windows style exe's (vs console apps) a simple "pipe" like this will wait for the process to end
# https://stackoverflow.com/questions/1741490/how-to-tell-powershell-to-wait-for-each-command-to-end-before-starting-the-next/1742758#1742758
# but also note that i've added win32 calls to enable console output of all logging info 
#   this just seemed more convenient visibility in this command line scripting context than the log files used by the original code
.\KingstonFuryRgbCLI.exe -setrgb=all_off | out-default

Stop-Service NTIOLib_KSFX

rm -r log
