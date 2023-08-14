Feature: ContentManagement

This feature contains all the scenarios for content management

Background: 
	Given I have accessed to the Admin Application
	Then the Login screen should display
	When I login as an Admin
	Then the Home screen should display

@contentmanagement
Scenario: CM01 - The admin user adds a new book
	When I select the Content Management
	Then the Content Management screen should display
	When I select Add Book button
	Then the Add Book screen should display
	When I add a new book as below
	| Name       | ShortName | Description                |
	| Automation | AU        | Book created by automation |
	Then the Book thumbnail should display
	When I click Save button
	Then the Saved message should display
	Then the Content Management screen should display
	When I search for the book 'Automation'
	Then the book 'Automation' should display


@contentmanagement
Scenario: CM02 - The admin user edits a book
	When I select the Content Management
	Then the Content Management screen should display
	When I select Add Book button
	Then the Add Book screen should display
	When I add a new book as below
	| Name       | ShortName | Description                |
	| Automation | AU        | Book created by automation |
	Then the Book thumbnail should display
	When I click Save button
	Then the Saved message should display
	Then the Content Management screen should display
	When I search for the book 'Automation'
	Then the book 'Automation' should display
	When I edit the book 'Automation'
	Then the book detail screen should display as below
	| Name       | ShortName | Description                |
	| Automation | AU        | Book created by automation |
	Then the Short Name field should be disabled
	When I input 'Modified' into Book Name field
	When I input 'Modified' into Book Description field
	When I click Save button
	Then the Saved message should display
	Then the Content Management screen should display
	When I search for the book 'AutomationModified'
	Then the book 'AutomationModified' should display


@contentmanagement
Scenario: CM03 - The admin user deletes a book
	When I select the Content Management
	Then the Content Management screen should display
	When I select Add Book button
	Then the Add Book screen should display
	When I add a new book as below
	| Name       | ShortName | Description                |
	| Automation | AU        | Book created by automation |
	Then the Book thumbnail should display
	When I click Save button
	Then the Saved message should display
	Then the Content Management screen should display
	When I search for the book 'Automation'
	Then the book 'Automation' should display
	When I delete the book 'Automation'
	Then the Delete Confirmation popup should display
	When I select No in the popup
	Then the Delete Confirmation popup should dismiss
	When I delete the book 'Automation'
	Then the Delete Confirmation popup should display
	When I select Yes in the popup
	Then the book 'Automation' should be removed
	When I search for the book 'AutoBook'
	Then the book 'AutoBook' should display
	Then the Delete option should not show on the book 'AutoBook'


@contentmanagement
Scenario: CM04 - The validation on the book detail
	When I select the Content Management
	Then the Content Management screen should display
	When I select Add Book button
	Then the Add Book screen should display
	When I click Save button
	Then the validation message for Book Name field should display
	Then the validation message for Short Name field should display
	When I input 'Automation' into Book Name field
	When I input 'AB' into Book Short Name field
	When I click Save button
	Then the validation message for Duplicated Short Name should display


@contentmanagement
Scenario: CM05 - The user adds new unit to a book
	When I select the Content Management
	Then the Content Management screen should display
	When I select Add Book button
	Then the Add Book screen should display
	When I add a new book as below
	| Name       | ShortName | Description                |
	| Automation | AU        | Book created by automation |
	Then the Book thumbnail should display
	When I click Save button
	Then the Saved message should display
	Then the Content Management screen should display
	When I search for the book 'Automation'
	Then the book 'Automation' should display
	When I edit the book 'Automation'
	Then the book detail screen should display as below
	| Name       | ShortName | Description                |
	| Automation | AU        | Book created by automation |
	Then the Unit section should display
	When I add a new unit as below
	| Name         | LanguageFocus | Number | Phonics | Description           |
	| Automation 1 | English       | Unit 1 | həˈlō   | Created by automation |
	| Automation 2 | English       | Unit 2 | həˈlō   | Created by automation |
	| Automation 3 | English       | Unit 3 | həˈlō   | Created by automation |
	Then the Unit List of the book should display as below
	| Name         | Number | Description           |
	| Automation 1 | Unit 1 | Created by automation |
	| Automation 2 | Unit 2 | Created by automation |
	| Automation 3 | Unit 3 | Created by automation |
	When I search for the unit 'Automation 1'
	Then the unit 'Automation 1' should display


