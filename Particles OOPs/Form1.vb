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
            'If the X and Y coordinates of the particle are within the bounds of the simulation space
            If Particle.XCoord < (WindowWidth - Particle.Size) And (Particle.XCoord > 0) And Particle.YCoord < (WindowHeight - Particle.Size) And Particle.YCoord > 0 Then

                'Sets the new x and y coordinates based on velocity and bearing
                Particle.XCoord += Math.Cos(Particle.Bearing) * Particle.Velocity
                Particle.YCoord += Math.Sin(Particle.Bearing) * Particle.Velocity
                'Draws the particle based on the new x and y coordinates and the size
                e.Graphics.FillEllipse(Brushes.Black, Convert.ToInt32(Particle.XCoord), Convert.ToInt32(Particle.YCoord), Particle.Size, Particle.Size)
                'Draws text displaying the angle of the particle, with 0 degrees pointing to the right
                e.Graphics.DrawString(Math.Round((Particle.Bearing * (180 / Math.PI)), 1, MidpointRounding.AwayFromZero).ToString + "°", New Font("Tahoma", 7), Brushes.Red, New Point((Particle.XCoord + (Particle.Size / 2) - 9), (Particle.YCoord + (Particle.Size + 20))))
            End If
        Next
    End Sub

    Private Sub AddParticleButton_Click(sender As Object, e As EventArgs) Handles AddParticleButton.Click
        'Initialises a new particle object
        Particle = New Particles
    End Sub
End Class