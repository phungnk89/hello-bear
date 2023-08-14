Feature: Authorization

This feature contains all the scenarios for authorization

@authorization
Scenario: A01 - Authorization for the Admin user
	Given I have accessed to the Admin Application
	Then the Login screen should display
	When I login as an Admin
	Then the Home screen should display
	When I select the User Management
	Then the User Management screen should display
	When I select the Class Management
	Then the Class Management screen should display
	When I select the Content Management
	Then the Content Management screen should display
	When I select the Reports
	Then the Reports screen should display


@authorization
Scenario: A02 - Authorization for the Teacher user
	Given I have accessed to the Admin Application
	Then the Login screen should display
	When I login as an Teacher
	Then the Home screen should display
	Then the User Management should not display
	Then the Content Management should not display
	Then the Reports should not display
	When I select the Class Management
	Then the Class Management screen should display
	Then the Add Class button should not show