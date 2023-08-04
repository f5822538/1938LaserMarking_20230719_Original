Public Enum InspectStatu
    StopRun = 0
    SingleRun = 1
    ContinueRun = 2
    TestRun = 3
End Enum

Public Structure MeasureInfo
    Public IsUse As Boolean
    Public ROI As Rectangle
    Public Direction As iTVisionService.Direction
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