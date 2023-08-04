<?xml version="1.0" encoding="big5" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt"
                xmlns:iTVision="http://iTVision.com.tw">
    <xsl:output method="html" />
    <xsl:param name="Camera_PixelSize_Width" select="1" />       <!-- Pixel �e�� = um -->
    <xsl:param name="Camera_PixelSize_Height" select="1" />      <!-- Pixel ���� = um -->
    <xsl:param name="DM_Image" />                                <!-- DM Image -->
    <xsl:param name="X_DirChange" select="True" />               <!-- X Dir -->
    <xsl:param name="Y_DirChange" select="True" />               <!-- Y Dir -->
    <xsl:param name="ImagePath" />                               <!-- ImagePath Dir -->

    <xsl:variable name="styletitle1" select="'text-align: center; width: 1320px; margin-left: 0px;'" />

    <xsl:variable name="stylecase1" select="'width:  50px; background-color: rgb(100, 255, 100);'" />
    <xsl:variable name="stylecase2" select="'width: 300px; background-color: rgb(100, 255, 100);'" />
    <xsl:variable name="stylecase3" select="'width: 100px; background-color: rgb(100, 255, 100);'" />

    <xsl:variable name="stylecase4" select="'width:  50px; background-color: rgb(100, 100, 100);'" />
    <xsl:variable name="stylecase5" select="'width: 300px; background-color: rgb(  0, 100, 100);'" />
    <xsl:variable name="stylecase6" select="'width: 100px; background-color: rgb(100, 100,   0);'" />

    <xsl:variable name="stylespan1" select="'color: rgb(255, 255, 255);'" />

    <xsl:template match="/">
        <html>
            <body>
                <meta content='text/html; charset=big5' http-equiv='content-type' />
                <title>Defect List</title>
            </body>

            <table><xsl:call-template name="styletitle1" />
                <tbody>
                    <tr>
                        <td style='width: 1307px; background-color: rgb(51, 255, 51); font-weight: bold;'>
                            <big>�岫�I��C��</big>
                        </td>
                    </tr>
                </tbody>
            </table>

            <table><xsl:call-template name="styletitle1" />
                <tbody>
                    <tr>
                        <td style='width: 1307px; background-color: rgb(51, 255, 51); font-weight: bold;'>
                            <big>DM</big>
                        </td>
                    </tr>
                </tbody>
            </table>

            <table><xsl:call-template name="styletitle2" />
                <tbody>
                    <tr></tr>
                    <td style='width: 1307px; background-color: rgb(100, 100, 100);'>
                        <xsl:if test="$DM_Image=''">
                           N/A
                        </xsl:if>
                        <xsl:if test="$DM_Image!=''">
                            <img style='width: 1000px; hieght: 330px;' alt='jpeg'>
                                <xsl:attribute name="src">
                                    <xsl:value-of select='$DM_Image' />
                                </xsl:attribute>
                            </img>
                        </xsl:if>
                    </td>
                </tbody>
            </table>

            <table>
                <xsl:call-template name="styletitle1" />
                <tbody>
                    <tr>
                        <td style='width: 1320px; background-color: rgb(51, 255, 51); font-weight: bold;'>
                            <big>�岫�I��[�p�ϦC<br />Substrate ID: <xsl:value-of select="DefectFile/@CodeID" /><br />�C���`��: <xsl:value-of select="count(DefectFile/DefectList/Defect)" /></big>
                        </td>
                    </tr>
                </tbody>
            </table>

            <table><xsl:call-template name="styletitle2" />
                <tbody>
                    <tr>
                        <td><xsl:call-template name="stylecase1" />�C���</td>
                        <td><xsl:call-template name="stylecase2" />�岫�I��p��</td>
                        <td><xsl:call-template name="stylecase3" />Mark �y��</td>
                        <td><xsl:call-template name="stylecase3" />�岫�y��(Pixel)</td>
                        <td><xsl:call-template name="stylecase3" />�岫�W��</td>
                        <td><xsl:call-template name="stylecase3" />�岫�����Ƕ�</td>
                        <td><xsl:call-template name="stylecase3" />�岫���n(Pixel)</td>
                        <!--<td><xsl:call-template name="stylecase3" />�岫����</td>
                        <td><xsl:call-template name="stylecase3" />�岫����(Gray)</td>
                        <td><xsl:call-template name="stylecase3" />�I������(Gray)</td>-->
                        <td><xsl:call-template name="stylecase3" />�岫�j�p(mm2)</td>
                        <td><xsl:call-template name="stylecase3" />�岫�e��(mm)</td>
                        <td><xsl:call-template name="stylecase3" />�岫����(mm)</td>
                    </tr>
                    <xsl:apply-templates select="DefectFile/DefectList/Defect" />
                </tbody>
            </table>
        </html>
    </xsl:template>

    <xsl:template match="DefectFile/DefectList/Defect">
        <tr>
          <td><xsl:call-template name="stylecase4" /><span><xsl:call-template name="stylespan1" /><xsl:value-of select ='position()' /></span></td>
            <xsl:apply-templates mode ="DefectIndex1" select="DefectImage" />
            <xsl:apply-templates mode ="DefectIndex2" select="DefectIndex" />
            <xsl:apply-templates mode ="DefectCenter1" select="DefectCenter" />
            <td><xsl:call-template name="stylecase6" /><span><xsl:call-template name="stylespan1" /><xsl:value-of select ='@DefectName' /></span></td>
            <td><xsl:call-template name="stylecase6" /><span><xsl:call-template name="stylespan1" /><xsl:value-of select ='@MeanGray' /></span></td>
            <td><xsl:call-template name="stylecase6" /><span><xsl:call-template name="stylespan1" /><xsl:value-of select ='@BodyArea' /></span></td>
            <!--<td><xsl:call-template name="stylecase6" /><span><xsl:call-template name="stylespan1" /><xsl:value-of select ='@DefectType' /></span></td>
            <td><xsl:call-template name="stylecase6" /><span><xsl:call-template name="stylespan1" /><xsl:value-of select ='format-number(@MeanGray, "0")' /></span></td>
            <td><xsl:call-template name="stylecase6" /><span><xsl:call-template name="stylespan1" /><xsl:value-of select ='format-number(@BaseGray, "0")' /></span></td>-->
            <td><xsl:call-template name="stylecase6" /><span><xsl:call-template name="stylespan1" /><xsl:value-of select ='@DefectArea' /></span></td>
            <xsl:apply-templates select="DefectBoundary" />
        </tr>
    </xsl:template>

    <xsl:template mode='DefectIndex1' match="DefectImage">
        <xsl:param name="sFileName" select='@FileName' />
        <xsl:param name="sFullName" select="concat($ImagePath,'\',$sFileName)" />

        <td><xsl:call-template name="stylecase5" />
            <xsl:if test="iTVision:FileNameIsExist($sFullName)=''">
                <span>
                    <xsl:call-template name="stylespan1" />N/A
                </span>
            </xsl:if>
            <xsl:if test="iTVision:FileNameIsExist($sFullName)!=''">
                <img style='width: 200px; hieght: 200px;' alt='image'>
                    <xsl:attribute name="src">
                        <xsl:value-of select='$sFileName' />
                    </xsl:attribute>
                </img>
            </xsl:if>
        </td>
    </xsl:template>

    <xsl:template mode='DefectIndex2' match="DefectIndex">
        <td><xsl:call-template name="stylecase6" /><span><xsl:call-template name="stylespan1" />X=<xsl:value-of select ='format-number(@X , "0")' /> , Y=<xsl:value-of select ='format-number(@Y, "0")' /></span></td>
    </xsl:template>

    <xsl:template mode='DefectCenter1' match="DefectCenter">
        <td><xsl:call-template name="stylecase6" /><span><xsl:call-template name="stylespan1" />X=<xsl:value-of select ='format-number(@X , "0")' /> , Y=<xsl:value-of select ='format-number(@Y, "0")' /></span></td>
    </xsl:template>

    <xsl:template match="DefectBoundary">
        <td><xsl:call-template name="stylecase6" /><span><xsl:call-template name="stylespan1" /><xsl:value-of select ='format-number(@Width  * $Camera_PixelSize_Width * 0.001 , "0.00")' /></span></td>
        <td><xsl:call-template name="stylecase6" /><span><xsl:call-template name="stylespan1" /><xsl:value-of select ='format-number(@Height * $Camera_PixelSize_Height * 0.001 , "0.00")' /></span></td>
    </xsl:template>

    <xsl:template mode='DefectCenter2' match="DefectCenter">
        <td><xsl:call-template name="stylecase6" /><span><xsl:call-template name="stylespan1" />X=<xsl:value-of select ='format-number(@X , "0")' /> , Y=<xsl:value-of select ='format-number(@Y , "0")' /></span></td>
    </xsl:template>

    <xsl:template name ="styletitle1">
        <xsl:attribute name="style">
            <xsl:value-of select='$styletitle1' />
        </xsl:attribute>
        <xsl:attribute name="border">3</xsl:attribute>
        <xsl:attribute name="cellpadding">2</xsl:attribute>
        <xsl:attribute name="cellspacing">2</xsl:attribute>
    </xsl:template>

    <xsl:template name ="styletitle2">
        <xsl:attribute name="style">
            <xsl:value-of select='$styletitle1' />
        </xsl:attribute>
        <xsl:attribute name="border">2</xsl:attribute>
        <xsl:attribute name="cellpadding">1</xsl:attribute>
        <xsl:attribute name="cellspacing">1</xsl:attribute>
    </xsl:template>

    <xsl:template name ="stylecase1">
        <xsl:attribute name="style">
            <xsl:value-of select='$stylecase1' />
        </xsl:attribute>
    </xsl:template>

    <xsl:template name ="stylecase2">
        <xsl:attribute name="style">
            <xsl:value-of select='$stylecase2' />
        </xsl:attribute>
    </xsl:template>

    <xsl:template name ="stylecase3">
        <xsl:attribute name="style">
            <xsl:value-of select='$stylecase3' />
        </xsl:attribute>
    </xsl:template>

    <xsl:template name ="stylecase4">
        <xsl:attribute name="style">
            <xsl:value-of select='$stylecase4' />
        </xsl:attribute>
    </xsl:template>

    <xsl:template name ="stylecase5">
        <xsl:attribute name="style">
            <xsl:value-of select='$stylecase5' />
        </xsl:attribute>
    </xsl:template>

    <xsl:template name ="stylecase6">
        <xsl:attribute name="style">
            <xsl:value-of select='$stylecase6' />
        </xsl:attribute>
    </xsl:template>

    <xsl:template name ="stylespan1">
        <xsl:attribute name="style">
            <xsl:value-of select='$stylespan1' />
        </xsl:attribute>
    </xsl:template>

    <msxsl:script language="VBScript" implements-prefix="iTVision">
        Function FileNameIsExist(sFileName)
            Return Dir(sFileName)
        End Function
    </msxsl:script>
</xsl:stylesheet>