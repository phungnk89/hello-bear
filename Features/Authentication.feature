Feature: Authentication

This feature contains all the scenarios for authentication

@authentication
Scenario: A01 - The user login to the Admin Application successfully
	Given I have accessed to the Admin Application
	Then the Login screen should display
	When I login as an Admin
	Then the Home screen should display


@authentication
Scenario: A02 - The validation on the Login screen
	Given I have accessed to the Admin Application
	Then the Login screen should display
	When I click SignIn button
	Then the validation message for Email field is required should display
	Then the validation message for Password field is required should display
	When I input 'notexist@yopmail.com' into Email field
	When I input 'incorrectpassword' into Password field
	When I click SignIn button
	Then the Unauthorized message should display
	When I input 'auto.admin@yopmail.com' into Email field
	When I input 'incorrectpassword' into Password field
	When I click SignIn button
	Then the Unauthorized message should display