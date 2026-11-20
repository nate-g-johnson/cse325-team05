# HireLine: Job Application Tracker

**Team 05:** Nathan Johnson, Jared Morillo, Katherine Gonzalez

HireLine is a web app that helps people keep track of the jobs they apply to. Looking for a job gets messy fast, with different companies, dates, and interviews to remember. With HireLine you can see every application in one place and know what to do next.

It's for anyone looking for work: recent graduates, people looking for an internship, or people who want to change careers.

We are building it with C#, ASP.NET Core, and Blazor for our CSE325 class.

## What the app does

- Create an account, log in, and log out
- Add a job application (company, position, job post, location, salary, date applied, notes)
- Edit an application and update its status
- Delete applications from the dashboard
- Filter applications by status and search by company
- Add interview dates to an application
- Set a reminder with a due date and mark it as done (overdue reminders are highlighted)

## Scope

**In:**

- Register and log in
- Create, read, update, and delete applications
- Filter by status (finished, in progress, upcoming)
- In-app reminders for upcoming interviews
- Dashboard that shows all applications

**Out (for now):**

- Email or text notifications
- Job recommendations
- Resume uploads
- Sharing applications with other users
- Connecting to LinkedIn or other job sites

Email reminders could be a stretch goal if we have time.

## Technical notes

- **Data:** we store user accounts, applications, interviews, and reminders.
- **Security:** we use ASP.NET Core Identity for login and password hashing. Each user can only see their own applications.
- **Devices:** it should work on phones, tablets, and computers. On mobile the list changes to a card layout.

## Links

- [Trello board](https://trello.com/b/FRXmtYrF/cse325-team-05)
