USE TicketAppDb;

--Tickets with status, ordered by due date
SELECT   *
FROM     Tickets
WHERE    Status = 'Open'
ORDER BY CASE WHEN DueDate IS NULL THEN 1 ELSE 0 END, DueDate;

-- Ticket count per status
SELECT   Status,
         COUNT(Id)
FROM     Tickets
GROUP BY Status;

-- Tickets and projects name
SELECT Title,
       Name
FROM   Tickets
       INNER JOIN
       Projects
       ON Tickets.ProjectId = Projects.Id;

--Report per project
SELECT   Name,
         count(t.Id),
         COUNT(CASE WHEN DueDate < CAST (GETDATE() AS DATE)
                         AND Status <> 'Closed' THEN 1 END)
FROM     Projects AS p
         LEFT OUTER JOIN
         Tickets AS t
         ON t.ProjectId = p.Id
GROUP BY p.Name;

-- Critical open past-due tickets t.DueDate < DateOnly.FromDateTime(DateTime.Today)
SELECT *
FROM   Tickets
WHERE  Priority = 'Critical'
       AND Status = 'Open'
       AND DueDate < CAST (GETDATE() AS DATE);