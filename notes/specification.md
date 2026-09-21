# 1. Purpose & scope

Main purpose of the devolpment of this application is to revision and remember webdev.

In a fictional scenario, the application is used by te common user, without any technical knowledge, to report bugs for a dungeon videogame.

# 2. Domain model

Each of the following is an entity and the list items are fields.

Ticket has:

- id (unique)
- title
- problem description
- createdat
- status
- duedate
- projectId
- priority

Project has:

- Id
- Name
- Description(what the project does)
- Tickets(list of ticket ids)

# 3. Business Rules

Existing states of the tickets: open, in progress, resolved and closed.

A new ticket is in open state by default. Tickets status are manually updated for now.

A ticket's id is unique. The ticket can be reopened if the same issue reappears after the ticket on the topic is already closed. The ticket is back in open status.

The duedate can be in the past.

# 4. User stories

As a user, I want to open a ticket to report a bug in the videogame, so that quality is ensured. As a user, I want to be able to get updates over email so that I can track progress. As a user I want to be able to see the tickets opened and their status so I can check their progress. As a developer/ticket assignee, I want to be able to see my review report so that I can refine solution and adresss possible comments. As a reviewer I want to see the entire ticket(assignee view) to properly comment.

# 5. Others

- runs locally,
- data persists in a database,
- exposes an HTTP API
- possibly in future run on external server.

# 6. Later

Ticket has:

- asignee
- review report
- comments/updates of ticket
- review status(approved/ongoing)

Review report has:

- id (unique)
- author
- details(solution and comments on solution)
- status

An assignee can see the review report, and be able to add text and links in the comments/updates section. A ticket gets to be closed after the resolved ticket is reviewed (bot/person).
Tickets get to be in progress after they are assigned to a person/team/project.
A ticket is opened, then from project assigned based on description of the ticket the assignee is choose.

In progress tickets contain a part where updates can be written. A ticket gets resolved if issue is solved and proof is attached (code commit/report).

The duedate can be in the past, but once the due date is passed, the asigneed person show get daily emails and notifications to ensure urgency.

A user should not be able to see the comments on the ticket, as those are meant for developers/technical staff.

The reviwer gives feedback in the form of a report(?) and then the assignee can close it(button becomes active).
