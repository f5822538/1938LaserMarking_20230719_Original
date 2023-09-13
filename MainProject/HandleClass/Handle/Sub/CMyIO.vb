Public Class CMyIO

    Public Class SR
        Public Const LightVacuum1UpSensor As String = "燈源汽缸1上位檢知"
        Public Const LightVacuum1DownSensor As String = "燈源汽缸1下位檢知"
        Public Const LightVacuum2UpSensor As String = "燈源汽缸2上位檢知"
        Public Const LightVacuum2DownSensor As String = "燈源汽缸2下位檢知"
        Public Const ProductPresentSensor As String = "產品在席檢知"
        Public Const SafeSensor1 As String = "安全檢知 1"
        Public Const SafeSensor2 As String = "安全檢知 2"
        Public Const HomeSensor As String = "原點檢知"

        Public Const LightVacuumUp1 As String = "燈源汽缸1上"
        Public Const LightOn As String = "燈源開關"
        Public Const StartLight As String = "啟動燈號"
        Public Const AlarmLight As String = "警報燈號"
        Public Const LightVacuumUp2 As String = "燈源汽缸2上"
        Public Const [Error] As String = "緊急停止"
        Public Const Buzzer As String = "蜂鳴器"
    End Class

    <DisplayName(SR.LightVacuum1UpSensor)> Public Property LightVacuum1UpSensor As II_Input
    <DisplayName(SR.LightVacuum1DownSensor)> Public Property LightVacuum1DownSensor As II_Input
    <DisplayName(SR.LightVacuum2UpSensor)> Public Property LightVacuum2UpSensor As II_Input
    <DisplayName(SR.LightVacuum2DownSensor)> Public Property LightVacuum2DownSensor As II_Input
    <DisplayName(SR.ProductPresentSensor)> Public Property ProductPresentSensor As II_Input
    <DisplayName(SR.SafeSensor1)> Public Property SafeSensor1 As II_Input
    <DisplayName(SR.SafeSensor2)> Public Property SafeSensor2 As II_Input
    <DisplayName(SR.HomeSensor)> Public Property HomeSensor As II_Input

    <DisplayName(SR.LightVacuumUp1)> Public Property LightVacuumUp1 As II_OutPut
    <DisplayName(SR.LightOn)> Public Property LightOn As II_OutPut
    <DisplayName(SR.StartLight)> Public Property StartLight As II_OutPut
    <DisplayName(SR.AlarmLight)> Public Property AlarmLight As II_OutPut
    <DisplayName(SR.LightVacuumUp2)> Public Property LightVacuumUp2 As II_OutPut
    <DisplayName(SR.[Error])> Public Property [Error] As II_OutPut
    <DisplayName(SR.Buzzer)> Public Property Buzzer As II_OutPut

    ''' <summary>
    ''' LightVacuum1UpSensor.IsOn() = True AndAlso LightVacuum2UpSensor.IsOn() = True
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsUp() As Boolean
        '-------------------------20230913-開始--------------------------
        If LightVacuum1UpSensor IsNot Nothing AndAlso LightVacuum2UpSensor IsNot Nothing AndAlso LightVacuum1DownSensor IsNot Nothing AndAlso LightVacuum2DownSensor IsNot Nothing Then
            Return LightVacuum1UpSensor.IsOn() = True AndAlso LightVacuum2UpSensor.IsOn() = True AndAlso LightVacuum1DownSensor.IsOn() = False AndAlso LightVacuum2DownSensor.IsOn() = False
        Else
            Return False
        End If
        '-------------------------20230913-結束--------------------------
    End Function

    ''' <summary>
    ''' LightVacuum1DownSensor.IsOn() = True AndAlso LightVacuum2DownSensor.IsOn() = True
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsDown() As Boolean
        '-------------------------20230913-開始--------------------------
        If LightVacuum1UpSensor IsNot Nothing AndAlso LightVacuum2UpSensor IsNot Nothing AndAlso LightVacuum1DownSensor IsNot Nothing AndAlso LightVacuum2DownSensor IsNot Nothing Then
            Return LightVacuum1UpSensor.IsOn() = False AndAlso LightVacuum2UpSensor.IsOn() = False AndAlso LightVacuum1DownSensor.IsOn() = True AndAlso LightVacuum2DownSensor.IsOn() = True
        Else
            Return False
        End If
        '-------------------------20230913-結束--------------------------
    End Function

End Class