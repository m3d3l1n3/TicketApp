export const TicketStatus = {
  Open: 0,
  InProgress: 1,
  Resolved: 2,
  Closed: 3,
} as const;
export type TicketStatus = (typeof TicketStatus)[keyof typeof TicketStatus];

export const TicketPriority = {
  Low: 0,
  Medium: 1,
  High: 2,
  Critical: 3,
}
export type TicketPriority = (typeof TicketPriority)[keyof typeof TicketPriority];

export interface Ticket {
  id: number;
  title: string;
  description: string;
  status: TicketStatus;    // serialized as a NUMBER by your API
  priority: TicketPriority;
  createdAt: string;       // ISO datetime, e.g. "2026-09-30T14:24:18.Z"
  dueDate: string | null;  // "2026-10-01" or null
  projectId: number;
}