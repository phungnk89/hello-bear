Feature: ClassManagement

This feature contains all the scenarios for class management

@classmanagement
Scenario: CM01 - Admin user add a new class
	Given I have accessed to the Admin Application
	Then the Login screen should display
	When I login as an Admin
	Then the Home screen should display
	When I select the Class Management
	Then the Class Management screen should display
	When I select Add Class button
	Then the Add Class screen should display
	Then the Main Teacher should be populated as current user
	When I add a new class as below
	| ClassName       | Status | SecondaryTeacher   | TextBook |
	| AutomationClass | Active | Tester AutoTeacher | AutoBook |
	When I click Save button
	Then the Class Code field should auto generated
	Then the Community Link field should display
	Then the Class URL field should display
	Then the QR Code should display
	When I click Cancel button
	Then the Class Management screen should display
	When I search for the class 'AutomationClass'
	Then the new class 'AutomationClass' should display


@classmanagement
Scenario: CM02 - Teacher user will see their assigned class
	Given I have accessed to the Admin Application
	Then the Login screen should display
	When I login as an Admin
	Then the Home screen should display
	When I select the Class Management
	Then the Class Management screen should display
	When I select Add Class button
	Then the Add Class screen should display
	Then the Main Teacher should be populated as current user
	When I add a new class as below
	| ClassName       | Status | SecondaryTeacher   | TextBook |
	| AutomationClass | Active | Tester AutoTeacher | AutoBook |
	When I click Save button
	Then the Class Code field should auto generated
	Then the Community Link field should display
	Then the Class URL field should display
	Then the QR Code should display
	When I click Cancel button
	Then the Class Management screen should display
	When I search for the class 'AutomationClass'
	Then the new class 'AutomationClass' should display
	When I logout the Admin Application
	Then the Login screen should display
	When I login as an Teacher
	Then the Home screen should display
	When I select the Class Management
	Then the Class Management screen should display
	When I search for the class 'AutomationClass'
	Then the new class 'AutomationClass' should display


@classmanagement
Scenario: CM03 - Validation on the class detail screen
	Given I have accessed to the Admin Application
	Then the Login screen should display
	When I login as an Admin
	Then the Home screen should display
	When I select the Class Management
	Then the Class Management screen should display
	When I select Add Class button
	Then the Add Class screen should display
	When I click Save button
	Then the validation message for Class Name field should display
	Then the validation message for Textbook field should display


@classmanagement
Scenario: CM04 - The user edits an existing class
	Given I have accessed to the Admin Application
	Then the Login screen should display
	When I login as an Admin
	Then the Home screen should display
	When I select the Class Management
	Then the Class Management screen should display
	When I select Add Class button
	Then the Add Class screen should display
	Then the Main Teacher should be populated as current user
	When I add a new class as below
	| ClassName       | Status | SecondaryTeacher   | TextBook |
	| AutomationClass | Active | Tester AutoTeacher | AutoBook |
	When I click Save button
	Then the Class Code field should auto generated
	Then the Community Link field should display
	Then the Class URL field should display
	Then the QR Code should display
	When I click Cancel button
	Then the Class Management screen should display
	When I search for the class 'AutomationClass'
	Then the new class 'AutomationClass' should display
	When I edit the class 'AutomationClass'
	Then the class detail should be shown as below
	| ClassName       | Status | SecondaryTeacher   | TextBook |
	| AutomationClass | Active | Tester AutoTeacher | AutoBook |
	When I make some changes of the class as below
	| ClassName           | Status   | SecondaryTeacher | TextBook  |
	| AutomationClassEdit | Inactive |                  | AutoBook1 |
	When I click Save button
	Then the Class Code field should auto generated
	Then the Community Link field should display
	Then the Class URL field should display
	Then the QR Code should display
	When I click Cancel button
	Then the Class Management screen should display
	When I search for the class 'AutomationClassEdit'
	Then the new class 'AutomationClassEdit' should display
	Then the class detail should be updated as below
	| ClassName           | Status   | SecondaryTeacher | TextBook  |
	| AutomationClassEdit | Inactive |                  | AutoBook1 |