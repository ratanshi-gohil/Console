Imports EllieMae.Encompass.BusinessObjects.Loans
Imports EllieMae.Encompass.Client
Imports EllieMae.Encompass.Collections

Public Class MonitorWindow

    Public appSession As Session = Nothing
    Private currentLoan As Loan = Nothing

    Public Sub New()

        ' Required for Windows form Designer support
        InitializeComponent()

        'Init the form by loading the list of loan folders from the server
        PopulateLoanFolderList()

        'Set the state of the loan info portion of the form to disabled
        ClearCurrentLoan()

    End Sub

    ' Retrieves the list of loan folders from the server and lists them in the Listbox
    Private Sub PopulateLoanFolderList()
        'Clear the current list
        lstFolders.Items.Clear()

        'Get the list from the server
        For Each folder As LoanFolder In Globals.Session.Loans.Folders
            lstFolders.Items.Add(folder)
        Next
    End Sub

    ' When the user selects a folder, we will list all of the folder's loan in the 
    ' Loans listbox
    Private Sub lstFolders_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstFolders.SelectedIndexChanged

        'Clear the current loan
        ClearCurrentLoan()

        'Load the loan list
        populateLoanList(lstFolders.SelectedItem)

    End Sub

    'Populates the loan list with the contents of a folder
    Private Sub populateLoanList(parentFolder As LoanFolder)
        'Clear the current items from the list
        lstLoans.Items.Clear()

        'Get the contents of the folder
        Dim Loans As LoanIdentityList = parentFolder.GetContents()

        'Load the list with the identities of the loans
        For Each id As LoanIdentity In Loans
            lstLoans.Items.Add(id)
        Next
    End Sub

    ' When a user selects one of the loans, we need to display the loan in the
    ' body of the form.
    Private Sub lstLoans_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstLoans.SelectedIndexChanged

        'Clear the current loan
        ClearCurrentLoan()

        'Fetch the selected loan identity
        Dim id As LoanIdentity = lstLoans.SelectedItem

        'Retrieve the loan from the server
        Dim Loan As Loan = Globals.Session.Loans.Open(id.Guid)

        'Load this loan into the form
        setCurrentLoan(Loan)

    End Sub

    ' Clears the form of the current loan
    Private Sub ClearCurrentLoan()
        'Clear the listview and other fields
        lvwLoanData.Items.Clear()
        lblFieldId.Text = String.Empty
        lblFieldName.Text = String.Empty
        txtFieldValue.Text = String.Empty

        'Disable the elements
        lvwLoanData.Enabled = False
        txtFieldValue.Enabled = False
        btnCommit.Enabled = False
        btnRefresh.Enabled = False

        'Clear the current loan
        currentLoan = Nothing

    End Sub

    ' Sets the current loan and loads its data into the form
    Private Sub setCurrentLoan(loan As Loan)

        'Set the current loan value for future use
        currentLoan = loan

        'Load the listview with the loan's data
        populateCurrentLoanIntoListView()

        'Enable the controls
        lvwLoanData.Enabled = True
        txtFieldValue.Enabled = True
        btnCommit.Enabled = True
        btnRefresh.Enabled = True
    End Sub

    ' Loads the current loan's data into the listview
    Private Sub populateCurrentLoanIntoListView()
        'Clear the listview rows
        lvwLoanData.SelectedItems.Clear()
        lvwLoanData.Items.Clear()

        'Add a set of rows to the listview from the loan
        addLoanFieldToListView("364", "Loan Number")
        addLoanFieldToListView("11", "Property Address")
        addLoanFieldToListView("12", "Property City")
        addLoanFieldToListView("13", "Property County")
        addLoanFieldToListView("14", "Property State")
        addLoanFieldToListView("136", "Purchase Price")
        addLoanFieldToListView("4", "Term")
        addLoanFieldToListView("3", "Interest Rate")
        addLoanFieldToListView("1335", "Down Payment")

    End Sub

    ' Adds a field to the load data view for the current loan
    Private Sub addLoanFieldToListView(fieldId As String, fieldDesc As String)

        Dim item As ListViewItem = New ListViewItem(New String() {fieldId,
                                                                  fieldDesc,
                                                                  currentLoan.Fields(fieldId).FormattedValue})
        lvwLoanData.Items.Add(item)

    End Sub

    ' When a listview row is selected, move that row's data into the fields
    Private Sub lvwLoanData_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvwLoanData.SelectedIndexChanged

        'Save any changes made to the current field
        saveCurrentField()

        If (lvwLoanData.SelectedItems.Count > 0) Then
            loadCurrentField(lvwLoanData.SelectedItems(0))
        End If

    End Sub

    'Saves any changes to the current field back to the loan and updates the listview
    Private Sub saveCurrentField()
        'If no field was loaded, bail out immediately
        If (lblFieldId.Text = String.Empty) Then
            Return
        End If

        'Load the field values onto the page
        currentLoan.Fields(lblFieldId.Text).Value = txtFieldValue.Text

    End Sub

    'Loads the selected field into the editable form elements
    Private Sub loadCurrentField(item As ListViewItem)
        lblFieldId.Text = item.SubItems(0).Text
        lblFieldName.Text = item.SubItems(1).Text
        txtFieldValue.Text = item.SubItems(2).Text
    End Sub

    ' Locates an item in the listview by field Id
    Private Function findListViewItem(fieldId As String) As ListViewItem
        For i As Integer = 0 To lvwLoanData.Items.Count
            If (lvwLoanData.Items(i).SubItems(0).Text = fieldId) Then
                Return lvwLoanData.Items(i)
            End If
        Next

        Return Nothing

    End Function

    ' When the Commit button is pressed, all pending changes are saved back to the server
    Private Sub btnCommit_Click(sender As Object, e As EventArgs) Handles btnCommit.Click
        'Save the current field
        saveCurrentField()

        Try
            'Lock the loan and then commit the changes
            currentLoan.Lock()
            currentLoan.Commit()
            currentLoan.Unlock()

            'Notify the user
            MessageBox.Show("The current loan has been saved.")

            'Update the listview item for this field
            Dim item As ListViewItem = findListViewItem(lblFieldId.Text)

            If (item IsNot Nothing) Then
                item.SubItems(2).Text = currentLoan.Fields(lblFieldId.Text).FormattedValue
            End If

        Catch ex As Exception
            MessageBox.Show("Error saving loan: " & ex.Message)
        End Try

    End Sub

    ' The refresh button retrieves the loan data from the server and throws out
    ' any changes made to this point.
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        'Refresh the loan
        currentLoan.Refresh()

        'Re-populate the listview
        populateCurrentLoanIntoListView()
    End Sub

End Class
