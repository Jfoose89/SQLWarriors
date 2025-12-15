# SQLWarriors

# Education Program Client – Admin Console Guide

This project provides an interactive console-based admin system for managing courses, students, teachers, schedules, enrollments, and grading within an education program.
All actions are organized into structured menus with clear navigation.

This guide explains how to use the console application step-by-step.

## Prerequisites
- Visual Studio
- .NET 8.0 SDK 
- SQL Server (LocalDB or full SQL Server)
- Entity Framework Core tools

## How to Use (Quick Start)
1. Open the project in Visual Studio.
   
3. Verify target framework.
    - Ensure the project targets .NET 8.0
      
4. Connect to SQL Server.
    - Open SQL Server Object Explorer in Visual Studios.
    - Connect to your local SQL Server / LocalDB.

5. Create / Update the Database.
   #### Choose one of the following methods:
    - Option A: Package Manager Console (Visual Studio)
      ```
      Update-Database
      ```
    - Option B: Developer PowerShell / Terminal
      ```
      dotnet ef database update
      ```
6. Run the program.
    - Press Ctrl + F5 (Run without debugging)
2. Choose an option from MAIN MENU.
3. Follow the prompts to add, browse, edit, or remove data.
4. Use 0 in any menu to return back or exit.

# MAIN MENU

Options for performing administrative tasks
Navigate using number keys followed by Enter.
Main Navigation
Main Menu

You can:
- CREATE – Add new records.
- VIEW – Browse or search all data.
- EDIT – Perform modifications such as registering students.
- REMOVE – Delete students or reset tables.
- Exit.

Every submenu contains its own actions, described below.

## CREATE MENU

Use this menu to add new data to the system.

### 1. New Course

Create a course by providing:
- Course name
- Status
- Start and end dates

### 2. New Teacher

Register a teacher with:
- First name
- Last name
- Email

### 3. New Room

Create a room by specifying:
- Room name
- Capacity
- Assigned teacher

### 4. New Enrollment

Enroll a student in a course by selecting:
- Course ID
- Student ID
- Enrollment date

### 5. New Student

Register a student with:
- First name
- Last name
- Email

### 6. New Schedule

Create a schedule by entering:
- Date
- Course ID
- Room ID
- Teacher ID
- Start/End times

## VIEW MENU

Use this menu to display existing data.

### 1. Show All Selection

Browse full lists of:
- Courses
- Enrollments
- Grades
- Rooms
- Schedules
- Students
- Teachers
- Teacher Courses

### 2. Find Student

Search for a student by:
- First name
- Last name

Automatically prints student info + enrollments.

### 3. Show Students: Course, Grade, Teacher

Displays each student's:
- Course
- Grade
- Date
- Grading teacher

### 4. Show All Active Courses and Enrolled Students

Shows:
- Each Active Course
- All registered students under each

### 5. Show Student Report Per Term

Filter by date range and see:
- Passed / Not passed grades
- Student names
- Course and teacher info

## EDIT MENU
### 1. Register Student To Course

Allows you to:
- Select student
- Select course
- Enter enrollment date
- Validation prevents duplicate enrollments.

### 2. Edit Student

Allows you to select to edit:
- Student Name.
- Student email.
- Start Date.
- End Date.
- Student Status.

## REMOVE MENU
### 1. Remove Student

Delete a student permanently using email.

### 2. Clear & Reset Database Tables
#### NOTE: Dangerous action – wipes all data in selected table!
Reset any table to initial seed state:
- Courses
- Enrollments
- Grades
- Rooms
- Schedules
- Students
- Teachers
- Teacher Courses