@contentmanagement
Scenario: CM06 - The validation on the unit detail
	When I select the Content Management
	Then the Content Management screen should display
	When I select Add Book button
	Then the Add Book screen should display
	When I add a new book as below
	| Name       | ShortName | Description                |
	| Automation | AU        | Book created by automation |
	Then the Book thumbnail should display
	When I click Save button
	Then the Saved message should display
	Then the Content Management screen should display
	When I search for the book 'Automation'
	Then the book 'Automation' should display
	When I edit the book 'Automation'
	Then the book detail screen should display as below
	| Name       | ShortName | Description                |
	| Automation | AU        | Book created by automation |
	Then the Unit section should display
	When I select Add Unit button
	Then the Add Unit screen should display
	When I click Save button
	Then the validation message for the Unit Name should display
	Then the validation message for the Unit Number should display
	Then the validation message for the Language Focus should display
	Then the validation message for the Phonics should display


@contentmanagement
Scenario: CM07 - The user edits an unit of a book
	When I select the Content Management
	Then the Content Management screen should display
	When I select Add Book button
	Then the Add Book screen should display
	When I add a new book as below
	| Name       | ShortName | Description                |
	| Automation | AU        | Book created by automation |
	Then the Book thumbnail should display
	When I click Save button
	Then the Saved message should display
	Then the Content Management screen should display
	When I search for the book 'Automation'
	Then the book 'Automation' should display
	When I edit the book 'Automation'
	Then the book detail screen should display as below
	| Name       | ShortName | Description                |
	| Automation | AU        | Book created by automation |
	Then the Unit section should display
	When I add a new unit as below
	| Name         | LanguageFocus | Number | Phonics | Description           |
	| Automation 1 | English       | Unit 1 | həˈlō   | Created by automation |
	Then the Unit List of the book should display as below
	| Name         | Number | Description           |
	| Automation 1 | Unit 1 | Created by automation |
	When I search for the unit 'Automation 1'
	Then the unit 'Automation 1' should display
	When I edit the unit 'Automation 1'
	Then unit detail page should display as below
	| Name         | LanguageFocus | Number | Phonics | Description           |
	| Automation 1 | English       | Unit 1 | həˈlō   | Created by automation |
	When I change the unit detail as below
	| Name              | LanguageFocus | Number      | Phonics | Description           |
	| Automation 1 Edit | English Edit  | Unit 1 Edit | həˈlō   | Created by automation |
	Then the Unit List of the book should display as below
	| Name              | Number      | Description           |
	| Automation 1 Edit | Unit 1 Edit | Created by automation |


@contentmanagement
Scenario: CM08 - The user adds new contents for a unit
	When I select the Content Management
	Then the Content Management screen should display
	When I select Add Book button
	Then the Add Book screen should display
	When I add a new book as below
	| Name       | ShortName | Description                |
	| Automation | AU        | Book created by automation |
	Then the Book thumbnail should display
	When I click Save button
	Then the Saved message should display
	Then the Content Management screen should display
	When I search for the book 'Automation'
	Then the book 'Automation' should display
	When I edit the book 'Automation'
	Then the book detail screen should display as below
	| Name       | ShortName | Description                |
	| Automation | AU        | Book created by automation |
	Then the Unit section should display
	When I add a new unit as below
	| Name         | LanguageFocus | Number | Phonics | Description           |
	| Automation 1 | English       | Unit 1 | həˈlō   | Created by automation |
	Then the Unit List of the book should display as below
	| Name         | Number | Description           |
	| Automation 1 | Unit 1 | Created by automation |
	When I search for the unit 'Automation 1'
	Then the unit 'Automation 1' should display
	When I edit the unit 'Automation 1'
	Then unit detail page should display as below
	| Name         | LanguageFocus | Number | Phonics | Description           |
	| Automation 1 | English       | Unit 1 | həˈlō   | Created by automation |
	When I add a new content as below
	| Name                 | Type          | PageNumber | Link                | Description           |
	| Automation Content 1 | Read          | 1          | https://youtube.com | Created by automation |
	| Automation Content 2 | Music         | 2          | https://youtube.com | Created by automation |
	| Automation Content 3 | Video         | 3          | https://youtube.com | Created by automation |
	| Automation Content 4 | Game          | 4          | https://youtube.com | Created by automation |
	| Automation Content 5 | PushAndListen | 5          |                     | Created by automation |
	


