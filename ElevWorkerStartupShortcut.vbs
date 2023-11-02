Set WshShell = CreateObject("WScript.Shell")
strUserProfile = WshShell.ExpandEnvironmentStrings("%USERPROFILE%")
strProgramPath = strUserProfile & "\elevsw\elevsw.exe"
WshShell.Run strProgramPath, 0, True
Set WshShell = Nothing
