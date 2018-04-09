Imports System.ComponentModel
Imports System.Windows.Forms
Imports EllieMae.Encompass.Automation
Imports EllieMae.Encompass.BusinessObjects.Loans

Public Class MonitorWindow

    'Disconnects the events when the monitor window is closed
    Private Sub MonitorWindow_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        RemoveHandler EncompassApplication.CurrentLoan.FieldChange, New FieldChangeEventHandler(AddressOf CurrentLoan_FieldChange)
        RemoveHandler EncompassApplication.CurrentLoan.LogEntryAdded, New LogEntryEventHandler(AddressOf CurrentLoan_LogEntryAdded)
        RemoveHandler EncompassApplication.CurrentLoan.LogEntryChange, New LogEntryEventHandler(AddressOf CurrentLoan_LogEntryChange)
        RemoveHandler EncompassApplication.CurrentLoan.LogEntryRemoved, New LogEntryEventHandler(AddressOf CurrentLoan_LogEntryRemoved)

    End Sub

    'Connects the events when the form is loaded
    Private Sub MonitorWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        AddHandler EncompassApplication.CurrentLoan.FieldChange, New FieldChangeEventHandler(AddressOf CurrentLoan_FieldChange)
        AddHandler EncompassApplication.CurrentLoan.LogEntryAdded, New LogEntryEventHandler(AddressOf CurrentLoan_LogEntryAdded)
        AddHandler EncompassApplication.CurrentLoan.LogEntryChange, New LogEntryEventHandler(AddressOf CurrentLoan_LogEntryChange)
        AddHandler EncompassApplication.CurrentLoan.LogEntryRemoved, New LogEntryEventHandler(AddressOf CurrentLoan_LogEntryRemoved)

    End Sub

    'Event handler for when a log entry is removed from the Loan
    Private Sub CurrentLoan_LogEntryRemoved(ByVal source As Object, ByVal e As LogEntryEventArgs)

        Dim item As ListViewItem = New ListViewItem(e.LogEntry.GetType().Name)
        item.SubItems.Add("Removed")
        item.SubItems.Add(e.LogEntry.ID)
        lvwChanges.Items.Add(item)
        item.EnsureVisible()

    End Sub

    'Event handler for when a log entry is modified
    Private Sub CurrentLoan_LogEntryChange(ByVal source As Object, ByVal e As LogEntryEventArgs)

        Dim item As ListViewItem = New ListViewItem(e.LogEntry.GetType().Name)
        item.SubItems.Add("Change")
        item.SubItems.Add(e.LogEntry.ID)
        lvwChanges.Items.Add(item)
        item.EnsureVisible()

    End Sub

    ' Event Handler for when a new log item is added to the loan
    Private Sub CurrentLoan_LogEntryAdded(ByVal source As Object, ByVal e As LogEntryEventArgs)

        Dim item As ListViewItem = New ListViewItem(e.LogEntry.GetType().Name)
        item.SubItems.Add("Added")
        item.SubItems.Add(e.LogEntry.ID)
        lvwChanges.Items.Add(item)
        item.EnsureVisible()

    End Sub

    'Event handler for when a field is modified
    Private Sub CurrentLoan_FieldChange(ByVal source As Object, ByVal e As FieldChangeEventArgs)

        Dim item As ListViewItem = New ListViewItem(e.FieldID)
        item.SubItems.Add(e.PriorValue)
        item.SubItems.Add(e.NewValue)
        lvwChanges.Items.Add(item)
        item.EnsureVisible()

    End Sub

End Class