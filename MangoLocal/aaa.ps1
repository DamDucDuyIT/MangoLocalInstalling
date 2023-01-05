cd BackEndLocal

$curDir = Get-Location

.\nssm.exe stop BackEndLocal

.\nssm.exe remove BackEndLocal confirm

.\nssm.exe install BackEndLocal "$curDir\Web.API.exe"

.\nssm.exe start BackEndLocal