@contentmanagement
Scenario: CM09 - The book and unit is populated correctly in content detail
	When I select the Content Management
	Then the Content Management screen should display
	When I select Add Book button
	Then the Add Book screen should display
	When I add a new book as below
	| Name       | ShortName | Description                |
	| Automation | AU        | Book created by automation |
	Then the Book thumbnail should display
	When I click Save button
	Then the Saved message should display
	Then the Content Management screen should display
	When I search for the book 'Automation'
	Then the book 'Automation' should display
	When I edit the book 'Automation'
	Then the book detail screen should display as below
	| Name       | ShortName | Description                |
	| Automation | AU        | Book created by automation |
	Then the Unit section should display
	When I add a new unit as below
	| Name         | LanguageFocus | Number | Phonics | Description           |
	| Automation 1 | English       | Unit 1 | həˈlō   | Created by automation |
	Then the Unit List of the book should display as below
	| Name         | Number | Description           |
	| Automation 1 | Unit 1 | Created by automation |
	When I search for the unit 'Automation 1'
	Then the unit 'Automation 1' should display
	When I edit the unit 'Automation 1'
	Then unit detail page should display as below
	| Name         | LanguageFocus | Number | Phonics | Description           |
	| Automation 1 | English       | Unit 1 | həˈlō   | Created by automation |
	When I select Add Content button
	Then the Add Content screen should display
	Then the Book Name and Unit Name should be populated correctly


@contentmanagement
Scenario: CM10 - The validation on the content detail
	When I select the Content Management
	Then the Content Management screen should display
	When I select Add Book button
	Then the Add Book screen should display
	When I add a new book as below
	| Name       | ShortName | Description                |
	| Automation | AU        | Book created by automation |
	Then the Book thumbnail should display
	When I click Save button
	Then the Saved message should display
	Then the Content Management screen should display
	When I search for the book 'Automation'
	Then the book 'Automation' should display
	When I edit the book 'Automation'
	Then the book detail screen should display as below
	| Name       | ShortName | Description                |
	| Automation | AU        | Book created by automation |
	Then the Unit section should display
	When I add a new unit as below
	| Name         | LanguageFocus | Number | Phonics | Description           |
	| Automation 1 | English       | Unit 1 | həˈlō   | Created by automation |
	Then the Unit List of the book should display as below
	| Name         | Number | Description           |
	| Automation 1 | Unit 1 | Created by automation |
	When I search for the unit 'Automation 1'
	Then the unit 'Automation 1' should display
	When I edit the unit 'Automation 1'
	Then unit detail page should display as below
	| Name         | LanguageFocus | Number | Phonics | Description           |
	| Automation 1 | English       | Unit 1 | həˈlō   | Created by automation |
	When I select Add Content button
	Then the Add Content screen should display
	When I click Save button
	Then the validation message for Content Name field should display
	Then the validation message for Page Number field should display
	Then the validation message for Youtube Link field should display
	Then the validation message for Page Image should display


@contentmanagement
Scenario: CM11 - The user adds and configure Push Listen content
	When I select the Content Management
	Then the Content Management screen should display
	When I select Add Book button
	Then the Add Book screen should display
	When I add a new book as below
	| Name       | ShortName | Description                |
	| Automation | AU        | Book created by automation |
	Then the Book thumbnail should display
	When I click Save button
	Then the Saved message should display
	Then the Content Management screen should display
	When I search for the book 'Automation'
	Then the book 'Automation' should display
	When I edit the book 'Automation'
	Then the book detail screen should display as below
	| Name       | ShortName | Description                |
	| Automation | AU        | Book created by automation |
	Then the Unit section should display
	When I add a new unit as below
	| Name         | LanguageFocus | Number | Phonics | Description           |
	| Automation 1 | English       | Unit 1 | həˈlō   | Created by automation |
	Then the Unit List of the book should display as below
	| Name         | Number | Description           |
	| Automation 1 | Unit 1 | Created by automation |
	When I search for the unit 'Automation 1'
	Then the unit 'Automation 1' should display
	When I edit the unit 'Automation 1'
	Then unit detail page should display as below
	| Name         | LanguageFocus | Number | Phonics | Description           |
	| Automation 1 | English       | Unit 1 | həˈlō   | Created by automation |
	When I add a new content as below
	| Name                 | Type          | PageNumber | Description           |
	| Automation Content 1 | PushAndListen | 1          | Created by automation |
	When I edit the content 'Automation Content 1'
	Then content detail screen should display as below
	| Name                 | Type          | PageNumber | Description           |
	| Automation Content 1 | PushAndListen | 1          | Created by automation |
	Then the Push & Listen configuration section should display
	When I configure the Push & Listen on the image as below
	| Name         | Audio               |
	| Automation 1 | https://youtube.com |
	| Automation 2 | https://youtube.com |
	| Automation 3 | https://youtube.com |
	Then the grid configuration asset should display
	Then the bounding box should display on the image


