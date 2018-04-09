Imports EllieMae.Encompass.Client

' The globals will provide a place to hold the Session object for the current application
Module Globals

    Private _session As Session

    'Current Session Object
    Public Property Session() As Session
        Get
            Return _session
        End Get
        Set(ByVal value As Session)
            _session = value

            'Attach the event handlers to catch messages and disconnects
            AddHandler _session.MessageArrived, AddressOf sessionMessageArrived
            AddHandler Session.Disconnected, AddressOf sessionDisconnected
        End Set
    End Property

    'The event handler for an incoming asynchronous message
    Private Sub sessionMessageArrived(ByVal sender As Object, ByVal e As ServerMessageEventArgs)

        MessageBox.Show("A message has arrived from " + e.Source.ToString() & ":\r\n\r\n" & e.Text)

    End Sub

    'The event handler for unexpected disconnects from the server
    Private Sub sessionDisconnected(ByVal sender As Object, ByVal e As DisconnectedEventArgs)

        Try
            If e.Reason <> DisconnectReason.SessionDisposed Then
                MessageBox.Show("The session has been disconnected for the following reason: " & e.Reason.ToString() & ".")
            End If
        Catch ex As Exception
            MessageBox.Show("Session Disconnected with unknown error: " & ex.Message)
        End Try

    End Sub


End Module
