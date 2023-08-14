Feature: UserManagement

This feature contains all the scenarios for user management

Background: 
	Given I have accessed to the Admin Application
	Then the Login screen should display
	When I login as an Admin
	Then the Home screen should display

@usermanagement
Scenario: UM01 - The admin user add a new admin user
	When I select the User Management
	Then the User Management screen should display
	When I select Add User button
	Then the Add User screen should display
	When I add a new user as below
	| FirstName  | LastName | Email | Role          | Phone         | PhoneType |
	| Automation | Tester   |       | Administrator | 080-1234-5678 | Mobile    |
	Then the Add User successfully toast message should display
	Then the User Management screen should display
	When I search for the user 'Automation'
	Then the new user 'Automation' should display
	When I check the user mailbox
	Then the user should receive invitation mail
	When I click on the invitation link in the email
	Then the Reset Password screen should display
	When I input '123456789' for Password field
	When I input '123456789' for Confirm Password field
	When I click Update Password button
	Then the Password Updated screen should display
	When I click Back to Login button
	Then the Login screen should display
	When I login with the new user
	Then the Home screen should display


@usermanagement
Scenario: UM02 - The admin user add a new teacher user
	When I select the User Management
	Then the User Management screen should display
	When I select Add User button
	Then the Add User screen should display
	When I add a new user as below
	| FirstName  | LastName | Email | Role    | Phone         | PhoneType |
	| Automation | Tester   |       | Teacher | 080-1234-5678 | Mobile    |
	Then the Add User successfully toast message should display
	Then the User Management screen should display
	When I search for the user 'Automation'
	Then the new user 'Automation' should display
	When I check the user mailbox
	Then the user should receive invitation mail
	When I click on the invitation link in the email
	Then the Reset Password screen should display
	When I input '123456789' for Password field
	When I input '123456789' for Confirm Password field
	When I click Update Password button
	Then the Password Updated screen should display
	When I click Back to Login button
	Then the Login screen should display
	When I login with the new user
	Then the Home screen should display


@usermanagement
Scenario: UM03 - The user detail validation
	When I select the User Management
	Then the User Management screen should display
	When I select Add User button
	Then the Add User screen should display
	When I click Save button
	Then the validation for mandatory fields in User Detail screen should display
	When I input 'invalidemail' for Email field
	Then the validation for invalid email in User Detail screen should display
	When I add a new user as below
	| FirstName  | LastName | Email                  | Role    | Phone         | PhoneType |
	| Automation | Tester   | auto.admin@yopmail.com | Teacher | 080-1234-5678 | Mobile    |
	Then the validation for duplicated email in User Detail screen should display


@usermanagement
Scenario: UM04 - The admin user edits an existing user
	When I select the User Management
	Then the User Management screen should display
	When I search for the user 'AutoAdmin'
	Then the new user 'AutoAdmin' should display
	When I select the user 'AutoAdmin'
	Then the user detail screen should display with the below information populated
	| FirstName | LastName | Email                  | Role          | Phone |
	| AutoAdmin | Tester   | auto.admin@yopmail.com | Administrator |       |
	Then the Email field should be disabled
	When I input 'Modified' for First Name field
	When I input 'Modified' for Last Name field
	When I select 'Mobile' for Phone Type field
	When I input '080-1234-5678' for Phone Number field
	When I click Save button
	Then the Add User successfully toast message should display
	Then the User Management screen should display
	When I search for the user 'AutoAdminModified'
	Then the new user 'AutoAdminModified' should display
	When I select the user 'AutoAdminModified'
	Then the user detail screen should display with the below information populated
	| FirstName         | LastName       | Email                  | Role          | Phone         | PhoneType |
	| AutoAdminModified | TesterModified | auto.admin@yopmail.com | Administrator | 080-1234-5678 | Mobile    |


@usermanagement
Scenario: UM05 - The admin user deletes an existing user
	When I select the User Management
	Then the User Management screen should display
	When I select Add User button
	Then the Add User screen should display
	When I add a new user as below
	| FirstName  | LastName | Email | Role          | Phone         | PhoneType |
	| Automation | Tester   |       | Administrator | 080-1234-5678 | Mobile    |
	Then the Add User successfully toast message should display
	Then the User Management screen should display
	When I search for the user 'Automation'
	Then the new user 'Automation' should display
	When I delete the user 'Automation'
	Then the Delete Confirmation popup should display
	When I select No in the popup
	Then the Delete Confirmation popup should dismiss
	When I delete the user 'Automation'
	Then the Delete Confirmation popup should display
	When I select Yes in the popup
	Then the user 'Automation' should no longer show


@usermanagement
Scenario: UM06 - The admin user resends invitation to user
	When I select the User Management
	Then the User Management screen should display
	When I search for the user 'AutoAdmin'
	Then the new user 'AutoAdmin' should display
	When I open the option menu of the user 'AutoAdmin'
	Then the Resend Invitation option should not display
	When I select Add User button
	Then the Add User screen should display
	When I add a new user as below
	| FirstName  | LastName | Email | Role          | Phone         | PhoneType |
	| Automation | Tester   |       | Administrator | 080-1234-5678 | Mobile    |
	Then the Add User successfully toast message should display
	Then the User Management screen should display
	When I search for the user 'Automation'
	Then the new user 'Automation' should display
	When I resend invitation the user 'Automation'
	Then the Confirmation popup should display
	When I select No in the popup
	Then the Confirmation popup should dismiss
	When I resend invitation the user 'Automation'
	Then the Confirmation popup should display
	When I select Yes in the popup
	Then the Sent message should display
	When I check the user mailbox
	Then the user should receive invitation mail


@usermanagement
Scenario: UM07 - The admin user resets password for user
	When I select the User Management
	Then the User Management screen should display
	When I search for the user 'AutoAdmin'
	Then the new user 'AutoAdmin' should display
	When I reset password for user 'AutoAdmin'
	Then the Confirmation popup should display
	When I select No in the popup
	Then the Confirmation popup should dismiss
	When I reset password for user 'AutoAdmin'
	When I select Yes in the popup
	Then the Sent message should display
	When I check the mailbox 'auto.admin@yopmail.com'
	Then the user should receive reset password mail
	When I click on the reset password link in the email
	Then the Reset Password screen should display
	When I input '123456789' for Password field
	When I input '123456789' for Confirm Password field
	When I click Update Password button
	Then the Password Updated screen should display
	When I click Back to Login button
	Then the Login screen should display
	When I input 'auto.admin@yopmail.com' into Email field
	When I input '123456789' into Password field
	When I click SignIn button
	Then the Home screen should display