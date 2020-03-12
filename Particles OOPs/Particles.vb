Public Class Particles

    'Initialises the variables
    Public XCoord As Double
    Public YCoord As Double
    Public Velocity As Double
    Public Bearing As Double
    Public Size As Integer
    Public Mass As Double
    Public Sub New()
        Randomize()

        'Increases the number of particles by 1
        Form1.NumberofParticles += 1
        'Gives the new particle a random velocity between 1 and 5
        Me.Velocity = 1 'CInt(Math.Ceiling(Rnd() * 5))
        'Gives the new particle a random bearing between 0 and 2PI
        Me.Bearing = Rnd() * (2 * Math.PI)
        'Gives the new particle a size based off its type
        Me.Size = 50
        'Gives the new particle random coordinates
        Me.XCoord = CInt(Math.Ceiling(Rnd() * (Form1.WindowWidth - 17 - Me.Size)))
        Me.YCoord = CInt(Math.Ceiling(Rnd() * (Form1.WindowHeight - 220 - Me.Size)))
        'Adds the new particle object
        Form1.ParticleArray.Add(Me)
    End Sub
End Class
