Public Class BuildPad

    'String
    Property sShapeName As String
    Property GrowOrShrinkUnit As String

    'Mentor Graphics
    Property oPad As PadstackEditorLib.Pad
    Property oPadStack As PadstackEditorLib.Padstack
    Property oPadStackDB As PadstackEditorLib.PadstackDB

    'Double/Integer
    Property iGrowOrShrinkNum As Double

    'Boolean
    Property bGrow As Boolean
    Property NewPad As Boolean = False

    'Dictionary
    Property dicPads As New Dictionary(Of String, String)

    Function PadHeight() As PadstackEditorLib.Pad
        Dim oGrowDim As Object
        Dim oPadUnit As PadstackEditorLib.EPsDBUnit = oPad.Units

        oGrowDim = funcCalMaskPadGrowth()

        Dim alPadInfo As New ArrayList()
        Dim sPadHeightIN, sPadHeightTH, sPadHeightUM, sPadHeightMM As String

        Dim iPadHeight As Double = oPad.Height(PadstackEditorLib.EPsDBUnit.epsdbUnitCurrentObject)
        Dim iPadWidth As Double = oPad.Width(PadstackEditorLib.EPsDBUnit.epsdbUnitCurrentObject)

        Select Case oPadUnit

            Case PadstackEditorLib.EPsDBUnit.epsdbUnitInch

                If (bGrow = True) Then

                    sPadHeightIN = Math.Round(iPadHeight + oGrowDim.IN, 4)
                    sPadHeightTH = Math.Round((iPadHeight * 1000) + oGrowDim.TH, 4)
                    sPadHeightUM = Math.Round((iPadHeight * 25400) + oGrowDim.UM, 4)
                    sPadHeightMM = Math.Round((iPadHeight * 25.4) + oGrowDim.MM, 4)

                Else

                    sPadHeightIN = Math.Round(iPadHeight - oGrowDim.IN, 4)
                    sPadHeightTH = Math.Round((iPadHeight * 1000) - oGrowDim.TH, 4)
                    sPadHeightUM = Math.Round((iPadHeight * 25400) - oGrowDim.UM, 4)
                    sPadHeightMM = Math.Round((iPadHeight * 25.4) - oGrowDim.MM, 4)

                End If

            Case PadstackEditorLib.EPsDBUnit.epsdbUnitMils

                If (bGrow = True) Then

                    sPadHeightIN = Math.Round((iPadHeight / 1000) + oGrowDim.IN, 4)
                    sPadHeightTH = Math.Round(iPadHeight + oGrowDim.TH, 4)
                    sPadHeightUM = Math.Round((iPadHeight * 25.4) + oGrowDim.UM, 4)
                    sPadHeightMM = Math.Round((iPadHeight * 0.0254) + oGrowDim.MM, 4)

                Else

                    sPadHeightIN = Math.Round((iPadHeight / 1000) - oGrowDim.IN, 4)
                    sPadHeightTH = Math.Round(iPadHeight - oGrowDim.TH, 4)
                    sPadHeightUM = Math.Round((iPadHeight * 25.4) - oGrowDim.UM, 4)
                    sPadHeightMM = Math.Round((iPadHeight * 0.0254) - oGrowDim.MM, 4)

                End If

            Case PadstackEditorLib.EPsDBUnit.epsdbUnitMM

                If (bGrow = True) Then

                    sPadHeightIN = Math.Round((iPadHeight * 0.03937) + oGrowDim.IN, 4)
                    sPadHeightTH = Math.Round((iPadHeight * 39.37) + oGrowDim.TH, 4)
                    sPadHeightUM = Math.Round((iPadHeight / 1000) + oGrowDim.UM, 4)
                    sPadHeightMM = Math.Round(iPadHeight + oGrowDim.MM, 4)

                Else

                    sPadHeightIN = Math.Round((iPadHeight * 0.03937) - oGrowDim.IN, 4)
                    sPadHeightTH = Math.Round((iPadHeight * 39.37) - oGrowDim.TH, 4)
                    sPadHeightUM = Math.Round((iPadHeight / 1000) - oGrowDim.UM, 4)
                    sPadHeightMM = Math.Round(iPadHeight - oGrowDim.MM, 4)

                End If

            Case PadstackEditorLib.EPsDBUnit.epsdbUnitUM

                If (bGrow = True) Then

                    sPadHeightIN = Math.Round((iPadHeight * 0.00003937) + oGrowDim.IN, 4)
                    sPadHeightTH = Math.Round((iPadHeight * 0.03937) + oGrowDim.TH, 4)
                    sPadHeightUM = Math.Round(iPadHeight + oGrowDim.UM, 4)
                    sPadHeightMM = Math.Round((iPadHeight * 1000) + oGrowDim.MM, 4)

                Else

                    sPadHeightIN = Math.Round((iPadHeight * 0.00003937) - oGrowDim.IN, 4)
                    sPadHeightTH = Math.Round((iPadHeight * 0.03937) - oGrowDim.TH, 4)
                    sPadHeightUM = Math.Round(iPadHeight - oGrowDim.UM, 4)
                    sPadHeightMM = Math.Round((iPadHeight * 1000) - oGrowDim.MM, 4)

                End If

        End Select

        alPadInfo.Add(sPadHeightIN)
        alPadInfo.Add(sPadHeightTH)
        alPadInfo.Add(sPadHeightUM)
        alPadInfo.Add(sPadHeightMM)

        Dim PadDim As String
        Dim sPadName As String
        Dim bFoundPad As Boolean = False

        For Each PadDim In alPadInfo

            If dicPads.ContainsKey(sShapeName & " " & PadDim & " " & oPad.OriginOffsetX & " " & oPad.OriginOffsetY) Then

                sPadName = dicPads.Item(sShapeName & " " & PadDim & " " & oPad.OriginOffsetX & " " & oPad.OriginOffsetY)

                Return oPadStackDB.FindPad(sPadName)

            End If

        Next

        NewPad = True

        Dim oNewPad As PadstackEditorLib.Pad = oPadStackDB.NewPad

        Select Case oPad.Shape

            Case PadstackEditorLib.EPsDBPadShape.epsdbPadShapeRound

                oNewPad.Shape = PadstackEditorLib.EPsDBPadShape.epsdbPadShapeRound

            Case PadstackEditorLib.EPsDBPadShape.epsdbPadShapeOctagon

                oNewPad.Shape = PadstackEditorLib.EPsDBPadShape.epsdbPadShapeOctagon

            Case PadstackEditorLib.EPsDBPadShape.epsdbPadShapeSquare

                oNewPad.Shape = PadstackEditorLib.EPsDBPadShape.epsdbPadShapeSquare

        End Select

        Dim genericName As String
        oNewPad.OriginOffsetX = oPad.OriginOffsetX
        oNewPad.OriginOffsetY = oPad.OriginOffsetY

        Select Case oPad.Units

            Case PadstackEditorLib.EPsDBUnit.epsdbUnitInch

                oNewPad.Units = PadstackEditorLib.EPsDBUnit.epsdbUnitInch
                If oNewPad.Shape = PadstackEditorLib.EPsDBPadShape.epsdbPadShapeRound Then
                    oNewPad.Height(PadstackEditorLib.EPsDBUnit.epsdbUnitCurrentObject) = sPadHeightIN
                Else
                    oNewPad.Width(PadstackEditorLib.EPsDBUnit.epsdbUnitCurrentObject) = sPadHeightIN
                End If

                genericName = sShapeName & " " & sPadHeightIN & " " & oPad.OriginOffsetX & " " & oPad.OriginOffsetY

            Case PadstackEditorLib.EPsDBUnit.epsdbUnitMils

                oNewPad.Units = PadstackEditorLib.EPsDBUnit.epsdbUnitMils
                If oNewPad.Shape = PadstackEditorLib.EPsDBPadShape.epsdbPadShapeRound Then
                    oNewPad.Height(PadstackEditorLib.EPsDBUnit.epsdbUnitCurrentObject) = sPadHeightTH
                Else
                    oNewPad.Width(PadstackEditorLib.EPsDBUnit.epsdbUnitCurrentObject) = sPadHeightTH
                End If

                genericName = sShapeName & " " & sPadHeightTH & " " & oPad.OriginOffsetX & " " & oPad.OriginOffsetY

            Case PadstackEditorLib.EPsDBUnit.epsdbUnitMM

                oNewPad.Units = PadstackEditorLib.EPsDBUnit.epsdbUnitMM
                If oNewPad.Shape = PadstackEditorLib.EPsDBPadShape.epsdbPadShapeRound Then
                    oNewPad.Height(PadstackEditorLib.EPsDBUnit.epsdbUnitCurrentObject) = sPadHeightMM
                Else
                    oNewPad.Width(PadstackEditorLib.EPsDBUnit.epsdbUnitCurrentObject) = sPadHeightMM
                End If

                genericName = sShapeName & " " & sPadHeightMM & " " & oPad.OriginOffsetX & " " & oPad.OriginOffsetY

            Case PadstackEditorLib.EPsDBUnit.epsdbUnitUM

                oNewPad.Units = PadstackEditorLib.EPsDBUnit.epsdbUnitUM
                If oNewPad.Shape = PadstackEditorLib.EPsDBPadShape.epsdbPadShapeRound Then
                    oNewPad.Height(PadstackEditorLib.EPsDBUnit.epsdbUnitCurrentObject) = sPadHeightUM
                Else
                    oNewPad.Width(PadstackEditorLib.EPsDBUnit.epsdbUnitCurrentObject) = sPadHeightUM
                End If

                genericName = sShapeName & " " & sPadHeightUM & " " & oPad.OriginOffsetX & " " & oPad.OriginOffsetY

        End Select

        oNewPad.Commit()
        oNewPad.AutoName = False
        Dim beforepadName As String = oNewPad.Name
        oNewPad.AutoName = True

        Dim padName As String = oNewPad.Name
        dicPads.Add(genericName, oNewPad.Name)

        Return oNewPad

    End Function

    Private Function funcCalMaskPadGrowth() As Object

        Dim iGrowIN As Double
        Dim iGrowTH As Double
        Dim iGrowUM As Double
        Dim iGrowMM As Double

        Select Case GrowOrShrinkUnit

            Case "inch"

                iGrowIN = iGrowOrShrinkNum
                iGrowTH = iGrowOrShrinkNum * 1000
                iGrowUM = iGrowOrShrinkNum * 25400
                iGrowMM = iGrowOrShrinkNum * 25.4

            Case "th"

                iGrowIN = iGrowOrShrinkNum / 1000
                iGrowTH = iGrowOrShrinkNum
                iGrowUM = iGrowOrShrinkNum * 25.4
                iGrowMM = iGrowOrShrinkNum * 0.0254

            Case "mm"

                iGrowIN = iGrowOrShrinkNum * 0.03937
                iGrowTH = iGrowOrShrinkNum * 39.37
                iGrowUM = iGrowOrShrinkNum * 1000
                iGrowMM = iGrowOrShrinkNum

            Case "um"

                iGrowIN = iGrowOrShrinkNum * 0.00003937
                iGrowTH = iGrowOrShrinkNum * 0.03937
                iGrowUM = iGrowOrShrinkNum
                iGrowMM = iGrowOrShrinkNum / 1000

        End Select

        Dim oGrowDim As Object = New With {.IN = iGrowIN, .TH = iGrowTH, .UM = iGrowUM, .MM = iGrowMM}

        Return oGrowDim

    End Function

    Function PadWidthHeight()
        Dim oGrowDim As Object
        Dim oPadUnit As PadstackEditorLib.EPsDBUnit = oPad.Units

        'oGrowDim = New Object() {iGrowIN, iGrowTH, iGrowUM, iGrowMM}

        oGrowDim = funcCalMaskPadGrowth()

        Dim alPadInfo As New ArrayList()

        Dim sPadHeightIN, sPadHeightTH, sPadHeightUM, sPadHeightMM As String
        Dim sPadWidthIN, sPadWidthTH, sPadWidthUM, sPadWidthMM As String

        Dim iPadHeight As Double = oPad.Height(PadstackEditorLib.EPsDBUnit.epsdbUnitCurrentObject)
        Dim iPadWidth As Double = oPad.Width(PadstackEditorLib.EPsDBUnit.epsdbUnitCurrentObject)

        Select Case oPadUnit

            Case PadstackEditorLib.EPsDBUnit.epsdbUnitInch

                If (bGrow = True) Then

                    sPadHeightIN = Math.Round(iPadHeight + oGrowDim.IN, 4)
                    sPadHeightTH = Math.Round((iPadHeight * 1000) + oGrowDim.TH, 4)
                    sPadHeightUM = Math.Round((iPadHeight * 25400) + oGrowDim.UM, 4)
                    sPadHeightMM = Math.Round((iPadHeight * 25.4) + oGrowDim.MM, 4)

                    sPadWidthIN = Math.Round(iPadWidth + oGrowDim.IN, 4)
                    sPadWidthTH = Math.Round((iPadWidth * 1000) + oGrowDim.TH, 4)
                    sPadWidthUM = Math.Round((iPadWidth * 25400) + oGrowDim.UM, 4)
                    sPadWidthMM = Math.Round((iPadWidth * 25.4) + oGrowDim.MM, 4)

                Else

                    sPadHeightIN = Math.Round(iPadHeight - oGrowDim.IN, 4)
                    sPadHeightTH = Math.Round((iPadHeight * 1000) - oGrowDim.TH, 4)
                    sPadHeightUM = Math.Round((iPadHeight * 25400) - oGrowDim.UM, 4)
                    sPadHeightMM = Math.Round((iPadHeight * 25.4) - oGrowDim.MM, 4)

                    sPadWidthIN = Math.Round(iPadWidth - oGrowDim.IN, 4)
                    sPadWidthTH = Math.Round((iPadWidth * 1000) - oGrowDim.TH, 4)
                    sPadWidthUM = Math.Round((iPadWidth * 25400) - oGrowDim.UM, 4)
                    sPadWidthMM = Math.Round((iPadWidth * 25.4) - oGrowDim.MM, 4)

                End If

            Case PadstackEditorLib.EPsDBUnit.epsdbUnitMils

                If (bGrow = True) Then

                    sPadHeightIN = Math.Round((iPadHeight / 1000) + oGrowDim.IN, 4)
                    sPadHeightTH = Math.Round(iPadHeight + oGrowDim.TH, 4)
                    sPadHeightUM = Math.Round((iPadHeight * 25.4) + oGrowDim.UM, 4)
                    sPadHeightMM = Math.Round((iPadHeight * 0.0254) + oGrowDim.MM, 4)

                    sPadWidthIN = Math.Round((iPadWidth / 1000) + oGrowDim.IN, 4)
                    sPadWidthTH = Math.Round(iPadWidth + oGrowDim.TH, 4)
                    sPadWidthUM = Math.Round((iPadWidth * 25.4) + oGrowDim.UM, 4)
                    sPadWidthMM = Math.Round((iPadWidth * 0.0254) + oGrowDim.MM, 4)

                Else

                    sPadHeightIN = Math.Round((iPadHeight / 1000) - oGrowDim.IN, 4)
                    sPadHeightTH = Math.Round(iPadHeight - oGrowDim.TH, 4)
                    sPadHeightUM = Math.Round((iPadHeight * 25.4) - oGrowDim.UM, 4)
                    sPadHeightMM = Math.Round((iPadHeight * 0.0254) - oGrowDim.MM, 4)

                    sPadWidthIN = Math.Round((iPadWidth / 1000) - oGrowDim.IN, 4)
                    sPadWidthTH = Math.Round(iPadWidth - oGrowDim.TH, 4)
                    sPadWidthUM = Math.Round((iPadWidth * 25.4) - oGrowDim.UM, 4)
                    sPadWidthMM = Math.Round((iPadWidth * 0.0254) - oGrowDim.MM, 4)

                End If

            Case PadstackEditorLib.EPsDBUnit.epsdbUnitMM

                If (bGrow = True) Then

                    sPadHeightIN = Math.Round((iPadHeight * 0.03937) + oGrowDim.IN, 4)
                    sPadHeightTH = Math.Round((iPadHeight * 39.37) + oGrowDim.TH, 4)
                    sPadHeightUM = Math.Round((iPadHeight * 1000) + oGrowDim.UM, 4)
                    sPadHeightMM = Math.Round(iPadHeight + oGrowDim.MM, 4)

                    sPadWidthIN = Math.Round((iPadWidth * 0.03937) + oGrowDim.IN, 4)
                    sPadWidthTH = Math.Round((iPadWidth * 39.37) + oGrowDim.TH, 4)
                    sPadWidthUM = Math.Round((iPadWidth * 1000) + oGrowDim.UM, 4)
                    sPadWidthMM = Math.Round(iPadWidth + oGrowDim.MM, 4)

                Else

                    sPadHeightIN = Math.Round((iPadHeight * 0.03937) - oGrowDim.IN, 4)
                    sPadHeightTH = Math.Round((iPadHeight * 39.37) - oGrowDim.TH, 4)
                    sPadHeightUM = Math.Round((iPadHeight * 1000) - oGrowDim.UM, 4)
                    sPadHeightMM = Math.Round(iPadHeight - oGrowDim.MM, 4)

                    sPadWidthIN = Math.Round((iPadWidth * 0.03937) - oGrowDim.IN, 4)
                    sPadWidthTH = Math.Round((iPadWidth * 39.37) - oGrowDim.TH, 4)
                    sPadWidthUM = Math.Round((iPadWidth * 1000) - oGrowDim.UM, 4)
                    sPadWidthMM = Math.Round(iPadWidth - oGrowDim.MM, 4)

                End If

            Case PadstackEditorLib.EPsDBUnit.epsdbUnitUM

                If (bGrow = True) Then

                    sPadHeightIN = Math.Round((iPadHeight * 0.00003937) + oGrowDim.IN, 4)
                    sPadHeightTH = Math.Round((iPadHeight * 0.03937) + oGrowDim.TH, 4)
                    sPadHeightUM = Math.Round(iPadHeight + oGrowDim.UM, 4)
                    sPadHeightMM = Math.Round((iPadHeight * 1000) + oGrowDim.MM, 4)

                    sPadWidthIN = Math.Round((iPadWidth * 0.00003937) + oGrowDim.IN, 4)
                    sPadWidthTH = Math.Round((iPadWidth * 0.03937) + oGrowDim.TH, 4)
                    sPadWidthUM = Math.Round(iPadWidth + oGrowDim.UM, 4)
                    sPadWidthMM = Math.Round((iPadWidth / 1000) + oGrowDim.MM, 4)

                Else

                    sPadHeightIN = Math.Round((iPadHeight * 0.00003937) - oGrowDim.IN, 4)
                    sPadHeightTH = Math.Round((iPadHeight * 0.03937) - oGrowDim.TH, 4)
                    sPadHeightUM = Math.Round(iPadHeight - oGrowDim.UM, 4)
                    sPadHeightMM = Math.Round((iPadHeight * 1000) - oGrowDim.MM, 4)

                    sPadWidthIN = Math.Round((iPadWidth * 0.00003937) - oGrowDim.IN, 4)
                    sPadWidthTH = Math.Round((iPadWidth * 0.03937) - oGrowDim.TH, 4)
                    sPadWidthUM = Math.Round(iPadWidth - oGrowDim.UM, 4)
                    sPadWidthMM = Math.Round((iPadWidth / 1000) - oGrowDim.MM, 4)

                End If

        End Select

        alPadInfo.Add(sPadHeightIN & " " & sPadWidthIN)
        alPadInfo.Add(sPadHeightTH & " " & sPadWidthTH)
        alPadInfo.Add(sPadHeightUM & " " & sPadWidthUM)
        alPadInfo.Add(sPadHeightMM & " " & sPadWidthMM)

        Dim bFoundPad As Boolean = False

        For Each PadDim As String In alPadInfo

            Dim name As String = sShapeName & " " & PadDim & " " & oPad.OriginOffsetX & " " & oPad.OriginOffsetY

            If dicPads.ContainsKey(name) Then

                Return oPadStackDB.FindPad(dicPads.Item(sShapeName & " " & PadDim & " " & oPad.OriginOffsetX & " " & oPad.OriginOffsetY))

            End If

        Next

        NewPad = True

        Dim oNewPad As PadstackEditorLib.Pad = oPadStackDB.NewPad

        Select Case oPad.Shape

            Case PadstackEditorLib.EPsDBPadShape.epsdbPadShapeOblong

                oNewPad.Shape = PadstackEditorLib.EPsDBPadShape.epsdbPadShapeOblong

            Case PadstackEditorLib.EPsDBPadShape.epsdbPadShapeRectangle

                oNewPad.Shape = PadstackEditorLib.EPsDBPadShape.epsdbPadShapeRectangle

            Case PadstackEditorLib.EPsDBPadShape.epsdbPadShapeOctagonalFinger

                oNewPad.Shape = PadstackEditorLib.EPsDBPadShape.epsdbPadShapeOctagonalFinger

        End Select

        oNewPad.OriginOffsetX = oPad.OriginOffsetX
        oNewPad.OriginOffsetY = oPad.OriginOffsetY

        Dim genericName As String

        Select Case oPad.Units

            Case PadstackEditorLib.EPsDBUnit.epsdbUnitInch

                oNewPad.Units = PadstackEditorLib.EPsDBUnit.epsdbUnitInch
                oNewPad.Height = sPadHeightIN
                oNewPad.Width = sPadWidthIN
                genericName = sShapeName & " " & sPadHeightIN & " " & sPadWidthIN & " " & oPad.OriginOffsetX & " " & oPad.OriginOffsetY

            Case PadstackEditorLib.EPsDBUnit.epsdbUnitMils

                oNewPad.Units = PadstackEditorLib.EPsDBUnit.epsdbUnitMils
                oNewPad.Height = sPadHeightTH
                oNewPad.Width = sPadWidthTH
                oNewPad.AutoName = True
                genericName = sShapeName & " " & sPadHeightTH & " " & sPadWidthTH & " " & oPad.OriginOffsetX & " " & oPad.OriginOffsetY

            Case PadstackEditorLib.EPsDBUnit.epsdbUnitMM

                oNewPad.Units = PadstackEditorLib.EPsDBUnit.epsdbUnitMM
                oNewPad.Height = sPadHeightMM
                oNewPad.Width = sPadWidthMM
                oNewPad.AutoName = True
                genericName = sShapeName & " " & sPadHeightMM & " " & sPadWidthMM & " " & oPad.OriginOffsetX & " " & oPad.OriginOffsetY

            Case PadstackEditorLib.EPsDBUnit.epsdbUnitUM

                oNewPad.Units = PadstackEditorLib.EPsDBUnit.epsdbUnitUM
                oNewPad.Height = sPadHeightUM
                oNewPad.Width = sPadWidthUM
                genericName = sShapeName & " " & sPadHeightUM & " " & sPadWidthUM & " " & oPad.OriginOffsetX & " " & oPad.OriginOffsetY

        End Select

        oNewPad.Commit()
        oNewPad.AutoName = False
        Dim beforepadName As String = oNewPad.Name
        oNewPad.AutoName = True

        Dim padName As String = oNewPad.Name

        dicPads.Add(genericName, oNewPad.Name)

        Return oNewPad

    End Function

    Function PadWidthHeightCorner()

        Dim oGrowDim As Object
        Dim oPadUnit As PadstackEditorLib.EPsDBUnit = oPad.Units

        'oGrowDim = New Object() {iGrowIN, iGrowTH, iGrowUM, iGrowMM}
        oGrowDim = funcCalMaskPadGrowth()

        Dim alPadInfo As New ArrayList()

        Dim sPadHeightIN, sPadHeightTH, sPadHeightUM, sPadHeightMM As String
        Dim sPadWidthIN, sPadWidthTH, sPadWidthUM, sPadWidthMM As String
        Dim sPadCornerIN, sPadCornerTH, sPadCornerUM, sPadCornerMM As String

        Dim iPadHeight As Double = oPad.Height(PadstackEditorLib.EPsDBUnit.epsdbUnitCurrentObject)
        Dim iPadWidth As Double = oPad.Width(PadstackEditorLib.EPsDBUnit.epsdbUnitCurrentObject)

        Select Case oPadUnit

            Case PadstackEditorLib.EPsDBUnit.epsdbUnitInch

                If (bGrow = True) Then

                    sPadHeightIN = Math.Round(iPadHeight + oGrowDim.IN, 4)
                    sPadHeightTH = Math.Round((iPadHeight * 1000) + oGrowDim.TH, 4)
                    sPadHeightUM = Math.Round((iPadHeight * 25400) + oGrowDim.UM, 4)
                    sPadHeightMM = Math.Round((iPadHeight * 25.4) + oGrowDim.MM, 4)

                    sPadWidthIN = Math.Round(iPadWidth + oGrowDim.IN, 4)
                    sPadWidthTH = Math.Round((iPadWidth * 1000) + oGrowDim.TH, 4)
                    sPadWidthUM = Math.Round((iPadWidth * 25400) + oGrowDim.UM, 4)
                    sPadWidthMM = Math.Round((iPadWidth * 25.4) + oGrowDim.MM, 4)

                    sPadCornerIN = Math.Round(oPad.CornerHeight + oGrowDim.IN, 4)
                    sPadCornerTH = Math.Round((oPad.CornerHeight * 1000) + oGrowDim.TH, 4)
                    sPadCornerUM = Math.Round((oPad.CornerHeight * 25400) + oGrowDim.UM, 4)
                    sPadCornerMM = Math.Round((oPad.CornerHeight * 25.4) + oGrowDim.MM, 4)

                Else

                    sPadHeightIN = Math.Round(iPadHeight - oGrowDim.IN, 4)
                    sPadHeightTH = Math.Round((iPadHeight * 1000) - oGrowDim.TH, 4)
                    sPadHeightUM = Math.Round((iPadHeight * 25400) - oGrowDim.UM, 4)
                    sPadHeightMM = Math.Round((iPadHeight * 25.4) - oGrowDim.MM, 4)

                    sPadWidthIN = Math.Round(iPadWidth - oGrowDim.IN, 4)
                    sPadWidthTH = Math.Round((iPadWidth * 1000) - oGrowDim.TH, 4)
                    sPadWidthUM = Math.Round((iPadWidth * 25400) - oGrowDim.UM, 4)
                    sPadWidthMM = Math.Round((iPadWidth * 25.4) - oGrowDim.MM, 4)

                    sPadCornerIN = Math.Round(oPad.CornerHeight - oGrowDim.IN, 4)
                    sPadCornerTH = Math.Round((oPad.CornerHeight * 1000) - oGrowDim.TH, 4)
                    sPadCornerUM = Math.Round((oPad.CornerHeight * 25400) - oGrowDim.UM, 4)
                    sPadCornerMM = Math.Round((oPad.CornerHeight * 25.4) - oGrowDim.MM, 4)

                End If

            Case PadstackEditorLib.EPsDBUnit.epsdbUnitMils

                If (bGrow = True) Then

                    sPadHeightIN = Math.Round((iPadHeight / 1000) + oGrowDim.IN, 4)
                    sPadHeightTH = Math.Round(iPadHeight + oGrowDim.TH, 4)
                    sPadHeightUM = Math.Round((iPadHeight * 25.4) + oGrowDim.UM, 4)
                    sPadHeightMM = Math.Round((iPadHeight * 0.0254) + oGrowDim.MM, 4)

                    sPadWidthIN = Math.Round((iPadWidth / 1000) + oGrowDim.IN, 4)
                    sPadWidthTH = Math.Round(iPadWidth + oGrowDim.TH, 4)
                    sPadWidthUM = Math.Round((iPadWidth * 25.4) + oGrowDim.UM, 4)
                    sPadWidthMM = Math.Round((iPadWidth * 0.0254) + oGrowDim.MM, 4)

                    sPadCornerIN = Math.Round((oPad.CornerHeight / 1000) + oGrowDim.IN, 4)
                    sPadCornerTH = Math.Round(oPad.CornerHeight + oGrowDim.TH, 4)
                    sPadCornerUM = Math.Round((oPad.CornerHeight * 25.4) + oGrowDim.UM, 4)
                    sPadCornerMM = Math.Round((oPad.CornerHeight * 0.0254) + oGrowDim.MM, 4)

                Else

                    sPadHeightIN = Math.Round((iPadHeight / 1000) - oGrowDim.IN, 4)
                    sPadHeightTH = Math.Round(iPadHeight - oGrowDim.TH, 4)
                    sPadHeightUM = Math.Round((iPadHeight * 25.4) - oGrowDim.UM, 4)
                    sPadHeightMM = Math.Round((iPadHeight * 0.0254) - oGrowDim.MM, 4)

                    sPadWidthIN = Math.Round((iPadWidth / 1000) - oGrowDim.IN, 4)
                    sPadWidthTH = Math.Round(iPadWidth - oGrowDim.TH, 4)
                    sPadWidthUM = Math.Round((iPadWidth * 25.4) - oGrowDim.UM, 4)
                    sPadWidthMM = Math.Round((iPadWidth * 0.0254) - oGrowDim.MM, 4)

                    sPadCornerIN = Math.Round((oPad.CornerHeight / 1000) - oGrowDim.IN, 4)
                    sPadCornerTH = Math.Round(oPad.CornerHeight - oGrowDim.TH, 4)
                    sPadCornerUM = Math.Round((oPad.CornerHeight * 25.4) - oGrowDim.UM, 4)
                    sPadCornerMM = Math.Round((oPad.CornerHeight * 0.0254) - oGrowDim.MM, 4)

                End If

            Case PadstackEditorLib.EPsDBUnit.epsdbUnitMM

                If (bGrow = True) Then
                    sPadHeightIN = Math.Round((iPadHeight * 0.03937) + oGrowDim.IN, 4)
                    sPadHeightTH = Math.Round((iPadHeight * 39.37) + oGrowDim.TH, 4)
                    sPadHeightUM = Math.Round((iPadHeight / 1000) + oGrowDim.UM, 4)
                    sPadHeightMM = Math.Round(iPadHeight + oGrowDim.MM, 4)

                    sPadWidthIN = Math.Round((iPadWidth * 0.03937) + oGrowDim.IN, 4)
                    sPadWidthTH = Math.Round((iPadWidth * 39.37) + oGrowDim.TH, 4)
                    sPadWidthUM = Math.Round((iPadWidth * 1000) + oGrowDim.UM, 4)
                    sPadWidthMM = Math.Round(iPadWidth + oGrowDim.MM, 4)

                    sPadCornerIN = Math.Round((oPad.CornerHeight * 0.03937) + oGrowDim.IN, 4)
                    sPadCornerTH = Math.Round((oPad.CornerHeight * 39.37) + oGrowDim.TH, 4)
                    sPadCornerUM = Math.Round((oPad.CornerHeight * 1000) + oGrowDim.UM, 4)
                    sPadCornerMM = Math.Round(oPad.CornerHeight + oGrowDim.MM, 4)
                Else
                    sPadHeightIN = Math.Round((iPadHeight * 0.03937) - oGrowDim.IN, 4)
                    sPadHeightTH = Math.Round((iPadHeight * 39.37) - oGrowDim.TH, 4)
                    sPadHeightUM = Math.Round((iPadHeight * 1000) - oGrowDim.UM, 4)
                    sPadHeightMM = Math.Round(iPadHeight - oGrowDim.MM, 4)

                    sPadWidthIN = Math.Round((iPadWidth * 0.03937) - oGrowDim.IN, 4)
                    sPadWidthTH = Math.Round((iPadWidth * 39.37) - oGrowDim.TH, 4)
                    sPadWidthUM = Math.Round((iPadWidth * 1000) - oGrowDim.UM, 4)
                    sPadWidthMM = Math.Round(iPadWidth + oGrowDim.MM, 4)

                    sPadCornerIN = Math.Round((oPad.CornerHeight * 0.03937) - oGrowDim.IN, 4)
                    sPadCornerTH = Math.Round((oPad.CornerHeight * 39.37) - oGrowDim.TH, 4)
                    sPadCornerUM = Math.Round((oPad.CornerHeight * 1000) - oGrowDim.UM, 4)
                    sPadCornerMM = Math.Round(oPad.CornerHeight - oGrowDim.MM, 4)
                End If

            Case PadstackEditorLib.EPsDBUnit.epsdbUnitUM

                If (bGrow = True) Then
                    sPadHeightIN = Math.Round((iPadHeight * 0.00003937) + oGrowDim.IN, 4)
                    sPadHeightTH = Math.Round((iPadHeight * 0.03937) + oGrowDim.TH, 4)
                    sPadHeightUM = Math.Round(iPadHeight + oGrowDim.UM, 4)
                    sPadHeightMM = Math.Round((iPadHeight * 1000) + oGrowDim.MM, 4)

                    sPadWidthIN = Math.Round((iPadWidth * 0.00003937) + oGrowDim.IN, 4)
                    sPadWidthTH = Math.Round((iPadWidth * 0.03937) + oGrowDim.TH, 4)
                    sPadWidthUM = Math.Round(iPadWidth + oGrowDim.UM, 4)
                    sPadWidthMM = Math.Round((iPadWidth / 1000) + oGrowDim.MM, 4)

                    sPadCornerIN = Math.Round((oPad.CornerHeight * 0.00003937) + oGrowDim.IN, 4)
                    sPadCornerTH = Math.Round((oPad.CornerHeight * 0.03937) + oGrowDim.TH, 4)
                    sPadCornerUM = Math.Round(oPad.CornerHeight + oGrowDim.UM, 4)
                    sPadCornerMM = Math.Round((oPad.CornerHeight / 1000) + oGrowDim.MM, 4)
                Else
                    sPadHeightIN = Math.Round((iPadHeight * 0.00003937) - oGrowDim.IN, 4)
                    sPadHeightTH = Math.Round((iPadHeight * 0.03937) - oGrowDim.TH, 4)
                    sPadHeightUM = Math.Round(iPadHeight - oGrowDim.UM, 4)
                    sPadHeightMM = Math.Round((iPadHeight / 1000) - oGrowDim.MM, 4)

                    sPadWidthIN = Math.Round((iPadWidth * 0.00003937) - oGrowDim.IN, 4)
                    sPadWidthTH = Math.Round((iPadWidth * 0.03937) - oGrowDim.TH, 4)
                    sPadWidthUM = Math.Round(iPadWidth - oGrowDim.UM, 4)
                    sPadWidthMM = Math.Round((iPadWidth / 1000) - oGrowDim.MM, 4)

                    sPadCornerIN = Math.Round((oPad.CornerHeight * 0.00003937) - oGrowDim.IN, 4)
                    sPadCornerTH = Math.Round((oPad.CornerHeight * 0.03937) - oGrowDim.TH, 4)
                    sPadCornerUM = Math.Round(oPad.CornerHeight - oGrowDim.UM, 4)
                    sPadCornerMM = Math.Round((oPad.CornerHeight / 1000) - oGrowDim.MM, 4)
                End If

        End Select

        alPadInfo.Add(sPadHeightIN & " " & sPadWidthIN & " " & sPadCornerIN)
        alPadInfo.Add(sPadHeightTH & " " & sPadWidthTH & " " & sPadCornerTH)
        alPadInfo.Add(sPadHeightUM & " " & sPadWidthUM & " " & sPadCornerUM)
        alPadInfo.Add(sPadHeightMM & " " & sPadWidthMM & " " & sPadCornerMM)

        Dim PadDim As String
        Dim bFoundPad As Boolean = False

        For Each PadDim In alPadInfo

            If dicPads.ContainsKey(sShapeName & " " & PadDim & " " & oPad.OriginOffsetX & " " & oPad.OriginOffsetY) Then

                Return oPadStackDB.FindPad(dicPads.Item(sShapeName & " " & PadDim & " " & oPad.OriginOffsetX & " " & oPad.OriginOffsetY))

            End If

        Next

        NewPad = True

        Dim oNewPad As PadstackEditorLib.Pad = oPadStackDB.NewPad

        Select Case oPad.Shape

            Case PadstackEditorLib.EPsDBPadShape.epsdbPadShapeOblong

                oNewPad.Shape = PadstackEditorLib.EPsDBPadShape.epsdbPadShapeOblong

            Case PadstackEditorLib.EPsDBPadShape.epsdbPadShapeRectangle

                oNewPad.Shape = PadstackEditorLib.EPsDBPadShape.epsdbPadShapeRectangle

            Case PadstackEditorLib.EPsDBPadShape.epsdbPadShapeOctagonalFinger

                oNewPad.Shape = PadstackEditorLib.EPsDBPadShape.epsdbPadShapeOctagonalFinger

        End Select

        Dim genericName As String
        oNewPad.OriginOffsetX = oPad.OriginOffsetX
        oNewPad.OriginOffsetY = oPad.OriginOffsetY

        Select Case oPad.Units

            Case PadstackEditorLib.EPsDBUnit.epsdbUnitInch

                oNewPad.Units = PadstackEditorLib.EPsDBUnit.epsdbUnitInch
                oNewPad.Height = sPadHeightTH
                oNewPad.Width = sPadWidthTH
                oNewPad.CornerHeight = sPadCornerTH

                genericName = sShapeName & sPadHeightTH & " " & sPadWidthTH & " " & sPadCornerTH & " " & oPad.OriginOffsetX & " " & oPad.OriginOffsetY

            Case PadstackEditorLib.EPsDBUnit.epsdbUnitMils

                oNewPad.Units = PadstackEditorLib.EPsDBUnit.epsdbUnitMils
                oNewPad.Height = sPadHeightTH
                oNewPad.Width = sPadWidthTH
                oNewPad.CornerHeight = sPadCornerTH
                oNewPad.AutoName = True
                genericName = sShapeName & sPadHeightTH & " " & sPadWidthTH & " " & sPadHeightTH & " " & oPad.OriginOffsetX & " " & oPad.OriginOffsetY

            Case PadstackEditorLib.EPsDBUnit.epsdbUnitMM

                oNewPad.Units = PadstackEditorLib.EPsDBUnit.epsdbUnitMM
                oNewPad.Height = sPadHeightTH
                oNewPad.Width = sPadWidthTH
                oNewPad.CornerHeight = sPadCornerTH
                oNewPad.AutoName = True
                genericName = sShapeName & sPadHeightTH & " " & sPadWidthTH & " " & sPadCornerTH & " " & oPad.OriginOffsetX & " " & oPad.OriginOffsetY

            Case PadstackEditorLib.EPsDBUnit.epsdbUnitUM

                oNewPad.Units = PadstackEditorLib.EPsDBUnit.epsdbUnitUM
                oNewPad.Height = sPadHeightTH
                oNewPad.Width = sPadWidthTH
                oNewPad.CornerHeight = sPadCornerTH
                oNewPad.AutoName = True
                genericName = sShapeName & sPadHeightTH & " " & sPadWidthTH & " " & sPadCornerTH & " " & oPad.OriginOffsetX & " " & oPad.OriginOffsetY

        End Select

        oNewPad.Commit()
        oNewPad.AutoName = False
        Dim beforepadName As String = oNewPad.Name
        oNewPad.AutoName = True

        Dim padName As String = oNewPad.Name

        dicPads.Add(genericName, oNewPad.Name)

        Return oNewPad

    End Function

End Class