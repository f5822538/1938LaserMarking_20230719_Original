<TypeConverter(GetType(CEnumConvecter))>
Public Enum CameraType As Integer
    <Description("虛擬相機取像")> NullCamera = 0
    <Description("取像卡取像")> CaptureCamera = 1
End Enum

<TypeConverter(GetType(CEnumConvecter))>
Public Enum DefectSizeType As Integer
    <Description("長乘寬")> DefectAnd = 0
    <Description("對角線")> DefectOr = 1
End Enum