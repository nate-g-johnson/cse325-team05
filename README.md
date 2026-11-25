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

## Style Guide

This guide shows the colors, fonts and spacing we use in HireLine so every page looks the same.
All the values are CSS variables in `HireLine/wwwroot/app.css`. Please use the variables
instead of writing colors by hand.

### Colors

| Variable                  | Color   | Use                                                  |
| ------------------------- | ------- | ---------------------------------------------------- |
| `--hl-primary`            | #2F5D8C | Main buttons, links, focus ring                      |
| `--hl-primary-dark`       | #234669 | Sidebar, skip link                                   |
| `--hl-primary-hover`      | #4FCFE2 | Background of the main button on hover               |
| `--hl-accent`             | #2A9D8F | Decoration only (borders, icons). Not for text       |
| `--hl-bg`                 | #F5F7FA | Page background                                      |
| `--hl-surface`            | #FFFFFF | Cards, tables and forms                              |
| `--hl-text`               | #1F2933 | Main text                                            |
| `--hl-text-muted`         | #5A6572 | Secondary text, table headings, footer               |
| `--hl-border`             | #D9E0E7 | Borders                                              |
| `--hl-status-upcoming`    | #6C5CE7 | "Saved" badge                                        |
| `--hl-status-in-progress` | #E9A23B | "Applied" and "Interviewing" badges (with dark text) |
| `--hl-status-finished`    | #2E7D32 | "Offer" badge                                        |
| `--hl-overdue-bg`         | #FDECEA | Background for overdue reminders                     |
| `--hl-overdue-text`       | #B3261E | Text for overdue reminders and "Rejected" badge      |

### Fonts

We use one font family, `--hl-font-base` (Helvetica Neue, Helvetica, Arial).

| Variable         | Size     | Use                |
| ---------------- | -------- | ------------------ |
| `--hl-text-sm`   | 0.875rem | Small text, footer |
| `--hl-text-base` | 1rem     | Normal text        |
| `--hl-text-lg`   | 1.25rem  | Subheadings        |
| `--hl-text-xl`   | 1.75rem  | Page titles        |

### Spacing

| Variable        | Size   |
| --------------- | ------ |
| `--hl-space-sm` | 0.5rem |
| `--hl-space-md` | 1rem   |
| `--hl-space-lg` | 1.5rem |

### Accessibility

- Text and background colors have a contrast ratio of at least 4.5:1 (WCAG AA).
- The application status is always shown with text, not only with color.
- Every form input has a visible label linked to it.
- The layout has a "Skip to main content" link and a visible focus ring for keyboard users.
