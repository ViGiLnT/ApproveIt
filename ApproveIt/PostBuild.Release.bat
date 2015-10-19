set umbracoweb=D:\WebMatrix\ApproveIt

@rem XCOPY "%ProjectFolder%bin\%ProjectName%.*" "%umbracoweb%\bin\" /d /Y
@rem XCOPY "%ProjectFolder%bin\PackageActionsContrib.dll" "%umbracoweb%\bin\" /d /Y
@rem XCOPY "%ProjectFolder%App_Plugins\ApproveIt\*.*" "%umbracoweb%\App_Plugins\ApproveIt\*.*" /d /Y /s
@rem XCOPY "%ProjectFolder%Dashboard\Views\dashboard\approveIt\*.*" "%umbracoweb%\Umbraco\Views\dashboard\approveIt\*.*" /d /Y /s
@rem XCOPY "%ProjectFolder%Umbraco\Config\Lang\*.*" "%umbracoweb%\Umbraco\Config\Lang\*.*" /d /Y /s
@rem XCOPY "%ProjectFolder%Config\*.*" "%umbracoweb%\Config\*.*" /d /Y /s
@rem XCOPY "%ProjectFolder%Global.asax" "%umbracoweb%" /d /Y
@rem XCOPY "%ProjectFolder%Web.config" "%umbracoweb%" /d /Y
