Feature: ForgotPassword

This feature contains all the scenarios for forgot password

@forgotpassword
Scenario: FP01 - Validation on the forgot password
	Given I have accessed to the Admin Application
	Then the Login screen should display
	When I click the Forgot Password link
	Then the Forgot Password screen should display
	When I click Reset Password button
	Then the validation message for Email field is required should display
	When I input 'notanemail' into Email field
	Then the validation message for invalid email should display
	When I input 'notexist@yopmail.com' into Email field
	When I click Reset Password button
	Then the validation message for not exist email should display


@forgotpassword
Scenario: FP02 - The user reset their password through forgot password flow
	Given I have accessed to the Admin Application
	Then the Login screen should display
	When I click the Forgot Password link
	Then the Forgot Password screen should display
	When I input 'auto.admin@yopmail.com' into Email field
	When I click Reset Password button
	Then the Request Sent screen should display
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
	When I check the mailbox 'auto.admin@yopmail.com'
	Then the user should receive password reset successfully mail