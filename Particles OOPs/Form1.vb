﻿Public Class Form1

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
        For Each Particle In ParticleArray
            Dim XVector As Double = Math.Cos(Particle.Bearing) * Particle.Velocity
            Dim YVector As Double = Math.Sin(Particle.Bearing) * Particle.Velocity
            Dim NewXVector As Double
            Dim NewYVector As Double
            Console.WriteLine(YVector.ToString)
            'If the X and Y coordinates of the particle are within the bounds of the simulation space
            If Particle.XCoord < (WindowWidth - Particle.Size) And (Particle.XCoord > 0) And Particle.YCoord < (WindowHeight - Particle.Size) And Particle.YCoord > 0 Then
                'Sets the new x and y coordinates based on velocity and bearing
                Particle.XCoord += XVector
                Particle.YCoord += YVector
            Else
                If Particle.XCoord < (WindowWidth - Particle.Size) And (Particle.XCoord > 0) Then
                    If Particle.YCoord > 0 Then 'If bouncing off bottom wall
                        'Use bottom wall vector
                        NewXVector += ReflectVector(0, XVector, YVector, 0, 1)
                        NewYVector += ReflectVector(1, XVector, YVector, 0, 1)
                        Console.WriteLine()
                    ElseIf Particle.YCoord < (WindowHeight - Particle.Size) Then 'If bouncing off top wall
                        'Use top wall vector
                        NewXVector += ReflectVector(0, XVector, YVector, 0, 1)
                        NewYVector += ReflectVector(1, XVector, YVector, 0, 1)
                        Console.WriteLine()
                    End If
                ElseIf Particle.YCoord < (WindowHeight - Particle.Size) And Particle.YCoord > 0 Then
                    If Particle.XCoord > 0 Then 'If bouncing off right wall
                        'Use right wall vector
                        NewXVector += ReflectVector(0, XVector, YVector, 1, 0)
                        NewYVector += ReflectVector(1, XVector, YVector, 1, 0)
                    ElseIf Particle.XCoord < (WindowWidth - Particle.Size) Then 'If bouncing off left wall
                        'Use left wall vector
                        NewXVector += ReflectVector(0, XVector, YVector, 1, 0)
                        NewYVector += ReflectVector(1, XVector, YVector, 1, 0)
                    End If
                End If
                Dim Angle As Double = Math.Atan(Math.Abs(NewXVector) / Math.Abs(NewYVector))

                If NewXVector > 0 And NewYVector < 0 Then
                    Particle.Bearing = (2 * Math.PI) - ((Math.PI / 2) - Angle)
                ElseIf NewXVector < 0 And NewYVector < 0 Then
                    Particle.Bearing = (2 * Math.PI) - ((Math.PI / 2) + Angle)
                ElseIf NewXVector < 0 And NewYVector > 0 Then
                    Particle.Bearing = ((Math.PI / 2) + Angle)
                ElseIf NewXVector > 0 And NewYVector > 0 Then
                    Particle.Bearing = ((Math.PI / 2) - Angle)
                End If

                Particle.XCoord += NewXVector
                Particle.YCoord += NewYVector

            End If

            'Draws the particle based on the new x and y coordinates and the size
            e.Graphics.FillEllipse(Brushes.Black, Convert.ToInt32(Particle.XCoord), Convert.ToInt32(Particle.YCoord), Particle.Size, Particle.Size)
            'Draws text displaying the angle of the particle, with 0 degrees pointing to the right
            e.Graphics.DrawString(Math.Round((Particle.Bearing * (180 / Math.PI)), 1, MidpointRounding.AwayFromZero).ToString + "°", New Font("Tahoma", 7), Brushes.Red, New Point((Particle.XCoord + (Particle.Size / 2) - 9), (Particle.YCoord + (Particle.Size + 20))))

        Next


    End Sub

    Private Sub AddParticleButton_Click(sender As Object, e As EventArgs) Handles AddParticleButton.Click
        'Initialises a new particle object
        Particle = New Particles
    End Sub

    Private Function ReflectVector(CurrentVector As Integer, XVector As Double, YVector As Double, XNormal As Double, YNormal As Double)


        If CurrentVector = 0 Then
            Dim newvector As Double = XVector - (2 * ((XVector * XNormal) + (YVector * YNormal)) * XNormal)
            Return newvector
        Else
            Dim newvector As Double = YVector - (2 * ((XVector * XNormal) + (YVector * YNormal)) * YNormal)
            Return newvector
        End If
    End Function

End Class