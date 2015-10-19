set umbracoweb=D:\WebMatrix\ApproveIt

XCOPY "%ProjectFolder%bin\%ProjectName%.*" "%umbracoweb%\bin\" /d /Y
XCOPY "%ProjectFolder%bin\PackageActionsContrib.dll" "%umbracoweb%\bin\" /d /Y
XCOPY "%ProjectFolder%App_Plugins\ApproveIt\*.*" "%umbracoweb%\App_Plugins\ApproveIt\*.*" /d /Y /s
XCOPY "%ProjectFolder%Dashboard\Views\dashboard\approveIt\*.*" "%umbracoweb%\Umbraco\Views\dashboard\approveIt\*.*" /d /Y /s
@rem XCOPY "%ProjectFolder%Umbraco\Config\Lang\*.*" "%umbracoweb%\Umbraco\Config\Lang\*.*" /d /Y /s
@rem XCOPY "%ProjectFolder%Config\*.*" "%umbracoweb%\Config\*.*" /d /Y /s
@rem XCOPY "%ProjectFolder%Global.asax" "%umbracoweb%" /d /Y
@rem XCOPY "%ProjectFolder%Web.config" "%umbracoweb%" /d /Y

@rem If any parameter, recycle
if "%1" == "" (	
	set RECYCLE=false
) else (
	set RECYCLE=true
)

@rem NOTE: It is necessary to do pushd outside of the if, because it does not work properly inside the if
pushd %umbracoweb%
if %RECYCLE% == false (	
	@echo App Pool not recycled ...
) else (
	@echo Recycling App Pool - Touch web.config - force plugin assembly reload ...
	copy /Y /b "%umbracoweb%\web.config" +,,
)
popd