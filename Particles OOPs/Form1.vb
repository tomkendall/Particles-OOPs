Public Class Form1

    'initialises variables
    Public WindowWidth As Integer
    Public WindowHeight As Integer
    Public NumberofParticles As Integer
    Public ParticleArray As New List(Of Particles)(100)
    Public Particle As Particles


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'Sets the window width and height based on the simlation space
        WindowWidth = Me.Width - 17
        WindowHeight = Me.Height - 220
        'Wipes the painted screen and calls Form1_Paint
        Invalidate()
    End Sub

    Private Sub Form1_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        'Enables antialiasing
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        'Draws the border of the simulation space
        e.Graphics.DrawRectangle(Pens.Black, 0, 0, WindowWidth, WindowHeight)
        'loops through each particle object
        For i = 0 To ParticleArray.Count - 1
            'Initialises the current vector of the particle
            Dim XVector As Double = Math.Cos(ParticleArray(i).Bearing) * ParticleArray(i).Velocity
            Dim YVector As Double = Math.Sin(ParticleArray(i).Bearing) * ParticleArray(i).Velocity
            'Initialises the vector of the particle after the collision
            Dim NewXVector As Double
            Dim NewYVector As Double
            'If the X and Y coordinates of the particle are within the bounds of the simulation space
            If ParticleArray(i).XCoord < (WindowWidth - ParticleArray(i).Size) And (ParticleArray(i).XCoord > 0) And ParticleArray(i).YCoord < (WindowHeight - ParticleArray(i).Size) And ParticleArray(i).YCoord > 0 Then
                'Sets the new x and y coordinates based on velocity and bearing
                ParticleArray(i).XCoord += XVector
                ParticleArray(i).YCoord += YVector
            Else
                'Otherwise if bouncing off the top/bottom wall
                If ParticleArray(i).XCoord < (WindowWidth - ParticleArray(i).Size) And (ParticleArray(i).XCoord > 0) Then
                    If ParticleArray(i).YCoord > 0 Then 'If bouncing off bottom wall
                        'Use bottom wall vector
                        NewXVector += XVector - (2 * ((XVector * 0) + (YVector * 1)) * 0)
                        NewYVector += YVector - (2 * ((XVector * 0) + (YVector * 1)) * 1)
                    ElseIf ParticleArray(i).YCoord < (WindowHeight - ParticleArray(i).Size) Then 'If bouncing off top wall
                        'Use top wall vector
                        NewXVector += XVector - (2 * ((XVector * 0) + (YVector * -1)) * 0)
                        NewYVector += YVector - (2 * ((XVector * 0) + (YVector * -1)) * -1)
                    End If
                    'Otherwise if bouncing off the left/right wall
                ElseIf ParticleArray(i).YCoord < (WindowHeight - ParticleArray(i).Size) And ParticleArray(i).YCoord > 0 Then
                    If ParticleArray(i).XCoord > 0 Then 'If bouncing off right wall
                        'Use right wall vector
                        NewXVector += XVector - (2 * ((XVector * -1) + (YVector * 0)) * -1)
                        NewYVector += YVector - (2 * ((XVector * -1) + (YVector * 0)) * 0)
                    ElseIf ParticleArray(i).XCoord < (WindowWidth - ParticleArray(i).Size) Then 'If bouncing off left wall
                        'Use left wall vector
                        NewXVector += XVector - (2 * ((XVector * 1) + (YVector * 0)) * 1)
                        NewYVector += YVector - (2 * ((XVector * 1) + (YVector * 0)) * 0)
                    End If
                End If

                'Initialises the angle between the X and Y Vector
                Dim Angle As Double = Math.Atan(Math.Abs(NewXVector) / Math.Abs(NewYVector))

                'Finds the new bearing of the Particle dependant on the quadrant
                If NewXVector > 0 And NewYVector < 0 Then 'If X is positive and Y is negative
                    ParticleArray(i).Bearing = (2 * Math.PI) - ((Math.PI / 2) - Angle)
                ElseIf NewXVector < 0 And NewYVector < 0 Then 'If X is negative and Y is negative
                    ParticleArray(i).Bearing = (2 * Math.PI) - ((Math.PI / 2) + Angle)
                ElseIf NewXVector < 0 And NewYVector > 0 Then 'If X is negative and Y is positive
                    ParticleArray(i).Bearing = ((Math.PI / 2) + Angle)
                ElseIf NewXVector > 0 And NewYVector > 0 Then 'If X is positive and Y is positive
                    ParticleArray(i).Bearing = ((Math.PI / 2) - Angle)
                Else 'If it is 
                    ParticleArray(i).Bearing += Math.PI
                End If

                'Sets the new x and y coordinates based on new velocity and bearing
                ParticleArray(i).XCoord += NewXVector
                ParticleArray(i).YCoord += NewYVector

            End If

            'Draws the particle based on the new x and y coordinates and the size
            e.Graphics.FillEllipse(Brushes.Black, Convert.ToInt32(ParticleArray(i).XCoord), Convert.ToInt32(ParticleArray(i).YCoord), ParticleArray(i).Size, ParticleArray(i).Size)
            'Draws text displaying the angle of the particle, with 0 degrees pointing to the right
            e.Graphics.DrawString(Math.Round((ParticleArray(i).Bearing * (180 / Math.PI)), 1, MidpointRounding.AwayFromZero).ToString + "°", New Font("Tahoma", 7), Brushes.Red, New Point((ParticleArray(i).XCoord + (ParticleArray(i).Size / 2) - 9), (ParticleArray(i).YCoord + (ParticleArray(i).Size + 20))))
            e.Graphics.DrawString(i.ToString, New Font("Tahoma", 10), Brushes.Blue, New Point((ParticleArray(i).XCoord + (ParticleArray(i).Size / 2) - 9), (ParticleArray(i).YCoord + (ParticleArray(i).Size))))
        Next

        If CollisionsCheckbox.Checked = True And ParticleArray.Count > 1 Then 'Checks whether collisions are enabled and there are 2 particles to collide
            For i = 0 To ParticleArray.Count - 1 'Loops through each particle
                For j = (i + 1) To (ParticleArray.Count - 1) 'Checks each particle against each other
                    If (ParticleArray(j).XCoord - ParticleArray(i).XCoord) ^ 2 + (ParticleArray(i).YCoord - ParticleArray(j).YCoord) ^ 2 <= ((ParticleArray(i).Size / 2) + (ParticleArray(j).Size / 2)) ^ 2 Then 'Checks whether the two particles are colliding
                        'Finds the centre points of each particles instead of the top left corner (default)
                        Dim CentrePointParticleI() As Integer = {(ParticleArray(i).XCoord + (ParticleArray(i).Size / 2)), ParticleArray(i).YCoord + (ParticleArray(i).Size / 2)}
                        Dim CentrePointParticleJ() As Integer = {(ParticleArray(j).XCoord + (ParticleArray(j).Size / 2)), ParticleArray(j).YCoord + (ParticleArray(j).Size / 2)}

                        'Finds the point of collision between the two particles
                        Dim midpointX As Integer = ((CentrePointParticleI(0) + CentrePointParticleJ(0)) / 2)
                        Dim midpointY As Integer = ((CentrePointParticleI(1) + CentrePointParticleJ(1)) / 2)

                        'finds the gradient between the two particles
                        Dim Gradient As Double = (((-CentrePointParticleI(1)) - (-CentrePointParticleJ(1))) / (CentrePointParticleI(0) - CentrePointParticleJ(0)))

                        'finds the gradient of the pseudowall
                        Dim PWallGradient As Double = (-(1 / Gradient))

                        'finds c (in the equation y=mx+c) for the gradient between the centre of the two particles
                        Dim OriginalC As Double = -midpointY - (Gradient * midpointX)

                        'finds c (in the equation y=mx+c) for the gradient of the pseudowall
                        Dim PwallC As Double = -midpointY - (PWallGradient * midpointX)

                        'Draws the pseudowall by finding the x position if y is changed by either +20 or -20 (using y=mx+c)
                        Dim DrawPoint1 As New Point((((-(midpointY + 20) - PwallC) / PWallGradient)), (midpointY + 20))
                        Dim DrawPoint2 As New Point((((-(midpointY - 20) - PwallC) / PWallGradient)), (midpointY - 20))
                        Dim CentrePoints As Point() = {DrawPoint1, DrawPoint2}

                        'Draws psuedowall
                        e.Graphics.DrawPolygon(Pens.Red, CentrePoints)

                        'Dim pwallbearing As Double = (((ParticleArray(i).Bearing * (180 / Math.PI)) * ParticleArray(i).Velocity) + ((ParticleArray(j).Bearing * (180 / Math.PI)) * ParticleArray(j).Velocity) / (ParticleArray(j).Velocity + ParticleArray(i).Velocity))
                        Dim pwallbearing As Double = (((ParticleArray(i).Bearing) * ParticleArray(i).Velocity) + ((ParticleArray(j).Bearing) * ParticleArray(j).Velocity) / (ParticleArray(j).Velocity + ParticleArray(i).Velocity))

                        Dim PWallXVector As Double = Math.Cos(pwallbearing)
                        Dim PWallYVector As Double = Math.Sin(pwallbearing)

                        'Initialises the current vector of the particle
                        Dim XVectorI As Double = Math.Cos(ParticleArray(i).Bearing) * ParticleArray(i).Velocity
                        Dim YVectorI As Double = Math.Sin(ParticleArray(i).Bearing) * ParticleArray(i).Velocity
                        'Initialises the current vector of the particle
                        Dim XVectorJ As Double = Math.Cos(ParticleArray(j).Bearing) * ParticleArray(j).Velocity
                        Dim YVectorJ As Double = Math.Sin(ParticleArray(j).Bearing) * ParticleArray(j).Velocity

                        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        Dim PWallXPerpendicularI = PWallYVector
                        Dim PWallYPerpendicularI = -PWallXVector

                        Dim NewXVectorI As Double = XVectorI - (2 * ((XVectorI * PWallXPerpendicularI) + (YVectorI * PWallYPerpendicularI)) * PWallXPerpendicularI)
                        Dim NewYVectorI As Double = YVectorI - (2 * ((XVectorI * PWallXPerpendicularI) + (YVectorI * PWallYPerpendicularI)) * PWallYPerpendicularI)

                        'Initialises the angle between the X and Y Vector
                        Dim IAngle As Double = Math.Atan(Math.Abs(NewXVectorI) / Math.Abs(NewYVectorI))

                        'Finds the new bearing of the Particle dependant on the quadrant
                        If NewXVectorI > 0 And NewYVectorI < 0 Then 'If X is positive and Y is negative
                            ParticleArray(i).Bearing = (2 * Math.PI) - ((Math.PI / 2) - IAngle)
                        ElseIf NewXVectorI < 0 And NewYVectorI < 0 Then 'If X is negative and Y is negative
                            ParticleArray(i).Bearing = (2 * Math.PI) - ((Math.PI / 2) + IAngle)
                        ElseIf NewXVectorI < 0 And NewYVectorI > 0 Then 'If X is negative and Y is positive
                            ParticleArray(i).Bearing = ((Math.PI / 2) + IAngle)
                        ElseIf NewXVectorI > 0 And NewYVectorI > 0 Then 'If X is positive and Y is positive
                            ParticleArray(i).Bearing = ((Math.PI / 2) - IAngle)
                        Else 'If it is 
                            ParticleArray(i).Bearing += Math.PI
                        End If

                        ParticleArray(i).XCoord += NewXVectorI
                        ParticleArray(i).YCoord += NewYVectorI
                        ParticleArray(i).XCoord += NewXVectorI
                        ParticleArray(i).YCoord += NewYVectorI
                        ParticleArray(i).XCoord += NewXVectorI
                        ParticleArray(i).YCoord += NewYVectorI
                        ParticleArray(i).XCoord += NewXVectorI
                        ParticleArray(i).YCoord += NewYVectorI

                        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        Dim PWallXPerpendicularJ = -PWallYVector
                        Dim PWallYPerpendicularJ = PWallXVector

                        Dim NewXVectorJ As Double = XVectorJ - (2 * ((XVectorJ * PWallXPerpendicularJ) + (YVectorJ * PWallYPerpendicularJ)) * PWallXPerpendicularJ)
                        Dim NewYVectorJ As Double = YVectorJ - (2 * ((XVectorJ * PWallXPerpendicularJ) + (YVectorJ * PWallYPerpendicularJ)) * PWallYPerpendicularJ)

                        'Initialises the angle between the X and Y Vector
                        Dim JAngle As Double = Math.Atan(Math.Abs(NewXVectorJ) / Math.Abs(NewYVectorJ))

                        'Finds the new bearing of the Particle dependant on the quadrant
                        If NewXVectorJ > 0 And NewYVectorJ < 0 Then 'If X is positive and Y is negative
                            ParticleArray(j).Bearing = (2 * Math.PI) - ((Math.PI / 2) - JAngle)
                        ElseIf NewXVectorJ < 0 And NewYVectorJ < 0 Then 'If X is negative and Y is negative
                            ParticleArray(j).Bearing = (2 * Math.PI) - ((Math.PI / 2) + JAngle)
                        ElseIf NewXVectorJ < 0 And NewYVectorJ > 0 Then 'If X is negative and Y is positive
                            ParticleArray(j).Bearing = ((Math.PI / 2) + JAngle)
                        ElseIf NewXVectorJ > 0 And NewYVectorJ > 0 Then 'If X is positive and Y is positive
                            ParticleArray(j).Bearing = ((Math.PI / 2) - JAngle)
                        Else 'If it is 
                            ParticleArray(j).Bearing += Math.PI
                        End If

                        ParticleArray(j).XCoord += NewXVectorJ
                        ParticleArray(j).YCoord += NewYVectorJ
                        ParticleArray(j).XCoord += NewXVectorJ
                        ParticleArray(j).YCoord += NewYVectorJ
                        ParticleArray(j).XCoord += NewXVectorJ
                        ParticleArray(j).YCoord += NewYVectorJ
                        ParticleArray(j).XCoord += NewXVectorJ
                        ParticleArray(j).YCoord += NewYVectorJ

                    End If
                Next
            Next
        End If

    End Sub

    Private Sub AddParticleButton_Click(sender As Object, e As EventArgs) Handles AddParticleButton.Click
        'Initialises a new particle object
        Particle = New Particles
    End Sub

    'Private Function ReflectVector(CurrentVector As Integer, XVector As Double, YVector As Double, XNormal As Double, YNormal As Double)


    '    If CurrentVector = 0 Then
    '        Dim newvector As Double = XVector - (2 * ((XVector * XNormal) + (YVector * YNormal)) * XNormal)
    '        Return newvector
    '    Else
    '        Dim newvector As Double = YVector - (2 * ((XVector * XNormal) + (YVector * YNormal)) * YNormal)
    '        Return newvector
    '    End If

    'End Function

    Private Sub PauseButton_Click(sender As Object, e As EventArgs) Handles PauseButton.Click

        'Pauses the timer and therefore the simulation when the button is pressed
        If Timer1.Enabled = True Then
            'If the simulation is running, pause it
            Timer1.Enabled = False
            'Change the text to 'resume'
            PauseButton.Text = "Resume"
        Else
            'If the simulation is paused, run it
            Timer1.Enabled = True
            'Change the text to 'pause'
            PauseButton.Text = "Pause"
        End If

    End Sub

End Class