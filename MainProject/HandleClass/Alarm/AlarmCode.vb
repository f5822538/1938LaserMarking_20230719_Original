Public Enum AlarmCode
    <Description(SRAlarmCode.IsOK)> IsOK = 0
    <Description(SRAlarmCode.IsDoorOpen)> IsDoorOpen = 1
    <Description(SRAlarmCode.IsNotLoadRecipe)> IsNotLoadRecipe = 2
    <Description(SRAlarmCode.IsStop)> IsStop = 3
    <Description(SRAlarmCode.IsPowerOff)> IsPowerOff = 4
    <Description(SRAlarmCode.IsTimeOut)> IsTimeOut = 5
    <Description(SRAlarmCode.IsFailed)> IsFailed = 6
    <Description(SRAlarmCode.IsCanNotInitial)> IsCanNotInitial = 7
    <Description(SRAlarmCode.IsInitialFailed)> IsInitialFailed = 8
    <Description(SRAlarmCode.IsNotDefine)> IsNotDefine = 9
    <Description(SRAlarmCode.IsHardwareError)> IsHardwareError = 10
    <Description(SRAlarmCode.IsInspectError)> IsInspectError = 11
    <Description(SRAlarmCode.IsCanNotInspect)> IsCanNotInspect = 12
    <Description(SRAlarmCode.IsDataBaseReadFailed)> IsDataBaseReadFailed = 13
    <Description(SRAlarmCode.IsRecipeIDNotNumber)> IsRecipeIDNotNumber = 14
    <Description(SRAlarmCode.IsThreadCreateFailed)> IsThreadCreateFailed = 15
    <Description(SRAlarmCode.IsNotProductInformation)> IsNotProductInformation = 16
    <Description(SRAlarmCode.IsNotSafty)> IsNotSafty = 17
    <Description(SRAlarmCode.IsUpdateModelFailed)> IsUpdateModelFailed = 18
    <Description(SRAlarmCode.IsAllocateInspectFailed)> IsAllocateInspectFailed = 19

    <Description(SRAlarmCode.IsLightMoveUpTimeout)> IsLightMoveUpTimeout = 21
    <Description(SRAlarmCode.IsLightMoveDownTimeout)> IsLightMoveDownTimeout = 22
    <Description(SRAlarmCode.IsLightMoveUpFailed)> IsLightMoveUpFailed = 23
    <Description(SRAlarmCode.IsLightMoveDownFailed)> IsLightMoveDownFailed = 24

    <Description(SRAlarmCode.IsChangeExposureTimeFailed)> IsChangeExposureTimeFailed = 101
    <Description(SRAlarmCode.IsSnapFailed)> IsSnapFailed = 102
    <Description(SRAlarmCode.IsNotSafe)> IsNotSafe = 103
    <Description(SRAlarmCode.IsLocateFailed)> IsLocateFailed = 104
    <Description(SRAlarmCode.IsChangeModelFailed)> IsChangeModelFailed = 105
    <Description(SRAlarmCode.IsReadCodeFailed)> IsReadCodeFailed = 106
    <Description(SRAlarmCode.IsUpdateImageFailed)> IsUpdateImageFailed = 107
    <Description(SRAlarmCode.IsCodeReaderUpdateImageFailed)> IsCodeReaderUpdateImageFailed = 108
    <Description(SRAlarmCode.IsCodeReaderParameterFailed)> IsCodeReaderParameterFailed = 109

    <Description(SRAlarmCode.IsSetRecipeIDFailed)> IsSetRecipeIDFailed = 161

    <Description(SRAlarmCode.IsHandshakeReadFailed)> IsHandshakeReadFailed = 171
    <Description(SRAlarmCode.IsSendLotInfoACKFailed)> IsSendLotInfoACKFailed = 172
    <Description(SRAlarmCode.IsSendStripMapDownloadACKFailed)> IsSendStripMapDownloadACKFailed = 173
    <Description(SRAlarmCode.IsSendStripMapUploadFailed)> IsSendStripMapUploadFailed = 174
    <Description(SRAlarmCode.IsReadStripMapUploadACKFailed)> IsReadStripMapUploadACKFailed = 175
    <Description(SRAlarmCode.IsChangeRecipeFailed)> IsChangeRecipeFailed = 176
    <Description(SRAlarmCode.IsRecipeImageIsNothing)> IsRecipeImageIsNothing = 177
    <Description(SRAlarmCode.IsUpdateRecipeModelFailed)> IsUpdateRecipeModelFailed = 178
    <Description(SRAlarmCode.IsWaitHandshakeTimeout)> IsWaitHandshakeTimeout = 179
    <Description(SRAlarmCode.IsChangeCameraExposureTimeFailed)> IsChangeCameraExposureTimeFailed = 180
    <Description(SRAlarmCode.IsChangeCodeReaderCameraExposureTimeFailed)> IsChangeCodeReaderCameraExposureTimeFailed = 181
    <Description(SRAlarmCode.IsSendUpdateAIInfoFailed)> IsSendUpdateAIInfoFailed = 182

    <Description(SRAlarmCode.IsHandshakeNotConnected)> IsHandshakeNotConnected = 191
    <Description(SRAlarmCode.IsHandshakeFailed)> IsHandshakeFailed = 192
End Enum