@contentmanagement
Scenario: CM12 - The user edits and delete a configuration asset of Push Listen content
	When I select the Content Management
	Then the Content Management screen should display
	When I select Add Book button
	Then the Add Book screen should display
	When I add a new book as below
	| Name       | ShortName | Description                |
	| Automation | AU        | Book created by automation |
	Then the Book thumbnail should display
	When I click Save button
	Then the Saved message should display
	Then the Content Management screen should display
	When I search for the book 'Automation'
	Then the book 'Automation' should display
	When I edit the book 'Automation'
	Then the book detail screen should display as below
	| Name       | ShortName | Description                |
	| Automation | AU        | Book created by automation |
	Then the Unit section should display
	When I add a new unit as below
	| Name         | LanguageFocus | Number | Phonics | Description           |
	| Automation 1 | English       | Unit 1 | həˈlō   | Created by automation |
	Then the Unit List of the book should display as below
	| Name         | Number | Description           |
	| Automation 1 | Unit 1 | Created by automation |
	When I search for the unit 'Automation 1'
	Then the unit 'Automation 1' should display
	When I edit the unit 'Automation 1'
	Then unit detail page should display as below
	| Name         | LanguageFocus | Number | Phonics | Description           |
	| Automation 1 | English       | Unit 1 | həˈlō   | Created by automation |
	When I add a new content as below
	| Name                 | Type          | PageNumber | Description           |
	| Automation Content 1 | PushAndListen | 1          | Created by automation |
	When I edit the content 'Automation Content 1'
	Then content detail screen should display as below
	| Name                 | Type          | PageNumber | Description           |
	| Automation Content 1 | PushAndListen | 1          | Created by automation |
	Then the Push & Listen configuration section should display
	When I configure the Push & Listen on the image as below
	| Name         | Audio               |
	| Automation 1 | https://youtube.com |
	| Automation 2 | https://youtube.com |
	When I edit the configuration asset 'Automation 1'
	Then the configuration popup should display
	When I change the configuration asset as below
	| Name            | Audio                    |
	| Automation Edit | https://youtube.edit.com |
	When I click Save button
	Then the Saved message should display
	Then the configuration asset 'Automation Edit' should display on the grid
	When I delete the configuration asset 'Automation 2'
	Then the Confirmation popup should display
	When I select No in the popup
	Then the Confirmation popup should dismiss
	When I delete the configuration asset 'Automation 2'
	Then the Confirmation popup should display
	When I select Yes in the popup
	Then the Saved message should display
	Then the configuration asset 'Automation 2' should be removed


@contentmanagement
Scenario: CM13 - The user exports QR code of a unit
	When I select the Content Management
	Then the Content Management screen should display
	When I select Add Book button
	Then the Add Book screen should display
	When I add a new book as below
	| Name       | ShortName | Description                |
	| Automation | AU        | Book created by automation |
	Then the Book thumbnail should display
	When I click Save button
	Then the Saved message should display
	Then the Content Management screen should display
	When I search for the book 'Automation'
	Then the book 'Automation' should display
	When I edit the book 'Automation'
	Then the book detail screen should display as below
	| Name       | ShortName | Description                |
	| Automation | AU        | Book created by automation |
	Then the Unit section should display
	When I add a new unit as below
	| Name         | LanguageFocus | Number | Phonics | Description           |
	| Automation 1 | English       | Unit 1 | həˈlō   | Created by automation |
	Then the Unit List of the book should display as below
	| Name         | Number | Description           |
	| Automation 1 | Unit 1 | Created by automation |
	When I search for the unit 'Automation 1'
	Then the unit 'Automation 1' should display
	When I edit the unit 'Automation 1'
	Then unit detail page should display as below
	| Name         | LanguageFocus | Number | Phonics | Description           |
	| Automation 1 | English       | Unit 1 | həˈlō   | Created by automation |
	When I add a new content as below
	| Name                 | Type          | PageNumber | Link                | Description           |
	| Automation Content 1 | Read          | 1          | https://youtube.com | Created by automation |
	| Automation Content 2 | Music         | 2          | https://youtube.com | Created by automation |
	| Automation Content 3 | Video         | 3          | https://youtube.com | Created by automation |
	| Automation Content 4 | Game          | 4          | https://youtube.com | Created by automation |
	| Automation Content 5 | PushAndListen | 5          |                     | Created by automation |
	| Automation Content 6 | Record        | 6          |                     | Created by automation |
	When I click Export QR Code button
	Then the Download Successfully message should display
	Then the zip file for the QR code should be downloaded
	When I extract the QR code zip file
	Then the list of QR code should display as below
	| Name                 | Type          |
	| Automation Content 1 | Read          |
	| Automation Content 2 | Music         |
	| Automation Content 3 | Video         |
	| Automation Content 4 | Game          |
	| Automation Content 5 | PushAndListen |
	| Automation Content 6 | Record        |