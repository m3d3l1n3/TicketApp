import type { Ticket } from "../types";

interface TicketRowProps {
  ticket: Ticket;
}

export function TicketRow({ ticket }: TicketRowProps) {
  return (
    <li>
      <strong>{ticket.title}</strong> — status {ticket.status} — due{" "}
      {ticket.dueDate ?? "no date"}
    </li>
  );
}