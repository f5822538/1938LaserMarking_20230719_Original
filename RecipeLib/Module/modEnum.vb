Public Enum InspectStatu
    StopRun = 0
    SingleRun = 1
    ContinueRun = 2
    TestRun = 3
End Enum

<TypeConverter(GetType(CEnumConvecter))>
Public Enum MeasurePositive As Integer
    <Description("任意")> Any = 0
    <Description("黑至白")> BlackToWhite = 1
    <Description("白至黑")> WhiteToBlack = 2
End Enum

<TypeConverter(GetType(CEnumConvecter))>
Public Enum PatternMatchingType As Integer
    <Description("第一次")> PatternMatching1St = 1
    <Description("第二次")> PatternMatching2Nd = 2
End Enum

<TypeConverter(GetType(CEnumConvecter))>
Public Enum CodeReaderForeground As Integer
    <Description("白")> White = 128
    <Description("黑")> Black = 256
End Enum

Public Structure MeasureInfo
    Public IsUse As Boolean
    Public ROI As Rectangle
    Public Direction As Direction
    Public MeasurePositive As MeasurePositive
    Public ResultX As Double
    Public ResultY As Double
    Public Succeed As Boolean
End Structure

Public Structure ModelFinderShift
    Public refX As Double
    Public refY As Double
    Public shiftX As Double
    Public shiftY As Double
    Public Angle As Double
End Structure

<TypeConverter(GetType(CEnumConvecter))>
Public Enum UpLoadInspectImage As Integer
    <Description("ON")> OPEN = 1
    <Description("OFF")> CLOSE = 2
End Enum

<TypeConverter(GetType(CEnumConvecter))>
Public Enum UpLoadMarkShiftImage As Integer
    <Description("ON")> OPEN = 1
    <Description("OFF")> CLOSE = 2
End Enum